using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// BACKLOG
// TODO: General - Configuration file
// TODO: General - Localization
// TODO: Menu - 'Adjust chart' for visual effects manipulations
// TODO: Menu - Reading and saving set of curves from a file

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
                ChartData.RemoveInvalidPointsFromPatternCurveSet();
                MsgBxShower.Ui.PointsNotValidToChartProblem();
            }

            if ( ChartData.PatternCurveSet.Points.Count > 0 ) {
                UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType.Pattern );
            }
        }

        private void UpdateUiByShowingCurveOnChart( Enums.DataSetCurveType curveType, int indexOfGeneratedCurve = 1 )
        {
            try {
                uiCharts_PtrnCrv.Series.Clear();

                switch ( curveType ) {
                case Enums.DataSetCurveType.Pattern:
                    uiCharts_PtrnCrv.Series.Add( ChartData.PatternCurveSet );
                    uiCharts_PtrnCrv.Series[0].Color = System.Drawing.Color.Black;
                    break;
                case Enums.DataSetCurveType.Generated:
                    uiCharts_PtrnCrv.Series.Add( ChartData.GeneratedCurvesSet[indexOfGeneratedCurve - 1] );
                    uiCharts_PtrnCrv.Series[0].Color = System.Drawing.Color.Crimson;
                    break;
                case Enums.DataSetCurveType.Average:
                    uiCharts_PtrnCrv.Series.Add( ChartData.AverageCurveSet );
                    uiCharts_PtrnCrv.Series[0].Color = System.Drawing.Color.ForestGreen;
                    break;
                }

                uiCharts_PtrnCrv.Series[0].BorderWidth = 3;
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

            ShowMeansWarnings( meanType );
            ChartData.MakeAverageCurveFromGeneratedCurves( meanType, numberOfCurves );
            WinFormsHelper.SetSelectedIndexSafe( uiPnlDtSh_CrvT_ComBx, (int) Enums.DataSetCurveType.Average );
        }

        private void UpdateUiByDefaultSettings()
        {
            CurvesDataManager.SetDefaultProperties( uiCharts_PtrnCrv );
            WinFormsHelper.SetSelectedIndexSafe( uiPnlGen_MeanT_ComBx, (int) Enums.MeanType.Geometric );
        }

        private void UiMainWindow_Resize( object sender, EventArgs e )
        {
            if ( Settings.Menu.Program.KeepPanelProportions ) {
                uiMw_SpCtn.SplitterDistance = 275;
            }
        }

        private void UiMenuProgram_KeepPanelProportions_Click( object sender, EventArgs e )
        {
            Settings.Menu.Program.KeepPanelProportions = !Settings.Menu.Program.KeepPanelProportions;
        }

        private void ShowMeansWarnings( Enums.MeanType mean )
        {
            switch ( mean ) {
            case Enums.MeanType.Geometric:
                ShowGeometricMeanWarningIfNeeded();
                break;
            }
        }

        private void ShowGeometricMeanWarningIfNeeded()
        {
            if ( Settings.Panel.Generate.GeometricMeanApplyingWarning ) {
                using ( var msgBox = new ExplMsgBox() ) {
                    msgBox.SetInfo1Text( Consts.Expl.Means.Geometric.MainTxt );
                    msgBox.SetInfo2Text1( Consts.Expl.Means.Geometric.AuxTxt1 );
                    msgBox.SetInfo2Text2( Consts.Expl.Means.Geometric.AuxTxt2 );
                    msgBox.SetTitleBarText( Consts.Expl.Means.Geometric.TitleBarTxt );
                    msgBox.SetInfo2Image1( Properties.Resources.GeometricMean_OriginEquation );
                    msgBox.SetInfo2Image2( Properties.Resources.GeometricMean_ModifiedEquation );
                    WinFormsHelper.ShowDialogSafe( msgBox, this );

                    if ( msgBox.DialogResult == DialogResult.OK && msgBox.IsChecked() ) {
                        Settings.Panel.Generate.GeometricMeanApplyingWarning = false;
                    }
                }
            }
        }

    }

}
