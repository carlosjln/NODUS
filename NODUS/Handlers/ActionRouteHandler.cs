using System.Web;
using System.Web.Routing;

namespace NODUS.Handlers {

	public class ActionRouteHandler : IRouteHandler {
		public IHttpHandler GetHttpHandler( RequestContext request_context ) {
			return new ActionHttpHandler( request_context );
		}
	}

}