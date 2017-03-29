using System.IO;
using System;
using System.Globalization;

namespace Praca_inżynierska
    {
    #region Logger
    /// <summary>
    /// Builds logs using standard output of Console.Write or Console.WriteLine.
    /// </summary>

    static class Logger
        {
        #region Members
        public const string WRITER_PATH = @"..\..\Logs\log.txt";
        public const int WRITER_BUFFER_SIZE = 4096;

        private static StreamWriter m_Writer;
        #endregion

        #region Initialize() : int
        public static int Initialize()
            {
            try {
                m_Writer = new StreamWriter( WRITER_PATH, false, System.Text.Encoding.UTF8, WRITER_BUFFER_SIZE );
                PrintLogHeader();
                }
            catch ( ArgumentNullException ) {
                return SharedDefinitions.ARGUMENT_NULL_EXCEPTION;
                }
            catch ( ArgumentOutOfRangeException ) {
                return SharedDefinitions.ARGUMENT_OUT_OF_RANGE_EXCEPTION;
                }
            catch ( ArgumentException ) {
                return SharedDefinitions.ARGUMENT_EXCEPTION;
                }
            catch ( DirectoryNotFoundException ) {
                return SharedDefinitions.DIRECTORY_NOT_FOUND_EXCEPTION;
                }
            catch ( PathTooLongException ) {
                return SharedDefinitions.PATH_TOO_LONG_EXCEPTION;
                }
            catch ( IOException ) {
                return SharedDefinitions.IO_EXCEPTION;
                }
            catch ( System.Security.SecurityException ) {
                return SharedDefinitions.SECURITY_EXCEPTION;
                }
            catch ( UnauthorizedAccessException ) {
                return SharedDefinitions.UNAUTHORIZED_ACCESS_EXCEPTION;
                }
            catch ( Exception ) {
                return SharedDefinitions.EXCEPTION;
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
                m_Writer.WriteLine( text );
                }
            catch ( ObjectDisposedException ) {
                return SharedDefinitions.OBJECT_DISPOSED_EXCEPTION;
                }
            catch ( IOException ) {
                return SharedDefinitions.IO_EXCEPTION;
                }
            catch ( Exception ) {
                return SharedDefinitions.EXCEPTION;
                }

            return 0;
            }
        #endregion

        #region Write(...) : int
        public static int Write( string text )
            {
            try {
                m_Writer.Write( text );
                }
            catch ( ObjectDisposedException ) {
                return SharedDefinitions.OBJECT_DISPOSED_EXCEPTION;
                }
            catch ( IOException ) {
                return SharedDefinitions.IO_EXCEPTION;
                }
            catch ( NotSupportedException ) {
                return SharedDefinitions.NOT_SUPPORTED_EXCEPTION;
                }
            catch ( Exception ) {
                return SharedDefinitions.EXCEPTION;
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
                m_Writer.Close();
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
