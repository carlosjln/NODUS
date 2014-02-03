using System;

namespace NODUS {

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class NodeAttributes : Attribute {
		public bool IsRoot { get; set; }
		public bool IsInternal { get; set; }
		
		public NodeAttributes( ) {
			IsRoot = false;
			IsInternal = false;
		}
	}

}