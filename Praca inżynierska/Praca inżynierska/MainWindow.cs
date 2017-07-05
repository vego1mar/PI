using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    public partial class MainWindow : Form
    {

        private Thread Timer { get; set; }
        private UiSettings Settings { get; set; }
        private CurvesDataManager DataChart { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitalizeFields();
        }

        private void InitalizeFields()
        {
            Timer = null;
            Settings = new UiSettings();
            DataChart = new CurvesDataManager( Settings.Presets.Pcd.Parameters );
        }

        private void UiMainWindow_FormClosed( object sender, FormClosedEventArgs e )
        {
            Logger.WriteLine( Environment.NewLine + nameof( Logger.NumberOfLoggedExceptions ) + ": " + Logger.NumberOfLoggedExceptions );
            Logger.Close();
        }

        private void UiMainWindow_Load( object sender, EventArgs e )
        {
            Logger.Initialize();
            UpdateUiByDotNetFrameworkVersion();
            UpdateUiByOsVersionName();
            UpdateUiByLogFileFullPathLocation();
            UpdateUiByDefaultSettings();
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
            using ( var pcdDialog = new PatternCurveDefiner( Settings.Presets.Pcd ) ) {
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
            Settings.Presets.Pcd.Scaffold = pcdDialog.Settings.Scaffold;
            Settings.Presets.Pcd.Parameters = pcdDialog.Settings.Parameters;
        }

        private void UpdateUiByChosenScaffoldStatus()
        {
            switch ( Settings.Presets.Pcd.Scaffold ) {
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
                UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Generated, WinFormsHelper.GetValue( uiPnlDtSh_CrvIdx_TrBr ) );
                break;
            case Enums.DataSetCurveType.Pattern:
                uiPnlDtSh_CrvIdx_Num.Enabled = false;
                uiPnlDtSh_CrvIdx_TrBr.Enabled = false;
                UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Pattern );
                break;
            case Enums.DataSetCurveType.Average:
                uiPnlDtSh_CrvIdx_Num.Enabled = false;
                uiPnlDtSh_CrvIdx_TrBr.Enabled = false;
                UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Average );
                break;
            }
        }

        private void UiPanelDataSheet_CurveIndex_NumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            int curveIndex = WinFormsHelper.GetValue<int>( uiPnlDtSh_CrvIdx_Num );
            WinFormsHelper.SetValue( uiPnlDtSh_CrvIdx_TrBr, curveIndex );
            UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Generated, curveIndex );
        }

        private void UiPanelDataSheet_CurveIndex_TrackBar_Scroll( object sender, EventArgs e )
        {
            int curveIndex = WinFormsHelper.GetValue( uiPnlDtSh_CrvIdx_TrBr );
            WinFormsHelper.SetValue( uiPnlDtSh_CrvIdx_Num, curveIndex );
            UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Generated, curveIndex );
        }

        private void UiPanelGenerate_GenerateSet_Click( object sender, EventArgs e )
        {
            if ( uiPnlGen_CrvScaff2_TxtBx.Text == Consts.Ui.Panel.Generate.TxtNotChosen ) {
                MsgBxShower.Ui.PatternCurveNotChosenPrerequisite();
                return;
            }

            GrabPreSetsForCurvesGeneration();
            GenerateAndShowPatternCurve();
            DataChart.SpreadPatternCurveSetToGeneratedCurveSet( Settings.Presets.Ui.NumberOfCurves );
            DataChart.ClearAverageCurveSetPoints();
            UpdateUiBySettingRangesForCurvesNumber();
            WinFormsHelper.SetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx, (int) Enums.DataSetCurveType.Generated );
        }

        private void GrabPreSetsForCurvesGeneration()
        {
            Settings.Presets.Ui.NumberOfCurves = WinFormsHelper.GetValue<int>( uiPnlGen_Crvs1No_Num );
            Settings.Presets.Ui.StartingXPoint = WinFormsHelper.GetValue<double>( uiPnlGen_StartX_Num );
            Settings.Presets.Ui.EndingXPoint = WinFormsHelper.GetValue<double>( uiPnlGen_EndX_Num );
            Settings.Presets.Ui.PointsDensity = WinFormsHelper.GetValue<int>( uiPnlGen_Dens_Num );
        }

        private void GenerateAndShowPatternCurve()
        {
            Enums.PatternCurveScaffold scaffoldType = Settings.Presets.Pcd.Scaffold;
            double xStart = Settings.Presets.Ui.StartingXPoint;
            double xEnd = Settings.Presets.Ui.EndingXPoint;
            int density = Settings.Presets.Ui.PointsDensity;

            if ( !DataChart.GeneratePatternCurve( scaffoldType, xStart, xEnd, density ) ) {
                DataChart.RemoveInvalidPoints( Enums.DataSetCurveType.Pattern );
                MsgBxShower.Ui.PointsNotValidToChartProblem();
            }

            if ( DataChart.PatternCurveSet.Points.Count > 0 ) {
                UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Pattern );
            }
        }

        private void UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType curveType, int indexOfGeneratedCurve = 1 )
        {
            try {
                uiCharts_Crv.Series.Clear();

                switch ( curveType ) {
                case Enums.DataSetCurveType.Pattern:
                    uiCharts_Crv.Series.Add( DataChart.PatternCurveSet );
                    SetPatternCurveSeriesSettings( uiCharts_Crv );
                    break;
                case Enums.DataSetCurveType.Generated:
                    uiCharts_Crv.Series.Add( DataChart.GeneratedCurvesSet[indexOfGeneratedCurve - 1] );
                    SetGeneratedCurveSeriesSettings( uiCharts_Crv );
                    break;
                case Enums.DataSetCurveType.Average:
                    uiCharts_Crv.Series.Add( DataChart.AverageCurveSet );
                    SetAverageCurveSeriesSettings( uiCharts_Crv );
                    break;
                }

                uiCharts_Crv.ChartAreas[0].RecalculateAxesScale();
                uiCharts_Crv.Visible = true;
                uiCharts_Crv.Invalidate();
            }
            catch ( InvalidOperationException x ) {
                MsgBxShower.Ui.ChartRefreshingError();
                Logger.WriteException( x );
            }
            catch ( ArgumentOutOfRangeException x ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                Logger.WriteException( x );
            }
        }

        private void SetPatternCurveSeriesSettings( Chart chart, int seriesNo = 0 )
        {
            chart.Series[seriesNo].Color = Settings.Series.Pattern.Color;
            chart.Series[seriesNo].BorderWidth = Settings.Series.Pattern.BorderWidth;
            chart.Series[seriesNo].BorderDashStyle = Settings.Series.Pattern.BorderDashStyle;
            chart.Series[seriesNo].ChartType = Settings.Series.Pattern.ChartType;
        }

        private void SetGeneratedCurveSeriesSettings( Chart chart, int seriesNo = 0 )
        {
            chart.Series[seriesNo].Color = Settings.Series.Generated.Color;
            chart.Series[seriesNo].BorderWidth = Settings.Series.Generated.BorderWidth;
            chart.Series[seriesNo].BorderDashStyle = Settings.Series.Generated.BorderDashStyle;
            chart.Series[seriesNo].ChartType = Settings.Series.Generated.ChartType;
        }

        private void SetAverageCurveSeriesSettings( Chart chart, int seriesNo = 0 )
        {
            chart.Series[seriesNo].Color = Settings.Series.Average.Color;
            chart.Series[seriesNo].BorderWidth = Settings.Series.Average.BorderWidth;
            chart.Series[seriesNo].BorderDashStyle = Settings.Series.Average.BorderDashStyle;
            chart.Series[seriesNo].ChartType = Settings.Series.Average.ChartType;
        }

        private void UpdateUiBySettingRangesForCurvesNumber()
        {
            uiPnlGen_Crvs2No_Nm.Minimum = 1;
            uiPnlGen_Crvs2No_Nm.Maximum = Settings.Presets.Ui.NumberOfCurves;
            uiPnlGen_Crvs2No_Nm.Value = uiPnlGen_Crvs2No_Nm.Maximum;
            uiPnlDtSh_CrvIdx_Num.Minimum = 1;
            uiPnlDtSh_CrvIdx_Num.Maximum = Settings.Presets.Ui.NumberOfCurves;
            uiPnlDtSh_CrvIdx_Num.Value = uiPnlDtSh_CrvIdx_Num.Minimum;
            uiPnlDtSh_CrvIdx_TrBr.Minimum = 1;
            uiPnlDtSh_CrvIdx_TrBr.Maximum = Settings.Presets.Ui.NumberOfCurves;
            uiPnlDtSh_CrvIdx_TrBr.Value = uiPnlDtSh_CrvIdx_TrBr.Minimum;
            uiPnlDtSh_CrvNo_Num.Minimum = 1;
            uiPnlDtSh_CrvNo_Num.Maximum = Settings.Presets.Ui.NumberOfCurves;
            uiPnlDtSh_CrvNo_Num.Value = uiPnlDtSh_CrvNo_Num.Maximum;
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
            case Enums.DataSetCurveType.Average:
                break;
            default:
                MsgBxShower.Ui.CurveTypeNotSelectedInfo();
                return;
            }

            Series selectedCurveSeries = SpecifyCurveSeries( selectedCurveType, selectedCurveIndex );

            if ( selectedCurveSeries == null || DataChart.PatternCurveSet.Points.Count == 0 ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                return;
            }

            using ( var gprvDialog = new GridPreviewer( selectedCurveSeries ) ) {
                WinFormsHelper.ShowDialogSafe( gprvDialog, this );

                try {
                    if ( gprvDialog.DialogResult == DialogResult.OK ) {
                        DataChart.AbsorbSeriesPoints( gprvDialog.ChartDataSet, selectedCurveType, selectedCurveIndex );
                        uiCharts_Crv.Series.Clear();
                        uiCharts_Crv.Series.Add( gprvDialog.ChartDataSet );
                        uiCharts_Crv.Series[0].BorderWidth = 3;
                        uiCharts_Crv.Series[0].Color = System.Drawing.Color.Indigo;
                        uiCharts_Crv.ChartAreas[0].RecalculateAxesScale();
                        uiCharts_Crv.Visible = true;
                        uiCharts_Crv.Invalidate();
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
                    return DataChart.PatternCurveSet;
                case Enums.DataSetCurveType.Generated:
                    return DataChart.GeneratedCurvesSet[curveIndex - 1];
                case Enums.DataSetCurveType.Average:
                    return DataChart.AverageCurveSet;
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

        private void UiPanelDataSheet_Malform_Click( object sender, EventArgs e )
        {
            if ( DataChart.PatternCurveSet.Points.Count == 0 ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                return;
            }

            int numberOfCurves = WinFormsHelper.GetValue<int>( uiPnlDtSh_CrvNo_Num );
            double surrounding = WinFormsHelper.GetValue<double>( uiPnlDtSh_Surr_Num );
            bool? result = DataChart.MakeGaussianNoiseForGeneratedCurves( numberOfCurves, surrounding );

            if ( result == null ) {
                MsgBxShower.Ui.SpecifiedCurveDoesntExistProblem();
                return;
            }

            if ( !result.Value ) {
                MsgBxShower.Ui.OperationMalformRejectedStop();
                return;
            }

            DataChart.ClearAverageCurveSetPoints();
            WinFormsHelper.SetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx, (int) Enums.DataSetCurveType.Generated );
            UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Generated );
        }

        private void UiPanelGenerate_Apply_Click( object sender, EventArgs e )
        {
            if ( DataChart.PatternCurveSet.Points.Count == 0 ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                return;
            }

            Enums.MeanType meanType = (Enums.MeanType) WinFormsHelper.GetSelectedIndexSafe( uiPnlGen_MeanT_ComBx );
            int numberOfCurves = WinFormsHelper.GetValue<int>( uiPnlGen_Crvs2No_Nm );

            if ( meanType == Enums.MeanType.Mediana && numberOfCurves < 3 ) {
                MsgBxShower.Ui.NotEnoughCurvesForMedianaStop();
                return;
            }

            bool? averageResult = DataChart.MakeAverageCurveFromGeneratedCurves( meanType, numberOfCurves );

            if ( !averageResult.Value ) {
                DataChart.RemoveInvalidPoints( Enums.DataSetCurveType.Average );
                MsgBxShower.Ui.PointsNotValidToChartProblem();
            }

            WinFormsHelper.SetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx, (int) Enums.DataSetCurveType.Average );
        }

        private void UpdateUiByDefaultSettings()
        {
            CurvesDataManager.SetDefaultProperties( uiCharts_Crv );
            WinFormsHelper.SetSelectedIndexSafe( uiPnlGen_MeanT_ComBx, (int) Enums.MeanType.Geometric );
        }

        private void UiMainWindow_Resize( object sender, EventArgs e )
        {
            if ( Settings.Menu.Panel.Hide ) {
                return;
            }

            if ( Settings.Menu.Panel.KeepProportions ) {
                uiMw_SpCtn.SplitterDistance = 275;
            }
        }

        private void UiMenuPanel_KeepProportions_Click( object sender, EventArgs e )
        {
            Settings.Menu.Panel.KeepProportions = !Settings.Menu.Panel.KeepProportions;
            uiMenuPnl_KeepProp.Checked = Settings.Menu.Panel.KeepProportions;
        }

        private void UiMenuPanel_Hide_Click( object sender, EventArgs e )
        {
            Settings.Menu.Panel.Hide = !Settings.Menu.Panel.Hide;
            uiMenuPnl_Hide.Checked = Settings.Menu.Panel.Hide;

            if ( uiMenuPnl_Hide.Checked ) {
                Settings.Menu.Panel.SplitterDistance = uiMw_SpCtn.SplitterDistance;
                uiMw_SpCtn.SplitterDistance = 0;
                uiMw_SpCtn.Panel1.Hide();
                uiMw_SpCtn.Enabled = false;
                return;
            }

            uiMw_SpCtn.SplitterDistance = Settings.Menu.Panel.SplitterDistance;
            uiMw_SpCtn.Panel1.Show();
            uiMw_SpCtn.Enabled = true;
        }

        private void UiMenuPanel_Lock_Click( object sender, EventArgs e )
        {
            Settings.Menu.Panel.Lock = !Settings.Menu.Panel.Lock;
            uiMenuPnl_Lock.Checked = Settings.Menu.Panel.Lock;
            uiMw_SpCtn.Panel1.Enabled = !Settings.Menu.Panel.Lock;
        }

        private void UiMenuMeans_AveragingInfo_Click( object sender, EventArgs e )
        {
            using ( var msgBox = new AvgInfo() ) {
                WinFormsHelper.ShowDialogSafe( msgBox, this );
            }
        }

        private void UiMenuMeans_Settings_Click( object sender, EventArgs e )
        {
            using ( var dialog = new MeansSettings() ) {
                dialog.SetPowerMeanRank( DataChart.PowerMeanRank );
                WinFormsHelper.ShowDialogSafe( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    DataChart.PowerMeanRank = dialog.PowerMeanRank;
                }
            }
        }

        private void UiMenuChart_Settings_Click( object sender, EventArgs e )
        {
            using ( var dialog = new ChartSettings( GetChartSettings( uiCharts_Crv ) ) ) {
                WinFormsHelper.ShowDialogSafe( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    SetChartSettings( dialog.Settings, uiCharts_Crv );
                    UpdateUiByInvalidatingChartSettings();
                }
            }
        }

        private ChartSettingsPool GetChartSettings( Chart chart, int areaNo = 0 )
        {
            ChartSettingsPool settings = new ChartSettingsPool();
            settings.Common.AntiAliasing = chart.AntiAliasing;
            settings.Common.SuppressExceptions = chart.SuppressExceptions;
            settings.Common.BackColor = chart.BackColor;
            settings.Areas.Common.Area3dStyle = chart.ChartAreas[areaNo].Area3DStyle.Enable3D;
            settings.Areas.Common.BackColor = chart.ChartAreas[areaNo].BackColor;
            settings.Areas.X.MajorGrid.Enabled = chart.ChartAreas[areaNo].AxisX.MajorGrid.Enabled;
            settings.Areas.X.MajorGrid.LineColor = chart.ChartAreas[areaNo].AxisX.MajorGrid.LineColor;
            settings.Areas.X.MajorGrid.LineDashStyle = chart.ChartAreas[areaNo].AxisX.MajorGrid.LineDashStyle;
            settings.Areas.X.MajorGrid.LineWidth = chart.ChartAreas[areaNo].AxisX.MajorGrid.LineWidth;
            settings.Areas.X.MinorGrid.Enabled = chart.ChartAreas[areaNo].AxisX.MinorGrid.Enabled;
            settings.Areas.X.MinorGrid.LineColor = chart.ChartAreas[areaNo].AxisX.MinorGrid.LineColor;
            settings.Areas.X.MinorGrid.LineDashStyle = chart.ChartAreas[areaNo].AxisX.MinorGrid.LineDashStyle;
            settings.Areas.X.MinorGrid.LineWidth = chart.ChartAreas[areaNo].AxisX.MinorGrid.LineWidth;
            settings.Areas.Y.MajorGrid.Enabled = chart.ChartAreas[areaNo].AxisY.MajorGrid.Enabled;
            settings.Areas.Y.MajorGrid.LineColor = chart.ChartAreas[areaNo].AxisY.MajorGrid.LineColor;
            settings.Areas.Y.MajorGrid.LineDashStyle = chart.ChartAreas[areaNo].AxisY.MajorGrid.LineDashStyle;
            settings.Areas.Y.MajorGrid.LineWidth = chart.ChartAreas[areaNo].AxisY.MajorGrid.LineWidth;
            settings.Areas.Y.MinorGrid.Enabled = chart.ChartAreas[areaNo].AxisY.MinorGrid.Enabled;
            settings.Areas.Y.MinorGrid.LineColor = chart.ChartAreas[areaNo].AxisY.MinorGrid.LineColor;
            settings.Areas.Y.MinorGrid.LineDashStyle = chart.ChartAreas[areaNo].AxisY.MinorGrid.LineDashStyle;
            settings.Areas.Y.MinorGrid.LineWidth = chart.ChartAreas[areaNo].AxisY.MinorGrid.LineWidth;
            settings.Series.Pattern.Color = Settings.Series.Pattern.Color;
            settings.Series.Pattern.BorderWidth = Settings.Series.Pattern.BorderWidth;
            settings.Series.Pattern.BorderDashStyle = Settings.Series.Pattern.BorderDashStyle;
            settings.Series.Pattern.ChartType = Settings.Series.Pattern.ChartType;
            settings.Series.Generated.Color = Settings.Series.Generated.Color;
            settings.Series.Generated.BorderWidth = Settings.Series.Generated.BorderWidth;
            settings.Series.Generated.BorderDashStyle = Settings.Series.Generated.BorderDashStyle;
            settings.Series.Generated.ChartType = Settings.Series.Generated.ChartType;
            settings.Series.Average.Color = Settings.Series.Average.Color;
            settings.Series.Average.BorderWidth = Settings.Series.Average.BorderWidth;
            settings.Series.Average.BorderDashStyle = Settings.Series.Average.BorderDashStyle;
            settings.Series.Average.ChartType = Settings.Series.Average.ChartType;
            return settings;
        }

        private void SetChartSettings( ChartSettingsPool settings, Chart chart, int areaNo = 0 )
        {
            switch ( settings.ApplyMode ) {
            case ChartSettings.ApplyToCurve.All:
                SetCommonChartSettings( settings, chart, areaNo );
                break;
            case ChartSettings.ApplyToCurve.Average:
            case ChartSettings.ApplyToCurve.Generated:
            case ChartSettings.ApplyToCurve.Pattern:
                SetSeriesChartSettings( settings );
                break;
            }
        }

        private void SetCommonChartSettings( ChartSettingsPool settings, Chart chart, int areaNo = 0 )
        {
            chart.AntiAliasing = settings.Common.AntiAliasing;
            chart.SuppressExceptions = settings.Common.SuppressExceptions;
            chart.BackColor = settings.Common.BackColor;
            chart.ChartAreas[areaNo].Area3DStyle.Enable3D = settings.Areas.Common.Area3dStyle;
            chart.ChartAreas[areaNo].BackColor = settings.Areas.Common.BackColor;
            chart.ChartAreas[areaNo].AxisX.MajorGrid.Enabled = settings.Areas.X.MajorGrid.Enabled;
            chart.ChartAreas[areaNo].AxisX.MajorGrid.LineColor = settings.Areas.X.MajorGrid.LineColor;
            chart.ChartAreas[areaNo].AxisX.MajorGrid.LineDashStyle = settings.Areas.X.MajorGrid.LineDashStyle;
            chart.ChartAreas[areaNo].AxisX.MajorGrid.LineWidth = settings.Areas.X.MajorGrid.LineWidth;
            chart.ChartAreas[areaNo].AxisX.MinorGrid.Enabled = settings.Areas.X.MinorGrid.Enabled;
            chart.ChartAreas[areaNo].AxisX.MinorGrid.LineColor = settings.Areas.X.MinorGrid.LineColor;
            chart.ChartAreas[areaNo].AxisX.MinorGrid.LineDashStyle = settings.Areas.X.MinorGrid.LineDashStyle;
            chart.ChartAreas[areaNo].AxisX.MinorGrid.LineWidth = settings.Areas.X.MinorGrid.LineWidth;
            chart.ChartAreas[areaNo].AxisY.MajorGrid.Enabled = settings.Areas.Y.MajorGrid.Enabled;
            chart.ChartAreas[areaNo].AxisY.MajorGrid.LineColor = settings.Areas.Y.MajorGrid.LineColor;
            chart.ChartAreas[areaNo].AxisY.MajorGrid.LineDashStyle = settings.Areas.Y.MajorGrid.LineDashStyle;
            chart.ChartAreas[areaNo].AxisY.MajorGrid.LineWidth = settings.Areas.Y.MajorGrid.LineWidth;
            chart.ChartAreas[areaNo].AxisY.MinorGrid.Enabled = settings.Areas.Y.MinorGrid.Enabled;
            chart.ChartAreas[areaNo].AxisY.MinorGrid.LineColor = settings.Areas.Y.MinorGrid.LineColor;
            chart.ChartAreas[areaNo].AxisY.MinorGrid.LineDashStyle = settings.Areas.Y.MinorGrid.LineDashStyle;
            chart.ChartAreas[areaNo].AxisY.MinorGrid.LineWidth = settings.Areas.Y.MinorGrid.LineWidth;
        }

        private void SetSeriesChartSettings( ChartSettingsPool settings )
        {
            switch ( settings.ApplyMode ) {
            case ChartSettings.ApplyToCurve.Pattern:
                SetPatternSeriesChartSettings( settings );
                break;
            case ChartSettings.ApplyToCurve.Generated:
                SetGeneratedSeriesChartSettings( settings );
                break;
            case ChartSettings.ApplyToCurve.Average:
                SetAverageSeriesChartSettings( settings );
                break;
            }
        }

        private void SetPatternSeriesChartSettings( ChartSettingsPool settings )
        {
            Settings.Series.Pattern.Color = settings.Series.Pattern.Color;
            Settings.Series.Pattern.BorderWidth = settings.Series.Pattern.BorderWidth;
            Settings.Series.Pattern.BorderDashStyle = settings.Series.Pattern.BorderDashStyle;
            Settings.Series.Pattern.ChartType = settings.Series.Pattern.ChartType;
        }

        private void SetGeneratedSeriesChartSettings( ChartSettingsPool settings )
        {
            Settings.Series.Generated.Color = settings.Series.Generated.Color;
            Settings.Series.Generated.BorderWidth = settings.Series.Generated.BorderWidth;
            Settings.Series.Generated.BorderDashStyle = settings.Series.Generated.BorderDashStyle;
            Settings.Series.Generated.ChartType = settings.Series.Generated.ChartType;
        }

        private void SetAverageSeriesChartSettings( ChartSettingsPool settings )
        {
            Settings.Series.Average.Color = settings.Series.Average.Color;
            Settings.Series.Average.BorderWidth = settings.Series.Average.BorderWidth;
            Settings.Series.Average.BorderDashStyle = settings.Series.Average.BorderDashStyle;
            Settings.Series.Average.ChartType = settings.Series.Average.ChartType;
        }

        private void UpdateUiByInvalidatingChartSettings()
        {
            switch ( (Enums.DataSetCurveType) WinFormsHelper.GetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx ) ) {
            case Enums.DataSetCurveType.Pattern:
                UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Pattern );
                break;
            case Enums.DataSetCurveType.Generated:
                UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Generated );
                break;
            case Enums.DataSetCurveType.Average:
                UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Average );
                break;
            }
        }

    }

}
