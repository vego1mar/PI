using log4net;
using System;
using System.Reflection;
using System.Threading;

namespace PI.src.helpers
{
    public static class StringFormatter
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public static string TryAsNumeric( int decimalPlacesNo, object argument, IFormatProvider provider = null )
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
    }
}
