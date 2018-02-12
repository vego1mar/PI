using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace PI.src.helpers
{
    public static class Informations
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

        public static string TryGetLogPath()
        {
            string signature = string.Empty;
            string path = null;

            try {
                signature = MethodBase.GetCurrentMethod().Name + "()";
                var rootAppender = ((Hierarchy) LogManager.GetRepository()).Root.Appenders.OfType<FileAppender>().FirstOrDefault();
                path = (rootAppender != null) ? rootAppender.File : string.Empty;
            }
            catch ( FileNotFoundException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }

            return path;
        }
    }
}
