using Machine.Specifications;
using NODUS.Extensions;

namespace NODUS.UnitTests.Extensions {

	[Subject( typeof( object_extensions ) )]
	public class object_extensions_spec {
		static object instance;
		static string json;

		Establish context = ( ) => {
			instance = new { CammelCase = "YOLO!" };
		};

		Because of = ( ) => json = instance.to_json();

		It should_return_a_snake_case_json = ( ) => json.ShouldEqual( "{\"cammel_case\":\"YOLO!\"}" );
	}

}