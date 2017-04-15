using System;

namespace PI
{
    static class SystemInfoHelper
    {

        static public string ObtainUsedDotNetFrameworkVersion( string invokerName )
        {
            string version = null;

            try {
                version = System.Diagnostics.FileVersionInfo.GetVersionInfo( typeof( int ).Assembly.Location ).ProductVersion;
            }
            catch ( NotSupportedException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( System.IO.FileNotFoundException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }

            return version;
        }

        static public string ObtaingApplicationRunningOSVersion( string invokerName )
        {
            string osVersion = null;

            try {
                osVersion = Environment.OSVersion.Version.ToString();
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }

            return osVersion;
        }

    }
}
