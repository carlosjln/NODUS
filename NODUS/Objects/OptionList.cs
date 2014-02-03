using System;
using System.Collections.Generic;
using System.Reflection;
using NODUS.Objects.Interfaces;

namespace NODUS.Objects {

	public abstract class OptionList : IOptionList {
		string name;
		public string Name {
			get { return name; }
			set {
				name = value;
				if( Value == null ) Value = value;
			}
		}

		string value;
		public string Value {
			get { return value; }
			set {
				this.value = value;

				if( name == null ) name = value;
			}
		}

		public string Description { get; set; }
	}
	
	public class OptionListItems {
		static readonly Dictionary<Type, IEnumerable<OptionList>> option_lists = new Dictionary<Type, IEnumerable<OptionList>>();

		public static IEnumerable<OptionList> GetAll( Type type ) {
			if( option_lists.ContainsKey( type ) ) return option_lists[type];

			var properties = type.GetFields( BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly );
			var instance = Activator.CreateInstance(type);
			var option_list = new List<OptionList>();

			foreach ( var field_info in properties ) {
				var item = (OptionList) field_info.GetValue( instance );
				option_list.Add( item );
			}
			
			option_lists[type] = option_list;

			return option_list;
		}

		// TODO: consider returning a list
		public static IEnumerable<T> GetAll<T>() where T : OptionList {
			var lists = GetAll( typeof( T ) );
			
			foreach( var item in lists ) {
				yield return (T) item;
			}
		}
	}

	public class OptionList<T> where T : OptionList  {
		public static IEnumerable<OptionList> GetItems( Type type ) {
			return OptionListItems.GetAll( type );
		}

		public static IEnumerable<T> GetItems() {
			return OptionListItems.GetAll<T>();
		}
	}

}