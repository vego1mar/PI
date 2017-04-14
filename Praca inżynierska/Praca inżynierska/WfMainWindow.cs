using System.Windows.Forms;
using System;
using System.Threading;

// INTERNAL BACKLOG
// TODO: Update info
// TODO: Configuration file
// TODO: Reading set of curves from a file
// TODO: Saving set of curves into a file
// TODO: I18N
// TODO: Refreshing charts and curves datasheet automatically
// TODO: Using DataGridView for displaying dataset of curves in a new modal window
// TODO: Drawing curves in charts
// TODO: Implement Gaussian noise option
// TODO: Menu rebuild - adding icons etc. + functionality
// TODO: Move timer definition to ThreadTasker and generalize the methods
// TODO: Log viewier from the application runtime context
// TODO: Center dialog windows and message boxes
// TODO: Working on threads
// TODO: Errors notification icons

namespace PI
{
    #region WfMainWindow : Form
    public partial class WfMainWindow : Form
    {

        #region Members
        private bool IsPropertiesPanelHidden;
        private int PropertiesPanelWidth;
        private Thread TimerThread;
        private CurvesDataset ChartsCurvesDataset;
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
            IsPropertiesPanelHidden = false;
            PropertiesPanelWidth = wfPropertiesPanel.Size.Width;
            TimerThread = null;
            DefineTimerThread();
            ThreadTasker.StartThreadSafe( TimerThread );
            UpdateComponentRelatedWithActualStatusOfTimerThread();
            ChartsCurvesDataset = new CurvesDataset();
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
            if ( IsPropertiesPanelHidden ) {
                wfPropertiesPanel.Visible = true;
                wfPropertiesPanel.Enabled = true;
                var panelSize = wfPropertiesPanel.Size;
                panelSize.Width = PropertiesPanelWidth;
                wfPropertiesPanel.Size = panelSize;
                IsPropertiesPanelHidden = false;
            }
            else {
                wfPropertiesPanel.Visible = false;
                wfPropertiesPanel.Enabled = false;
                var panelSize = wfPropertiesPanel.Size;
                PropertiesPanelWidth = panelSize.Width;
                panelSize.Width = 0;
                wfPropertiesPanel.Size = panelSize;
                IsPropertiesPanelHidden = true;
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
            using ( var propertiesResizerDialog = new WfPropertiesPanelResizer() ) {
                WindowsFormsHelper.ShowDialogSafe( propertiesResizerDialog, this );
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
        /// Target: file log opening, Program tab info<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void WfMainWindow_Load( object sender, EventArgs e )
        {
            Logger.Initialize();
            UpdateComponentRelatedWithDotNetFrameworkVersion();
            UpdateComponentRelatedWithOSVersionName();
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
            TimerThread = new Thread( () => {
                try {
                    Thread.CurrentThread.IsBackground = true;
                    System.Timers.Timer timer = new System.Timers.Timer();
                    InstallEventForTimer( ref timer );
                    timer.Interval = 1000;
                    timer.Start();
                    timer.Enabled = true;
                }
                catch ( ThreadStateException x ) {
                    Logger.WriteExceptionInfo( x );
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
            } );
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
                this.BeginInvoke( (MethodInvoker) delegate {
                    wfPropertiesProgramCounts2TextBox.Text = text;
                    wfPropertiesProgramCounts2TextBox.Refresh();
                } );
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
            if ( TimerThread == null ) {
                wfPropertiesProgramActualState2TextBox.Text = SharedConstants.TIMER_START_FAILURE;
            }
            else {
                wfPropertiesProgramActualState2TextBox.Text = SharedConstants.TIMER_START_SUCCESS;
            }
        }
        #endregion

        #region wfPropertiesGenerateDefineButton_Click(...) : void
        /// <summary>
        /// Action: Click<para></para>
        /// Properties root tab: Generate<para></para>
        /// Properties tab section: Pattern curve scaffold<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfPropertiesGenerateDefineButton_Click( object sender, EventArgs e )
        {
            using ( var PCDDialog = new PatternCurveDefiner() ) {
                WindowsFormsHelper.ShowDialogSafe( PCDDialog, this );

                try {
                    if ( PCDDialog.DialogResult == DialogResult.OK ) {
                        PreSets.ChosenPatternCurveScaffold = PCDDialog.ChosenCurve;
                        PreSets.ParameterA = PCDDialog.ParameterA;
                        PreSets.ParameterB = PCDDialog.ParameterB;
                        PreSets.ParameterC = PCDDialog.ParameterC;
                        PreSets.ParameterD = PCDDialog.ParameterD;
                        PreSets.ParameterE = PCDDialog.ParameterE;
                        PreSets.ParameterF = PCDDialog.ParameterF;
                        UpdateComponentRelatedWithChosenPatternCurveScaffoldStatus();
                    }
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                    Logger.WriteExceptionInfo( x );
                }
                catch ( Exception x ) {
                    Logger.WriteExceptionInfo( x );
                }
            }
        }
        #endregion

        #region UpdateComponentRelatedWithChosenPatternCurveScaffoldStatus() : void
        private void UpdateComponentRelatedWithChosenPatternCurveScaffoldStatus()
        {
            switch ( PreSets.ChosenPatternCurveScaffold ) {
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL:
                wfPropertiesGenerateCurveScaffold2TextBox.Text = SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL_TEXT;
                break;
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC:
                wfPropertiesGenerateCurveScaffold2TextBox.Text = SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC_TEXT;
                break;
            default:
                wfPropertiesGenerateCurveScaffold2TextBox.Text = SharedConstants.CURVE_PATTERN_SCAFFOLD_DEFAULT_TEXT;
                break;
            }
        }
        #endregion

        #region wfPropertiesDatasheetCurveTypeComboBox_SelectedIndexChanged(...) : void
        /// <summary>
        /// Action: Selected index changed<para></para>
        /// Properties root tab: Datasheet<para></para>
        /// Properties tab section: Dataset control<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfPropertiesDatasheetCurveTypeComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            switch ( WindowsFormsHelper.GetSelectedIndexSafe( wfPropertiesDatasheetCurveTypeComboBox ) ) {
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_GENERATED:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = true;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = true;
                // TODO: curve refreshing
                break;
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_PATTERN:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = false;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = false;
                // TODO: curve refreshing
                break;
            default:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = false;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = false;
                break;
            }
        }
        #endregion

        #region wfPropertiesDatasheetCurveIndexNumericUpDown_ValueChanged(...) : void
        /// <summary>
        /// Action: value changed<para></para>
        /// Properties root tab: Datasheet<para></para>
        /// Properties tab section: Dataset control<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfPropertiesDatasheetCurveIndexNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            int numericUpDownValue = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesDatasheetCurveIndexNumericUpDown );
            WindowsFormsHelper.SetValueForTrackBar( wfPropertiesDatasheetCurveIndexTrackBar, numericUpDownValue );
        }
        #endregion

        #region wfPropertiesDatasheetCurveIndexTrackBar_Scroll(...) : void
        /// <summary>
        /// Action: scroll<para></para>
        /// Properties root tab: Datasheet<para></para>
        /// Properties tab section: Dataset control<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfPropertiesDatasheetCurveIndexTrackBar_Scroll( object sender, EventArgs e )
        {
            int trackBarValue = WindowsFormsHelper.GetValueFromTrackBar( wfPropertiesDatasheetCurveIndexTrackBar );
            WindowsFormsHelper.SetValueForNumericUpDown( wfPropertiesDatasheetCurveIndexNumericUpDown, trackBarValue );
        }
        #endregion

        #region wfPropertiesGenerateNumberOfCurves1NumericUpDown_ValueChanged(...) : void
        private void wfPropertiesGenerateNumberOfCurves1NumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            int numberOfCurves = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfCurves1NumericUpDown );
            wfPropertiesDatasheetCurveIndexNumericUpDown.Minimum = 1;
            wfPropertiesDatasheetCurveIndexNumericUpDown.Maximum = numberOfCurves;
            wfPropertiesDatasheetCurveIndexTrackBar.Minimum = 1;
            wfPropertiesDatasheetCurveIndexTrackBar.Maximum = numberOfCurves;
            PreSets.NumberOfCurves = numberOfCurves;
        }
        #endregion

        #region wfPropertiesGenerateGenerateSetButton_Click(...) : void
        /// <summary>
        /// Action: button click<para></para>
        /// Properties root tab: Generate<para></para>
        /// Properties tab section: Individual curves<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfPropertiesGenerateGenerateSetButton_Click( object sender, EventArgs e )
        {
            if ( wfPropertiesGenerateCurveScaffold2TextBox.Text == SharedConstants.CURVE_PATTERN_SCAFFOLD_DEFAULT_TEXT ) {
                string text = SharedConstants.GENERATE_SET_BUTTON_PREREQUISITE_WARNING_TEXT;
                string caption = SharedConstants.GENERATE_SET_BUTTON_PREREQUISITE_WARNING_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
                return;
            }

            GrabPreSetsForCurvesGeneration();
            GenerateAndShowPatternCurve();
            // TODO: generate set of curves
        }
        #endregion

        #region GrabPreSetsForCurvesGeneration() : void
        private void GrabPreSetsForCurvesGeneration()
        {
            PreSets.NumberOfCurves = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfCurves1NumericUpDown );
            PreSets.NumberOfPoints = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown );
            PreSets.StartingXPoint = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateStartingXPointNumericUpDown );
        }
        #endregion

        #region UpdateComponentRelatedWithChartsInterval() : void
        private void UpdateComponentRelatedWithChartsInterval()
        {
            int lowerLimit = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateStartingXPointNumericUpDown );
            int numberOfPoints = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown );
            int upperLimit = lowerLimit + numberOfPoints - 1;
            string intervalText = '<' + lowerLimit.ToString() + ';' + upperLimit.ToString() + '>';
            wfPropertiesGenerateInterval2TextBox.Text = intervalText;
        }
        #endregion

        #region GenerateAndShowPatternCurve() : void
        /// <summary>
        /// Refreshes the pattern curve chart by generated series of data.
        /// </summary>

        private void GenerateAndShowPatternCurve()
        {
            if ( ChartsCurvesDataset.GeneratePatternCurve( PreSets.ChosenPatternCurveScaffold, PreSets.NumberOfPoints, PreSets.StartingXPoint ) ) {
                wfChartsPatternCurve.Series.Clear();
                wfChartsPatternCurve.Series.Add( ChartsCurvesDataset.PatternCurveChartingSeries );
                wfChartsPatternCurve.Series[0].BorderWidth = 3;
                wfChartsPatternCurve.Visible = true;
                wfChartsPatternCurve.Invalidate();
            }

        }
        #endregion

        #region wfPropertiesGenerateNumberOfPointsNumericUpDown_ValueChanged(...) : void
        /// <summary>
        /// Action: value changed<para></para>
        /// Properties root tab: Generate<para></para>
        /// Properties tab section: Whole set of curves<para></para>
        /// Component signature: Number of points<para></para>
        /// Component type: NumericUpDown<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfPropertiesGenerateNumberOfPointsNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            UpdateComponentRelatedWithChartsInterval();
        }
        #endregion

        #region wfPropertiesGenerateStartingXPointNumericUpDown_ValueChanged(...) : void
        /// <summary>
        /// Action: value changed<para></para>
        /// Properties root tab: Generate<para></para>
        /// Properties tab section: Whole set of curves<para></para>
        /// Component signature: Starting X point<para></para>
        /// Component type: NumericUpDown<para></para>
        /// </summary>
        /// <param name="sender">No use.</param>
        /// <param name="e">No use.</param>

        private void wfPropertiesGenerateStartingXPointNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            UpdateComponentRelatedWithChartsInterval();
        }
        #endregion

        #region UpdateComponentRelatedWithDotNetFrameworkVersion() : void
        private void UpdateComponentRelatedWithDotNetFrameworkVersion()
        {
            string dotNetVersion = SystemInfoHelper.ObtainUsedDotNetFrameworkVersion();

            if ( dotNetVersion == null ) {
                wfPropertiesProgramDotNetFramework2TextBox.Text = SharedConstants.PROGRAM_INFO_OBTAINING_ERROR_TEXT;
                return;
            }

            wfPropertiesProgramDotNetFramework2TextBox.Text = SystemInfoHelper.ObtainUsedDotNetFrameworkVersion();

        }
        #endregion

        #region UpdateComponentRelatedWithOSVersionName() : void
        private void UpdateComponentRelatedWithOSVersionName()
        {
            string osVersion = SystemInfoHelper.ObtaingApplicationRunningOSVersion();

            if ( osVersion == null ) {
                wfPropertiesProgramOSVersion2TextBox.Text = SharedConstants.PROGRAM_INFO_OBTAINING_ERROR_TEXT;
                return;
            }

            wfPropertiesProgramOSVersion2TextBox.Text = osVersion;
        }
        #endregion

    }
    #endregion
}
