using System;
using System.Threading;

namespace PI
    {
    static class Tasker
        {

        #region StartThreadSafe(...) : int
        internal static int StartThreadSafe( Thread thread )
            {
            try {
                thread.Start();
                }
            catch ( ObjectDisposedException x ) {
                Logger.WriteExceptionInfo( x );
                return SharedConstants.OBJECT_DISPOSED_EXCEPTION;
                }
            catch ( ThreadStateException x ) {
                Logger.WriteExceptionInfo( x );
                return SharedConstants.THREAD_STATE_EXCEPTION;
                }
            catch ( OutOfMemoryException x ) {
                Logger.WriteExceptionInfo( x );
                return SharedConstants.OUT_OF_MEMORY_EXCEPTION;
                }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
                return SharedConstants.EXCEPTION;
                }

            return 0;
            }
        #endregion

        }
    }
