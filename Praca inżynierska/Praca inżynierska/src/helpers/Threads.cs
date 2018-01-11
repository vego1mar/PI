using log4net;
using PI.src.application;
using System;
using System.Reflection;
using System.Threading;

namespace PI.src.helpers
{
    public static class Threads
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public static bool TryStart( Thread thread )
        {
            try {
                thread.Start();
            }
            catch ( NullReferenceException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( ObjectDisposedException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( ThreadStateException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( OutOfMemoryException ex ) {
                log.Error( ex.Message, ex );
                Messages.General.OutOfMemoryExceptionStop();
                return false;
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
                return false;
            }

            return true;
        }
    }
}
