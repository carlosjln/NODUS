using System.Web.Routing;

namespace NODUS.Handlers {
	
	public class HttpRoutes {
		public static void Setup( ) {
			RouteTable.Routes.Add( "node_data", new Route( "node/{id}", new NodeRouteHandler() ) );
			RouteTable.Routes.Add( "node_view", new Route( "node/{id}/view", new ViewRouteHandler() ) );
			RouteTable.Routes.Add( "node_handlers", new Route( "node/{id}/handlers", new HttpRouteHandler() ) );
			RouteTable.Routes.Add( "node_action", new Route( "node/action/{id}", new ActionRouteHandler() ) );
		}

		public static void Reset( ) {
			RouteTable.Routes.Clear();
		}
	}

}