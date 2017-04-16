using System;

namespace PI
{
    static class StringFormatter
    {

        static public string FormatAsNumeric( uint numberOfDecimalPlaces, object argument, string invoker )
        {
            string result = null;
            string format = "{0:N" + numberOfDecimalPlaces + "}";

            try {
                result = string.Format( format, argument );
            }
            catch ( FormatException x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }

            return result;
        }

    }
}
