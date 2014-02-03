using System.IO;
using System.Text.RegularExpressions;
using NODUS.Extensions;
using NODUS.Interfaces;

namespace NODUS.Services {

	public class DefaultHandlerLoader : IHandlerLoader {

		public virtual string Load( Node node ) {
			var path = GetDirectoryInfo( node ).FullName;
			return GetContent( path );
		}
		
		/// <summary>
		/// Returns the content of the specified file.
		/// </summary>
		protected virtual string GetContent( string path ) {
			var filename = path + @"\handlers.js";

			if( !File.Exists( filename ) ) return null;
			
			var content = File.ReadAllText( filename );
			
			return ParseIncludes( path, content );
		}

		/// <summary>
		/// Returns the expected directory info based on the node's namespace
		/// </summary>
		protected virtual DirectoryInfo GetDirectoryInfo( Node node ) {
			var base_directory = Application.BaseDirectory;
			var path = @"Views\" + node.Namespace.Replace( ".", @"\" );

			return new DirectoryInfo( base_directory + path );
		}

		protected virtual string ParseIncludes( string path, string content ) {
			const string pattern = @"({include=([^{}]+)})";

			MatchEvaluator match_evaluator = match => {
				var filename = path + "\\" + match.Groups[2].Value;
				var file_content = File.Exists( filename ) ? File.ReadAllText( filename ) : "";

				return file_content.remove_tabs().remove_new_line_delimeter().remove_html_comments();
			};

			return Regex.Replace( content, pattern, match_evaluator, RegexOptions.Multiline & RegexOptions.IgnoreCase );
		}
	}

}