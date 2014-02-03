using System;
using FluentAssertions;
using Machine.Specifications;
using NODUS.Objects;
using NODUS.Services;
using Nodus.UnitTests.OptionLists;

namespace Nodus.UnitTests.Services {
	[ Subject( typeof( TypeConverter ) ) ]
	public class type_converter_specs {
		public abstract class context {
			Establish c = ( ) => {
				object_type = typeof( TestType );
				option_list_type_converter = new OptionListTypeConverter( object_type );
			};

			Because of = ( ) => TypeConverter.RegisterConverter( object_type, option_list_type_converter );

			protected static Type object_type;
			protected static OptionListTypeConverter option_list_type_converter;
		}

		public class when_registering_a_type_converter : context {
			It should_store_the_converter = ( ) => {
				var type_converter = TypeConverter.GetConverter( object_type );
				type_converter.Should().NotBeNull();
				type_converter.TargetType.Should().Be( object_type );
			};
		}
	}

}