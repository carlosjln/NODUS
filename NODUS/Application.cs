using System;
using System.Configuration;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using NODUS.Extensions;
using NODUS.JSON;
using NODUS.Objects;
using NODUS.OptionLists;

namespace NODUS {
	public sealed class Application {
		static string base_directory = AppDomain.CurrentDomain.BaseDirectory;
		static string bin_directory = Path.Combine( base_directory, "bin" );
		static readonly CurrentEnvironment current_environment = get_current_environment( );

		static readonly JsonSerializerSettings json_serializer_settings = new JsonSerializerSettings {
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
			ContractResolver = new CustomContractResolver()
		};

//		static HttpContextBase http_context = new HttpContextWrapper(
//			System.Web.HttpContext.Current ?? new HttpContext(
//				new HttpRequest( "", "http://contoso/", "" ),
//				new HttpResponse( new StringWriter( ) )
//				)
//			);

		/// <summary>
		/// Holds a singleton instance of the Settings class.
		/// </summary>
		public static Settings Settings {
			get { return Settings.Instance; }
		}

		public static JsonSerializerSettings JsonSerializerSettings {
			get { return json_serializer_settings; }
		}

		public static string BaseDirectory {
			get { return base_directory; }
		}

		public static string BinDirectory {
			get { return bin_directory; }
		}

		public static CurrentEnvironment CurrentEnvironment {
			get { return current_environment; }
		}


//		public static HttpContextBase HttpContext {
//			get { return http_context; }
//		}

		// TODO: make me better
		public static void SetBaseDirectory( string path ) {
			base_directory = path;
		}

		public static void SetBinDirectory( string path ) {
			bin_directory = path;
		}

//		public static void SetHttpContext( HttpContextBase http_context_base ) {
//			http_context = http_context_base;
//		}
//
//		public static void SetHttpContext( HttpContext current_http_context ) {
//			http_context = new HttpContextWrapper( current_http_context );
//		}

		static CurrentEnvironment get_current_environment( ) {
			var app_settings = ConfigurationManager.AppSettings;
			var environment = OptionList<CurrentEnvironment>.GetItems( ).first( x => x.Value == app_settings[ "CurrentEnvironment" ] );

			if( environment == null ) throw new Exception( "Hey! you need to define the 'CurrentEnvironment' variable on your web.config" );

			return environment;
		}
	}
}