using System;
using System.Diagnostics;
using System.Reflection;
using log4net;

namespace PI.src.helpers
{
    public static class SystemInfo
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public static string TryGetDotNetFrameworkVersion()
        {
            string version = null;

            try {
                version = FileVersionInfo.GetVersionInfo( typeof( int ).Assembly.Location ).ProductVersion;
            }
            catch ( NotSupportedException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( System.IO.FileNotFoundException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }

            return version;
        }

        public static string TryGetOSVersion()
        {
            string osVersion = null;

            try {
                osVersion = Environment.OSVersion.Version.ToString();
            }
            catch ( InvalidOperationException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }

            return osVersion;
        }
    }
}
