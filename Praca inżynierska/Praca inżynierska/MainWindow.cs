﻿using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// BACKLOG
// TODO: General - Configuration file
// TODO: General - Localization
// TODO: Menu - 'Adjust curves' for visual effects manipulations
// TODO: Menu - Reading and saving set of curves from a file
// TODO: Feature - Implement Gaussian noise option

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
            UpdateUiBySettingChartsProperties();
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
                uiPnlPrg_ActState2_TxtBx.Text = Consts.Ui.Panel.Program.TxtFailure;
            }
            else {
                uiPnlPrg_ActState2_TxtBx.Text = Consts.Ui.Panel.Program.TxtSuccess;
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
            Presets.Pcd.ChosenScaffold = pcdDialog.ChosenCurve;
            Presets.Pcd.Parameters = pcdDialog.Parameters;
        }

        private void UpdateUiByChosenScaffoldStatus()
        {
            switch ( Presets.Pcd.ChosenScaffold ) {
            case Enums.PatternCurveScaffold.Polynomial:
                uiPnlGen_CrvScaff2_TxtBx.Text = Consts.Ui.Panel.Generate.TxtPolynomial;
                break;
            case Enums.PatternCurveScaffold.Hyperbolic:
                uiPnlGen_CrvScaff2_TxtBx.Text = Consts.Ui.Panel.Generate.TxtHyperbolic;
                break;
            case Enums.PatternCurveScaffold.WaveformSine:
            case Enums.PatternCurveScaffold.WaveformSquare:
            case Enums.PatternCurveScaffold.WaveformTriangle:
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                uiPnlGen_CrvScaff2_TxtBx.Text = Consts.Ui.Panel.Generate.TxtWaveform;
                break;
            default:
                uiPnlGen_CrvScaff2_TxtBx.Text = Consts.Ui.Panel.Generate.TxtNotChosen;
                break;
            }
        }

        private void UiPanelDataSheet_CurveType_SelectedIndexChanged( object sender, EventArgs e )
        {
            switch ( (Enums.DataSetCurveType) WinFormsHelper.GetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx ) ) {
            case Enums.DataSetCurveType.Generated:
                uiPnlDtSh_CrvIdx_Num.Enabled = true;
                uiPnlDtSh_CrvIdx_TrBr.Enabled = true;
                int selectedCurveIndex = WinFormsHelper.GetValue( uiPnlDtSh_CrvIdx_TrBr );
                ShowGeneratedCurveSeriesOnChart( selectedCurveIndex );
                break;
            case Enums.DataSetCurveType.Pattern:
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
            Presets.Ui.NumberOfCurves = numberOfCurves;
            uiPnlGen_Crvs2No_Nm.Minimum = 1;
            uiPnlGen_Crvs2No_Nm.Maximum = numberOfCurves;
        }

        private void UiPanelGenerate_GenerateSet_Click( object sender, EventArgs e )
        {
            if ( uiPnlGen_CrvScaff2_TxtBx.Text == Consts.Ui.Panel.Generate.TxtNotChosen ) {
                MsgBxShower.Ui.PatternCurveNotChosenPrerequisite();
                return;
            }

            GrabPreSetsForCurvesGeneration();
            GenerateAndShowPatternCurve();
            ChartsData.SpreadPatternCurveSetToGeneratedCurveSet( Presets.Ui.NumberOfCurves );
        }

        private void GrabPreSetsForCurvesGeneration()
        {
            Presets.Ui.NumberOfCurves = WinFormsHelper.GetValue<int>( uiPnlGen_Crvs1No_Num );
            Presets.Ui.StartingXPoint = WinFormsHelper.GetValue<double>( uiPnlGen_StartX_Num );
            Presets.Ui.EndingXPoint = WinFormsHelper.GetValue<double>( uiPnlGen_EndX_Num );
            Presets.Ui.PointsDensity = WinFormsHelper.GetValue<int>( uiPnlGen_Dens_Num );
        }

        private void GenerateAndShowPatternCurve()
        {
            Enums.PatternCurveScaffold scaffoldType = Presets.Pcd.ChosenScaffold;
            double xStart = Presets.Ui.StartingXPoint;
            double xEnd = Presets.Ui.EndingXPoint;
            int density = Presets.Ui.PointsDensity;

            if ( !ChartsData.GeneratePatternCurve( scaffoldType, xStart, xEnd, density ) ) {
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

        private void UpdateUiByDotNetFrameworkVersion()
        {
            string dotNetVersion = SysInfoHelper.ObtainUsedDotNetFrameworkVersion();

            if ( dotNetVersion == null ) {
                uiPnlPrg_DotNetFr2_TxtBx.Text = Consts.Ui.Panel.Program.InfoObtainingErrTxt;
                return;
            }

            uiPnlPrg_DotNetFr2_TxtBx.Text = dotNetVersion;

        }

        private void UpdateUiByOsVersionName()
        {
            string osVersion = SysInfoHelper.ObtaingApplicationRunningOSVersion();

            if ( osVersion == null ) {
                uiPnlPrg_OsVer2_TxtBx.Text = Consts.Ui.Panel.Program.InfoObtainingErrTxt;
                return;
            }

            uiPnlPrg_OsVer2_TxtBx.Text = osVersion;
        }

        private void UiPanelDataSheet_ShowDataSet_Click( object sender, EventArgs e )
        {
            int selectedCurveType = WinFormsHelper.GetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx );
            int selectedCurveIndex = WinFormsHelper.GetValue<int>( uiPnlDtSh_CrvIdx_Num );

            switch ( (Enums.DataSetCurveType) selectedCurveType ) {
            case Enums.DataSetCurveType.Pattern:
            case Enums.DataSetCurveType.Generated:
                break;
            default:
                MsgBxShower.Ui.CurveTypeNotSelectedInfo();
                return;
            }

            Series selectedCurveSeries = SpecifyCurveSeries( selectedCurveType, selectedCurveIndex );

            if ( selectedCurveSeries == null || ChartsData.PatternCurveSet.Points.Count == 0 ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                return;
            }

            using ( var gprvDialog = new GridPreviewer( selectedCurveSeries ) ) {
                WinFormsHelper.ShowDialogSafe( gprvDialog, this );

                try {
                    if ( gprvDialog.DialogResult == DialogResult.OK ) {
                        ChartsData.AbsorbSeriesPoints( gprvDialog.ChartDataSet, selectedCurveType, selectedCurveIndex );
                        uiCharts_PtrnCrv.Series.Clear();
                        uiCharts_PtrnCrv.Series.Add( gprvDialog.ChartDataSet );
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
                switch ( (Enums.DataSetCurveType) curveType ) {
                case Enums.DataSetCurveType.Pattern:
                    return ChartsData.PatternCurveSet;
                case Enums.DataSetCurveType.Generated:
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

        private void UpdateUiBySettingChartsProperties()
        {
            CurvesDataset.SetDefaultProperties( uiCharts_PtrnCrv );
            CurvesDataset.SetDefaultProperties( uiCharts_MeanCrv );
        }

    }

}
