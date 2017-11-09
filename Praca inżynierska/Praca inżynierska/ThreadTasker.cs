using System;
using System.Threading;

namespace PI
{
    static class ThreadTasker
    {

        internal static Enums.Exceptions StartThreadSafe( Thread thread )
        {
            try {
                thread.Start();
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteException( x );
                return Enums.Exceptions.ObjectDisposedException;
            }
            catch ( ThreadStateException x ) {
                Logger.WriteException( x );
                return Enums.Exceptions.ThreadStateException;
            }
            catch ( OutOfMemoryException x ) {
                MsgBxShower.General.OutOfMemoryExceptionStop();
                Logger.WriteException( x );
                return Enums.Exceptions.OutOfMemoryException;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return Enums.Exceptions.Exception;
            }

            return 0;
        }

    }
}
