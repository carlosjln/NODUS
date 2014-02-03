using System.Configuration;

namespace NODUS.Interfaces {

	public interface IModuleSettings {
		ConnectionStringSettingsCollection ConnectionStrings { get; }
	}

}