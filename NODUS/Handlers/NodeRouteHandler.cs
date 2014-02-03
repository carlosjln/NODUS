using System.Web;
using System.Web.Routing;

namespace NODUS.Handlers {

	public class NodeRouteHandler : IRouteHandler {
		public IHttpHandler GetHttpHandler( RequestContext request_context ) {
			return new NodeHandler( request_context );
		}
	}

}