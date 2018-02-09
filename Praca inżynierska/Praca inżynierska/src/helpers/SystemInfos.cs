using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using log4net;

namespace PI.src.helpers
{
    public static class SystemInfos
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public static string TryGetDotNetFrameworkVersion()
        {
            string signature = string.Empty;
            string version = null;

            try {
                signature = MethodBase.GetCurrentMethod().Name + "()";
                version = FileVersionInfo.GetVersionInfo( typeof( int ).Assembly.Location ).ProductVersion;
            }
            catch ( NotSupportedException ex ) {
                log.Error( signature, ex );
            }
            catch ( FileNotFoundException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }

            return version;
        }

        public static string TryGetAssemblyVersion()
        {
            string signature = string.Empty;
            string version = null;

            try {
                signature = MethodBase.GetCurrentMethod().Name + "()";
                version = Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
            catch ( NotSupportedException ex ) {
                log.Error( signature, ex );
            }
            catch ( FileNotFoundException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }

            return version;
        }

        public static string TryGetCompanyName()
        {
            string signature = string.Empty;
            string version = null;

            try {
                signature = MethodBase.GetCurrentMethod().Name + "()";
                version = FileVersionInfo.GetVersionInfo( Assembly.GetEntryAssembly().Location ).CompanyName;
            }
            catch ( NotSupportedException ex ) {
                log.Error( signature, ex );
            }
            catch ( FileNotFoundException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }

            return version;
        }
    }
}
