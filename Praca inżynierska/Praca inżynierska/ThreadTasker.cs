using System;
using System.Threading;

namespace PI
{
    static class ThreadTasker
    {

        public static string Context { get; set; }

        internal static int StartThreadSafe( Thread thread )
        {
            Logger.Context = Context;

            try {
                thread.Start();
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteException( x );
                return Constants.Exceptions.OBJECT_DISPOSED;
            }
            catch ( ThreadStateException x ) {
                Logger.WriteException( x );
                return Constants.Exceptions.THREAD_STATE;
            }
            catch ( OutOfMemoryException x ) {
                Logger.WriteException( x );
                return Constants.Exceptions.OUT_OF_MEMORY;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return Constants.Exceptions.EXCEPTION;
            }
            finally {
                Logger.Context = string.Empty;
            }

            return 0;
        }

    }
}
