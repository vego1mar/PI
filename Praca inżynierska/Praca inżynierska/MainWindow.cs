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
    public partial class MainWindow : Form
    {

        private Thread TimerThread { get; set; }
        private CurvesDataset LeftChartDataset { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitalizeFields();
        }

        private void InitalizeFields()
        {
            TimerThread = null;
            LeftChartDataset = new CurvesDataset();
        }

        private void WfMainWindow_FormClosed( object sender, FormClosedEventArgs e )
        {
            Logger.Close();
        }

        private void WfMainWindow_Load( object sender, EventArgs e )
        {
            Logger.Initialize();
            UpdateUIWithDotNetFrameworkVersion();
            UpdateUIWithOSVersionName();
            UpdateUIWithLogFileFullPathLocation();
            DefineTimerThread();
            ThreadTasker.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ThreadTasker.StartThreadSafe( TimerThread );
            UpdateUIWithStatusOfTimerThread();
            UpdateUIWithNumbersOfExceptionsCaught();
        }

        private void WfMenuProgramExit_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

        private void DefineTimerThread()
        {
            Logger.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

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
                    Logger.WriteException( x );
                }
                catch ( ObjectDisposedException x ) {
                    Logger.WriteException( x );
                }
                catch ( ArgumentOutOfRangeException x ) {
                    Logger.WriteException( x );
                }
                catch ( ArgumentException x ) {
                    Logger.WriteException( x );
                }
                catch ( Exception x ) {
                    Logger.WriteException( x );
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
                UpdateUIWithTimerCount( numberOfDaysText + ":" + numberOfHoursText + ":" + numberOfMinutesText + ":" + numberOfSecondsText );
                UpdateUIWithNumbersOfExceptionsCaught();
            };
        }

        private void UpdateUIWithTimerCount( string text )
        {
            Logger.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try {
                BeginInvoke( (MethodInvoker) delegate {
                    wfPropertiesProgramCounts2TextBox.Text = text;
                    wfPropertiesProgramCounts2TextBox.Refresh();
                } );
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteException( x );
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        private void UpdateUIWithNumbersOfExceptionsCaught()
        {
            Logger.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try {
                BeginInvoke( (MethodInvoker) delegate {
                    wfPropertiesProgramExceptionsCaught2TextBox.Text = Logger.NumberOfLoggedExceptions.ToString();
                    wfPropertiesProgramExceptionsCaught2TextBox.Refresh();
                } );
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteException( x );
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        private void UpdateUIWithStatusOfTimerThread()
        {
            if ( TimerThread == null ) {
                wfPropertiesProgramActualState2TextBox.Text = Constants.Ui.Panel.Program.TIMER_START_FAILURE;
            }
            else {
                wfPropertiesProgramActualState2TextBox.Text = Constants.Ui.Panel.Program.TIMER_START_SUCCESS;
            }
        }

        private void WfPropertiesGenerateDefineButton_Click( object sender, EventArgs e )
        {
            using ( var PCDDialog = new PatternCurveDefiner() ) {
                WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
                WinFormsHelper.ShowDialogSafe( PCDDialog, this );

                try {
                    if ( PCDDialog.DialogResult == DialogResult.OK ) {
                        PreSets.Pcd.ChosenScaffold = PCDDialog.ChosenCurve;
                        PreSets.Pcd.ParameterA = PCDDialog.ParameterA;
                        PreSets.Pcd.ParameterB = PCDDialog.ParameterB;
                        PreSets.Pcd.ParameterC = PCDDialog.ParameterC;
                        PreSets.Pcd.ParameterD = PCDDialog.ParameterD;
                        PreSets.Pcd.ParameterE = PCDDialog.ParameterE;
                        PreSets.Pcd.ParameterF = PCDDialog.ParameterF;
                        PreSets.Pcd.ParameterG = PCDDialog.ParameterG;
                        UpdateUIWithChosenScaffoldStatus();
                    }
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                    Logger.WriteException( x );
                }
                catch ( Exception x ) {
                    Logger.WriteException( x );
                }
            }
        }

        private void UpdateUIWithChosenScaffoldStatus()
        {
            switch ( PreSets.Pcd.ChosenScaffold ) {
            case Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL:
                wfPropertiesGenerateCurveScaffold2TextBox.Text = Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL_TEXT;
                break;
            case Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                wfPropertiesGenerateCurveScaffold2TextBox.Text = Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC_TEXT;
                break;
            default:
                wfPropertiesGenerateCurveScaffold2TextBox.Text = Constants.Ui.Panel.Generate.SCAFFOLD_DEFAULT_TEXT;
                break;
            }
        }

        private void WfPropertiesDatasheetCurveTypeComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            switch ( WinFormsHelper.GetSelectedIndexSafe( wfPropertiesDatasheetCurveTypeComboBox ) ) {
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_GENERATED:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = true;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = true;
                int selectedCurveIndex = WinFormsHelper.GetValue( wfPropertiesDatasheetCurveIndexTrackBar );
                ShowGeneratedCurveSeriesOnChart( selectedCurveIndex );
                break;
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_PATTERN:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = false;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = false;
                ShowPatternCurveSeriesOnChart();
                break;
            }
        }

        private void WfPropertiesDatasheetCurveIndexNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int numericUpDownValue = WinFormsHelper.GetValue<int>( wfPropertiesDatasheetCurveIndexNumericUpDown );
            WinFormsHelper.SetValue( wfPropertiesDatasheetCurveIndexTrackBar, numericUpDownValue );
            ShowGeneratedCurveSeriesOnChart( numericUpDownValue );
        }

        private void WfPropertiesDatasheetCurveIndexTrackBar_Scroll( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int trackBarValue = WinFormsHelper.GetValue( wfPropertiesDatasheetCurveIndexTrackBar );
            WinFormsHelper.SetValue( wfPropertiesDatasheetCurveIndexNumericUpDown, trackBarValue );
            ShowGeneratedCurveSeriesOnChart( trackBarValue );
        }

        private void WfPropertiesGenerateNumberOfCurves1NumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int numberOfCurves = WinFormsHelper.GetValue<int>( wfPropertiesGenerateNumberOfCurves1NumericUpDown );
            wfPropertiesDatasheetCurveIndexNumericUpDown.Minimum = 1;
            wfPropertiesDatasheetCurveIndexNumericUpDown.Maximum = numberOfCurves;
            wfPropertiesDatasheetCurveIndexTrackBar.Minimum = 1;
            wfPropertiesDatasheetCurveIndexTrackBar.Maximum = numberOfCurves;
            PreSets.Ui.NumberOfCurves = numberOfCurves;
            wfPropertiesGenerateNumberOfCurves2NumericUpDown.Minimum = 1;
            wfPropertiesGenerateNumberOfCurves2NumericUpDown.Maximum = numberOfCurves;
        }

        private void WfPropertiesGenerateGenerateSetButton_Click( object sender, EventArgs e )
        {
            if ( wfPropertiesGenerateCurveScaffold2TextBox.Text == Constants.Ui.Panel.Generate.SCAFFOLD_DEFAULT_TEXT ) {
                string text = Constants.Ui.Panel.Generate.GENERATE_SET_BTN_PREREQUISITE_WARNING_TEXT;
                string caption = Constants.Ui.Panel.Generate.GENERATE_SET_BTN_PREREQUISITE_WARNING_CAPTION;
                WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
                return;
            }

            GrabPreSetsForCurvesGeneration();
            GenerateAndShowPatternCurve();
            LeftChartDataset.SpreadPatternCurveSeriesToGeneratedCurveSeriesCollection( PreSets.Ui.NumberOfCurves );
        }

        private void GrabPreSetsForCurvesGeneration()
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            PreSets.Ui.NumberOfCurves = WinFormsHelper.GetValue<int>( wfPropertiesGenerateNumberOfCurves1NumericUpDown );
            PreSets.Ui.NumberOfPoints = WinFormsHelper.GetValue<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown );
            PreSets.Ui.StartingXPoint = WinFormsHelper.GetValue<int>( wfPropertiesGenerateStartingXPointNumericUpDown );
        }

        private void UpdateUIWithChartsInterval()
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int lowerLimit = WinFormsHelper.GetValue<int>( wfPropertiesGenerateStartingXPointNumericUpDown );
            int numberOfPoints = WinFormsHelper.GetValue<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown );
            int upperLimit = lowerLimit + numberOfPoints - 1;
            string intervalText = '<' + lowerLimit.ToString() + ';' + upperLimit.ToString() + '>';
            wfPropertiesGenerateInterval2TextBox.Text = intervalText;
        }

        private void GenerateAndShowPatternCurve()
        {
            if ( LeftChartDataset.GeneratePatternCurve( PreSets.Pcd.ChosenScaffold, PreSets.Ui.NumberOfPoints, PreSets.Ui.StartingXPoint ) ) {
                ShowPatternCurveSeriesOnChart();
            }
        }

        private void ShowPatternCurveSeriesOnChart()
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try {
                wfChartsPatternCurve.Series.Clear();
                wfChartsPatternCurve.Series.Add( LeftChartDataset.PatternCurveChartingSeries );
                wfChartsPatternCurve.Series[0].BorderWidth = 3;
                wfChartsPatternCurve.Series[0].Color = System.Drawing.Color.Black;
                wfChartsPatternCurve.ChartAreas[0].RecalculateAxesScale();
                wfChartsPatternCurve.Visible = true;
                wfChartsPatternCurve.Invalidate();
            }
            catch ( InvalidOperationException x ) {
                string text = Constants.Ui.Charts.REFRESHING_ERR_TEXT;
                string caption = Constants.Ui.Charts.REFRESHING_ERR_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        private void ShowGeneratedCurveSeriesOnChart( int indexOfCurve )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try {
                wfChartsPatternCurve.Series.Clear();
                wfChartsPatternCurve.Series.Add( LeftChartDataset.GeneratedCurvesChartingSeriesCollection[indexOfCurve - 1] );
                wfChartsPatternCurve.Series[0].BorderWidth = 3;
                wfChartsPatternCurve.Series[0].Color = System.Drawing.Color.Crimson;
                wfChartsPatternCurve.ChartAreas[0].RecalculateAxesScale();
                wfChartsPatternCurve.Visible = true;
                wfChartsPatternCurve.Invalidate();
            }
            catch ( InvalidOperationException x ) {
                string text = Constants.Ui.Charts.REFRESHING_ERR_TEXT;
                string caption = Constants.Ui.Charts.REFRESHING_ERR_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        private void WfPropertiesGenerateNumberOfPointsNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            UpdateUIWithChartsInterval();
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int numberOfPoints = WinFormsHelper.GetValue<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown );
            PreSets.Ui.NumberOfPoints = numberOfPoints;
        }

        private void WfPropertiesGenerateStartingXPointNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            UpdateUIWithChartsInterval();
        }

        private void UpdateUIWithDotNetFrameworkVersion()
        {
            SysInfoHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string dotNetVersion = SysInfoHelper.ObtainUsedDotNetFrameworkVersion();

            if ( dotNetVersion == null ) {
                wfPropertiesProgramDotNetFramework2TextBox.Text = Constants.Ui.Panel.Program.INFO_OBTAINING_ERR_TEXT;
                return;
            }

            wfPropertiesProgramDotNetFramework2TextBox.Text = dotNetVersion;

        }

        private void UpdateUIWithOSVersionName()
        {
            SysInfoHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string osVersion = SysInfoHelper.ObtaingApplicationRunningOSVersion();

            if ( osVersion == null ) {
                wfPropertiesProgramOSVersion2TextBox.Text = Constants.Ui.Panel.Program.INFO_OBTAINING_ERR_TEXT;
                return;
            }

            wfPropertiesProgramOSVersion2TextBox.Text = osVersion;
        }

        private void WfPropertiesDatasheetShowDatasetButton_Click( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int selectedCurveType = WinFormsHelper.GetSelectedIndexSafe( wfPropertiesDatasheetCurveTypeComboBox );
            int selectedCurveIndex = WinFormsHelper.GetValue<int>( wfPropertiesDatasheetCurveIndexNumericUpDown );

            switch ( selectedCurveType ) {
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_PATTERN:
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_GENERATED:
                break;
            default:
                string text = Constants.Ui.Panel.Datasheet.CURVE_TYPE_NOT_SELECTED_TEXT;
                string caption = Constants.Ui.Panel.Datasheet.CURVE_TYPE_NOT_SELECTED_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
                return;
            }

            using ( var DSVDialog = new DatasetViewer( SpecifyCurveSeries( selectedCurveType, selectedCurveIndex ) ) ) {
                WinFormsHelper.ShowDialogSafe( DSVDialog, this );

                try {
                    if ( DSVDialog.DialogResult == DialogResult.OK ) {
                        LeftChartDataset.AbsorbSeriesPoints( DSVDialog.CurveDataSet, selectedCurveType, selectedCurveIndex );
                        wfChartsPatternCurve.Series.Clear();
                        wfChartsPatternCurve.Series.Add( DSVDialog.CurveDataSet );
                        wfChartsPatternCurve.Series[0].BorderWidth = 3;
                        wfChartsPatternCurve.Series[0].Color = System.Drawing.Color.Indigo;
                        wfChartsPatternCurve.ChartAreas[0].RecalculateAxesScale();
                        wfChartsPatternCurve.Visible = true;
                        wfChartsPatternCurve.Invalidate();
                    }
                }
                catch ( InvalidOperationException x ) {
                    string text = Constants.Ui.Charts.REFRESHING_ERR_TEXT;
                    string caption = Constants.Ui.Charts.REFRESHING_ERR_CAPTION;
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    Logger.WriteException( x );
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                    Logger.WriteException( x );
                }
                catch ( Exception x ) {
                    Logger.WriteException( x );
                }
                finally {
                    Logger.Context = string.Empty;
                }
            }
        }

        private Series SpecifyCurveSeries( int curveType, int curveIndex )
        {
            switch ( curveType ) {
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_PATTERN:
                return LeftChartDataset.PatternCurveChartingSeries;
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_GENERATED:
                return LeftChartDataset.GeneratedCurvesChartingSeriesCollection[curveIndex - 1];
            }

            return null;
        }

        private void UpdateUIWithLogFileFullPathLocation()
        {
            wfPropertiesProgramLogPath2TextBox.Text = Logger.GetFullPathOfLogFileLocation();
        }

    }

}
