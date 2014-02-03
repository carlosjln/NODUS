using System;
using Machine.Specifications;
using NODUS;
using NODUS.Utilities;
using NODUS.Extensions;

namespace Nodus.UnitTests {

	[ Subject( typeof( Node ) ) ]
	public class node_specs  {

		public class after_nodus_is_initialized : NodusInitialContext {
			It should_contain_its_actions = ( ) => {
				var node = Create.SingletonOf<node_with_actions>();
				node.Actions.to_list().Count.ShouldBeGreaterThan( 0 );
			};

			It should_have_a_propper_namespace = ( ) => {
				var node = Create.SingletonOf<node_with_parent>();
				node.Namespace.ShouldEqual( "node_with_actions.node_with_parent" );
			};

			It should_have_an_action_serialized_properly = ( ) => {
				var node_action = Create.SingletonOf<node_action>();
				var expected_result = "{\"id\":\""+ node_action.Id +"\",\"name\":\"node_action\",\"caption\":\"node_action\"}";
				node_action.to_json().ShouldEqual( expected_result );
			};
		}

	}

}