using NODUS.OptionLists;

namespace NODUS {

	public class NodusException {
		public string Name { get; private set; }
		public string Message { get; private set; }

		public NodusException( ExceptionType type,  string message ) {
			Name = type.Value;
			Message = message;
		}
	}

	// TODO: delete
//	public class Errors : List<object> {
//		readonly BsonArray application_exceptions = new BsonArray();
//		readonly BsonArray validation_exceptions = new BsonArray();
//
//		public Errors() {
//			Add( "application", application_exceptions );
//			Add( "validation", validation_exceptions );
//		}
//
//		public void Add( ApplicationException exception ) {
//			var bson_document = new BsonDocument {
//				{ "name", exception.Name },
//				{ "message", exception.Message },
//			};
//
//			application_exceptions.AddRange( bson_document );
//		}
//
//		public void Add( ValidationResult error ) {
//			validation_exceptions.Add( error.to_bson_document()  );
//		}
//
//		public void Add( List<ValidationResult> errors ) {
//			foreach( var error in errors ) {
//				Add( error );
//			}
//		}
//	}

}