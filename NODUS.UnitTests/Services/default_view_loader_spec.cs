using Machine.Specifications;
using NODUS;
using NODUS.Services;
using NODUS.UnitTests;
using NODUS.Utilities;

namespace Nodus.UnitTests.Services {
	[ Subject( typeof( DefaultViewLoader ) ) ]
	public class default_view_loader_spec : NodusInitialContext {
		static DefaultViewLoader default_view_loader;
		static string view_html;
		static string view_style;

		Establish c = ( ) => {
			default_view_loader = new DefaultViewLoader( );
			view_html = "<div>Html goes here</div>";
			view_style = "div{display:none}";
		};

		public class and_the_view {
			public class files_are_missing {
				static View view;

				Because of = ( ) => {
					var node_with_missing_view_files = Create.SingletonOf<node_with_missing_view_files>( );
					view = default_view_loader.Load( node_with_missing_view_files );
				};

				It should_have_null_html = ( ) => view.Html.ShouldBeNull( );
				It should_have_null_style = ( ) => view.Style.ShouldBeNull( );
			}

			public class files_exist {
				static View view;

				Because of = ( ) => {
					var node_with_html_view = Create.SingletonOf<node_with_html_view>( );
					view = default_view_loader.Load( node_with_html_view );
				};

				It should_have_the_view_html = ( ) => view.Html.ShouldEqual( view_html );
				It should_have_the_view_style = ( ) => view.Style.ShouldEqual( view_style );
			}
		}
	}
}