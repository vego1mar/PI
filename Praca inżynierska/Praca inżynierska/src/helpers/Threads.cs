using log4net;
using PI.src.messages;
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
            string signature = string.Empty;

            try {
                signature = MethodBase.GetCurrentMethod().Name + '(' + thread.Name + ',' + thread.ThreadState + ',' + thread.ManagedThreadId + ')';
                thread.Start();
            }
            catch ( NullReferenceException ex ) {
                log.Error( signature, ex );
                return false;
            }
            catch ( ObjectDisposedException ex ) {
                log.Error( signature, ex );
                return false;
            }
            catch ( ThreadStateException ex ) {
                log.Error( signature, ex );
                return false;
            }
            catch ( OutOfMemoryException ex ) {
                log.Error( signature, ex );
                AppMessages.General.StopOfOutOfMemoryException();
                return false;
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
                return false;
            }

            return true;
        }
    }
}
