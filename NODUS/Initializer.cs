using System;
using System.Reflection;
using NODUS.Interfaces;
using NODUS.Extensions;
using NODUS.Utilities;

namespace NODUS {

	public sealed class Initializer {
		public static void Run() {
			var initializers = AssemblyScanner.GetImplementations<IInitializer>().to_list();
			
			// Ensure NODUS initialization first
			SettingsInitializer.Run();

			foreach( var initializer in initializers ) {
				InvokeRunMethod( initializer );
			}
		}

		public static void InvokeRunMethod(Type type) {
			// Get the method
			const string method_name = "Run";
			var method = type.GetMethod( method_name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
			
			if (method == null) throw new ArgumentException( String.Format("The type {0} doesn't have a static method named {1}", type, method_name));
			
			// Invoke it
			method.Invoke(null, null);
		}
	}
	
}