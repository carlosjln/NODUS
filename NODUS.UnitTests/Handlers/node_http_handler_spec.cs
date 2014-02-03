using System;
using Machine.Specifications;
using NODUS;
using NODUS.Exceptions;
using NODUS.Extensions;
using NODUS.Handlers;
using NODUS.Utilities;

namespace Nodus.UnitTests.Handlers {

	[ Subject( typeof( NodeHandler ) ) ]
	public class node_http_handler_specs : NodusInitialContext {
		protected static MockedHttpContext mock = new MockedHttpContext();

		public class and_the_node_is_no_found {
			static NodeHandler node_handler;

			Establish c = ( ) => {
				var node_id = Guid.NewGuid( ).ToString();

				mock.set_relative_path( "~/node/{0}".format( node_id ) );
				node_handler = new NodeHandler( mock.request_context );
			};

			Because of = ( ) => node_handler.ProcessRequest( mock.http_context );

			It should_reply_failed_with_invalid_resource = ( ) => {
				var reply = new Reply();
				reply.failed().with( ReplyException.InvalidResource );

				mock.string_builder.ToString().ShouldEqual( reply.to_json() );
			};

			Cleanup cu = ( ) => {
				mock.string_builder.Clear();
			};
		}

		public class and_the_node_is_found {
			static NodeHandler node_handler;
			static dummy_node dummy_node;

			Establish c = ( ) => {
				dummy_node = Create.SingletonOf<dummy_node>();
				var node_id = dummy_node.Id.ToString();
				
				mock.set_relative_path( "~/node/{0}".format( node_id ) );
				node_handler = new NodeHandler( mock.request_context );
			};

			Because of = ( ) => node_handler.ProcessRequest( mock.http_context );

			It should_reply_with_the_nodes_data = () => {
				var reply = new Reply().sucessfully().with( dummy_node );
				mock.string_builder.ToString().ShouldEqual( reply.to_json() );
			};

			Cleanup cu = ( ) => {
				mock.string_builder.Clear();
			};
		}
		
		public class dummy_node : Node {
			public dummy_node() {
				Name = "dummy_node";
			}
		}
	}

}