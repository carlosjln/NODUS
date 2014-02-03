using System;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using NODUS.Exceptions;
using NODUS.Extensions;
using NODUS.Interfaces;
using NODUS.Utilities;

namespace NODUS.Handlers {

	public class ActionHttpHandler : BaseHttpHandler, IRequiresSessionState {
		readonly IActionCollection action_collection = Create.SingletonOf<IActionCollection>( );
		readonly RequestContext request_context;

		public ActionHttpHandler( RequestContext request_context ) {
			this.request_context = request_context;
		}
		
		public override void ProcessRequest( HttpContextBase context ) {
			var http_response = context.Response;
			var reply = new Reply();

			Guid action_id;
			Guid.TryParse( request_context.RouteData.GetRequiredString("id"), out action_id );
			
			var action = action_collection.GetById( action_id );
			
			if( action == null ) {
				reply.failed().with( ReplyException.InvalidResource );
			} else {
				reply = action.Execute();
			}

			http_response.Write( reply.to_json() );
		}

	}

}