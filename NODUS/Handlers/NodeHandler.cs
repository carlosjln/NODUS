using System;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using NODUS.Exceptions;
using NODUS.Extensions;
using NODUS.Interfaces;
using NODUS.Utilities;

namespace NODUS.Handlers {

	public class NodeHandler : BaseHttpHandler, IRequiresSessionState {
		static readonly INodeCollection node_collection = Create.SingletonOf<INodeCollection>( );
		
		public RequestContext request_context;
		
		public NodeHandler( RequestContext request_context ) {
			this.request_context = request_context;
		}

		public override void ProcessRequest( HttpContextBase context ) {
			var http_response = context.Response;
			var reply = new Reply();

			Guid node_id;
			Guid.TryParse( request_context.RouteData.GetRequiredString("id"), out node_id );
			
			var node = node_collection.GetById( node_id );
			
			if( node == null ) {
				reply.failed().with( ReplyException.InvalidResource );
				http_response.Write( reply.to_json() );
				return;
			}
			
			reply.sucessfully().with( node );
//			var action_handler = action_handler_service.GetActionHandler( node );
			
			http_response.Write( reply.to_json() );
		}
	}

}