using System.Configuration;
using NODUS.Interfaces;

namespace NODUS {

	// TODO: DELETE
	public abstract class ModuleSettings : IModuleSettings {
//		public CodeSettings JavaScript { get; private set; }
//		public CssSettings Css { get; private set; }
		public ConnectionStringSettingsCollection ConnectionStrings {get; private set;}
		
		protected ModuleSettings() {
			ConnectionStrings = ConfigurationManager.ConnectionStrings;

//			var current_environment = Application.CurrentEnvironment;

//			JavaScript = new CodeSettings{ TermSemicolons = true };
//			Css = new CssSettings { CommentMode = CssComment.Hacks, ColorNames = CssColor.Hex, CssType = CssType.FullStyleSheet };

//			if( current_environment == CurrentEnvironment.Development ) {
//				JavaScript.OutputMode = OutputMode.MultipleLines;
//				JavaScript.StripDebugStatements = false;
//				JavaScript.MinifyCode = false;
//
//				Css.OutputMode = OutputMode.SingleLine;
//			} else {
//				JavaScript.OutputMode = OutputMode.SingleLine;
//				JavaScript.MinifyCode = true;
//
//				Css.OutputMode = OutputMode.MultipleLines;
//			}
		}
	}

}