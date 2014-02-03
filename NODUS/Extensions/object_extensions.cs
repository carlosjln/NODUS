using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using NODUS.Services;
using NODUS.Utilities;

namespace NODUS.Extensions {
	public static class object_extensions {
		public static void copy_properties_from( this object obj, object data_source ) {
			var self_properties = obj.GetType( ).GetProperties( BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance );
			var source_type = data_source.GetType( );

			foreach( var property in self_properties ) {
				var property_type = property.PropertyType;
				var source_property = source_type.GetProperty( property.Name );
				var source_property_value = source_property.GetValue( data_source, null );

				var value = Convert.ChangeType( source_property_value, Nullable.GetUnderlyingType( property_type ) ?? property_type );

				if( value == null ) continue;

				property.SetValue( obj, value, null );
			}
		}

		public static object [ ] get_attributes( this object obj, Type attribute ) {
			var attributes = obj.GetType( ).GetCustomAttributes( attribute, false );
			return attributes;
		}

		// INHERITANCE
		static readonly Type type_of_object = typeof( object );

		public static bool inherits_from( this object reference, Type type ) {
			if( reference == null ) return false;

			return inherits_from( reference.GetType(), type );
		}

		public static bool inherits_from( this Type reference_type, Type type ) {
			if( reference_type == null || reference_type.BaseType == null ) return false;

			var base_type = reference_type.BaseType;

			while( base_type != type_of_object ) {
				if( base_type == type || ( base_type.IsGenericType && base_type.GetGenericTypeDefinition( ) == type ) ) return true;

				base_type = base_type.BaseType;
			}

			return false;
		}

		public static bool inherits_from( this Type reference_type, IEnumerable<Type> types ) {
			foreach( var type in types ) {
				if( reference_type == type ) return true;
			}

			return false;
		}

		public static bool implements( this object self, Type interface_type ) {
			return implements( self.GetType( ), interface_type );
		}

		public static bool implements( this Type self, Type interface_type ) {
			return self.GetInterface( interface_type.Name ) != null;
		}

		public static Type get_interface( this object self, Type interface_type ) {
			return get_interface( self.GetType( ), interface_type );
		}

		public static Type get_interface( this Type self, Type interface_type ) {
			return self.GetInterface( interface_type.Name );
		}

		// TYPE CONVERTER
		public static object convert_to( this object obj, Type type ) {
			var type_converter = TypeConverter.GetConverter( type );
			return type_converter.ConvertFrom( obj );
		}

		public static string to_json( this object obj ) {
			return JsonConvert.SerializeObject( obj, Formatting.None, Application.JsonSerializerSettings );
		}

		// OTHERS
		public static Guid get_uid( this object obj ) {
			return obj.GetType( ).FullName.to_guid( );
		}
	}

}