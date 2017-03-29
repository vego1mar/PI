using System.Windows.Forms;
using System;
using System.Threading;

// TODO: Update info.
// TODO: Configuration file.
// TODO: Reading set of curves from a file.
// TODO: Internationalization (I18N).

namespace Praca_inżynierska
    {
    #region WfMainWindow : Form
    public partial class WfMainWindow : Form
        {
        
        #region Members
        private bool m_IsPropertiesPanelHidden;
        private int m_PropertiesPanelWidth;
        private Thread m_TimerThread;
        #endregion

        #region WfMainWindow()
        /// <summary>
        /// The default constructor.<para></para>
        /// Initializes the components. Initializes the fields (members).
        /// </summary>

        public WfMainWindow()
            {
            InitializeComponent();
            InitalizeFields();
            }
        #endregion

        #region InitalizeFields() : void
        private void InitalizeFields()
            {
            m_IsPropertiesPanelHidden = false;
            m_PropertiesPanelWidth = wfPropertiesPanel.Size.Width;
            m_TimerThread = null;
            DefineTimerThread();
            m_TimerThread.Start();
            UpdateComponentRelatedWithActualStatusOfTimerThread();
            }
        #endregion

        #region wfViewHideShowPropertiesPanelToolStripMenuItem_Click(...) : void
        /// <summary>
        /// Action: Click<para></para>
        /// Menu root option: View<para></para>
        /// Menu strip option: Hide/Show Properties panel<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfViewHideShowPropertiesPanelToolStripMenuItem_Click( object sender, System.EventArgs e )
            {
            if ( m_IsPropertiesPanelHidden ) {
                wfPropertiesPanel.Visible = true;
                wfPropertiesPanel.Enabled = true;
                var panelSize = wfPropertiesPanel.Size;
                panelSize.Width = m_PropertiesPanelWidth;
                wfPropertiesPanel.Size = panelSize;
                m_IsPropertiesPanelHidden = false;
                }
            else {
                wfPropertiesPanel.Visible = false;
                wfPropertiesPanel.Enabled = false;
                var panelSize = wfPropertiesPanel.Size;
                m_PropertiesPanelWidth = panelSize.Width;
                panelSize.Width = 0;
                wfPropertiesPanel.Size = panelSize;
                m_IsPropertiesPanelHidden = true;
                }
            }
        #endregion

        #region wfViewResizePropertiesPanelToolStripMenuItem_Click(...) : void
        /// <summary>
        /// Action: Click<para></para>
        /// Menu root option: View<para></para>
        /// Menu strip option: Resize Properties panel<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfViewResizePropertiesPanelToolStripMenuItem_Click( object sender, System.EventArgs e )
            {
            var propertiesResizerDialog = new WfPropertiesPanelResizer();

            try {
                propertiesResizerDialog.ShowDialog( this );
                }
            catch ( ArgumentException x ) {
                Logger.WriteExceptionInfo( x );
                }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x );
                }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
                }
            }
        #endregion

        #region WfMainWindow_FormClosed(...) : void
        /// <summary>
        /// Action: Form Closed<para></para>
        /// Root form: WfMainWindow<para></para>
        /// Target: file log closing.<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>
        
        private void WfMainWindow_FormClosed( object sender, FormClosedEventArgs e )
            {
            Logger.Close();
            }
        #endregion

        #region WfMainWindow_Load(...) : void
        /// <summary>
        /// Action: Form Loading<para></para>
        /// Root form: WfMainWindow<para></para>
        /// Target: file log opening.<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>
        
        private void WfMainWindow_Load( object sender, EventArgs e )
            {
            Logger.Initialize();
            }
        #endregion

        #region wfMenuProgramExit_Click(...) : void
        /// <summary>
        /// Action: Click<para></para>
        /// Menu root option: Program<para></para>
        /// Menu strip option: Exit<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfMenuProgramExit_Click( object sender, EventArgs e )
            {
            Application.Exit();
            }
        #endregion

        #region DefineTimerThread() : void
        /// <summary>
        /// Provides a definition for a thread to the member related with the timer thread.
        /// </summary>

        private void DefineTimerThread()
            {
            m_TimerThread = new Thread( () => {
                try {
                    System.Timers.Timer timer = new System.Timers.Timer();
                    InstallEventForTimer( ref timer );
                    timer.Interval = 1000;
                    timer.Start();
                    timer.Enabled = true;
                    }
                catch ( ObjectDisposedException x ) {
                    Logger.WriteExceptionInfo( x );
                    }
                catch ( ArgumentOutOfRangeException x ) {
                    Logger.WriteExceptionInfo( x );
                    }
                catch ( ArgumentException x ) {
                    Logger.WriteExceptionInfo( x );
                    }
                catch ( Exception x ) {
                    Logger.WriteExceptionInfo( x );
                    }
                });
            }
        #endregion

        #region InstallEventForTimer(...) : void
        /// <summary>
        /// Installs an event of type System.Timer.Timers.Elapsed. 
        /// This definiton will be refreshing the timer-related component of Windows Forms on the UI thread.
        /// </summary>
        /// <param name="timer">An instance of a timer from System.Timers.Timer.</param>

        private void InstallEventForTimer( ref System.Timers.Timer timer )
            {
            ushort numberOfSeconds = 0;
            ushort numberOfMinutes = 0;
            ushort numberOfHours = 0;
            ulong numberOfDays = 0;

            timer.Elapsed += ( object sender, System.Timers.ElapsedEventArgs e ) => {
                numberOfSeconds++;

                if ( numberOfSeconds > 59 ) {
                    numberOfSeconds = 0;
                    numberOfMinutes++;
                    }

                if ( numberOfMinutes > 59 ) {
                    numberOfMinutes = 0;
                    numberOfHours++;
                    }

                if ( numberOfHours > 23 ) {
                    numberOfHours = 0;
                    numberOfDays++;
                    }

                string numberOfHoursText = numberOfHours.ToString( "00" );
                string numberOfMinutesText = numberOfMinutes.ToString( "00" );
                string numberOfSecondsText = numberOfSeconds.ToString( "00" );
                string numberOfDaysText = numberOfDays.ToString();
                RefreshComponentRelatedWithTimerThread( numberOfDaysText + ":" + numberOfHoursText + ":" + numberOfMinutesText + ":" + numberOfSecondsText );
                };
            }
        #endregion

        #region RefreshComponentRelatedWithTimerThread(...) : void
        /// <summary>
        /// Sets the given text to the Windows Forms component related with the timer thread and refreshes it.
        /// </summary>
        /// <param name="text">A text to pass to the Windows Forms active control.</param>

        private void RefreshComponentRelatedWithTimerThread( string text )
            {
            try {
                this.BeginInvoke( ( MethodInvoker ) delegate {
                    wfPropertiesProgramCounts2TextBox.Text = text;
                    wfPropertiesProgramCounts2TextBox.Refresh();
                    });
                }
            catch ( ObjectDisposedException x ) {
                Logger.WriteExceptionInfo( x );
                }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x );
                }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
                }
            }
        #endregion

        #region UpdateComponentRelatedWithActualStatusOfTimerThread() : void
        /// <summary>
        /// Sets a status text to the Windows Forms component responsible by informing about the actual status of the timer thread.
        /// </summary>

        private void UpdateComponentRelatedWithActualStatusOfTimerThread()
            {
            if ( m_TimerThread == null ) {
                wfPropertiesProgramActualState2TextBox.Text = "Failure";
                }
            else {
                wfPropertiesProgramActualState2TextBox.Text = "Success";
                }
            }
        #endregion

        }
    #endregion
    }
