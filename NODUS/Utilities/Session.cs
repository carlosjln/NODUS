using System.Web;
using System.Web.SessionState;
using NODUS.OptionLists;

namespace NODUS.Utilities {

	public class Session {
		public static HttpSessionState Store = HttpContext.Current.Session;

		public static void Add( SessionKey key, object value ) {
			Store.Add( key.Value, value );
		}

		public static object Get( SessionKey key ) {
			return Store[key.Value];
		}

		public static void Remove( SessionKey key ) {
			Store.Remove(key.Value);
		}
	}

}