using System.IO;
using System;
using System.Globalization;

namespace PI
{
    static class Logger
    {
        #region Constants
        public const string WRITER_PATH = @"..\..\Logs\log.txt";
        public const int WRITER_BUFFER_SIZE = 4096;
        #endregion

        #region Properties
        private static StreamWriter LogWriter { set; get; }
        public static uint NumberOfInvokesForExceptionInfoWriter { get; private set; } = 0;
        #endregion

        public static int Initialize()
        {
            try {
                LogWriter = new StreamWriter( WRITER_PATH, false, System.Text.Encoding.UTF8, WRITER_BUFFER_SIZE );
                PrintLogHeader();
            }
            catch ( ArgumentNullException ) {
                return SharedConstants.ARGUMENT_NULL_EXCEPTION;
            }
            catch ( ArgumentOutOfRangeException ) {
                return SharedConstants.ARGUMENT_OUT_OF_RANGE_EXCEPTION;
            }
            catch ( ArgumentException ) {
                return SharedConstants.ARGUMENT_EXCEPTION;
            }
            catch ( DirectoryNotFoundException ) {
                return SharedConstants.DIRECTORY_NOT_FOUND_EXCEPTION;
            }
            catch ( PathTooLongException ) {
                return SharedConstants.PATH_TOO_LONG_EXCEPTION;
            }
            catch ( IOException ) {
                return SharedConstants.IO_EXCEPTION;
            }
            catch ( System.Security.SecurityException ) {
                return SharedConstants.SECURITY_EXCEPTION;
            }
            catch ( UnauthorizedAccessException ) {
                return SharedConstants.UNAUTHORIZED_ACCESS_EXCEPTION;
            }
            catch ( Exception ) {
                return SharedConstants.EXCEPTION;
            }

            return 0;
        }

        private static void PrintLogHeader()
        {
            WriteLine( DateTime.Now.ToString( CultureInfo.InvariantCulture ) + " " + DateTime.Now.DayOfWeek );
        }

        public static int WriteLine( string text )
        {
            try {
                LogWriter.WriteLine( text );
            }
            catch ( ObjectDisposedException ) {
                return SharedConstants.OBJECT_DISPOSED_EXCEPTION;
            }
            catch ( IOException ) {
                return SharedConstants.IO_EXCEPTION;
            }
            catch ( Exception ) {
                return SharedConstants.EXCEPTION;
            }

            return 0;
        }

        public static int Write( string text )
        {
            try {
                LogWriter.Write( text );
            }
            catch ( ObjectDisposedException ) {
                return SharedConstants.OBJECT_DISPOSED_EXCEPTION;
            }
            catch ( IOException ) {
                return SharedConstants.IO_EXCEPTION;
            }
            catch ( NotSupportedException ) {
                return SharedConstants.NOT_SUPPORTED_EXCEPTION;
            }
            catch ( Exception ) {
                return SharedConstants.EXCEPTION;
            }

            return 0;
        }

        public static int WriteExceptionInfo( Exception x, string methodNameContext )
        {
            NumberOfInvokesForExceptionInfoWriter++;

            return
            Write( Environment.NewLine + Environment.NewLine + DateTime.Now.ToString( CultureInfo.InvariantCulture ) + Environment.NewLine +
                   "EXCEPTION TYPE: " + x.GetType() + Environment.NewLine +
                   "METHOD NAME CONTEXT: " + methodNameContext + Environment.NewLine +
                   "STACK TRACE:" + Environment.NewLine + x.StackTrace + Environment.NewLine +
                   "MESSAGE:" + Environment.NewLine + x.Message + Environment.NewLine +
                   "SOURCE: " + x.Source + Environment.NewLine +
                   "DATA: " + x.Data + Environment.NewLine +
                   "INNER EXCEPTION:" + Environment.NewLine + x.InnerException + Environment.NewLine +
                   "TARGET SITE:" + Environment.NewLine + x.TargetSite + Environment.NewLine +
                   Environment.NewLine + Environment.NewLine
                   );
        }

        public static void Close()
        {
            string methodName = "PI.Logger.Close()";

            try {
                LogWriter.Close();
            }
            catch ( System.Text.EncoderFallbackException x ) {
                WriteExceptionInfo( x, methodName );
            }
            catch ( Exception x ) {
                WriteExceptionInfo( x, methodName );
            }
        }

        public static string GetFullPathOfLogFileLocation() 
        {
            string invoker = "PI.Logger.GetFullPathOfLogLocation()";
            string fullPath = null;

            try {
                fullPath = ((FileStream) (LogWriter.BaseStream)).Name;
            }
            catch ( InvalidCastException x ) {
                WriteExceptionInfo( x, invoker );
            }
            catch ( Exception x ) {
                WriteExceptionInfo( x, invoker );
            }

            return fullPath;
        }

    }
}
