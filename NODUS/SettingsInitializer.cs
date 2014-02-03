using System;
using NODUS.Collections;
using NODUS.Interfaces;
using NODUS.Objects;
using NODUS.Services;
using NODUS.Utilities;
using StructureMap;
using NODUS.Extensions;
using AssemblyScanner = NODUS.Utilities.AssemblyScanner;

namespace NODUS {

	public sealed class SettingsInitializer {
		static INodeCollection nodes_collection;
		static IActionCollection action_collection;

		static bool structuremap_is_initialized;

		/// <summary>
		/// Initializes all required settings for NODUS.
		/// </summary>
		public static void Run( ) {
			// STEP #1
			initialize_structuremap( );
			
			// STEP #2
			nodes_collection = Create.SingletonOf<INodeCollection>( );
			action_collection = Create.SingletonOf<IActionCollection>( );

			setup_service_collection();

			// STEP #3 resetting the collections, just in case :P
			nodes_collection.Reset( );
			action_collection.Reset( );
			Handlers.HttpRoutes.Reset();

			// STEP #4
			Handlers.HttpRoutes.Setup();

			// STEP #5
			initialize_mongodb_settings( );

			// STEP #6
			// these are initialized first so that its parent node
			// can pick it up at initialization
			initialize_nodes_actions();

			// STEP #7
			initialize_all_nodes( );
		}
		
		static void initialize_structuremap( ) {
			if( structuremap_is_initialized ) return;

			ObjectFactory.Initialize( x => {
				x.For<Application>( ).Singleton( ).Use<Application>( );
				
				x.For<INodeCollection>( ).Singleton( ).Use<NodeCollection>( );
				x.For<IActionCollection>( ).Singleton( ).Use<ActionCollection>( );
				
				x.UseDefaultStructureMapConfigFile = false;

				x.Scan( scan => {
					scan.AssembliesFromPath( Application.BinDirectory );
					scan.WithDefaultConventions( );
					scan.RegisterConcreteTypesAgainstTheFirstInterface( );
				} );

			} );

			structuremap_is_initialized = true;
		}

		static void setup_service_collection() {
			ServiceProvider.Set<IViewLoader>( new DefaultViewLoader() );
			ServiceProvider.Set<IHandlerLoader>( new DefaultHandlerLoader() );
			ServiceProvider.Set<IHttpContextProvider>( new DefaultHttpContextProvider() );
		}

		// MONGODB SETTINGS
		static void initialize_mongodb_settings( ) {
			register_all_option_lists_serializers( );
		}

		public static void register_all_option_lists_serializers( ) {
			var item_types = AssemblyScanner.GetInheritors( typeof( OptionList ) );

			foreach( var type in item_types ) {
				OptionListItems.GetAll( type );

				var option_list_type_converter = new OptionListTypeConverter(type);

				TypeConverter.RegisterConverter( type, option_list_type_converter );
			}
		}
		
		// INITIALIZE NODES ACTIONS
		static void initialize_nodes_actions( ) {
			var actions_collection = AssemblyScanner.GetInheritors<NodeAction>().filter( type => type.IsClass && type.IsAbstract == false );
			
			foreach( var type in actions_collection ) {
				var action_type = type;

				ObjectFactory.Configure( x => x.For( action_type ).Singleton( ).Use( action_type ) );

				Create.SingletonOf( action_type );
			}
		}

		// INITIALIZE NODES
		static void initialize_all_nodes( ) {
			var nodes = AssemblyScanner.GetInheritors<Node>().filter( type => type.IsClass && type.IsAbstract == false );

			foreach( var item in nodes ) {
				var node = item;

				ObjectFactory.Configure( x => x.For( node ).Singleton( ).Use( node ) );

				Create.SingletonOf( node );
			}

			foreach( var node in nodes_collection.GetAll() ) {
				node.Update();

				if( node.Name == null ) {
					throw new Exception( "The node [" + node + "] must have a name." );
				}
				
				if( node.Name.Contains( " " ) ) {
					throw new Exception( "The node [" + node + "] must have a valid name (no spaces)." );
				}

				if( node.Namespace == null ) {
					throw new Exception( "The node [" + node + "] must have a namespace." );
				}
			}
		}
	}
}