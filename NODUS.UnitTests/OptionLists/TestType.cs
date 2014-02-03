using NODUS.Objects;

namespace Nodus.UnitTests.OptionLists {

	public class TestType : OptionList {
		public static TestType Basic = new TestType { Name = "Basic" };
		public static TestType Advanced = new TestType { Name = "Advanced" };
	}
	
}