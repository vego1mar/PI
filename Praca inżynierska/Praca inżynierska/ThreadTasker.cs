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
                Logger.WriteException( x, LoggerSection.ThreadTasker );
                return Constants.Exceptions.OBJECT_DISPOSED;
            }
            catch ( ThreadStateException x ) {
                Logger.WriteException( x, LoggerSection.ThreadTasker );
                return Constants.Exceptions.THREAD_STATE;
            }
            catch ( OutOfMemoryException x ) {
                Logger.WriteException( x, LoggerSection.ThreadTasker );
                return Constants.Exceptions.OUT_OF_MEMORY;
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.ThreadTasker );
                return Constants.Exceptions.EXCEPTION;
            }

            return 0;
        }

    }
}
