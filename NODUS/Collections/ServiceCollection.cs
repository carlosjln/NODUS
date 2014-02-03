using System;
using System.Collections.Generic;

namespace NODUS.Collections {

	public class ServiceProvider {
		static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

		public static T Get<T>( ) {
			return (T) services[ typeof(T) ];
		}

		public static void Set<T>( T service ) {
			services[ typeof(T) ] = service;
		}
	}
}