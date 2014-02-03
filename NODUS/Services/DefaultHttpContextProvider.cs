using System.IO;
using System.Web;
using NODUS.Interfaces;

namespace NODUS.Services {
	public class DefaultHttpContextProvider : IHttpContextProvider {
		readonly HttpContext new_context;

		public DefaultHttpContextProvider() {
			new_context = new HttpContext(
				new HttpRequest( "", "http://contoso/", "" ),
				new HttpResponse( new StringWriter( ) )
			);
		}

		public virtual HttpContextBase GetCurrent() {
			return new HttpContextWrapper( System.Web.HttpContext.Current ?? new_context );
		}
	}
}