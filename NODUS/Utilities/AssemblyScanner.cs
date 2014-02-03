using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NODUS.Extensions;

namespace NODUS.Utilities {

	public class AssemblyScanner {
		// readonly static AssemblyScanner singleton_instance = new AssemblyScanner( );
		readonly static IEnumerable<Assembly> assemblies = ReloadAssemblies();

		/// <summary>
		/// Returns the list of assemblies found in 'HttpRuntime.BinDirectory'
		/// </summary>
		public static IEnumerable<string> GetAssemblyList() {
			IEnumerable<string> files = Directory.GetFiles( Application.BinDirectory, "*.dll" );
			
			files = files.filter( x => {
				var str = x.ToLower();

				return
					str.IndexOf( "structuremap" ) == -1 &&
					str.IndexOf( "specexpress" ) == -1 &&
					str.IndexOf( "newtonsoft" ) == -1 &&
					str.IndexOf( "machine" ) == -1 &&
					str.IndexOf( "fluent" ) == -1 &&
					str.IndexOf( "dummy" ) == -1 &&
					str.IndexOf( "moq" ) == -1;
			} );

			return files;
		}

		public static IEnumerable<Assembly> ReloadAssemblies() {
			var assembly_list = new List<Assembly>( );
			// assembly_list.Remove(  )
			try {
				foreach( var assembly_file in GetAssemblyList() ) {
					assembly_list.Add( Assembly.LoadFrom(assembly_file) );
				}				
			} catch( Exception ) {}

			return assembly_list;
		}

		/// <summary>
		/// Returns the list of assemblies that contain the specified type.
		/// </summary>
		public static List<Assembly> GetAssemblies( Type type ) {
			var assembly_list = new List<Assembly>( );
			
			foreach( var assembly in assemblies ) {
				var types = assembly.GetTypes( );
				
				foreach( var item in types ) {
					if( type.IsAssignableFrom( item ) ) {
						assembly_list.Add( assembly );
						break;
					}
				}
			}

			return assembly_list;
		}

		public static List<Assembly> GetAssemblies<T>() {
			var type = typeof(T);
			return GetAssemblies( type );
		}

		/// <summary>
		/// Gets all inheritors of the specified type.
		/// </summary>
		public static List<Type> GetInheritors( Type type ) {
			var return_types = new List<Type>( );
			var generic_type_definition = type.IsGenericType ? type.GetGenericTypeDefinition( ) : null;
			
			foreach( var assembly in assemblies ) {
				var types = assembly.GetTypes( );

				foreach( var item in types ) {
					if( item.inherits_from( type ) || ( generic_type_definition != null && item.inherits_from( generic_type_definition ) ) ) return_types.Add( item );
				}
			}
			
			return return_types;
		}
		
		/// <summary>
		/// Gets all inheritors of the specified type of T.
		/// </summary>
		public static List<Type> GetInheritors<T>() {
			return GetInheritors( typeof(T) );
		}

		/// <summary>
		/// Tries to gets all inheritors of the specified type.
		/// </summary>
		public static List<Type> TryGetInheritors( Type type ) {
			var return_types = new List<Type>( );
			var generic_type_definition = type.IsGenericType ? type.GetGenericTypeDefinition( ) : null;
			
			foreach( var assembly in assemblies ) {
				// This try-catch is required because it can't get all types located in some assemblies (eg: mscorlib)
				try {
					var types = assembly.GetTypes( );

					foreach( var item in types ) {
						// if( !item.IsClass || item.IsAbstract ) continue;
						if( item.inherits_from( type ) || ( generic_type_definition != null && item.inherits_from( generic_type_definition ) ) ) return_types.Add( item );
					}
				} catch( Exception ) {
					// No exception is thrown because if it cant load all types withing the assembly it might not matter anyways.
				}
			}

			return return_types;
		}


		public static IEnumerable<Type> GetImplementations( Type type ) {
			var result_list = new List<Type>( );

			if( !type.IsInterface ) throw new Exception("The type '"+ type.Name +"' is not an Interface.");
			
			var interface_name = type.Name;

			foreach( var assembly in assemblies ) {
				var types = assembly.GetTypes( );

				foreach( var item in types ) {
					var implementations = item.GetInterface( interface_name );
					if( implementations != null ) result_list.Add( item );
				}
			}

			return result_list;
		}

		public static IEnumerable<Type> GetImplementations<T>() {
			return GetImplementations( typeof(T) );
		}
	
	}

}