using System.Reflection;
using System.Runtime.InteropServices;

[assembly: Guid( "10BC0F23-9C53-452C-96A5-4D35572A7C9A" )]

[assembly: AssemblyTitle( "NODUS" )]
[assembly: AssemblyProduct("NODUS")]
[assembly: AssemblyDescription( "Building awesome web applications :)" )]

[assembly: AssemblyCompany("Carlos J. López")]
[assembly: AssemblyCopyright("Copyright © 2013 Carlos J. López")]
[assembly: AssemblyTrademark("")]

[assembly: AssemblyVersion( "1.1.1" )]
//[assembly: AssemblyFileVersion( "1.0.0.*" )]
//[assembly: AssemblyInformationalVersion("1.0.1")]

//[ assembly : System.Web.PreApplicationStartMethod( typeof( HttpHandlers ), "Setup" ) ]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: ComVisible( false )]
[assembly: AssemblyCulture( "" )]