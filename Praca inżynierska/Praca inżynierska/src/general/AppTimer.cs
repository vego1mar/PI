using PI.src.helpers;
using System;

namespace PI.src.general
{
    public class AppTimer
    {
        private readonly System.Threading.Thread thread;
        private ulong days;
        private ushort hours;
        private ushort minutes;
        private ushort seconds;
        private readonly OnCountEventArgs countEventArgs;
        public event EventHandler<OnCountEventArgs> OnCount;

        public AppTimer()
        {
            thread = new System.Threading.Thread( new System.Threading.ThreadStart( OnThreadStart ) );
            days = 0;
            hours = 0;
            minutes = 0;
            seconds = 0;
            countEventArgs = new OnCountEventArgs() { Time = "0:00:00:00" };
        }

        private void OnThreadStart()
        {
            System.Threading.Thread.CurrentThread.IsBackground = true;
            System.Timers.Timer timer = new System.Timers.Timer( 1000 );
            timer.Elapsed += new System.Timers.ElapsedEventHandler( OnElapsed );
            timer.Start();
        }

        protected virtual void OnElapsed( object sender, System.Timers.ElapsedEventArgs e )
        {
            seconds++;

            if ( seconds > 59 ) {
                seconds = 0;
                minutes++;
            }

            if ( minutes > 59 ) {
                minutes = 0;
                hours++;
            }

            if ( hours > 23 ) {
                hours = 0;
                days++;
            }

            countEventArgs.Time = days.ToString() + ':' + hours.ToString( "00" ) + ':' + minutes.ToString( "00" ) + ':' + seconds.ToString( "00" );
            OnCount( this, countEventArgs );
        }

        public void Start()
        {
            if ( thread.ThreadState == System.Threading.ThreadState.Unstarted ) {
                Threads.TryStart( thread );
            }
        }
    }

    public class OnCountEventArgs : EventArgs
    {
        public string Time { get; set; }
    }
}
