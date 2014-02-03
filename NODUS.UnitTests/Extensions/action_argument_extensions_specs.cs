using Machine.Specifications;
using NODUS.Extensions;
using NODUS.Objects;

namespace UnitTests.NODUS.Extensions {
	[Subject( typeof( action_argument_extensions ) )]
	public class action_argument_extensions_specs {
//		public abstract class context : Observes<> {
//		}

	}

	public class Test {
		public TestType TestType { get; set; }
		public string String { get; set; }
		public int Int { get; set; }

		public const string CollectionName = "Users.BankAccounts";
	}

	public class TestType : OptionList {
		public static TestType Basic = new TestType { Name = "Basic" };
		public static TestType Advanced = new TestType { Name = "Advanced" };
	}
}