using System;
using System.Web.Routing;
using Machine.Specifications;
using NODUS;
using NODUS.Exceptions;
using NODUS.Extensions;
using NODUS.Handlers;

namespace Nodus.UnitTests.Handlers {

	[ Subject( typeof( ViewHandler ) ) ]
	public class view_http_handler_specs : NodusInitialContext {
		public abstract class context {
			Establish c = ( ) => {
				mocked_http_context = new MockedHttpContext();
				request_context = mocked_http_context.request_context;
				view_handler = new ViewHandler( request_context );
			};

			protected static MockedHttpContext mocked_http_context;
			protected static ViewHandler view_handler;
			protected static RequestContext request_context;
		}

		public class and_the_node_is_no_found : context {
			Establish c = ( ) => {
				var node_id = Guid.NewGuid( ).ToString();
				mocked_http_context.set_relative_path( "~/node/{0}".format( node_id ) );
			};

			Because of = ( ) => view_handler.ProcessRequest( mocked_http_context.http_context );

			It should_reply_failed_with_invalid_resource = ( ) => {
				var reply = new Reply();
				reply.failed().with( ReplyException.InvalidResource );

				mocked_http_context.string_builder.ToString().ShouldEqual( reply.to_json() );
			};
		}
	}

}