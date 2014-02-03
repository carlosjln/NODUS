using System;
using System.Collections.Generic;
using NODUS.Extensions;
using StructureMap;

namespace NODUS.Utilities {

	public static class Create {

		public static T InstanceOf<T>() {
			return ObjectFactory.GetInstance<T>();
		}

		public static T InstanceOf<T>(Action<T> action) {
			var instance = ObjectFactory.GetInstance<T>();
			action(instance);

			return instance;
		}

		public static object InstanceOf( Type type ) {
			return ObjectFactory.GetInstance( type );
		}

		private static readonly Dictionary<string, Object> initialized_intances = new Dictionary<string, object>();

		public static T SingletonOf<T>() {
			return (T) SingletonOf( typeof (T) );
		}
		public static object SingletonOf(Type type) {
			var type_name = type.AssemblyQualifiedName.encrypt_using_sha256();

			if( type_name == null ) {
				throw new Exception("The assembly qualified name can not be identified.");
			}

			if( initialized_intances.ContainsKey(type_name) ) {
				return initialized_intances[type_name];
			}

			var instance = ObjectFactory.GetInstance( type );
			
			initialized_intances[type_name] = instance;

			return instance;
		}

		public static void AddInstance(object instance) {
			var type_name = instance.GetType().AssemblyQualifiedName;

			if( type_name == null ) {
				throw new Exception("The assembly qualified name can not be identified.");
			}

			if( initialized_intances.ContainsKey(type_name) == false ) {
				initialized_intances.Add(type_name, instance);
			}
		}

	}

}