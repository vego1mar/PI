using System;
using System.Threading;
using System.ComponentModel;

namespace PI
{
    static class ThreadTasker
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

        #region RunBackgroundWorkerSafe(...) : int
        internal static int RunBackgroundWorkerSafe( BackgroundWorker worker, object argument )
        {
            try {
                worker.RunWorkerAsync( argument );
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteExceptionInfo( x );
                return SharedConstants.OBJECT_DISPOSED_EXCEPTION;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x );
                return SharedConstants.INVALID_OPERATION_EXCEPTION;
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
