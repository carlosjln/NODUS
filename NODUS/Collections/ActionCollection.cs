using System;
using System.Collections.Generic;
using NODUS.Interfaces;

namespace NODUS.Collections {
	public class ActionCollection : IActionCollection {
		readonly Dictionary<Guid, INodeAction> actions_by_id = new Dictionary<Guid, INodeAction>( );
		readonly Dictionary<Type, List<INodeAction>> actions_by_parent_type = new Dictionary<Type, List<INodeAction>>( );

		public INodeAction GetById( Guid node_id ) {
			INodeAction node = null;

			if( actions_by_id.ContainsKey( node_id ) ) node = actions_by_id[ node_id ];

			return node;
		}

		public IEnumerable<INodeAction> GetByNodeType( Type node_type ) {
			var actions = new List<INodeAction>( );

			if( actions_by_parent_type.ContainsKey( node_type ) ) actions = actions_by_parent_type[ node_type ];

			return actions;
		}

		public Dictionary<Guid, INodeAction> GetAllById( ) {
			return actions_by_id;
		}

		
		public void Add( INodeAction node_action ) {
			var parent_type = node_action.ParentType();

			actions_by_id[ node_action.Id ] = node_action;
			
			// ensures the node action lists exists
			if( actions_by_parent_type.ContainsKey( parent_type ) == false ) {
				actions_by_parent_type[ parent_type ] = new List<INodeAction>();
			}

			// adds the action if its not already added
			var node_actions = actions_by_parent_type[ parent_type ];
			if( node_actions.Contains( node_action ) == false ) {
				node_actions.Add( node_action );
			}
		}

		public void Reset( ) {
			actions_by_id.Clear( );
			actions_by_parent_type.Clear();
		}
	}
}