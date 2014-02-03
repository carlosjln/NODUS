namespace NODUS.Extensions {
	
	public static class node_extensions {

		public static string get_location( this Node node ) {
			return @"Views\" + node.Namespace.Replace( ".", @"\" );
		}
	}

}