using NODUS.Objects;

namespace NODUS.Exceptions {

	public class ReplyExceptionType : OptionList {
		public static ReplyExceptionType Validation = new ReplyExceptionType { Value = "Validation" };
		public static ReplyExceptionType Application = new ReplyExceptionType { Value = "Application" };
		public static ReplyExceptionType System = new ReplyExceptionType { Value = "System" };
	}

}