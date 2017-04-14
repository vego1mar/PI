using System;

namespace PI
{
    static class SystemInfoHelper
    {

        #region ObtainUsedDotNetFrameworkVersion() : string
        static public string ObtainUsedDotNetFrameworkVersion()
        {
            string version = null;

            try {
                version = System.Diagnostics.FileVersionInfo.GetVersionInfo( typeof( int ).Assembly.Location ).ProductVersion;
            }
            catch ( NotSupportedException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( System.IO.FileNotFoundException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
            }

            return version;
        }
        #endregion

        #region ObtaingApplicationRunningOSVersion() : string
        static public string ObtaingApplicationRunningOSVersion()
        {
            string osVersion = null;

            try {
                osVersion = Environment.OSVersion.Version.ToString();
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
            }

            return osVersion;
        }
        #endregion

    }
}
