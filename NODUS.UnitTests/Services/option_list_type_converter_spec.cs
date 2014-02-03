using System;
using FluentAssertions;
using Machine.Specifications;
using NODUS.Extensions;
using NODUS.Objects;

namespace Nodus.UnitTests.Services {

	[ Subject( typeof( OptionListTypeConverter ) ) ]
	public class option_list_type_converter_spec : NodusInitialContext {
		public abstract class context {
			Establish c = ( ) => {
				target_type = typeof( TestType );
			    option_list_type_converter = new OptionListTypeConverter( target_type );
			};

			protected static Type target_type;
			protected static OptionListTypeConverter option_list_type_converter;

			protected class TestType : OptionList {
				public static TestType Basic = new TestType { Name = "Basic" };
				public static TestType Advanced = new TestType { Name = "Advanced" };
			}
		}

		public class after_creating_a_new_instance : context {
			Establish c = ( ) => {};

			It should_contain_the_expected_keys = ( ) => {
			    var expected_keys = new [] {
					TestType.Basic.Value,
					TestType.Advanced.Value
				};

			    option_list_type_converter.Items.Should().ContainKeys( expected_keys );
			};
		}

		public class when_converting_strings_to_option_list_type : context {
			It should_return_the_correct_option_when_the_value_is_correct = () => {
				option_list_type_converter.ConvertFrom( "Basic" ).Should().BeSameAs( TestType.Basic );
			};
			
			It should_return_null_when_the_value_is_not_correct = () => {
				option_list_type_converter.ConvertFrom( "wrong" ).Should().Be( null );
			};
		}

		public class when_using_the_convert_to_extension_method : context {
			It should_return_the_correct_option_when_converting_from_ = () => {
				"Basic".convert_to( target_type ).Should().BeSameAs( TestType.Basic );
			};
			
			It should_return_null_when_the_value_is_not_correct = () => {
				"wrong".convert_to( target_type ).Should().Be( null );
			};
		}
	}

}