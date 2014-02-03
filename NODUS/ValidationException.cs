using NODUS.OptionLists;

namespace NODUS {

	public class ValidationException : NodusException {
		public string Field { get; private set; }

		public ValidationException( string field, string message ) : base( ExceptionType.Validation, message ) {
			Field = field;
		}
	}

}