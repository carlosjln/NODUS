using System.Web;
using NODUS.Interfaces;

namespace Nodus.UnitTests {
	public class CustomHttpContextProvider : IHttpContextProvider {
		static readonly MockedHttpContext mock = new MockedHttpContext();
		
		public HttpContextBase GetCurrent( ) {
			return mock.http_context;
		}

		public static MockedHttpContext GetMock() {
			return mock;
		}
	}
}