using NODUS.Objects;

namespace NODUS.OptionLists {

	public class CurrentEnvironment : OptionList {
		public static CurrentEnvironment Development = new CurrentEnvironment { Name = "Development" };
		public static CurrentEnvironment Production = new CurrentEnvironment { Name = "Production" };
	}

}