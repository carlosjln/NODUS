using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Routing;
using Moq;
using NODUS.Extensions;

namespace Nodus.UnitTests {
	public class MockedHttpContext {
		string last_relative_path;

		public Mock<HttpContextBase> mock_http_context;
		public Mock<HttpRequestBase> mock_http_request;
		public Mock<HttpResponseBase> mock_http_response;
		public Mock<RequestContext> mock_request_context;

		public HttpContextBase http_context {
			get { return mock_http_context.Object; }
		}

		public HttpRequestBase http_request {
			get { return mock_http_request.Object; }
		}

		public HttpResponseBase http_response {
			get { return mock_http_response.Object; }
		}

		public RequestContext request_context {
			get { return mock_request_context.Object; }
		}

		public StringBuilder string_builder = new StringBuilder( );

		public MockedHttpContext( ) {
			mock_http_context = new Mock<HttpContextBase>( );
			mock_http_request = new Mock<HttpRequestBase>( );
			mock_http_response = new Mock<HttpResponseBase>( );
			mock_request_context = new Mock<RequestContext>( );

			set_request_default_values();

			// SETUP HTTP RESPONSE BASE
			mock_http_response.Setup( x => x.Write( It.IsAny<string>( ) ) ).Callback( ( string str ) => {
				string_builder.Append( str );
			} );

			mock_http_response.Setup( x => x.Write( It.IsAny<object>( ) ) ).Callback( ( object str ) => {
				string_builder.Append( str.to_json( ) );
			} );

			// SETUP HTTP CONTEXT REQUEST/RESPONSE
			mock_http_context.Setup( x => x.Request ).Returns( () => mock_http_request.Object );
			mock_http_context.Setup( x => x.Response ).Returns( () => mock_http_response.Object );

			mock_request_context.Setup( x => x.RouteData ).Returns( () => {
				return RouteTable.Routes.GetRouteData( http_context );
			} );

			mock_request_context.Setup( x => x.HttpContext ).Returns( http_context );
		}

		public void set_request_form_data( NameValueCollection data ) {
			mock_http_request.Setup( x => x.Form ).Returns( data );
		}
		
		public void set_request_indexed_data( string key, string value) {
			mock_http_request.Setup( x => x[key] ).Returns( value );

			set_request_default_values();
			set_relative_path( last_relative_path );
		}

		public void set_request_default_values( ) {
			mock_http_request.Setup( x => x.Url ).Returns( new Uri( "http://localhost/" ) );
			mock_http_request.Setup( x => x.AppRelativeCurrentExecutionFilePath ).Returns( "" );

			mock_http_request.Setup( x => x.HttpMethod ).Returns( "GET" );
			mock_http_request.Setup( x => x.Headers ).Returns( new NameValueCollection( ) );
			mock_http_request.Setup( x => x.Form ).Returns( new NameValueCollection( ) );
			mock_http_request.Setup( x => x.QueryString ).Returns( new NameValueCollection( ) );
		}

		public void set_relative_path( string relative_path ) {
			last_relative_path = relative_path;
			mock_http_request.Setup( x => x.AppRelativeCurrentExecutionFilePath ).Returns( relative_path );
		}

//		protected void set_full_url( string url ) {
//			last_url = url;
//			mock_http_request.Setup( x => x.Url ).Returns( new Uri( url ) );
//		}
	}
}