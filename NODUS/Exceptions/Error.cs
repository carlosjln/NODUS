namespace NODUS.Exceptions {

	public class Error {
		public int Number { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }

		public Error( int number, string message, object data ) {
			Number = number;
			Message = message;
			Data = data;
		}

		public Error( int number, string message ) {
			Number = number;
			Message = message;
		}
	}

}