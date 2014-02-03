using NODUS.OptionLists;

namespace NODUS {

	public class ApplicationException : NodusException {
		public ApplicationException( string message ) : base( ExceptionType.Application, message ) {}
	}

}