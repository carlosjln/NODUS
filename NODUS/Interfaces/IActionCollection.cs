using System;
using System.Collections.Generic;

namespace NODUS.Interfaces {

	public interface IActionCollection {
		INodeAction GetById( Guid node_id );
		Dictionary<Guid, INodeAction> GetAllById();
		IEnumerable<INodeAction> GetByNodeType( Type node_type );

		void Add( INodeAction node_action );
		void Reset();
	}

}