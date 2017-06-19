using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// BACKLOG
// TODO: Configuration file
// TODO: Reading and saving set of curves from a file
// TODO: I18N
// TODO: Menu item - 'Adjust curves' for visual effects manipulations 
// TODO: Rearrange Dsv dialog to use db context for efficienty
// TODO: Implement Gaussian noise option
// TODO: Add new pattern curve scaffold - rectangular function
// TODO: Generate/Charts - change 'Interval' control
// TODO: Charts - density of points (points per unit)

namespace PI
{
    public partial class MainWindow : Form
    {

        private Thread Timer { get; set; }
        private CurvesDataset ChartsData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitalizeFields();
        }

        private void InitalizeFields()
        {
            Timer = null;
            ChartsData = new CurvesDataset();
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
            ThreadTasker.StartThreadSafe( Timer );
            UpdateUiByStatusOfTimerThread();
            UpdateUiByNumbersOfExceptionsCaught();
        }

        private void UiMenuProgram_Exit_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

        private void DefineTimerThread()
        {
            Timer = new Thread( () => {
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
            if ( Timer == null ) {
                uiPnlPrg_ActState2_TxtBx.Text = Constants.Ui.Panel.Program.TIMER_START_FAILURE;
            }
            else {
                uiPnlPrg_ActState2_TxtBx.Text = Constants.Ui.Panel.Program.TIMER_START_SUCCESS;
            }
        }

        private void UiPanelGenerate_Define_Click( object sender, EventArgs e )
        {
            using ( var pcdDialog = new PatternCurveDefiner() ) {
                WinFormsHelper.ShowDialogSafe( pcdDialog, this );

                try {
                    if ( pcdDialog.DialogResult == DialogResult.OK ) {
                        CopyDialogPropertiesIntoPreSetsArea( pcdDialog );
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

        private void CopyDialogPropertiesIntoPreSetsArea( PatternCurveDefiner pcdDialog )
        {
            PreSets.Pcd.ChosenScaffold = pcdDialog.ChosenCurve;
            PreSets.Pcd.Parameters.A = pcdDialog.ParameterA;
            PreSets.Pcd.Parameters.B = pcdDialog.ParameterB;
            PreSets.Pcd.Parameters.C = pcdDialog.ParameterC;
            PreSets.Pcd.Parameters.D = pcdDialog.ParameterD;
            PreSets.Pcd.Parameters.E = pcdDialog.ParameterE;
            PreSets.Pcd.Parameters.F = pcdDialog.ParameterF;
            PreSets.Pcd.Parameters.I = pcdDialog.ParameterI;
            PreSets.Pcd.Parameters.G = pcdDialog.ParameterG;
            PreSets.Pcd.Parameters.J = pcdDialog.ParameterJ;
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
            int numericUpDownValue = WinFormsHelper.GetValue<int>( uiPnlDtSh_CrvIdx_Num );
            WinFormsHelper.SetValue( uiPnlDtSh_CrvIdx_TrBr, numericUpDownValue );
            ShowGeneratedCurveSeriesOnChart( numericUpDownValue );
        }

        private void UiPanelDataSheet_CurveIndex_TrackBar_Scroll( object sender, EventArgs e )
        {
            int trackBarValue = WinFormsHelper.GetValue( uiPnlDtSh_CrvIdx_TrBr );
            WinFormsHelper.SetValue( uiPnlDtSh_CrvIdx_Num, trackBarValue );
            ShowGeneratedCurveSeriesOnChart( trackBarValue );
        }

        private void UiPanelGenerate_Curves1No_ValueChanged( object sender, EventArgs e )
        {
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
                MsgBxShower.Ui.PatternCurveNotChosenPrerequisite();
                return;
            }

            GrabPreSetsForCurvesGeneration();
            GenerateAndShowPatternCurve();
            ChartsData.SpreadPatternCurveSetToGeneratedCurveSet( PreSets.Ui.NumberOfCurves );
        }

        private void GrabPreSetsForCurvesGeneration()
        {
            PreSets.Ui.NumberOfCurves = WinFormsHelper.GetValue<int>( uiPnlGen_Crvs1No_Num );
            PreSets.Ui.NumberOfPoints = WinFormsHelper.GetValue<int>( uiPnlGen_PointsNo_Num );
            PreSets.Ui.StartingXPoint = WinFormsHelper.GetValue<int>( uiPnlGen_StartX_Num );
        }

        private void UpdateUiByChartsInterval()
        {
            int lowerLimit = WinFormsHelper.GetValue<int>( uiPnlGen_StartX_Num );
            int numberOfPoints = WinFormsHelper.GetValue<int>( uiPnlGen_PointsNo_Num );
            int upperLimit = lowerLimit + numberOfPoints - 1;
            string intervalText = '<' + lowerLimit.ToString() + ';' + upperLimit.ToString() + '>';
            uiPnlGen_Interval2_TxtBx.Text = intervalText;
        }

        private void GenerateAndShowPatternCurve()
        {
            if ( !ChartsData.GeneratePatternCurve( PreSets.Pcd.ChosenScaffold, PreSets.Ui.NumberOfPoints, PreSets.Ui.StartingXPoint ) ) {
                ChartsData.RemoveInvalidPointsFromPatternCurveSet();
                MsgBxShower.Ui.PointsNotValidToChartProblem();
            }

            if ( ChartsData.PatternCurveSet.Points.Count > 0 ) {
                ShowPatternCurveSeriesOnChart();
            }
        }

        private void ShowPatternCurveSeriesOnChart()
        {
            try {
                uiCharts_PtrnCrv.Series.Clear();
                uiCharts_PtrnCrv.Series.Add( ChartsData.PatternCurveSet );
                uiCharts_PtrnCrv.Series[0].BorderWidth = 3;
                uiCharts_PtrnCrv.Series[0].Color = System.Drawing.Color.Black;
                uiCharts_PtrnCrv.ChartAreas[0].RecalculateAxesScale();
                uiCharts_PtrnCrv.Visible = true;
                uiCharts_PtrnCrv.Invalidate();
            }
            catch ( InvalidOperationException x ) {
                MsgBxShower.Ui.ChartRefreshingError();
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        private void ShowGeneratedCurveSeriesOnChart( int indexOfCurve )
        {
            try {
                uiCharts_PtrnCrv.Series.Clear();
                uiCharts_PtrnCrv.Series.Add( ChartsData.GeneratedCurvesSet[indexOfCurve - 1] );
                uiCharts_PtrnCrv.Series[0].BorderWidth = 3;
                uiCharts_PtrnCrv.Series[0].Color = System.Drawing.Color.Crimson;
                uiCharts_PtrnCrv.ChartAreas[0].RecalculateAxesScale();
                uiCharts_PtrnCrv.Visible = true;
                uiCharts_PtrnCrv.Invalidate();
            }
            catch ( InvalidOperationException x ) {
                MsgBxShower.Ui.ChartRefreshingError();
                Logger.WriteException( x );
            }
            catch ( ArgumentOutOfRangeException x ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        private void UiPanelGenerate_PointsNo_ValueChanged( object sender, EventArgs e )
        {
            UpdateUiByChartsInterval();
            PreSets.Ui.NumberOfPoints = WinFormsHelper.GetValue<int>( uiPnlGen_PointsNo_Num );
        }

        private void UiPanelGenerate_StartingXPoint_ValueChanged( object sender, EventArgs e )
        {
            UpdateUiByChartsInterval();
        }

        private void UpdateUiByDotNetFrameworkVersion()
        {
            string dotNetVersion = SysInfoHelper.ObtainUsedDotNetFrameworkVersion();

            if ( dotNetVersion == null ) {
                uiPnlPrg_DotNetFr2_TxtBx.Text = Constants.Ui.Panel.Program.INFO_OBTAINING_ERR_TEXT;
                return;
            }

            uiPnlPrg_DotNetFr2_TxtBx.Text = dotNetVersion;

        }

        private void UpdateUiByOsVersionName()
        {
            string osVersion = SysInfoHelper.ObtaingApplicationRunningOSVersion();

            if ( osVersion == null ) {
                uiPnlPrg_OsVer2_TxtBx.Text = Constants.Ui.Panel.Program.INFO_OBTAINING_ERR_TEXT;
                return;
            }

            uiPnlPrg_OsVer2_TxtBx.Text = osVersion;
        }

        private void UiPanelDataSheet_ShowDataSet_Click( object sender, EventArgs e )
        {
            int selectedCurveType = WinFormsHelper.GetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx );
            int selectedCurveIndex = WinFormsHelper.GetValue<int>( uiPnlDtSh_CrvIdx_Num );

            switch ( selectedCurveType ) {
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_PATTERN:
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_GENERATED:
                break;
            default:
                MsgBxShower.Ui.CurveTypeNotSelectedInfo();
                return;
            }

            Series selectedCurveSeries = SpecifyCurveSeries( selectedCurveType, selectedCurveIndex );

            if ( selectedCurveSeries == null ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                return;
            }

            using ( var DsvDialog = new DatasetViewer( selectedCurveSeries ) ) {
                WinFormsHelper.ShowDialogSafe( DsvDialog, this );

                try {
                    if ( DsvDialog.DialogResult == DialogResult.OK ) {
                        ChartsData.AbsorbSeriesPoints( DsvDialog.CurveDataSet, selectedCurveType, selectedCurveIndex );
                        uiCharts_PtrnCrv.Series.Clear();
                        uiCharts_PtrnCrv.Series.Add( DsvDialog.CurveDataSet );
                        uiCharts_PtrnCrv.Series[0].BorderWidth = 3;
                        uiCharts_PtrnCrv.Series[0].Color = System.Drawing.Color.Indigo;
                        uiCharts_PtrnCrv.ChartAreas[0].RecalculateAxesScale();
                        uiCharts_PtrnCrv.Visible = true;
                        uiCharts_PtrnCrv.Invalidate();
                    }
                }
                catch ( InvalidOperationException x ) {
                    MsgBxShower.Ui.ChartRefreshingError();
                    Logger.WriteException( x );
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                    Logger.WriteException( x );
                }
                catch ( Exception x ) {
                    Logger.WriteException( x );
                }
            }
        }

        private Series SpecifyCurveSeries( int curveType, int curveIndex )
        {
            try {
                switch ( curveType ) {
                case Constants.Ui.Panel.Datasheet.CURVE_TYPE_PATTERN:
                    return ChartsData.PatternCurveSet;
                case Constants.Ui.Panel.Datasheet.CURVE_TYPE_GENERATED:
                    return ChartsData.GeneratedCurvesSet[curveIndex - 1];
                }
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }

            return null;
        }

        private void UpdateUiByLogFileFullPathLocation()
        {
            uiPnlPrg_LogPath2_TxtBx.Text = Logger.GetFullPathOfLogFileLocation();
        }

        private void UiMenuProgram_CheckUpdate_Click( object sender, EventArgs e )
        {
            string httpContent = WebHelper.GetContentThroughHttp( Properties.Settings.Default["UpdateUrl"].ToString() );

            if ( httpContent == null ) {
                MsgBxShower.Menu.CannotDownloadUpdateInfoProblem();
                return;
            }

            ushort currentVersion = 0;
            ushort latestVersion = 0;

            try {
                currentVersion = Convert.ToUInt16( Properties.Settings.Default["Commits"] );
                latestVersion = GetCommitsNumber( httpContent );
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteException( x );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( FormatException x ) {
                Logger.WriteException( x );
            }
            catch ( InvalidCastException x ) {
                Logger.WriteException( x );
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
            finally {
                if ( currentVersion == 0 || latestVersion == 0 ) {
                    MsgBxShower.Menu.CannotMatchVersionsError();
                }
            }

            if ( currentVersion >= latestVersion ) {
                MsgBxShower.Menu.RunningLatestReleaseAppInfo();
                return;
            }

            MsgBxShower.Menu.RunningObsoleteAppInfo();
        }

        private ushort GetCommitsNumber( string urlContent )
        {
            const int NUMBER_LINE_LENGTH = 32;
            const string SEARCH_MARKER = "<span class=\"num text-emphasized\">";
            int markerIndex = urlContent.IndexOf( SEARCH_MARKER );
            string commitsLine = urlContent.Substring( markerIndex + 1, SEARCH_MARKER.Length + NUMBER_LINE_LENGTH );
            int startIndex = commitsLine.IndexOf( '\n' ) + 1;
            int numberLength = commitsLine.LastIndexOf( '\n' ) - startIndex;
            string commits = commitsLine.Substring( startIndex, numberLength );
            return Convert.ToUInt16( commits );
        }

    }

}
