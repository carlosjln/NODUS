using System;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using NODUS.Collections;
using NODUS.Exceptions;
using NODUS.Extensions;
using NODUS.Interfaces;
using NODUS.Utilities;

namespace NODUS.Handlers {

	public class HttpHandler : BaseHttpHandler, IRequiresSessionState {
		readonly INodeCollection nodes_collection;
		readonly IHandlerLoader handler_loader;

		readonly RequestContext request_context;

		public HttpHandler( RequestContext request_context ) {
			this.request_context = request_context;

			nodes_collection = Create.SingletonOf<INodeCollection>( );
			handler_loader = ServiceProvider.Get<IHandlerLoader>();
		}
		
		public override void ProcessRequest( HttpContextBase context ) {
			var http_response = context.Response;
			var reply = new Reply();

			Guid node_id;
			Guid.TryParse( request_context.RouteData.GetRequiredString("id"), out node_id );
			
			var node = nodes_collection.GetById( node_id );
			
			if( node == null ) {
				reply.failed().with( ReplyException.InvalidResource );
				http_response.Write( reply.to_json() );
				return;
			}

			var handlers = handler_loader.Load( node );
			if (handlers == null){
				reply.failed().with( new ReplyException( 404, "Handler not found") );
				http_response.Write( reply.to_json() );
				return;
			}

			reply.sucessfully(  ).with( handlers );
			http_response.Write( reply.to_json() );
		}

	}
}