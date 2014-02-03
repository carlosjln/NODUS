using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using NODUS.Collections;
using NODUS.Interfaces;

namespace NODUS.Extensions {
	public static class string_extensions {
		// BOOLEANS
		public static bool is_null( this string s ) {
			return s == null;
		}

		public static bool is_null_or_empty( this string s ) {
			return string.IsNullOrEmpty( s );
		}

		public static bool is_not_null( this string s ) {
			return s != null;
		}

		public static bool is_not_null_or_empty( this string s ) {
			return !string.IsNullOrEmpty( s );
		}

		public static bool is_empty( this string s ) {
			return s == string.Empty;
		}

		public static bool is_not_empty( this string s ) {
			return string.IsNullOrEmpty( s ) == false;
		}

		public static bool matches( this string str, string regex, RegexOptions options = RegexOptions.None ) {
			return Regex.IsMatch( str, regex, options );
		}


		// E-MAIL VALIDATOR
		public static bool is_valid_email( this string email ) {
			try {
				var address = new System.Net.Mail.MailAddress(email);
				return true;
			}catch {
				return false;
			}
		}


		// MODIFIERS
		public static void print( this string str ) {
			var http_context_provider = ServiceProvider.Get<IHttpContextProvider>();
			http_context_provider.GetCurrent().Response.Write( str );
		}

		public static string remove_html_comments( this string str ) {
			return str.Replace( "<!--(.*?)-->", "" );
		}

		public static string remove_tabs( this string str ) {
			return str.Replace( "\t", "" );
		}

		public static string remove_new_line_delimeter( this string str ) {
			return str.Replace( "\r\n", "" );
		}

		public static string escape_backslashes( this string str ) {
			return str.Replace( @"\", @"\\" );
		}

		public static string escape_single_quotes( this string str ) {
			return str.Replace( "'", @"\'" );
		}

		public static string escape_double_quotes( this string str ) {
			return str.Replace( "\"", @"\\" + @"""" );
		}


		// BASE64 ENCODING
		public static string encode_to_base64( this string str ) {
			return str.is_null_or_empty() ? null : Convert.ToBase64String( Encoding.UTF8.GetBytes( str ) );
		}

		public static string decode_from_base64( this string str ) {
			return str.is_null_or_empty() ? null : Encoding.UTF8.GetString( System.Convert.FromBase64String( str ) );
		}


		// CRYPTOGRAPHY
		public static string to_string( this byte[ ] bytes ) {
			var output = new StringBuilder( "" );
			
			for( var i = 0; i < bytes.Length; i++ ) {
				output.Append( bytes[i].ToString( "x2" ) );
			}

			return output.ToString();
		}

		public static string encrypt_using_sha256( this string str ) {
			var sha = new SHA256Managed();

			var input_bytes = Encoding.UTF8.GetBytes( str );
			var hashed_bytes = sha.ComputeHash( input_bytes );

			return hashed_bytes.to_string();
		}

		public static string encrypt_using_sha384( this string str ) {
			var sha = new SHA384Managed();

			var input_bytes = Encoding.UTF8.GetBytes( str );
			var hashed_bytes = sha.ComputeHash( input_bytes );

			return hashed_bytes.to_string();
		}

		public static string encrypt_using_sha512( this string str ) {
			var sha = new SHA512Managed();

			var input_bytes = Encoding.UTF8.GetBytes( str );
			var hashed_bytes = sha.ComputeHash( input_bytes );

			return hashed_bytes.to_string();
		}


		// FORMAT
		public static string format( this string str, params string[] values ) {
			return String.IsNullOrEmpty(str) ? "" : string.Format( str, values );
		}

		public static string wordify( this string str, string spacer = " " ) {
			if( !Regex.IsMatch( str, "[a-z]" ) ) return str;

			return string.Join( spacer, Regex.Split( str, @"(?<!^)(?=[A-Z])" ) );
		}

		/// <summary>
		/// Returns the current string formated as a lower case dash separated string
		/// </summary>
		public static string to_dashed_string( this string str ) {
			var strings = Regex.Split( str, @"(?<!^)(?=[A-Z])" );
			return string.Join( "-", strings ).ToLower( );
		}

		/// <summary>
		/// Returns a deterministic GUID based on the current string
		/// </summary>
		public static Guid to_guid( this string input ) {
			var provider = new MD5CryptoServiceProvider( );

			var input_bytes = Encoding.Default.GetBytes( input );
			var compute_hash = provider.ComputeHash( input_bytes );

			return new Guid( compute_hash );
		}

		public static string to_snake_case( this string str ) {
			str = Regex.Replace( str, @"( +)", "_" );
			str = Regex.Replace( str, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4" ).ToLower( );

			return str;
		}
	}
}