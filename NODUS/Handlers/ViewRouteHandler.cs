using System.Web;
using System.Web.Routing;

namespace NODUS.Handlers {
	
	public class ViewRouteHandler : IRouteHandler {
		public IHttpHandler GetHttpHandler( RequestContext request_context ) {
			return new ViewHandler( request_context );
		}
	}

}