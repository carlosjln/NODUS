using System;
using Machine.Specifications;
using NODUS.Services;
using NODUS.UnitTests;
using NODUS.Utilities;

namespace Nodus.UnitTests.Services {
	[ Subject( typeof( DefaultHandlerLoader ) ) ]
	public class default_handler_loader_spec : NodusInitialContext {
		static DefaultHandlerLoader handlers_loader;
		
		Establish c = ( ) => {
			handlers_loader = new DefaultHandlerLoader();
		};

		public class loading_node_with_handlers {
			static string expected_content ;
			static node_with_handlers node;
			static string result;

			Establish c = () => {
				expected_content = "Handle.view = {};";
				node = Create.SingletonOf<node_with_handlers>( );
			};

			Because of = ( ) => result = handlers_loader.Load( node );

			It should_return_the_expected_handlers = ( ) => result.ShouldEqual( expected_content );			 
		}

		public class loading_node_with_handlers_and_includes {
			static string expected_content ;
			static node_with_handlers_and_includes node;
			static string result;

			Establish c = () => {
				expected_content = "var x = 'template-1 content & template-2 content';";
				node = Create.SingletonOf<node_with_handlers_and_includes>( );
			};

			Because of = ( ) => {
				result = handlers_loader.Load( node );
			};

			It should_return_the_expected_handlers = ( ) => result.ShouldEqual( expected_content );			 
		}
	}
}