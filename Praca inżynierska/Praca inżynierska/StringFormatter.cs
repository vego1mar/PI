using System;

namespace PI
{
    static class StringFormatter
    {

        static public string FormatAsNumeric( uint numberOfDecimalPlaces, object argument )
        {
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

            return result;
        }

    }
}
