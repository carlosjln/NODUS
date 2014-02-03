using System;
using System.Collections.Generic;

namespace NODUS.Interfaces {
	
	public interface INodeCollection {
		Node GetById( Guid node_id );

		IEnumerable<Node> GetAll( );
		Dictionary<Guid, Node> GetAllById( );

		void Add( Node node );
		void Reset();
	}

}