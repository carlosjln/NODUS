namespace NODUS.Exceptions {

	public class ReplyException {
		public int? Number { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }
		public string Type { get; set; }
		public object Data { get; set; }

		public ReplyException( int? number, string name, string message, ReplyExceptionType type, object data ) {
			Number = number;
			Name = name;
			Message = message;
			Type = type != null ? type.Value : ReplyExceptionType.Application.Value;
			Data = data ?? new {};
		}

		public ReplyException( int number, string message, ReplyExceptionType type = null, object data = null )
			: this( number, null, message, type, data ) {}

		public ReplyException( string name, string message, ReplyExceptionType type = null, object data = null  )
			: this( null, name, message, type, data ) {}


		// PRESET EXCEPTIONS
		public static ReplyException InvalidResource = new ReplyException( 500, "Invalid resource");
	}

}