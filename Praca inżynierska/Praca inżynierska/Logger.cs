using System.IO;
using System;
using System.Globalization;

namespace PI
{
    #region Logger
    /// <summary>
    /// Builds logs using standard output of Console.Write or Console.WriteLine.
    /// </summary>

    static class Logger
    {
        #region Constants
        public const string WRITER_PATH = @"..\..\Logs\log.txt";
        public const int WRITER_BUFFER_SIZE = 4096;
        #endregion

        #region Members
        private static StreamWriter logWriter { set; get; } 
        #endregion

        #region Initialize() : int
        public static int Initialize()
        {
            try {
                logWriter = new StreamWriter( WRITER_PATH, false, System.Text.Encoding.UTF8, WRITER_BUFFER_SIZE );
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
        #endregion

        #region PrintLogHeader() : void
        private static void PrintLogHeader()
        {
            WriteLine( DateTime.Now.ToString( CultureInfo.InvariantCulture ) + " " + DateTime.Now.DayOfWeek );
        }
        #endregion

        #region WriteLine(...) : int
        public static int WriteLine( string text )
        {
            try {
                logWriter.WriteLine( text );
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
        #endregion

        #region Write(...) : int
        public static int Write( string text )
        {
            try {
                logWriter.Write( text );
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
        #endregion

        #region WriteExceptionInfo(...) : int
        public static int WriteExceptionInfo( Exception x )
        {
            return
            Write( Environment.NewLine + Environment.NewLine + DateTime.Now.ToString( CultureInfo.InvariantCulture ) + Environment.NewLine +
                   "STACK TRACE:" + Environment.NewLine + x.StackTrace + Environment.NewLine +
                   "MESSAGE:" + Environment.NewLine + x.Message + Environment.NewLine +
                   "SOURCE:" + Environment.NewLine + x.Source + Environment.NewLine +
                   "DATA:" + Environment.NewLine + x.Data + Environment.NewLine +
                   "INNER EXCEPTION:" + Environment.NewLine + x.InnerException + Environment.NewLine +
                   "TARGET SITE:" + Environment.NewLine + x.TargetSite + Environment.NewLine +
                   Environment.NewLine + Environment.NewLine
                   );
        }
        #endregion

        #region Close() : void
        public static void Close()
        {
            try {
                logWriter.Close();
            }
            catch ( System.Text.EncoderFallbackException x ) {
                WriteExceptionInfo( x );
            }
            catch ( Exception x ) {
                WriteExceptionInfo( x );
            }
        }
        #endregion
    }
    #endregion
}
