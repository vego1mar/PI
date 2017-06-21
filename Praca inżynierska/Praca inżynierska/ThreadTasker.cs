using System;
using System.Threading;

namespace PI
{
    static class ThreadTasker
    {

        internal static int StartThreadSafe( Thread thread )
        {
            try {
                thread.Start();
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteException( x );
                return Consts.Exceptions.OBJECT_DISPOSED;
            }
            catch ( ThreadStateException x ) {
                Logger.WriteException( x );
                return Consts.Exceptions.THREAD_STATE;
            }
            catch ( OutOfMemoryException x ) {
                Logger.WriteException( x );
                return Consts.Exceptions.OUT_OF_MEMORY;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return Consts.Exceptions.EXCEPTION;
            }

            return 0;
        }

    }
}
