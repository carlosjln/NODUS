namespace NODUS {

	public sealed class Settings : ModuleSettings {
		/// <summary>
		/// Holds a singleton instance of the Settings class
		/// </summary>
		static readonly Settings instance = new Settings();
		public static Settings Instance {
			get { return instance; }
		}
	}

}