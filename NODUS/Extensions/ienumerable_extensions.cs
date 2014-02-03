using System;
using System.Collections.Generic;

namespace NODUS.Extensions {

	public static class ienumerable_extensions {
		public static List<T> to_list< T >( this IEnumerable<T> enumerable ) {
			var list = new List<T>();

			foreach( var item in enumerable ) {
				list.Add( item );
			}
			
			return list;
		}

		public static List<T> to_list< T >( this IEnumerable<T> enumerable, Func<T,bool> filter ) {
			return enumerable.filter( filter );
		}

		public static List<T> filter< T >( this IEnumerable<T> enumerable, Func<T,bool> filter ) {
			var list = new List<T>();

			foreach( var item in enumerable ) {
				if( filter(item) ) list.Add( item );
			}

			return list;
		}

		public static T first<T>( this IEnumerable<T> enumerable, Func<T,bool> filter ) {
			foreach( var item in enumerable ) {
				if( filter(item) ) return item;
			}

			return default(T);
		}
	}

}