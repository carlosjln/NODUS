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

	public class ViewHandler : BaseHttpHandler, IRequiresSessionState {
		readonly INodeCollection nodes_collection;
		readonly IViewLoader view_loader;

		readonly RequestContext request_context;

		public ViewHandler( RequestContext request_context ) {
			this.request_context = request_context;

			nodes_collection = Create.SingletonOf<INodeCollection>();
			view_loader = ServiceProvider.Get<IViewLoader>( );
		}

		public override void ProcessRequest( HttpContextBase context ) {
			var http_response = context.Response;
			var reply = new Reply();

			Guid node_id;
			Guid.TryParse( request_context.RouteData.GetRequiredString("id"), out node_id );
			
			var node = nodes_collection.GetById( node_id );
			
			if( node == null ) {
				reply.failed().with( ReplyException.InvalidResource );
			}else {
				var view = view_loader.Load( node );
				reply.sucessfully().with( view );
			}

			http_response.Write( reply.to_json() );
		}

	}
	
}