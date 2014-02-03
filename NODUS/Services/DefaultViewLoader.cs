using System.IO;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.UI;
using NODUS.Interfaces;

namespace NODUS.Services {

	public class DefaultViewLoader : IViewLoader {

		public virtual View Load( Node node ) {
			var path = GetDirectoryInfo( node ).FullName;

			var view = new View {
				Html = GetContent( path + @"\view.html" ) ?? GetAspx( path + @"\view.aspx" ),
				Style = GetContent( path + @"\style.css" )
			};

			return view;
		}
		
		/// <summary>
		/// Returns the output of the specified .aspx file
		/// </summary>
		protected virtual string GetAspx( string filepath ) {
			if( File.Exists( filepath ) == false ) return null;

			var aspx_relative_path = RemoveRootPath( filepath );

			var string_builder = new StringBuilder();
			var string_writer = new StringWriter( string_builder );
			var html_text_writer = new HtmlTextWriter( string_writer );
			var worker_request = new SimpleWorkerRequest( aspx_relative_path, "", html_text_writer );
			var new_context_current = new HttpContext( worker_request );
				
			var page = BuildManager.CreateInstanceFromVirtualPath( "~//"+ aspx_relative_path, typeof( Page ) ) as Page;
			
			new_context_current.Server.Execute( page, html_text_writer, true);
			
			return string_builder.ToString();
		}
		
		/// <summary>
		/// Returns the content of the specified file.
		/// </summary>
		protected virtual string GetContent( string filepath ) {
			string content = null;
			
			if( File.Exists( filepath ) ){
				content = File.ReadAllText( filepath );
			}

			return content;
		}

		/// <summary>
		/// Returns the expected directory info based on the node's namespace
		/// </summary>
		protected virtual DirectoryInfo GetDirectoryInfo( Node node ) {
			var base_directory = Application.BaseDirectory;
			var path = @"Views\" + node.Namespace.Replace( ".", @"\" );

			return new DirectoryInfo( base_directory + path );
		}

		/// <summary>
		/// Removes root path (Application.BaseDirectory) from the specified path
		/// </summary>
		protected virtual string RemoveRootPath( string path ) {
			if( Path.IsPathRooted(path) == false) return path;
			
			var map_path = Application.BaseDirectory;
			return @"\" + path.Replace( map_path, string.Empty );
		}
	}

}