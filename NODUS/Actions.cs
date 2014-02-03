using System;
using System.Collections.Generic;
using NODUS.Interfaces;

namespace NODUS {

	public sealed class Actions {
		public Dictionary<Guid, INodeAction> ById = new Dictionary<Guid, INodeAction>();
		public Dictionary<string, INodeAction> ByName = new Dictionary<string, INodeAction>();

		public void Add( INodeAction action ) {
			ById[ action.Id ] = action;
			ByName[ action.Name ] = action;
		}
	}

}