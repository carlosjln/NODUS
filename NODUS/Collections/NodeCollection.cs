using System;
using System.Collections.Generic;
using NODUS.Interfaces;

namespace NODUS.Collections {

	public class NodeCollection : INodeCollection {
		readonly Dictionary<Guid, Node> nodes_by_id = new Dictionary<Guid, Node>();
		readonly List<Node>  node_list = new List<Node>();

		public Node GetById( Guid node_id ) {
			Node node = null;
			
			if( nodes_by_id.ContainsKey( node_id ) ) {
				node = nodes_by_id[ node_id ];
			}

			return node;
		}
		
		public IEnumerable<Node> GetAll( ) {
			return node_list;
		}

		public Dictionary<Guid, Node> GetAllById( ) {
			return nodes_by_id;
		}

		public void Add( Node node ) {
			nodes_by_id[ node.Id ] = node;
			if( node_list.Contains( node ) == false ) node_list.Add( node );
		}

		public void Reset() {
			nodes_by_id.Clear();
			node_list.Clear();
		}
	}

}