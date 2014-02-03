using NODUS.Objects;

namespace NODUS.OptionLists {

	public class ErrorSeverity : OptionList {
		public static ErrorSeverity Warning = new ErrorSeverity { Name = "Warning" };
		public static ErrorSeverity Error = new ErrorSeverity { Name = "Error" };
	}

}