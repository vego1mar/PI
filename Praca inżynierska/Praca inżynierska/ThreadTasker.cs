using System;
using System.Threading;
using System.ComponentModel;

namespace PI
{
    static class ThreadTasker
    {

        internal static int StartThreadSafe( Thread thread, string invokerName )
        {
            try {
                thread.Start();
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.OBJECT_DISPOSED_EXCEPTION;
            }
            catch ( ThreadStateException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.THREAD_STATE_EXCEPTION;
            }
            catch ( OutOfMemoryException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.OUT_OF_MEMORY_EXCEPTION;
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.EXCEPTION;
            }

            return 0;
        }

        internal static int RunBackgroundWorkerSafe( BackgroundWorker worker, object argument, string invokerName )
        {
            try {
                worker.RunWorkerAsync( argument );
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.OBJECT_DISPOSED_EXCEPTION;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.INVALID_OPERATION_EXCEPTION;
            }
            catch ( ThreadStateException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.THREAD_STATE_EXCEPTION;
            }
            catch ( OutOfMemoryException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.OUT_OF_MEMORY_EXCEPTION;
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.EXCEPTION;
            }

            return 0;
        }

    }
}
