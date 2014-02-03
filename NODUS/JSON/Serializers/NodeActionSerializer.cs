using System;
using Newtonsoft.Json;
using NODUS.Extensions;

namespace NODUS.JSON.Serializers {
	public class NodeActionSerializer : JsonConverter {
		public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer ) {
			var action = value as NodeAction;

			writer.WriteStartObject( );

			if( action != null ) {
				writer.WritePropertyName( "id" );
				writer.WriteValue( action.Id.ToString( ) );

				writer.WritePropertyName( "name" );
				writer.WriteValue( action.Name.to_snake_case( ) );

				writer.WritePropertyName( "caption" );
				writer.WriteValue( action.Caption );
			}

			writer.WriteEndObject( );
		}

		public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer ) {
			throw new NotImplementedException( );
		}

		public override bool CanConvert( Type object_type ) {
			return true;
		}
	}
}