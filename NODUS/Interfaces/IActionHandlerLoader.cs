using System;
using System.Collections.Generic;

namespace NODUS.Interfaces {

	public interface IActionHandlerService {
		string GetActionHandler( Guid node_id );
		string GetActionHandler( Node node );

		Dictionary<Node, string> Handlers { get; }
	}

}