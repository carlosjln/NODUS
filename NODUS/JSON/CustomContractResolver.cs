using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NODUS.Extensions;
using NODUS.JSON.Serializers;

namespace NODUS.JSON {
	public class CustomContractResolver : DefaultContractResolver {
		protected override string ResolvePropertyName( string property_name ) {
			return Regex.Replace( property_name, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4" ).ToLower( );
		}

		readonly Type node_action_type = typeof(NodeAction);

		protected override JsonConverter ResolveContractConverter( Type object_type ) {
			if( object_type.inherits_from( node_action_type ) ) return new NodeActionSerializer();
			
			return base.ResolveContractConverter( object_type );
		}
	}
}