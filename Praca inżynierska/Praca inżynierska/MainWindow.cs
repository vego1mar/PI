using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    public partial class MainWindow : Form
    {

        private Thread Timer { get; set; }
        private CurvesDataManager ChartData { get; set; }
        private UiSettings Settings { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitalizeFields();
        }

        private void InitalizeFields()
        {
            Timer = null;
            ChartData = new CurvesDataManager();
            Settings = new UiSettings();
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
            ChartData.SpreadPatternCurveSetToGeneratedCurveSet( Presets.Ui.NumberOfCurves );
            ChartData.ClearAverageCurveSetPoints();
            UpdateUiBySettingRangesForCurvesNumber();
            WinFormsHelper.SetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx, (int) Enums.DataSetCurveType.Generated );
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

            if ( !ChartData.GeneratePatternCurve( scaffoldType, xStart, xEnd, density ) ) {
                ChartData.RemoveInvalidPoints( Enums.DataSetCurveType.Pattern );
                MsgBxShower.Ui.PointsNotValidToChartProblem();
            }

            if ( ChartData.PatternCurveSet.Points.Count > 0 ) {
                UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Pattern );
            }
        }

        private void UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType curveType, int indexOfGeneratedCurve = 1 )
        {
            try {
                uiCharts_Crv.Series.Clear();

                switch ( curveType ) {
                case Enums.DataSetCurveType.Pattern:
                    uiCharts_Crv.Series.Add( ChartData.PatternCurveSet );
                    uiCharts_Crv.Series[0].Color = System.Drawing.Color.Black;
                    break;
                case Enums.DataSetCurveType.Generated:
                    uiCharts_Crv.Series.Add( ChartData.GeneratedCurvesSet[indexOfGeneratedCurve - 1] );
                    uiCharts_Crv.Series[0].Color = System.Drawing.Color.Crimson;
                    break;
                case Enums.DataSetCurveType.Average:
                    uiCharts_Crv.Series.Add( ChartData.AverageCurveSet );
                    uiCharts_Crv.Series[0].Color = System.Drawing.Color.ForestGreen;
                    break;
                }

                uiCharts_Crv.Series[0].BorderWidth = 3;
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

        private void UpdateUiBySettingRangesForCurvesNumber()
        {
            uiPnlGen_Crvs2No_Nm.Minimum = 1;
            uiPnlGen_Crvs2No_Nm.Maximum = Presets.Ui.NumberOfCurves;
            uiPnlGen_Crvs2No_Nm.Value = uiPnlGen_Crvs2No_Nm.Maximum;
            uiPnlDtSh_CrvIdx_Num.Minimum = 1;
            uiPnlDtSh_CrvIdx_Num.Maximum = Presets.Ui.NumberOfCurves;
            uiPnlDtSh_CrvIdx_Num.Value = uiPnlDtSh_CrvIdx_Num.Minimum;
            uiPnlDtSh_CrvIdx_TrBr.Minimum = 1;
            uiPnlDtSh_CrvIdx_TrBr.Maximum = Presets.Ui.NumberOfCurves;
            uiPnlDtSh_CrvIdx_TrBr.Value = uiPnlDtSh_CrvIdx_TrBr.Minimum;
            uiPnlDtSh_CrvNo_Num.Minimum = 1;
            uiPnlDtSh_CrvNo_Num.Maximum = Presets.Ui.NumberOfCurves;
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

            if ( selectedCurveSeries == null || ChartData.PatternCurveSet.Points.Count == 0 ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                return;
            }

            using ( var gprvDialog = new GridPreviewer( selectedCurveSeries ) ) {
                WinFormsHelper.ShowDialogSafe( gprvDialog, this );

                try {
                    if ( gprvDialog.DialogResult == DialogResult.OK ) {
                        ChartData.AbsorbSeriesPoints( gprvDialog.ChartDataSet, selectedCurveType, selectedCurveIndex );
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
                    return ChartData.PatternCurveSet;
                case Enums.DataSetCurveType.Generated:
                    return ChartData.GeneratedCurvesSet[curveIndex - 1];
                case Enums.DataSetCurveType.Average:
                    return ChartData.AverageCurveSet;
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
            if ( ChartData.PatternCurveSet.Points.Count == 0 ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                return;
            }

            int numberOfCurves = WinFormsHelper.GetValue<int>( uiPnlDtSh_CrvNo_Num );
            double surrounding = WinFormsHelper.GetValue<double>( uiPnlDtSh_Surr_Num );
            bool? result = ChartData.MakeGaussianNoiseForGeneratedCurves( numberOfCurves, surrounding );

            if ( result == null ) {
                MsgBxShower.Ui.SpecifiedCurveDoesntExistProblem();
                return;
            }

            if ( !result.Value ) {
                MsgBxShower.Ui.OperationMalformRejectedStop();
                return;
            }

            ChartData.ClearAverageCurveSetPoints();
            WinFormsHelper.SetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx, (int) Enums.DataSetCurveType.Generated );
            UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Generated );
        }

        private void UiPanelGenerate_Apply_Click( object sender, EventArgs e )
        {
            if ( ChartData.PatternCurveSet.Points.Count == 0 ) {
                MsgBxShower.Ui.SeriesSelectionProblem();
                return;
            }

            Enums.MeanType meanType = (Enums.MeanType) WinFormsHelper.GetSelectedIndexSafe( uiPnlGen_MeanT_ComBx );
            int numberOfCurves = WinFormsHelper.GetValue<int>( uiPnlGen_Crvs2No_Nm );

            if ( meanType == Enums.MeanType.Mediana && numberOfCurves < 3 ) {
                MsgBxShower.Ui.NotEnoughCurvesForMedianaStop();
                return;
            }

            bool? averageResult = ChartData.MakeAverageCurveFromGeneratedCurves( meanType, numberOfCurves );

            if ( !averageResult.Value ) {
                ChartData.RemoveInvalidPoints( Enums.DataSetCurveType.Average );
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
                dialog.SetPowerMeanRank( ChartData.PowerMeanRank );
                WinFormsHelper.ShowDialogSafe( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    ChartData.PowerMeanRank = dialog.PowerMeanRank;
                }
            }
        }

        private void UiMenuChart_Settings_Click( object sender, EventArgs e )
        {
            using ( var dialog = new ChartSettings( GetChartSettingsPool() ) ) {
                WinFormsHelper.ShowDialogSafe( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    SetChartSettings( dialog.Settings );
                }
            }
        }

        private ChartSettingsPool GetChartSettingsPool()
        {
            ChartSettingsPool settings = new ChartSettingsPool();
            settings.Common.AntiAliasing = uiCharts_Crv.AntiAliasing;
            settings.Common.SuppressExceptions = uiCharts_Crv.SuppressExceptions;
            settings.Common.BackColor = uiCharts_Crv.BackColor;
            settings.Areas.Common.Area3dStyle = uiCharts_Crv.ChartAreas[0].Area3DStyle.Enable3D;
            settings.Areas.Common.BackColor = uiCharts_Crv.ChartAreas[0].BackColor;
            settings.Areas.X.MajorGrid.Enabled = uiCharts_Crv.ChartAreas[0].AxisX.MajorGrid.Enabled;
            settings.Areas.X.MajorGrid.LineColor = uiCharts_Crv.ChartAreas[0].AxisX.MajorGrid.LineColor;
            settings.Areas.X.MajorGrid.LineDashStyle = uiCharts_Crv.ChartAreas[0].AxisX.MajorGrid.LineDashStyle;
            settings.Areas.X.MajorGrid.LineWidth = uiCharts_Crv.ChartAreas[0].AxisX.MajorGrid.LineWidth;
            settings.Areas.X.MinorGrid.Enabled = uiCharts_Crv.ChartAreas[0].AxisX.MinorGrid.Enabled;
            settings.Areas.X.MinorGrid.LineColor = uiCharts_Crv.ChartAreas[0].AxisX.MinorGrid.LineColor;
            settings.Areas.X.MinorGrid.LineDashStyle = uiCharts_Crv.ChartAreas[0].AxisX.MinorGrid.LineDashStyle;
            settings.Areas.X.MinorGrid.LineWidth = uiCharts_Crv.ChartAreas[0].AxisX.MinorGrid.LineWidth;
            settings.Areas.Y.MajorGrid.Enabled = uiCharts_Crv.ChartAreas[0].AxisY.MajorGrid.Enabled;
            settings.Areas.Y.MajorGrid.LineColor = uiCharts_Crv.ChartAreas[0].AxisY.MajorGrid.LineColor;
            settings.Areas.Y.MajorGrid.LineDashStyle = uiCharts_Crv.ChartAreas[0].AxisY.MajorGrid.LineDashStyle;
            settings.Areas.Y.MajorGrid.LineWidth = uiCharts_Crv.ChartAreas[0].AxisY.MajorGrid.LineWidth;
            settings.Areas.Y.MinorGrid.Enabled = uiCharts_Crv.ChartAreas[0].AxisY.MinorGrid.Enabled;
            settings.Areas.Y.MinorGrid.LineColor = uiCharts_Crv.ChartAreas[0].AxisY.MinorGrid.LineColor;
            settings.Areas.Y.MinorGrid.LineDashStyle = uiCharts_Crv.ChartAreas[0].AxisY.MinorGrid.LineDashStyle;
            settings.Areas.Y.MinorGrid.LineWidth = uiCharts_Crv.ChartAreas[0].AxisY.MinorGrid.LineWidth;
            return settings;
        }

        private void SetChartSettings( ChartSettingsPool settings )
        {
            uiCharts_Crv.AntiAliasing = settings.Common.AntiAliasing;
            uiCharts_Crv.SuppressExceptions = settings.Common.SuppressExceptions;
            uiCharts_Crv.BackColor = settings.Common.BackColor;
            uiCharts_Crv.ChartAreas[0].Area3DStyle.Enable3D = settings.Areas.Common.Area3dStyle;
            uiCharts_Crv.ChartAreas[0].BackColor = settings.Areas.Common.BackColor;
            uiCharts_Crv.ChartAreas[0].AxisX.MajorGrid.Enabled = settings.Areas.X.MajorGrid.Enabled;
            uiCharts_Crv.ChartAreas[0].AxisX.MajorGrid.LineColor = settings.Areas.X.MajorGrid.LineColor;
            uiCharts_Crv.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = settings.Areas.X.MajorGrid.LineDashStyle;
            uiCharts_Crv.ChartAreas[0].AxisX.MajorGrid.LineWidth = settings.Areas.X.MajorGrid.LineWidth;
        }

    }

}
