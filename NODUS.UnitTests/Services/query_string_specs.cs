using System.Collections.Specialized;
using FluentAssertions;
using Machine.Specifications;
using NODUS.Extensions;
using NODUS.Interfaces;
using NODUS.Services;
using Nodus.UnitTests;
using Nodus.UnitTests.OptionLists;
using It = Machine.Specifications.It;

namespace NODUS.UnitTests.Services {
	[ Subject( typeof( QueryString ) ) ]
	public class query_string_specs {

		public class context : NodusInitialContext {
			protected static MockedHttpContext mock = CustomHttpContextProvider.GetMock();
		}

		public class feeding_an_action_argument : context {
			protected static NameValueCollection properties;
			protected static Argument argument;

			Establish c = ( ) => {
				var query = "int={0}&string={1}&test-type={2}".format( "9001", "Hello world!", TestType.Basic.Value ).encode_to_base64( );
				mock.set_request_indexed_data( "query", query );
				
				argument = new Argument( );
			};

			Because of = ( ) => QueryString.Feed( argument );

			It should_correctly_set_all_the_property_values = ( ) => {
				argument.Int.Should( ).Be( 9001 );
				argument.String.Should( ).Be( "Hello world!" );
				argument.TestType.Should( ).Be( TestType.Basic );
				argument.Bummer.Should( ).BeNull( );
			};
		}

		public class Argument : IActionArgument {
			public int Int { get; set; }
			public string String { get; set; }
			public TestType TestType { get; set; }
			public TestType Bummer { get; set; }
		}
	}

}