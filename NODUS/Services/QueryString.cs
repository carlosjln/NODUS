using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Web;
using NODUS.Collections;
using NODUS.Extensions;
using NODUS.Interfaces;

namespace NODUS.Services {
	// TODO: on application start identify all IActionArgument and force a get_properties in order to cache all their properties
	// and add a wrapper that contains the valid form name

	// TODO: this could be turned, instead of looping through the entity properties it could be done through the query_params
	public class QueryString {
		static readonly Dictionary<Type, PropertyInfo[]> object_properties = new Dictionary<Type, PropertyInfo [ ]>();

		public static void Feed( IActionArgument argument ) {
			var query_params = get_query_params();
			var properties = get_properties( argument );
			
			foreach( var property in properties ){
				var key = property.Name.to_dashed_string();
				var value = query_params[key];

				// The key doesn't exist on the source name value collection when the form doesn't contain an input field with such name
				if( value == null ) continue;

				var type_converter = TypeConverter.GetConverter( property.PropertyType );
				var property_value = type_converter.ConvertFrom( value );

				property.SetValue( argument, property_value, null );
			}
		}

		static NameValueCollection get_query_params() {
			var http_context = ServiceProvider.Get<IHttpContextProvider>().GetCurrent();
			
			var query = (http_context.Request[ "query" ] ?? "").decode_from_base64() ?? "";
			var collection = HttpUtility.ParseQueryString( query, Encoding.UTF8 );

			return collection;
		}

		static IEnumerable<PropertyInfo> get_properties( object obj ) {
			var object_type = obj.GetType();

			if( object_properties.ContainsKey(object_type) ) return object_properties[object_type];
			
			var properties = object_type.GetProperties( BindingFlags.Public | BindingFlags.Instance );
			object_properties.Add( object_type, properties );

			return properties;
		}

	}

}