using System.Web;

namespace NODUS.Interfaces {
	public interface IHttpContextProvider {
		HttpContextBase GetCurrent();
	}
}