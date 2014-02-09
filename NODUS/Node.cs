using System;
using System.Collections.Generic;
using NODUS.Extensions;
using NODUS.Interfaces;
using NODUS.Utilities;

namespace NODUS {

	public abstract class Node {
		// STATIC
		static readonly INodeCollection nodes_collection = Create.SingletonOf<INodeCollection>( );
		protected readonly IActionCollection action_collection = Create.SingletonOf<IActionCollection>( );

		// PROTECTED STUFF
		Node parent_node { get; set; }

		// INSTANCE PUBLIC MEMBERS
		public Guid Id;

		public string Name { get; protected set; }
		public string Caption { get; protected set; }
		public string Description { get; protected set; }

		public int Index { get; protected set; }
		public Guid? ParentId { get; protected set; }
		public string Namespace { get; private set; }
		
		public bool IsInternal { get; protected set; }
		public bool IsProtected { get; protected set; }
		
		public IEnumerable<INodeAction> Actions { get; private set; }

		protected Node() : this( null ) {}

		protected Node( Node parent_node ) {
			Id = this.get_uid();

			Actions = action_collection.GetByNodeType( GetType() );

			Index = 0;
			
			if( parent_node == null ) {
				ParentId = null;
			} else {
				ParentId = parent_node.Id;
				this.parent_node = parent_node;
			}

			// the node is responsible for adding itself into the nodes collection
			nodes_collection.Add( this );
		}

		public void Update( ) {
			// parent must be asigned before identifying the namespace
			Namespace = (parent_node == null ? Name : parent_node.Namespace + "." + Name);
		}
	}

	public abstract class Node<TParent> : Node where TParent : Node, new() {
		protected Node() : base( Create.SingletonOf<TParent>() ) {}
	}

}