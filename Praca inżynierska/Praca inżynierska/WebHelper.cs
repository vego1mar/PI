using System;
using System.IO;
using System.Net;

namespace PI
{

    internal static class WebHelper
    {

        internal static string GetContentThroughHttp( string uri )
        {
            try {
                HttpWebRequest request = WebRequest.CreateHttp( uri );
                return GetHttpResponse( request );
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteException( x );
            }
            catch ( NotSupportedException x ) {
                Logger.WriteException( x );
            }
            catch ( System.Security.SecurityException x ) {
                Logger.WriteException( x );
            }
            catch ( UriFormatException x ) {
                Logger.WriteException( x );
            }
            catch ( InvalidCastException x ) {
                Logger.WriteException( x );
            }
            catch ( ProtocolViolationException x ) {
                Logger.WriteException( x );
            }
            catch ( IOException x ) {
                Logger.WriteException( x );
            }
            catch ( OutOfMemoryException x ) {
                Logger.WriteException( x );
            }
            catch ( WebException x ) {
                Logger.WriteException( x );
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteException( x );
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }

            return null;
        }

        private static string GetHttpResponse( HttpWebRequest request )
        {
            string content = string.Empty;

            using ( HttpWebResponse response = (HttpWebResponse) request.GetResponse() ) {
                using ( var reader = new StreamReader( response.GetResponseStream() ) ) {
                    content = reader.ReadToEnd();
                }
            }

            return content;
        }

    }

}
