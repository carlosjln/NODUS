using NODUS;
using NODUS.Extensions;

namespace Nodus.UnitTests {
	public class node_action_with_argument : NodeAction<node_with_actions, node_action_argument> {
		public override Reply Execute( node_action_argument argument ) {
			return new Reply().sucessfully().with( argument );
		}
	}
}