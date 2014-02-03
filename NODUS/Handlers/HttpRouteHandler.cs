using System.Web;
using System.Web.Routing;

namespace NODUS.Handlers {
	
	public class HttpRouteHandler : IRouteHandler {
		public IHttpHandler GetHttpHandler( RequestContext request_context ) {
			return new HttpHandler( request_context );
		}
	}

}