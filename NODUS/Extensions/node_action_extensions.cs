using Newtonsoft.Json;

namespace NODUS.Extensions {
	public static class node_action_extensions {
		public static string to_json( this NodeAction node_action ) {
			var json_serializer_settings = Application.JsonSerializerSettings;
			var json_converters = json_serializer_settings.Converters;
			json_converters.Clear();

			return JsonConvert.SerializeObject( node_action, Formatting.None, json_serializer_settings );
		}
	}
}