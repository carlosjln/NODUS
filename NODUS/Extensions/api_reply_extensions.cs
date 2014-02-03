using System.Collections.Generic;
using NODUS.Exceptions;

namespace NODUS.Extensions {

	public static class api_reply_extensions {
		public static Reply with( this Reply reply, ReplyException exception ) {
			reply.Exceptions.Add( exception );
			reply.Success = false;
			
			return reply;
		}

		public static Reply with( this Reply reply, IEnumerable<ReplyException> exceptions ) {
			reply.Exceptions.AddRange( exceptions );
			reply.Success = false;
			
			return reply;
		}

		public static Reply with( this Reply reply, object data ) {
			reply.Data = data;
			return reply;
		}

		public static Reply sucessfully( this Reply reply ) {
			reply.Success = true;
			return reply;
		}

		public static Reply failed( this Reply reply ) {
			reply.Success = false;
			return reply;
		}

	}

}