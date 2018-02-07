using PI.src.windows;
using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PI.src.helpers;
using PI.src.messages;
using PI.src.settings;
using PI.src.general;
using log4net;
using System.Reflection;
using PI.src.enumerators;
using PI.src.localization.enums;
using PI.src.localization.windows;
using PI.src.localization.general;
using System.Drawing;

namespace PI
{
    public partial class MainWindow : Form
    {
        private static readonly MethodBase @base = MethodBase.GetCurrentMethod();
        private static readonly ILog log = LogManager.GetLogger( @base.DeclaringType );

        private UiSettings Settings { get; set; }
        private CurvesDataManager DataChart { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Settings = new UiSettings();
            DataChart = new CurvesDataManager();
            UpdateUiBySettings();
        }

        private void UpdateUiBySettings()
        {
            // Ui
            Width = Settings.MainWindow.Dimensions.Width;
            Height = Settings.MainWindow.Dimensions.Height;
        }

        private void CopyDialogPropertiesIntoPreSetsArea( PatternCurveDefiner pcdDialog )
        {
            Settings.Presets.Pcd.Scaffold = pcdDialog.Settings.Scaffold;
            Settings.Presets.Pcd.Parameters = pcdDialog.Settings.Parameters;
        }

        private void UpdateUiByChosenScaffoldStatus()
        {
            MainWindowStrings names = new MainWindowStrings();

            switch ( Settings.Presets.Pcd.Scaffold ) {
            case IdealCurveScaffold.Polynomial:
                uiPnlGen_Def_Btn.Text = names.Ui.Panel.GenerateScaffoldPolynomial.GetString();
                break;
            case IdealCurveScaffold.Hyperbolic:
                uiPnlGen_Def_Btn.Text = names.Ui.Panel.GenerateScaffoldHyperbolic.GetString();
                break;
            case IdealCurveScaffold.WaveformSine:
            case IdealCurveScaffold.WaveformSquare:
            case IdealCurveScaffold.WaveformTriangle:
            case IdealCurveScaffold.WaveformSawtooth:
                uiPnlGen_Def_Btn.Text = names.Ui.Panel.GenerateScaffoldWaveform.GetString();
                break;
            default:
                uiPnlGen_Def_Btn.Text = names.Ui.Panel.GenerateScaffoldNone.GetString();
                break;
            }
        }

        private void GrabPreSetsForCurvesGeneration()
        {
            Settings.Presets.Ui.CurvesNo = UiControls.TryGetValue<int>( uiPnlGen_Crvs1No_Num );
            Settings.Presets.Ui.StartX = UiControls.TryGetValue<double>( uiPnlGen_StartX_Num );
            Settings.Presets.Ui.EndX = UiControls.TryGetValue<double>( uiPnlGen_EndX_Num );
            Settings.Presets.Ui.PointsNo = UiControls.TryGetValue<int>( uiPnlGen_Dens_Num );
        }

        private bool GenerateAndShowPatternCurve()
        {
            double xStart = Settings.Presets.Ui.StartX;
            double xEnd = Settings.Presets.Ui.EndX;
            int density = Settings.Presets.Ui.PointsNo;

            if ( !DataChart.GenerateIdealCurve( Settings.Presets.Pcd.Scaffold, Settings.Presets.Pcd.Parameters, xStart, xEnd, density ) ) {
                DataChart.RemoveInvalidPoints( DataSetCurveType.Ideal );
                AppMessages.MainWindow.ExclamationOfPointsNotValidToChart();
            }

            if ( DataChart.IdealCurve.Points.Count > 0 ) {
                return UpdateUiByShowingCurveOnChart( DataSetCurveType.Ideal );
            }

            return false;
        }

        private bool UpdateUiByShowingCurveOnChart( DataSetCurveType curveType, int indexOfModifiedCurve = 1 )
        {
            try {
                Series selectedSeries = null;

                switch ( curveType ) {
                case DataSetCurveType.Ideal:
                    uiCharts_Crv.Series[0].Color = Settings.Series.Ideal.Color;
                    uiCharts_Crv.Series[0].BorderWidth = Settings.Series.Ideal.BorderWidth;
                    uiCharts_Crv.Series[0].BorderDashStyle = Settings.Series.Ideal.BorderDashStyle;
                    uiCharts_Crv.Series[0].ChartType = Settings.Series.Ideal.ChartType;
                    selectedSeries = DataChart.IdealCurve;
                    break;
                case DataSetCurveType.Modified:
                    uiCharts_Crv.Series[0].Color = Settings.Series.Modified.Color;
                    uiCharts_Crv.Series[0].BorderWidth = Settings.Series.Modified.BorderWidth;
                    uiCharts_Crv.Series[0].BorderDashStyle = Settings.Series.Modified.BorderDashStyle;
                    uiCharts_Crv.Series[0].ChartType = Settings.Series.Modified.ChartType;
                    selectedSeries = DataChart.ModifiedCurves[indexOfModifiedCurve - 1];
                    break;
                case DataSetCurveType.Average:
                    uiCharts_Crv.Series[0].Color = Settings.Series.Average.Color;
                    uiCharts_Crv.Series[0].BorderWidth = Settings.Series.Average.BorderWidth;
                    uiCharts_Crv.Series[0].BorderDashStyle = Settings.Series.Average.BorderDashStyle;
                    uiCharts_Crv.Series[0].ChartType = Settings.Series.Average.ChartType;
                    selectedSeries = DataChart.AverageCurve;
                    break;
                }

                ChartAssist.Refresh( selectedSeries, uiCharts_Crv.Series[0].Color, uiCharts_Crv );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( ex.Message, ex );
                AppMessages.MainWindow.ErrorOfChartRefreshing();
                return false;
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
                AppMessages.MainWindow.ExclamationOfSeriesSelection();
                return false;
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
                return false;
            }

            return true;
        }

        private void UpdateUiBySettingRangesForCurvesNumber()
        {
            uiPnlGen_Crvs2No_Num.Minimum = 1;
            uiPnlGen_Crvs2No_Num.Maximum = Settings.Presets.Ui.CurvesNo;
            uiPnlGen_Crvs2No_Num.Value = uiPnlGen_Crvs2No_Num.Maximum;
            uiPnlDtSh_CrvIdx_Num.Minimum = 1;
            uiPnlDtSh_CrvIdx_Num.Maximum = Settings.Presets.Ui.CurvesNo;
            uiPnlDtSh_CrvIdx_Num.Value = uiPnlDtSh_CrvIdx_Num.Minimum;
            uiPnlDtSh_CrvIdx_TrBr.Minimum = 1;
            uiPnlDtSh_CrvIdx_TrBr.Maximum = Settings.Presets.Ui.CurvesNo;
            uiPnlDtSh_CrvIdx_TrBr.Value = uiPnlDtSh_CrvIdx_TrBr.Minimum;
            uiPnlDtSh_CrvNo_Num.Minimum = 1;
            uiPnlDtSh_CrvNo_Num.Maximum = Settings.Presets.Ui.CurvesNo;
            uiPnlDtSh_CrvNo_Num.Value = uiPnlDtSh_CrvNo_Num.Maximum;
        }

        private void UpdateUiByDotNetFrameworkVersion()
        {
            string dotNetVersion = SystemInfo.TryGetDotNetFrameworkVersion();

            if ( dotNetVersion == null ) {
                uiPnlPrg_DotNetFr2_TxtBx.Text = new MainWindowStrings().Ui.Panel.ProgramDotNetFramework1.GetString();
                return;
            }

            uiPnlPrg_DotNetFr2_TxtBx.Text = dotNetVersion;

        }

        private void UpdateUiByOsVersionName()
        {
            string osVersion = SystemInfo.TryGetOSVersion();

            if ( osVersion == null ) {
                uiPnlPrg_OsVer2_TxtBx.Text = new MainWindowStrings().Ui.Panel.ProgramOsVersion1.GetString();
                return;
            }

            uiPnlPrg_OsVer2_TxtBx.Text = osVersion;
        }

        private Series SpecifyCurveSeries( int curveType, int curveIndex )
        {
            string signature = string.Empty;

            try {
                signature = @base.DeclaringType.Name + "." + @base.Name + "(" + curveType + ", " + curveIndex + ")";

                switch ( (DataSetCurveType) curveType ) {
                case DataSetCurveType.Ideal:
                    return DataChart.IdealCurve;
                case DataSetCurveType.Modified:
                    return DataChart.ModifiedCurves[curveIndex - 1];
                case DataSetCurveType.Average:
                    return DataChart.AverageCurve;
                }
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }

            return null;
        }

        private void UpdateUiByDefaultSettings()
        {
            ChartAssist.SetDefaultSettings( uiCharts_Crv );
            UiControls.TrySetSelectedIndex( uiPnlGen_MeanT_ComBx, (int) MeanType.NN );
            UpdateUiByChosenScaffoldStatus();
        }

        private void UpdateUiByInvalidatingChartSettings()
        {
            switch ( (DataSetCurveType) UiControls.TryGetSelectedIndex( uiPnlDtSh_CrvT_ComBx ) ) {
            case DataSetCurveType.Ideal:
                UpdateUiByShowingCurveOnChart( DataSetCurveType.Ideal );
                break;
            case DataSetCurveType.Modified:
                UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified );
                break;
            case DataSetCurveType.Average:
                UpdateUiByShowingCurveOnChart( DataSetCurveType.Average );
                break;
            }
        }

        private void DelegatorForStatAnalysis()
        {
            using ( var dialog = new StatisticalAnalysis( Settings.Presets ) ) {
                dialog.ShowDialog();
            }
        }

        private void LocalizeWindow()
        {
            MainWindowStrings names = new MainWindowStrings();
            Text = names.Form.Text.GetString();

            // Menu: Program
            uiMenu_Prg.Text = names.Ui.Menu.ProgramTitle.GetString();
            uiMenuPrg_StatAnal.Text = names.Ui.Menu.ProgramStatisticalAnalysis.GetString();
            uiMenuPrg_Lang.Text = names.Ui.Menu.ProgramSelectLanguage.GetString();
            uiMenuPrg_Exit.Text = names.Ui.Menu.ProgramExit.GetString();

            // Menu: Panel
            uiMenu_Pnl.Text = names.Ui.Menu.PanelTitle.GetString();
            uiMenuPnl_KeepProp.Text = names.Ui.Menu.PanelKeepProportions.GetString();
            uiMenuPnl_Hide.Text = names.Ui.Menu.PanelHide.GetString();
            uiMenuPnl_Lock.Text = names.Ui.Menu.PanelLock.GetString();

            // Menu: Means
            uiMenu_Means.Text = names.Ui.Menu.MeansTitle.GetString();
            uiMenuMeans_Settings.Text = names.Ui.Menu.MeansSettings.GetString();

            // Menu: Chart
            uiMenu_Chart.Text = names.Ui.Menu.ChartTitle.GetString();
            uiMenuChart_Settings.Text = names.Ui.Menu.ChartSettings.GetString();

            // Tab: Generate
            uiPnlGen_TbPg.Text = names.Ui.Panel.GenerateTitle.GetString();
            uiPnlGen_IdealCrvScaff_TxtBx.Text = names.Ui.Panel.GeneratePatternCurveScaffold.GetString();
            uiPnlGen_CrvScaff1_TxtBx.Text = names.Ui.Panel.GenerateCurveScaffold1.GetString();
            uiPnlGen_Def_Btn.Text = names.Ui.Panel.GenerateScaffoldNone.GetString();
            UpdateUiByChosenScaffoldStatus();
            uiPnlGen_CrvsSet_TxtBx.Text = names.Ui.Panel.GenerateCurvesSet.GetString();
            uiPnlGen_Crvs1No_TxtBx.Text = names.Ui.Panel.GenerateCurves1No.GetString();
            uiPnlGen_StartX_TxtBx.Text = names.Ui.Panel.GenerateStartX.GetString();
            uiPnlGen_EndX_TxtBx.Text = names.Ui.Panel.GenerateEndX.GetString();
            uiPnlGen_Dens_TxtBx.Text = names.Ui.Panel.GenerateDensity.GetString();
            UiControls.TryRefreshOfProperty( uiPnlGen_Crvs1No_Num );
            UiControls.TryRefreshOfProperty( uiPnlGen_StartX_Num );
            UiControls.TryRefreshOfProperty( uiPnlGen_EndX_Num );
            UiControls.TryRefreshOfProperty( uiPnlGen_Dens_Num );
            uiPnlGen_GenSet_Btn.Text = names.Ui.Panel.GenerateGenerateSet.GetString();
            uiPnlGen_Avg_TxtBx.Text = names.Ui.Panel.GenerateAveraging.GetString();
            uiPnlGen_MeanT_TxtBx.Text = names.Ui.Panel.GenerateMeanType.GetString();
            uiPnlGen_Crvs2No_TxtBx.Text = names.Ui.Panel.GenerateCurves2No.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.MeanType, uiPnlGen_MeanT_ComBx );
            UiControls.TryRefreshOfProperty( uiPnlGen_Crvs2No_Num );
            uiPnlGen_StdDev1_TxtBx.Text = names.Ui.Panel.GenerateStandardDeviation1.GetString();
            uiPnlGen_Apply_Btn.Text = names.Ui.Panel.GenerateApply.GetString();

            // Tab: Datasheet
            uiPnlDtSh_TbPg.Text = names.Ui.Panel.DatasheetTitle.GetString();
            uiPnlDtSh_DtSetCtrl_TxtBx.Text = names.Ui.Panel.DatasheetDataSetControl.GetString();
            uiPnlDtSh_CrvT_TxtBx.Text = names.Ui.Panel.DatasheetCurveType.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.DataSetCurveType, uiPnlDtSh_CrvT_ComBx );
            uiPnlDtSh_CrvIdx_TxtBx.Text = names.Ui.Panel.DatasheetCurveIndex.GetString();

            if ( DataChart.IdealCurve.Points.Count != 0 ) {
                UiControls.TryRefreshOfProperty( uiPnlDtSh_CrvIdx_Num );
            }

            uiPnlDtSh_ShowDtSet_Btn.Text = names.Ui.Panel.DatasheetShowDataSet.GetString();
            uiPnlDtSh_GsNoise_TxtBx.Text = names.Ui.Panel.DatasheetGaussianNoise.GetString();
            uiPnlDtSh_CrvNo_TxtBx.Text = names.Ui.Panel.DatasheetCurvesNo.GetString();
            UiControls.TryRefreshOfProperty( uiPnlDtSh_CrvNo_Num );
            uiPnlDtSh_Surr_TxtBx.Text = names.Ui.Panel.DatasheetSurrounding.GetString();
            UiControls.TryRefreshOfProperty( uiPnlDtSh_Surr_Num );
            uiPnlDtSh_Malform_Btn.Text = names.Ui.Panel.DatasheetMalform.GetString();

            // Tab: Program
            uiPnlPrg_TbPg.Text = names.Ui.Panel.ProgramTitle.GetString();
            uiPnlPrg_Info_TxtBx.Text = names.Ui.Panel.ProgramInformations.GetString();
            uiPnlPrg_DotNetFr1_TxtBx.Text = names.Ui.Panel.ProgramDotNetFramework1.GetString();
            uiPnlPrg_OsVer1_TxtBx.Text = names.Ui.Panel.ProgramOsVersion1.GetString();
        }

        #region Event handlers

        private void OnLoad( object sender, EventArgs e )
        {
            UpdateUiByDotNetFrameworkVersion();
            UpdateUiByOsVersionName();
            LanguageAssist.CurrentLanguage = Languages.English;
            LanguageAssist.TryLocalizeCulture( Languages.English );
            LocalizeWindow();
            UpdateUiByDefaultSettings();
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            Settings = null;
            DataChart = null;
            Dispose();
        }

        private void OnResize( object sender, EventArgs e )
        {
            if ( Settings.MainWindow.Menu.Panel.Hide ) {
                return;
            }

            if ( WindowState == FormWindowState.Minimized ) {
                return;
            }

            string signature = string.Empty;

            try {
                signature = @base.DeclaringType.Name + "." + @base.Name + "(" + sender + ", " + e + ")";

                if ( Settings.MainWindow.Menu.Panel.KeepProportions ) {
                    uiMw_SpCtn.SplitterDistance = Settings.MainWindow.Menu.Panel.SplitterDistance;
                }
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( signature, ex );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }
        }

        private void OnMenuExit( object sender, EventArgs e )
        {
            string signature = string.Empty;

            try {
                signature = @base.DeclaringType.Name + "." + @base.Name + "(" + sender + ", " + e + ")";
                Close();
                Application.Exit();
            }
            catch ( ObjectDisposedException ex ) {
                log.Error( signature, ex );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }
        }

        private void OnMenuSelectLanguage( object sender, EventArgs e )
        {
            using ( var dialog = new LanguageSelector() ) {
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    LanguageAssist.CurrentLanguage = dialog.GetSelectedLanguage();
                    LanguageAssist.TryLocalizeCulture( dialog.GetSelectedLanguage() );
                    LocalizeWindow();
                }
            }
        }

        private void OnMenuStatisticalAnalysis( object sender, EventArgs e )
        {
            string signature = string.Empty;

            try {
                signature = @base.DeclaringType.Name + "." + @base.Name + "(" + sender + ", " + e + ")";
                GrabPreSetsForCurvesGeneration();

                Thread window = new Thread( DelegatorForStatAnalysis ) {
                    Name = nameof( StatisticalAnalysis ),
                    IsBackground = true
                };

                window.Start();
            }
            catch ( ThreadStateException ex ) {
                log.Error( signature, ex );
            }
            catch ( OutOfMemoryException ex ) {
                log.Fatal( signature, ex );
                AppMessages.General.StopOfOutOfMemoryException();
            }
            catch ( ArgumentNullException ex ) {
                log.Error( signature, ex );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }
        }

        private void OnMenuKeepProportions( object sender, EventArgs e )
        {
            Settings.MainWindow.Menu.Panel.KeepProportions = !Settings.MainWindow.Menu.Panel.KeepProportions;
            uiMenuPnl_KeepProp.Checked = Settings.MainWindow.Menu.Panel.KeepProportions;
        }

        private void OnMenuPanelHide( object sender, EventArgs e )
        {
            Settings.MainWindow.Menu.Panel.Hide = !Settings.MainWindow.Menu.Panel.Hide;
            uiMenuPnl_Hide.Checked = Settings.MainWindow.Menu.Panel.Hide;

            if ( uiMenuPnl_Hide.Checked ) {
                Settings.MainWindow.Menu.Panel.SplitterDistance = uiMw_SpCtn.SplitterDistance;
                uiMw_SpCtn.SplitterDistance = 0;
                uiMw_SpCtn.Panel1.Hide();
                uiMw_SpCtn.Enabled = false;
                return;
            }

            uiMw_SpCtn.SplitterDistance = Settings.MainWindow.Menu.Panel.SplitterDistance;
            uiMw_SpCtn.Panel1.Show();
            uiMw_SpCtn.Enabled = true;
        }

        private void OnMenuPanelLock( object sender, EventArgs e )
        {
            Settings.MainWindow.Menu.Panel.Lock = !Settings.MainWindow.Menu.Panel.Lock;
            uiMenuPnl_Lock.Checked = Settings.MainWindow.Menu.Panel.Lock;
            uiMw_SpCtn.Panel1.Enabled = !Settings.MainWindow.Menu.Panel.Lock;
        }

        private void OnMenuMeansSettings( object sender, EventArgs e )
        {
            using ( var dialog = new MeansSettings() ) {
                dialog.MeansParams = DataChart.MeansParams;
                dialog.UpdateUiBySettings();
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    DataChart.MeansParams = dialog.MeansParams;
                }
            }
        }

        private void OnMenuChartSettings( object sender, EventArgs e )
        {
            using ( var dialog = new ChartSettings( MainChartSettings.Get( Settings.Series, uiCharts_Crv ) ) ) {
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    MainChartSettings.Set( dialog.Settings, Settings.Series, uiCharts_Crv );
                    UpdateUiByInvalidatingChartSettings();
                }
            }
        }

        private void OnMalformClick( object sender, EventArgs e )
        {
            if ( DataChart.IdealCurve.Points.Count == 0 ) {
                AppMessages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            int numberOfCurves = UiControls.TryGetValue<int>( uiPnlDtSh_CrvNo_Num );
            double surrounding = UiControls.TryGetValue<double>( uiPnlDtSh_Surr_Num );
            bool? result = DataChart.MakeNoiseOfGaussian( numberOfCurves, surrounding );

            if ( result == null ) {
                AppMessages.MainWindow.ExclamationOfSpecifiedCurveDoesNotExist();
                return;
            }

            if ( !result.Value ) {
                AppMessages.MainWindow.StopOfOperationMalformRejected();
                return;
            }

            UiControls.TrySetSelectedIndex( uiPnlDtSh_CrvT_ComBx, (int) DataSetCurveType.Modified );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified );
        }

        private void OnApplyClick( object sender, EventArgs e )
        {
            if ( DataChart.IdealCurve.Points.Count == 0 ) {
                AppMessages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            MeanType meanType = (MeanType) UiControls.TryGetSelectedIndex( uiPnlGen_MeanT_ComBx );
            int numberOfCurves = UiControls.TryGetValue<int>( uiPnlGen_Crvs2No_Num );
            bool? averageResult = DataChart.TryMakeAverageCurve( meanType, numberOfCurves );

            if ( !averageResult.Value ) {
                DataChart.RemoveInvalidPoints( DataSetCurveType.Average );
                AppMessages.MainWindow.ExclamationOfPointsNotValidToChart();
            }

            UiControls.TrySetSelectedIndex( uiPnlDtSh_CrvT_ComBx, (int) DataSetCurveType.Average );
            double standardDeviation = Mathematics.GetRelativeStandardDeviation( SeriesAssist.GetValues( DataChart.AverageCurve ), SeriesAssist.GetValues( DataChart.IdealCurve ) );
            uiPnlGen_StdDev2_TxtBx.Text = Strings.TryFormatAsNumeric( 8, standardDeviation );
        }

        private void OnShowDataSetClick( object sender, EventArgs e )
        {
            int selectedCurveType = UiControls.TryGetSelectedIndex( uiPnlDtSh_CrvT_ComBx );
            int selectedCurveIndex = UiControls.TryGetValue<int>( uiPnlDtSh_CrvIdx_Num );

            switch ( (DataSetCurveType) selectedCurveType ) {
            case DataSetCurveType.Ideal:
            case DataSetCurveType.Modified:
            case DataSetCurveType.Average:
                break;
            default:
                AppMessages.MainWindow.AsteriskOfCurveTypeNotSelected();
                return;
            }

            Series selectedCurveSeries = SpecifyCurveSeries( selectedCurveType, selectedCurveIndex );

            if ( selectedCurveSeries == null || selectedCurveSeries.Points.Count == 0 ) {
                AppMessages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            using ( var gprvDialog = new GridPreviewer( SeriesAssist.GetCopy( selectedCurveSeries ) ) ) {
                UiControls.TryShowDialog( gprvDialog, this );
                string signature = string.Empty;

                try {
                    signature = @base.DeclaringType.Name + "." + @base.Name + "(" + sender + ", " + e + ")";

                    if ( gprvDialog.DialogResult == DialogResult.OK ) {
                        DataChart.AlterCurve( gprvDialog.Curve, (DataSetCurveType) selectedCurveType, selectedCurveIndex );
                        ChartAssist.Refresh( gprvDialog.Curve, Color.Indigo, uiCharts_Crv );
                    }
                }
                catch ( InvalidOperationException ex ) {
                    log.Error( signature, ex );
                    AppMessages.MainWindow.ErrorOfChartRefreshing();
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException ex ) {
                    log.Error( signature, ex );
                }
                catch ( Exception ex ) {
                    log.Fatal( signature, ex );
                }
            }
        }

        private void OnCurveTypeSelection( object sender, EventArgs e )
        {
            switch ( (DataSetCurveType) UiControls.TryGetSelectedIndex( uiPnlDtSh_CrvT_ComBx ) ) {
            case DataSetCurveType.Modified:
                uiPnlDtSh_CrvIdx_Num.Enabled = true;
                uiPnlDtSh_CrvIdx_TrBr.Enabled = true;
                UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified, UiControls.TryGetValue<int>( uiPnlDtSh_CrvIdx_TrBr ) );
                break;
            case DataSetCurveType.Ideal:
                uiPnlDtSh_CrvIdx_Num.Enabled = false;
                uiPnlDtSh_CrvIdx_TrBr.Enabled = false;
                UpdateUiByShowingCurveOnChart( DataSetCurveType.Ideal );
                break;
            case DataSetCurveType.Average:
                uiPnlDtSh_CrvIdx_Num.Enabled = false;
                uiPnlDtSh_CrvIdx_TrBr.Enabled = false;
                UpdateUiByShowingCurveOnChart( DataSetCurveType.Average );
                break;
            }
        }

        private void OnCurveIndexAlteration( object sender, EventArgs e )
        {
            int curveIndex = UiControls.TryGetValue<int>( uiPnlDtSh_CrvIdx_Num );
            UiControls.TrySetValue( uiPnlDtSh_CrvIdx_TrBr, curveIndex );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified, curveIndex );
        }

        private void OnCurveIndexScroll( object sender, EventArgs e )
        {
            int curveIndex = UiControls.TryGetValue<int>( uiPnlDtSh_CrvIdx_TrBr );
            UiControls.TrySetValue( uiPnlDtSh_CrvIdx_Num, curveIndex );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified, curveIndex );
        }

        private void OnGenerateSetClick( object sender, EventArgs e )
        {
            if ( uiPnlGen_Def_Btn.Text == new MainWindowStrings().Ui.Panel.GenerateScaffoldNone.GetString() ) {
                AppMessages.MainWindow.StopOfPatternCurveNotChosen();
                return;
            }

            GrabPreSetsForCurvesGeneration();

            if ( !GenerateAndShowPatternCurve() ) {
                // TODO: show message - GREAT ISSUE (hyperbolic high)
                return;
            }

            DataChart.PropagateIdealCurve( Settings.Presets.Ui.CurvesNo );
            UpdateUiBySettingRangesForCurvesNumber();
            UiControls.TrySetSelectedIndex( uiPnlDtSh_CrvT_ComBx, (int) DataSetCurveType.Modified );
        }

        private void OnDefineClick( object sender, EventArgs e )
        {
            using ( var pcdDialog = new PatternCurveDefiner( Settings.Presets.Pcd ) ) {
                UiControls.TryShowDialog( pcdDialog, this );
                string signature = string.Empty;

                try {
                    signature = @base.DeclaringType.Name + "." + @base.Name + "(" + sender + ", " + e + ")";

                    if ( pcdDialog.DialogResult == DialogResult.OK ) {
                        CopyDialogPropertiesIntoPreSetsArea( pcdDialog );
                        UpdateUiByChosenScaffoldStatus();
                    }
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException ex ) {
                    log.Error( signature, ex );
                }
                catch ( Exception ex ) {
                    log.Fatal( signature, ex );
                }
            }
        }

        #endregion
    }

}
