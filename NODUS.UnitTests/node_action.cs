using NODUS;
using NODUS.Extensions;

namespace Nodus.UnitTests {
	public class node_action : NodeAction<node_with_actions> {
		public override Reply Execute( ) {
			return new Reply().sucessfully().with( "node_action executed!" );
		}
	}
}