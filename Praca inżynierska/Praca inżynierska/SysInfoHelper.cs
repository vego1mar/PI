using System;

namespace PI
{
    static class SysInfoHelper
    {

        static public string ObtainUsedDotNetFrameworkVersion()
        {
            string version = null;

            try {
                version = System.Diagnostics.FileVersionInfo.GetVersionInfo( typeof( int ).Assembly.Location ).ProductVersion;
            }
            catch ( NotSupportedException x ) {
                Logger.WriteException( x );
            }
            catch ( System.IO.FileNotFoundException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }

            return version;
        }

        static public string ObtaingApplicationRunningOSVersion()
        {
            string osVersion = null;

            try {
                osVersion = Environment.OSVersion.Version.ToString();
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }

            return osVersion;
        }

    }
}
