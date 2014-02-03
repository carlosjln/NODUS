using System;
using System.Collections.Generic;
using System.ComponentModel;
using NODUS.Extensions;

namespace NODUS.Services {

	// Wrapper for the System.ComponentModel.TypeConverter
	public abstract class TypeConverter {
		private static readonly Dictionary<Type, TypeConverter> type_converters = new Dictionary<Type, TypeConverter>();

		public Type TargetType { get; set; }

		public abstract bool IsValid( object value );
		public abstract object ConvertFrom( object value );
		
		public static void RegisterConverter( Type type, TypeConverter converter ) {
			if( type_converters.ContainsKey( type ) == false ) type_converters.Add( type, converter );
		}
		
		public static TypeConverter GetConverter( Type type ){
			if( type_converters.ContainsKey( type ) ) return type_converters[type];

			var type_converter = new DefaultTypeConverter( type );
			type_converters.Add( type, type_converter );

			return type_converter;
		}

		protected Exception can_not_convert_exception( object value ) {
			var message = "Cannot convert from '{0}' to '{1}'".format( value.GetType().ToString(), TargetType.ToString() );
			return new Exception( message );
		}

		/// <summary>
		/// Default type converter class that handles the convertion of primitive types.
		/// </summary>
		private class DefaultTypeConverter : TypeConverter {
			private readonly global::System.ComponentModel.TypeConverter type_converter;

			public DefaultTypeConverter( Type type ) {
				TargetType = type;
				type_converter = TypeDescriptor.GetConverter( type );
			}

			public override bool IsValid( object value ) {
				return type_converter.IsValid( value );
			}
			
			public override object ConvertFrom( object value ) {
				// Enable if necessary
				if( value == null || value.ToString().is_null_or_empty() ) {
					return TargetType.IsValueType ? Activator.CreateInstance( TargetType ) : null;
				}

				if( IsValid(value) == false ) {
					throw can_not_convert_exception( value );
				}

				return type_converter.ConvertFrom( value );
			}
		}
	}

}