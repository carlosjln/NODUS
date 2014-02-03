using System.Collections.Generic;
using NODUS.Exceptions;
using NODUS.Extensions;

namespace NODUS {

	public class Reply {
		public bool Success { get; set; }
		public object Data { get; set; }
		public readonly List<ReplyException> Exceptions = new List<ReplyException>();
		
		public override string ToString() {
			return this.to_json();
		}
	}

}