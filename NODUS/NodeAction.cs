using System;
using System.Collections.Generic;
using NODUS.Exceptions;
using NODUS.Extensions;
using NODUS.Interfaces;
using NODUS.Services;
using NODUS.Utilities;

namespace NODUS {
	public abstract class NodeAction : INodeAction {
		protected readonly IActionCollection action_collection = Create.SingletonOf<IActionCollection>( );

		public Guid Id { get; private set; }

		public string Name { get; protected set; }
		public string Caption { get; protected set; }

		readonly Type node;

		protected NodeAction( Type parent_node_type ) {
			Id = this.get_uid( );

			node = parent_node_type;

			Name = GetType( ).Name;

			Caption = Name;

			action_collection.Add( this );
		}

		public Type ParentType( ) {
			return node;
		}

		public abstract Reply Execute( );
	}

	public abstract class NodeAction< TParentNode > : NodeAction
		where TParentNode : Node, new( ) {
		protected NodeAction( ) : base( typeof( TParentNode ) ) {}
	}

	public abstract class NodeAction< TParentNode, TArgument > : NodeAction<TParentNode>
		where TParentNode : Node, new( )
		where TArgument : IActionArgument, new( ) {

		public override Reply Execute( ) {
			var argument = Create.InstanceOf<TArgument>( );
			var reply = new Reply( );

			QueryString.Feed( argument );

			var reply_exceptions = Validate( argument );
			
			if( reply_exceptions != null && reply_exceptions.Count > 0 ) {
				return reply.failed( ).with( reply_exceptions );
			}

			return Execute( argument );
		}

		public abstract Reply Execute( TArgument argument );

		public virtual List<ReplyException> Validate( TArgument argument ) {
			return null;
		}

	}
}