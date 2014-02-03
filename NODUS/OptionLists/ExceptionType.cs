using NODUS.Objects;

namespace NODUS.OptionLists {
	
	public class ExceptionType : OptionList {
		public static ExceptionType Validation = new ExceptionType { Value = "ValidationException" };
		public static ExceptionType Application = new ExceptionType { Value = "ApplicationException" };
	}

}