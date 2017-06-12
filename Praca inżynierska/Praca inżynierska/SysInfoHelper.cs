using System;

namespace PI
{
    static class SysInfoHelper
    {

        public static string Context { get; set; } 

        static public string ObtainUsedDotNetFrameworkVersion()
        {
            Logger.Context = Context;
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
            finally {
                Logger.Context = string.Empty;
            }

            return version;
        }

        static public string ObtaingApplicationRunningOSVersion()
        {
            Logger.Context = Context;
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
            finally {
                Logger.Context = string.Empty;
            }

            return osVersion;
        }

    }
}
