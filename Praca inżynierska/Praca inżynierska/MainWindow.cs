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
// TODO: In 'Datasheet' - showing dataset when there is no one should be forbidden
// TODO: In 'Datasheet' - showing dataset of pattern curve should be allowed, but alteration forbidden
// TODO: Charts - validation before using 'Generate set'

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

        private void UiMainWindow_FormClosed( object sender, FormClosedEventArgs e )
        {
            Logger.Close();
        }

        private void UiMainWindow_Load( object sender, EventArgs e )
        {
            Logger.Initialize();
            UpdateUiByDotNetFrameworkVersion();
            UpdateUiByOsVersionName();
            UpdateUiByLogFileFullPathLocation();
            DefineTimerThread();
            ThreadTasker.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ThreadTasker.StartThreadSafe( TimerThread );
            UpdateUiByStatusOfTimerThread();
            UpdateUiByNumbersOfExceptionsCaught();
        }

        private void UiMenuPrg_Exit_Click( object sender, EventArgs e )
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
                UpdateUiByTimerCounts( numberOfDaysText + ":" + numberOfHoursText + ":" + numberOfMinutesText + ":" + numberOfSecondsText );
                UpdateUiByNumbersOfExceptionsCaught();
            };
        }

        private void UpdateUiByTimerCounts( string text )
        {
            Logger.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try {
                BeginInvoke( (MethodInvoker) delegate {
                    uiPnlPrg_Cnts2_TxtBx.Text = text;
                    uiPnlPrg_Cnts2_TxtBx.Refresh();
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

        private void UpdateUiByNumbersOfExceptionsCaught()
        {
            Logger.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try {
                BeginInvoke( (MethodInvoker) delegate {
                    uiPnlPrg_Excp2_TxtBx.Text = Logger.NumberOfLoggedExceptions.ToString();
                    uiPnlPrg_Excp2_TxtBx.Refresh();
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

        private void UpdateUiByStatusOfTimerThread()
        {
            if ( TimerThread == null ) {
                uiPnlPrg_ActState2_TxtBx.Text = Constants.Ui.Panel.Program.TIMER_START_FAILURE;
            }
            else {
                uiPnlPrg_ActState2_TxtBx.Text = Constants.Ui.Panel.Program.TIMER_START_SUCCESS;
            }
        }

        private void UiPanelGenerate_Define_Click( object sender, EventArgs e )
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
                        UpdateUiByChosenScaffoldStatus();
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

        private void UpdateUiByChosenScaffoldStatus()
        {
            switch ( PreSets.Pcd.ChosenScaffold ) {
            case Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL:
                uiPnlGen_CrvScaff2_TxtBx.Text = Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL_TEXT;
                break;
            case Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                uiPnlGen_CrvScaff2_TxtBx.Text = Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC_TEXT;
                break;
            default:
                uiPnlGen_CrvScaff2_TxtBx.Text = Constants.Ui.Panel.Generate.SCAFFOLD_DEFAULT_TEXT;
                break;
            }
        }

        private void UiPanelDataSheet_CurveType_SelectedIndexChanged( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            switch ( WinFormsHelper.GetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx ) ) {
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_GENERATED:
                uiPnlDtSh_CrvIdx_Num.Enabled = true;
                uiPnlDtSh_CrvIdx_TrBr.Enabled = true;
                int selectedCurveIndex = WinFormsHelper.GetValue( uiPnlDtSh_CrvIdx_TrBr );
                ShowGeneratedCurveSeriesOnChart( selectedCurveIndex );
                break;
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_PATTERN:
                uiPnlDtSh_CrvIdx_Num.Enabled = false;
                uiPnlDtSh_CrvIdx_TrBr.Enabled = false;
                ShowPatternCurveSeriesOnChart();
                break;
            }
        }

        private void UiPanelDataSheet_CurveIndex_NumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int numericUpDownValue = WinFormsHelper.GetValue<int>( uiPnlDtSh_CrvIdx_Num );
            WinFormsHelper.SetValue( uiPnlDtSh_CrvIdx_TrBr, numericUpDownValue );
            ShowGeneratedCurveSeriesOnChart( numericUpDownValue );
        }

        private void UiPanelDataSheet_CurveIndex_TrackBar_Scroll( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int trackBarValue = WinFormsHelper.GetValue( uiPnlDtSh_CrvIdx_TrBr );
            WinFormsHelper.SetValue( uiPnlDtSh_CrvIdx_Num, trackBarValue );
            ShowGeneratedCurveSeriesOnChart( trackBarValue );
        }

        private void UiPanelGenerate_Curves1No_ValueChanged( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int numberOfCurves = WinFormsHelper.GetValue<int>( uiPnlGen_Crvs1No_Num );
            uiPnlDtSh_CrvIdx_Num.Minimum = 1;
            uiPnlDtSh_CrvIdx_Num.Maximum = numberOfCurves;
            uiPnlDtSh_CrvIdx_TrBr.Minimum = 1;
            uiPnlDtSh_CrvIdx_TrBr.Maximum = numberOfCurves;
            PreSets.Ui.NumberOfCurves = numberOfCurves;
            uiPnlGen_Crvs2No_Nm.Minimum = 1;
            uiPnlGen_Crvs2No_Nm.Maximum = numberOfCurves;
        }

        private void UiPanelGenerate_GenerateSet_Click( object sender, EventArgs e )
        {
            if ( uiPnlGen_CrvScaff2_TxtBx.Text == Constants.Ui.Panel.Generate.SCAFFOLD_DEFAULT_TEXT ) {
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
            PreSets.Ui.NumberOfCurves = WinFormsHelper.GetValue<int>( uiPnlGen_Crvs1No_Num );
            PreSets.Ui.NumberOfPoints = WinFormsHelper.GetValue<int>( uiPnlGen_PointsNo_Num );
            PreSets.Ui.StartingXPoint = WinFormsHelper.GetValue<int>( uiPnlGen_StartX_Num );
        }

        private void UpdateUiByChartsInterval()
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int lowerLimit = WinFormsHelper.GetValue<int>( uiPnlGen_StartX_Num );
            int numberOfPoints = WinFormsHelper.GetValue<int>( uiPnlGen_PointsNo_Num );
            int upperLimit = lowerLimit + numberOfPoints - 1;
            string intervalText = '<' + lowerLimit.ToString() + ';' + upperLimit.ToString() + '>';
            uiPnlGen_Interval2_TxtBx.Text = intervalText;
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
                uiCharts_PtrnCrv.Series.Clear();
                uiCharts_PtrnCrv.Series.Add( LeftChartDataset.PatternCurveChartingSeries );
                uiCharts_PtrnCrv.Series[0].BorderWidth = 3;
                uiCharts_PtrnCrv.Series[0].Color = System.Drawing.Color.Black;
                uiCharts_PtrnCrv.ChartAreas[0].RecalculateAxesScale();
                uiCharts_PtrnCrv.Visible = true;
                uiCharts_PtrnCrv.Invalidate();
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
                uiCharts_PtrnCrv.Series.Clear();
                uiCharts_PtrnCrv.Series.Add( LeftChartDataset.GeneratedCurvesChartingSeriesCollection[indexOfCurve - 1] );
                uiCharts_PtrnCrv.Series[0].BorderWidth = 3;
                uiCharts_PtrnCrv.Series[0].Color = System.Drawing.Color.Crimson;
                uiCharts_PtrnCrv.ChartAreas[0].RecalculateAxesScale();
                uiCharts_PtrnCrv.Visible = true;
                uiCharts_PtrnCrv.Invalidate();
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

        private void UiPanelGenerate_PointsNo_ValueChanged( object sender, EventArgs e )
        {
            UpdateUiByChartsInterval();
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int numberOfPoints = WinFormsHelper.GetValue<int>( uiPnlGen_PointsNo_Num );
            PreSets.Ui.NumberOfPoints = numberOfPoints;
        }

        private void UiPanelGenerate_StartingXPoint_ValueChanged( object sender, EventArgs e )
        {
            UpdateUiByChartsInterval();
        }

        private void UpdateUiByDotNetFrameworkVersion()
        {
            SysInfoHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string dotNetVersion = SysInfoHelper.ObtainUsedDotNetFrameworkVersion();

            if ( dotNetVersion == null ) {
                uiPnlPrg_DotNetFr2_TxtBx.Text = Constants.Ui.Panel.Program.INFO_OBTAINING_ERR_TEXT;
                return;
            }

            uiPnlPrg_DotNetFr2_TxtBx.Text = dotNetVersion;

        }

        private void UpdateUiByOsVersionName()
        {
            SysInfoHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string osVersion = SysInfoHelper.ObtaingApplicationRunningOSVersion();

            if ( osVersion == null ) {
                uiPnlPrg_OsVer2_TxtBx.Text = Constants.Ui.Panel.Program.INFO_OBTAINING_ERR_TEXT;
                return;
            }

            uiPnlPrg_OsVer2_TxtBx.Text = osVersion;
        }

        private void UiPanelDataSheet_ShowDataSet_Click( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int selectedCurveType = WinFormsHelper.GetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx );
            int selectedCurveIndex = WinFormsHelper.GetValue<int>( uiPnlDtSh_CrvIdx_Num );

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
                        uiCharts_PtrnCrv.Series.Clear();
                        uiCharts_PtrnCrv.Series.Add( DSVDialog.CurveDataSet );
                        uiCharts_PtrnCrv.Series[0].BorderWidth = 3;
                        uiCharts_PtrnCrv.Series[0].Color = System.Drawing.Color.Indigo;
                        uiCharts_PtrnCrv.ChartAreas[0].RecalculateAxesScale();
                        uiCharts_PtrnCrv.Visible = true;
                        uiCharts_PtrnCrv.Invalidate();
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

        private void UpdateUiByLogFileFullPathLocation()
        {
            uiPnlPrg_LogPath2_TxtBx.Text = Logger.GetFullPathOfLogFileLocation();
        }

     }

}
