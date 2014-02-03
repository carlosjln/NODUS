using System.Web;

namespace NODUS.Handlers {

	public abstract class BaseHttpHandler: IHttpHandler {
		public bool IsReusable {
			get { return false; }
		}

		public void ProcessRequest( HttpContext context ) {
			ProcessRequest( new HttpContextWrapper( context ) );
		}

		public abstract void ProcessRequest(HttpContextBase context);
	}

}