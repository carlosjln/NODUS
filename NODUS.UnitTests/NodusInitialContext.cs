using System;
using NODUS;
using NODUS.Collections;
using NODUS.Interfaces;

namespace Nodus.UnitTests {
	public abstract class NodusInitialContext {
		static bool initialized;

		protected NodusInitialContext() {
			if( initialized ) return;

			// NODUS INITIALIZATION
			var base_directory = AppDomain.CurrentDomain.BaseDirectory;
			Application.SetBinDirectory( base_directory );
			Application.SetBaseDirectory( base_directory.Replace( "bin", "" ) );

			Initializer.Run( );

			// OVERRIDE SERVICES
			ServiceProvider.Set<IHttpContextProvider>( new CustomHttpContextProvider() );

			initialized = true;
		}
	}
}