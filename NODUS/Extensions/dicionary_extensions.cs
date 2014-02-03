using System.Collections.Generic;

namespace NODUS.Extensions {
	
	public static class dicionary_extensions {
		public static string get( this IDictionary<string, string> dictionary, string key ) {
			string value = null;

			if( dictionary.ContainsKey( key ) ) {
				value = dictionary[key];
			}

			return value;
		}
	}

}