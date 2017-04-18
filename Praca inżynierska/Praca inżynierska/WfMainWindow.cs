using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// INTERNAL BACKLOG
// TODO: Update info
// TODO: Configuration file
// TODO: Reading and saving set of curves from a file
// TODO: I18N
// TODO: Implement Gaussian noise option
// TODO: Menu item - 'Adjust curves' for visual effects manipulations 
// TODO: Add new pattern curve scaffold - rectangular function

namespace PI
{
    public partial class WfMainWindow : Form
    {

        #region Properties
        private Thread TimerThread { get; set; }
        private CurvesDataset ChartsCurvesDataset { get; set; }
        #endregion

        public WfMainWindow()
        {
            InitializeComponent();
            InitalizeFields();
        }

        private void InitalizeFields()
        {
            TimerThread = null;
            DefineTimerThread();
            ThreadTasker.StartThreadSafe( TimerThread, "PI.WfMainWindow.InitializeFields()" );
            UpdateComponentRelatedWithActualStatusOfTimerThread();
            ChartsCurvesDataset = new CurvesDataset();
        }

        private void WfMainWindow_FormClosed( object sender, FormClosedEventArgs e )
        {
            Logger.Close();
        }

        private void WfMainWindow_Load( object sender, EventArgs e )
        {
            Logger.Initialize();
            UpdateComponentRelatedWithDotNetFrameworkVersion();
            UpdateComponentRelatedWithOSVersionName();
            UpdateComponentRelatedWithLogFileFullPathLocation();
        }

        private void WfMenuProgramExit_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

        private void DefineTimerThread()
        {
            string methodName = "PI.WfMainWindow.DefineTimerThread()";

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
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( ObjectDisposedException x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( ArgumentOutOfRangeException x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( ArgumentException x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( Exception x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
            } );
        }

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
                RefreshComponentRelatedWithNumbersOfExceptionsCaught();
            };
        }

        private void RefreshComponentRelatedWithTimerThread( string text )
        {
            string methodName = "PI.WfMainWindow.RefreshComponentRelatedWithTimerThread(text)";

            try {
                BeginInvoke( (MethodInvoker) delegate {
                    wfPropertiesProgramCounts2TextBox.Text = text;
                    wfPropertiesProgramCounts2TextBox.Refresh();
                } );
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteExceptionInfo( x, methodName );
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x, methodName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, methodName );
            }
        }

        private void RefreshComponentRelatedWithNumbersOfExceptionsCaught()
        {
            string invoker = "PI.WfMainWindow.RefreshComponentRelatedWithNumbersOfExceptionsCaught()";

            try {
                BeginInvoke( (MethodInvoker) delegate {
                    wfPropertiesProgramExceptionsCaught2TextBox.Text = Logger.NumberOfInvokesForExceptionInfoWriter.ToString();
                    wfPropertiesProgramExceptionsCaught2TextBox.Refresh();
                } );
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
        }

        private void UpdateComponentRelatedWithActualStatusOfTimerThread()
        {
            if ( TimerThread == null ) {
                wfPropertiesProgramActualState2TextBox.Text = SharedConstants.TIMER_START_FAILURE;
            }
            else {
                wfPropertiesProgramActualState2TextBox.Text = SharedConstants.TIMER_START_SUCCESS;
            }
        }

        private void WfPropertiesGenerateDefineButton_Click( object sender, EventArgs e )
        {
            using ( var PCDDialog = new PatternCurveDefiner() ) {
                string methodName = "PI.WfMainWindow.WfPropertiesGenerateDefineButton_Click(sender, e)";
                WindowsFormsHelper.ShowDialogSafe( PCDDialog, this, methodName );

                try {
                    if ( PCDDialog.DialogResult == DialogResult.OK ) {
                        PreSets.ChosenPatternCurveScaffold = PCDDialog.ChosenCurve;
                        PreSets.ParameterA = PCDDialog.ParameterA;
                        PreSets.ParameterB = PCDDialog.ParameterB;
                        PreSets.ParameterC = PCDDialog.ParameterC;
                        PreSets.ParameterD = PCDDialog.ParameterD;
                        PreSets.ParameterE = PCDDialog.ParameterE;
                        PreSets.ParameterF = PCDDialog.ParameterF;
                        PreSets.ParameterG = PCDDialog.ParameterG;
                        UpdateComponentRelatedWithChosenPatternCurveScaffoldStatus();
                    }
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( Exception x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
            }
        }

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

        private void WfPropertiesDatasheetCurveTypeComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            string invoker = "PI.WfMainWindow.WfPropertiesDatasheetCurveTypeComboBox_SelectedIndexChanged(sender, e)";

            switch ( WindowsFormsHelper.GetSelectedIndexSafe( wfPropertiesDatasheetCurveTypeComboBox, invoker ) ) {
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_GENERATED:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = true;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = true;
                int selectedCurveIndex = WindowsFormsHelper.GetValueFromTrackBar( wfPropertiesDatasheetCurveIndexTrackBar, invoker );
                ShowGeneratedCurveSeriesOnChart( selectedCurveIndex );
                break;
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_PATTERN:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = false;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = false;
                ShowPatternCurveSeriesOnChart();
                break;
            }
        }

        private void WfPropertiesDatasheetCurveIndexNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            string invoker = "PI.WfMainWindow.WfPropertiesDatasheetCurveIndexNumericUpDown_ValueChanged(sender, e)";
            int numericUpDownValue = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesDatasheetCurveIndexNumericUpDown, invoker );
            WindowsFormsHelper.SetValueForTrackBar( wfPropertiesDatasheetCurveIndexTrackBar, numericUpDownValue, invoker );
            ShowGeneratedCurveSeriesOnChart( numericUpDownValue );
        }

        private void WfPropertiesDatasheetCurveIndexTrackBar_Scroll( object sender, EventArgs e )
        {
            string invoker = "PI.WfMainWindow.WfPropertiesDatasheetCurveIndexTrackBar_Scroll(sender, e)";
            int trackBarValue = WindowsFormsHelper.GetValueFromTrackBar( wfPropertiesDatasheetCurveIndexTrackBar, invoker );
            WindowsFormsHelper.SetValueForNumericUpDown( wfPropertiesDatasheetCurveIndexNumericUpDown, trackBarValue, invoker );
            ShowGeneratedCurveSeriesOnChart( trackBarValue );
        }

        private void WfPropertiesGenerateNumberOfCurves1NumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            string methodName = "PI.WfMainWindow.WfPropertiesGenerateNumberOfCurves1NumericUpDown_ValueChanged(sender, e)";
            int numberOfCurves = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfCurves1NumericUpDown, methodName );
            wfPropertiesDatasheetCurveIndexNumericUpDown.Minimum = 1;
            wfPropertiesDatasheetCurveIndexNumericUpDown.Maximum = numberOfCurves;
            wfPropertiesDatasheetCurveIndexTrackBar.Minimum = 1;
            wfPropertiesDatasheetCurveIndexTrackBar.Maximum = numberOfCurves;
            PreSets.NumberOfCurves = numberOfCurves;
            wfPropertiesGenerateNumberOfCurves2NumericUpDown.Minimum = 1;
            wfPropertiesGenerateNumberOfCurves2NumericUpDown.Maximum = numberOfCurves;
        }

        private void WfPropertiesGenerateGenerateSetButton_Click( object sender, EventArgs e )
        {
            if ( wfPropertiesGenerateCurveScaffold2TextBox.Text == SharedConstants.CURVE_PATTERN_SCAFFOLD_DEFAULT_TEXT ) {
                string text = SharedConstants.GENERATE_SET_BUTTON_PREREQUISITE_WARNING_TEXT;
                string caption = SharedConstants.GENERATE_SET_BUTTON_PREREQUISITE_WARNING_CAPTION;
                string methodName = "PI.WfMainWindow.WfPropertiesGenerateGenerateSetButton_Click(sender, e)";
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop, methodName );
                return;
            }

            GrabPreSetsForCurvesGeneration();
            GenerateAndShowPatternCurve();
            ChartsCurvesDataset.SpreadPatternCurveSeriesToGeneratedCurveSeriesCollection( PreSets.NumberOfCurves );
        }

        private void GrabPreSetsForCurvesGeneration()
        {
            string methodName = "PI.WfMainWindow.GrabPreSetsForCurvesGeneration()";
            PreSets.NumberOfCurves = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfCurves1NumericUpDown, methodName );
            PreSets.NumberOfPoints = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown, methodName );
            PreSets.StartingXPoint = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateStartingXPointNumericUpDown, methodName );
        }

        private void UpdateComponentRelatedWithChartsInterval()
        {
            string invoker = "PI.WfMainWindow.UpdateComponentRelatedWithChartsInterval()";
            int lowerLimit = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateStartingXPointNumericUpDown, invoker );
            int numberOfPoints = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown, invoker );
            int upperLimit = lowerLimit + numberOfPoints - 1;
            string intervalText = '<' + lowerLimit.ToString() + ';' + upperLimit.ToString() + '>';
            wfPropertiesGenerateInterval2TextBox.Text = intervalText;
        }

        private void GenerateAndShowPatternCurve()
        {
            if ( ChartsCurvesDataset.GeneratePatternCurve( PreSets.ChosenPatternCurveScaffold, PreSets.NumberOfPoints, PreSets.StartingXPoint ) ) {
                ShowPatternCurveSeriesOnChart();
            }
        }

        private void ShowPatternCurveSeriesOnChart()
        {
            string invoker = "PI.WfMainWindow.ShowPatternCurveSeriesOnChart()";

            try {
                wfChartsPatternCurve.Series.Clear();
                wfChartsPatternCurve.Series.Add( ChartsCurvesDataset.PatternCurveChartingSeries );
                wfChartsPatternCurve.Series[0].BorderWidth = 3;
                wfChartsPatternCurve.Series[0].Color = System.Drawing.Color.Black;
                wfChartsPatternCurve.ChartAreas[0].RecalculateAxesScale();
                wfChartsPatternCurve.Visible = true;
                wfChartsPatternCurve.Invalidate();
            }
            catch ( InvalidOperationException x ) {
                string text = SharedConstants.CHARTS_REFRESHING_ERROR_TEXT;
                string caption = SharedConstants.CHARTS_REFRESHING_ERROR_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, invoker );
                Logger.WriteExceptionInfo( x, invoker );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
        }

        private void ShowGeneratedCurveSeriesOnChart( int indexOfCurve )
        {
            string invoker = "PI.WfMainWindow.ShowGeneratedCurveSeriesOnChart(indexOfCurve)";

            try {
                wfChartsPatternCurve.Series.Clear();
                wfChartsPatternCurve.Series.Add( ChartsCurvesDataset.GeneratedCurvesChartingSeriesCollection[indexOfCurve - 1] );
                wfChartsPatternCurve.Series[0].BorderWidth = 3;
                wfChartsPatternCurve.Series[0].Color = System.Drawing.Color.Crimson;
                wfChartsPatternCurve.ChartAreas[0].RecalculateAxesScale();
                wfChartsPatternCurve.Visible = true;
                wfChartsPatternCurve.Invalidate();
            }
            catch ( InvalidOperationException x ) {
                string text = SharedConstants.CHARTS_REFRESHING_ERROR_TEXT;
                string caption = SharedConstants.CHARTS_REFRESHING_ERROR_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, invoker );
                Logger.WriteExceptionInfo( x, invoker );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
        }

        private void WfPropertiesGenerateNumberOfPointsNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            UpdateComponentRelatedWithChartsInterval();
            string invoker = "PI.WfMainWindow.WfPropertiesGenerateNumberOfPointsNumericUpDown_ValueChanged(sender, e)";
            int numberOfPoints = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown, invoker );
            PreSets.NumberOfPoints = numberOfPoints;
        }

        private void WfPropertiesGenerateStartingXPointNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            UpdateComponentRelatedWithChartsInterval();
        }

        private void UpdateComponentRelatedWithDotNetFrameworkVersion()
        {
            string invoker = "PI.WfMainWindow.UpdateComponentRelatedWithDotNetFrameworkVersion()";
            string dotNetVersion = SystemInfoHelper.ObtainUsedDotNetFrameworkVersion( invoker );

            if ( dotNetVersion == null ) {
                wfPropertiesProgramDotNetFramework2TextBox.Text = SharedConstants.PROGRAM_INFO_OBTAINING_ERROR_TEXT;
                return;
            }

            wfPropertiesProgramDotNetFramework2TextBox.Text = SystemInfoHelper.ObtainUsedDotNetFrameworkVersion( invoker );

        }

        private void UpdateComponentRelatedWithOSVersionName()
        {
            string invoker = "PI.WfMainWindow.UpdateComponentRelatedWithOSVersionName()";
            string osVersion = SystemInfoHelper.ObtaingApplicationRunningOSVersion( invoker );

            if ( osVersion == null ) {
                wfPropertiesProgramOSVersion2TextBox.Text = SharedConstants.PROGRAM_INFO_OBTAINING_ERROR_TEXT;
                return;
            }

            wfPropertiesProgramOSVersion2TextBox.Text = osVersion;
        }

        private void WfPropertiesDatasheetShowDatasetButton_Click( object sender, EventArgs e )
        {
            string invoker = "PI.WfMainWindow.WfPropertiesDatasheetShowDatasetButton_Click(sender, e)";
            int selectedCurveType = WindowsFormsHelper.GetSelectedIndexSafe( wfPropertiesDatasheetCurveTypeComboBox, invoker );
            int selectedCurveIndex = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesDatasheetCurveIndexNumericUpDown, invoker );

            switch ( selectedCurveType ) {
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_PATTERN:
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_GENERATED:
                break;
            default:
                string text = SharedConstants.DATASET_CURVE_TYPE_CONTROL_NOT_SELECTED_TEXT;
                string caption = SharedConstants.DATASET_CURVE_TYPE_CONTROL_NOT_SELECTED_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, invoker );
                return;
            }

            using ( var DSVDialog = new DatasetViewer( SpecifyCurveSeries( selectedCurveType, selectedCurveIndex ) ) ) {
                WindowsFormsHelper.ShowDialogSafe( DSVDialog, this, invoker );

                try {
                    if ( DSVDialog.DialogResult == DialogResult.OK ) {
                        ChartsCurvesDataset.AbsorbSeriesPoints( DSVDialog.DatasetOfCurve, selectedCurveType, selectedCurveIndex );
                        wfChartsPatternCurve.Series.Clear();
                        wfChartsPatternCurve.Series.Add( DSVDialog.DatasetOfCurve );
                        wfChartsPatternCurve.Series[0].BorderWidth = 3;
                        wfChartsPatternCurve.Series[0].Color = System.Drawing.Color.Indigo;
                        wfChartsPatternCurve.ChartAreas[0].RecalculateAxesScale();
                        wfChartsPatternCurve.Visible = true;
                        wfChartsPatternCurve.Invalidate();
                    }
                }
                catch ( InvalidOperationException x ) {
                    string text = SharedConstants.CHARTS_REFRESHING_ERROR_TEXT;
                    string caption = SharedConstants.CHARTS_REFRESHING_ERROR_CAPTION;
                    WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error, invoker );
                    Logger.WriteExceptionInfo( x, invoker );
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                    Logger.WriteExceptionInfo( x, invoker );
                }
                catch ( Exception x ) {
                    Logger.WriteExceptionInfo( x, invoker );
                }
            }
        }

        private Series SpecifyCurveSeries( int curveType, int curveIndex )
        {
            switch ( curveType ) {
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_PATTERN:
                return ChartsCurvesDataset.PatternCurveChartingSeries;
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_GENERATED:
                return ChartsCurvesDataset.GeneratedCurvesChartingSeriesCollection[curveIndex - 1];
            }

            return null;
        }

        private void UpdateComponentRelatedWithLogFileFullPathLocation()
        {
            wfPropertiesProgramLogPath2TextBox.Text = Logger.GetFullPathOfLogFileLocation();
        }

    }

}
