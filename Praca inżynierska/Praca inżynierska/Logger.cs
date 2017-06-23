using System;
using System.IO;
using System.Globalization;

namespace PI
{

    static class Logger
    {

        public const string WRITER_PATH = @"..\..\Logs\log.txt";
        public const int WRITER_BUFFER_SIZE = 4096;
        private static StreamWriter LogWriter { set; get; }
        public static uint NumberOfLoggedExceptions { get; private set; } = 0;

        public static Enums.Exceptions Initialize()
        {
            try {
                LogWriter = new StreamWriter( WRITER_PATH, false, System.Text.Encoding.UTF8, WRITER_BUFFER_SIZE );
                PrintLogHeader();
            }
            catch ( ArgumentNullException ) {
                return Enums.Exceptions.ArgumentNullException;
            }
            catch ( ArgumentOutOfRangeException ) {
                return Enums.Exceptions.ArgumentOutOfRangeException;
            }
            catch ( ArgumentException ) {
                return Enums.Exceptions.ArgumentException;
            }
            catch ( DirectoryNotFoundException ) {
                return Enums.Exceptions.DirectoryNotFoundException;
            }
            catch ( PathTooLongException ) {
                return Enums.Exceptions.PathTooLongException;
            }
            catch ( IOException ) {
                return Enums.Exceptions.IOException;
            }
            catch ( System.Security.SecurityException ) {
                return Enums.Exceptions.SecurityException;
            }
            catch ( UnauthorizedAccessException ) {
                return Enums.Exceptions.UnauthorizedAccessException;
            }
            catch ( Exception ) {
                return Enums.Exceptions.Exception;
            }

            return Enums.Exceptions.None;
        }

        private static void PrintLogHeader()
        {
            WriteLine( DateTime.Now.ToString( CultureInfo.InvariantCulture ) + " " + DateTime.Now.DayOfWeek );
        }

        public static Enums.Exceptions WriteLine( string text )
        {
            try {
                LogWriter.WriteLine( text );
            }
            catch ( ObjectDisposedException ) {
                return Enums.Exceptions.ObjectDisposedException;
            }
            catch ( IOException ) {
                return Enums.Exceptions.IOException;
            }
            catch ( Exception ) {
                return Enums.Exceptions.Exception;
            }

            return Enums.Exceptions.None;
        }

        public static Enums.Exceptions Write( string text )
        {
            try {
                LogWriter.Write( text );
            }
            catch ( ObjectDisposedException ) {
                return Enums.Exceptions.ObjectDisposedException;
            }
            catch ( IOException ) {
                return Enums.Exceptions.IOException;
            }
            catch ( NotSupportedException ) {
                return Enums.Exceptions.NotSupportedException;
            }
            catch ( Exception ) {
                return Enums.Exceptions.Exception;
            }

            return Enums.Exceptions.None;
        }

        public static Enums.Exceptions WriteException( Exception x )
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
