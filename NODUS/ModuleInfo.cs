using NODUS.Interfaces;

namespace NODUS {

	public sealed class ModuleInfo : IModuleInfo {
		public string ModuleName {
			get { return "Nodus"; }
		}
		
		/// <summary>
		/// Holds a singleton instance of the current class
		/// </summary>
		static readonly ModuleInfo instance = new ModuleInfo();
		public static ModuleInfo Instance {
			get { return instance; }
		}
	}

}