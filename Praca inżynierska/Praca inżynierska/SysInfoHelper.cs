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
                Logger.WriteException( x, LoggerSection.SysInfoHelper );
            }
            catch ( System.IO.FileNotFoundException x ) {
                Logger.WriteException( x, LoggerSection.SysInfoHelper );
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.SysInfoHelper );
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
                Logger.WriteException( x, LoggerSection.SysInfoHelper );
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.SysInfoHelper );
            }

            return osVersion;
        }

    }
}
