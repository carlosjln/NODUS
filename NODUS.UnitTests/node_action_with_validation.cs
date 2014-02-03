using System.Collections.Generic;
using NODUS;
using NODUS.Exceptions;
using NODUS.Extensions;

namespace Nodus.UnitTests {
	public class node_action_with_validation : NodeAction<node_with_actions, node_action_argument> {
		public override Reply Execute( node_action_argument argument ) {
			return new Reply().sucessfully().with( argument );
		}

		public override List<ReplyException> Validate( node_action_argument argument ) {
			if( argument.name.is_null_or_empty() ) {
				return new List<ReplyException> {
					new ReplyException(1, "Name is required.")
				};				
			}

			return null;
		}
	}
}