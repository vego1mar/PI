using System;

namespace PI
{
    static class StringFormatter
    {

        public static string Context { get; set; } 

        static public string FormatAsNumeric( uint numberOfDecimalPlaces, object argument )
        {
            Logger.Context = Context;
            string result = null;
            string format = "{0:N" + numberOfDecimalPlaces + "}";

            try {
                result = string.Format( format, argument );
            }
            catch ( FormatException x ) {
                Logger.WriteException( x );
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
            finally {
                Logger.Context = string.Empty;
            }

            return result;
        }

    }
}
