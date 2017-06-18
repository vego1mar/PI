using System.IO;
using System;
using System.Globalization;

namespace PI
{

    static class Logger
    {

        public const string WRITER_PATH = @"..\..\Logs\log.txt";
        public const int WRITER_BUFFER_SIZE = 4096;
        private static StreamWriter LogWriter { set; get; }
        public static uint NumberOfLoggedExceptions { get; private set; } = 0;

        public static int Initialize()
        {
            try {
                LogWriter = new StreamWriter( WRITER_PATH, false, System.Text.Encoding.UTF8, WRITER_BUFFER_SIZE );
                PrintLogHeader();
            }
            catch ( ArgumentNullException ) {
                return Constants.Exceptions.ARGUMENT_NULL;
            }
            catch ( ArgumentOutOfRangeException ) {
                return Constants.Exceptions.ARGUMENT_OUT_OF_RANGE;
            }
            catch ( ArgumentException ) {
                return Constants.Exceptions.ARGUMENT;
            }
            catch ( DirectoryNotFoundException ) {
                return Constants.Exceptions.DIRECTORY_NOT_FOUND;
            }
            catch ( PathTooLongException ) {
                return Constants.Exceptions.PATH_TOO_LONG;
            }
            catch ( IOException ) {
                return Constants.Exceptions.IO;
            }
            catch ( System.Security.SecurityException ) {
                return Constants.Exceptions.SECURITY;
            }
            catch ( UnauthorizedAccessException ) {
                return Constants.Exceptions.UNAUTHORIZED_ACCESS;
            }
            catch ( Exception ) {
                return Constants.Exceptions.EXCEPTION;
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
                return Constants.Exceptions.OBJECT_DISPOSED;
            }
            catch ( IOException ) {
                return Constants.Exceptions.IO;
            }
            catch ( Exception ) {
                return Constants.Exceptions.EXCEPTION;
            }

            return 0;
        }

        public static int Write( string text )
        {
            try {
                LogWriter.Write( text );
            }
            catch ( ObjectDisposedException ) {
                return Constants.Exceptions.OBJECT_DISPOSED;
            }
            catch ( IOException ) {
                return Constants.Exceptions.IO;
            }
            catch ( NotSupportedException ) {
                return Constants.Exceptions.NOT_SUPPORTED;
            }
            catch ( Exception ) {
                return Constants.Exceptions.EXCEPTION;
            }

            return 0;
        }

        public static int WriteException( Exception x )
        {
            NumberOfLoggedExceptions++;

            return
            Write( Environment.NewLine + Environment.NewLine +
                    DateTime.Now.ToString( CultureInfo.InvariantCulture ) + Environment.NewLine +
                   "EXCEPTION TYPE: " + x.GetType() + Environment.NewLine +
                   "STACK TRACE:" + Environment.NewLine + x.StackTrace + Environment.NewLine +
                   "MESSAGE:" + Environment.NewLine + x.Message + Environment.NewLine +
                   "SOURCE: " + x.Source + Environment.NewLine +
                   Environment.NewLine + Environment.NewLine
                   );
        }

        public static void Close()
        {
            try {
                LogWriter.Close();
            }
            catch ( System.Text.EncoderFallbackException x ) {
                WriteException( x );
            }
            catch ( Exception x ) {
                WriteException( x );
            }
        }

        public static string GetFullPathOfLogFileLocation()
        {
            string fullPath = null;

            try {
                fullPath = ((FileStream) (LogWriter.BaseStream)).Name;
            }
            catch ( InvalidCastException x ) {
                WriteException( x );
            }
            catch ( Exception x ) {
                WriteException( x );
            }

            return fullPath;
        }

    }
}
