using NODUS.Extensions;

namespace NODUS {

	public class View {
		public string Style { get; set; }
		public string Html { get; set; }

		public bool HasContent() {
			return Style.is_not_null_or_empty() && Html.is_not_null_or_empty();
		}
	}

}