using System;
using System.Collections.Generic;
using NODUS.Services;

namespace NODUS.Objects {

	public class OptionListTypeConverter : TypeConverter {
		public readonly Dictionary<string, OptionList> Items = new Dictionary<string, OptionList>();

		public OptionListTypeConverter( Type option_list_type ) {
			if( option_list_type == null ) throw new Exception("Must specify the list type.");

			TargetType = option_list_type;

			var items = OptionListItems.GetAll( option_list_type );
			register_list_items( items );
		}

		public override bool IsValid( object value ) {
			return value is string;
		}
		
		public override object ConvertFrom( object value ) {
			if( IsValid(value) == false ) {
				throw can_not_convert_exception( value );
			}

			var str_value = (string) value;
			return Items.ContainsKey( str_value ) ? Items[str_value] : null;
		}

		void register_list_items( IEnumerable<OptionList> option_list_items ) {
			foreach( var item in option_list_items ) {
				Items.Add( item.Name, item );
			}
		}
	}

}