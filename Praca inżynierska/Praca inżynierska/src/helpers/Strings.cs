using log4net;
using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;

namespace PI.src.helpers
{
    public static class Strings
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public static string TryFormatAsNumeric( int decimalPlacesNo, object argument, IFormatProvider provider = null )
        {
            string result = null;

            try {
                if ( provider == null ) {
                    provider = Thread.CurrentThread.CurrentCulture;
                }

                string format = "{0:N" + Math.Abs( decimalPlacesNo ) + "}";
                result = string.Format( provider, format, argument );
            }
            catch ( FormatException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( ArgumentNullException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( OverflowException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }

            return result;
        }

        public static string GetCommon( double value1, double value2 )
        {
            string str1 = value1.ToString( CultureInfo.InvariantCulture );
            string str2 = value2.ToString( CultureInfo.InvariantCulture );

            if ( str1 == str2 ) {
                return str1;
            }

            StringBuilder builder = new StringBuilder();
            int commonLength = Math.Min( str1.Length, str2.Length );

            for ( int i = 0; i < commonLength; i++ ) {
                if ( str1[i] == str2[i] ) {
                    builder.Append( str2[i] );
                    continue;
                }

                break;
            }

            return builder.ToString();
        }
    }
}
