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

namespace PI.src.windows
{
    public partial class MainWindow : Form
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
        private static readonly About aboutDialog = new About();
        private UiSettings Settings { get; set; }
        private CurvesDataManager DataChart { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Settings = new UiSettings();
            aboutDialog.Height = Settings.Dimensions.About.Height;
            aboutDialog.Width = Settings.Dimensions.About.Width;
            DataChart = new CurvesDataManager();
        }

        private void UpdateUiBySettings()
        {
            // Form
            Width = Settings.Dimensions.MainWindow.Width;
            Height = Settings.Dimensions.MainWindow.Height;

            // Ui
            ChartAssist.SetDefaultSettings( uiCharts_Crv );
            UiControls.TrySetSelectedIndex( uiPnlAvg_MeanT_ComBx, (int) MeanType.NN );
            UpdateUiByCurveScaffold();
        }

        private void UpdateUiByCurveScaffold()
        {
            MainWindowStrings names = new MainWindowStrings();

            switch ( Settings.Presets.Pcd.Scaffold ) {
            case IdealCurveScaffold.Polynomial:
                uiPnlId_Def_Btn.Text = names.Ui.Panel.IdealScaffoldPolynomial.GetString();
                break;
            case IdealCurveScaffold.Hyperbolic:
                uiPnlId_Def_Btn.Text = names.Ui.Panel.IdealScaffoldHyperbolic.GetString();
                break;
            case IdealCurveScaffold.WaveformSine:
            case IdealCurveScaffold.WaveformSquare:
            case IdealCurveScaffold.WaveformTriangle:
            case IdealCurveScaffold.WaveformSawtooth:
                uiPnlId_Def_Btn.Text = names.Ui.Panel.IdealScaffoldWaveform.GetString();
                break;
            default:
                uiPnlId_Def_Btn.Text = names.Ui.Panel.IdealScaffoldNone.GetString();
                break;
            }
        }

        private void SavePresets()
        {
            Settings.Presets.Ui.CurvesNo = UiControls.TryGetValue<int>( uiPnlId_Crvs1No_Num );
            Settings.Presets.Ui.StartX = UiControls.TryGetValue<double>( uiPnlId_StartX_Num );
            Settings.Presets.Ui.EndX = UiControls.TryGetValue<double>( uiPnlId_EndX_Num );
            Settings.Presets.Ui.PointsNo = UiControls.TryGetValue<int>( uiPnlId_Dens_Num );
        }

        private void UpdateUiByCurveControlsRanges()
        {
            uiPnlAvg_Crvs2No_Num.Minimum = 1;
            uiPnlAvg_Crvs2No_Num.Maximum = Settings.Presets.Ui.CurvesNo;
            uiPnlAvg_Crvs2No_Num.Value = uiPnlAvg_Crvs2No_Num.Maximum;
            uiPnlMod_CrvIdx_Num.Minimum = 1;
            uiPnlMod_CrvIdx_Num.Maximum = Settings.Presets.Ui.CurvesNo;
            uiPnlMod_CrvIdx_Num.Value = uiPnlMod_CrvIdx_Num.Minimum;
            uiPnlMod_CrvIdx_TrBr.Minimum = 1;
            uiPnlMod_CrvIdx_TrBr.Maximum = Settings.Presets.Ui.CurvesNo;
            uiPnlMod_CrvIdx_TrBr.Value = uiPnlMod_CrvIdx_TrBr.Minimum;
            uiPnlMod_CrvNo_Num.Minimum = 1;
            uiPnlMod_CrvNo_Num.Maximum = Settings.Presets.Ui.CurvesNo;
            uiPnlMod_CrvNo_Num.Value = uiPnlMod_CrvNo_Num.Maximum;
        }

        private void UpdateUiByShowingCurveOnChart( DataSetCurveType curveType, int indexOfModifiedCurve = 1 )
        {
            const int CHART_AREA_INDEX = 0;
            const int SERIES_INDEX = 0;

            try {
                Series selectedSeries = null;

                switch ( curveType ) {
                case DataSetCurveType.Ideal:
                    CurvesSettings.Set( uiCharts_Crv, Settings.Series.Ideal, SERIES_INDEX );
                    selectedSeries = DataChart.IdealCurve;
                    break;
                case DataSetCurveType.Modified:
                    CurvesSettings.Set( uiCharts_Crv, Settings.Series.Modified, SERIES_INDEX );
                    selectedSeries = DataChart.ModifiedCurves[indexOfModifiedCurve - 1];
                    break;
                case DataSetCurveType.Average:
                    CurvesSettings.Set( uiCharts_Crv, Settings.Series.Average, SERIES_INDEX );
                    selectedSeries = DataChart.AverageCurve;
                    break;
                }

                ChartAssist.Refresh( selectedSeries, uiCharts_Crv.Series[0].Color, uiCharts_Crv, CHART_AREA_INDEX, SERIES_INDEX );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( ex.Message, ex );
                AppMessages.MainWindow.ErrorOfChartRefreshing();
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
                AppMessages.MainWindow.ExclamationOfSeriesSelection();
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }
        }

        private Series SpecifyCurveSeries( DataSetCurveType curveType, int curveIndex )
        {
            string signature = string.Empty;

            try {
                signature = MethodBase.GetCurrentMethod().Name + '(' + curveType + ',' + curveIndex + ')';

                switch ( curveType ) {
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

        private void ShowStatisticalAnalysis()
        {
            using ( var dialog = new StatisticalAnalysis( Settings.Presets ) ) {
                dialog.Width = Settings.Dimensions.StatisticalAnalysis.Width;
                dialog.Height = Settings.Dimensions.StatisticalAnalysis.Height;
                dialog.ShowDialog();
            }
        }

        private void LocalizeWindow()
        {
            MainWindowStrings names = new MainWindowStrings();
            int selectedIndex = 0;
            Text = names.Form.Text.GetString();

            // Menu: Program
            uiMenu_Prg.Text = names.Ui.Menu.ProgramTitle.GetString();
            uiMenuPrg_StatAnal.Text = names.Ui.Menu.ProgramStatisticalAnalysis.GetString();
            uiMenuPrg_Lang.Text = names.Ui.Menu.ProgramSelectLanguage.GetString();
            uiMenuPrg_About.Text = names.Ui.Menu.ProgramAbout.GetString();
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

            // Tab: Ideal
            uiPnlId_TbPg.Text = names.Ui.Panel.IdealTitle.GetString();
            uiPnlId_IdealCrvScaff_TxtBx.Text = names.Ui.Panel.IdealPatternCurveScaffold.GetString();
            uiPnlId_CrvScaff1_TxtBx.Text = names.Ui.Panel.IdealCurveScaffold1.GetString();
            uiPnlId_Def_Btn.Text = names.Ui.Panel.IdealScaffoldNone.GetString();
            UpdateUiByCurveScaffold();
            uiPnlId_CrvsSet_TxtBx.Text = names.Ui.Panel.IdealCurvesSet.GetString();
            uiPnlId_Crvs1No_TxtBx.Text = names.Ui.Panel.IdealCurves1No.GetString();
            uiPnlId_StartX_TxtBx.Text = names.Ui.Panel.IdealStartX.GetString();
            uiPnlId_EndX_TxtBx.Text = names.Ui.Panel.IdealEndX.GetString();
            uiPnlId_Dens_TxtBx.Text = names.Ui.Panel.IdealDensity.GetString();
            UiControls.TryRefreshOfProperty( uiPnlId_Crvs1No_Num );
            UiControls.TryRefreshOfProperty( uiPnlId_StartX_Num );
            UiControls.TryRefreshOfProperty( uiPnlId_EndX_Num );
            UiControls.TryRefreshOfProperty( uiPnlId_Dens_Num );
            uiPnlId_GenSet_Btn.Text = names.Ui.Panel.IdealGenerateSet.GetString();

            // Tab: Modified
            uiPnlMod_TbPg.Text = names.Ui.Panel.ModifiedTitle.GetString();
            uiPnlMod_DtSetCtrl_TxtBx.Text = names.Ui.Panel.ModifiedDataSetControl.GetString();
            uiPnlMod_CrvT_TxtBx.Text = names.Ui.Panel.ModifiedCurveType.GetString();
            selectedIndex = UiControls.TryGetSelectedIndex( uiPnlMod_CrvT_ComBx );
            EnumsLocalizer.Localize( LocalizableEnumerator.DataSetCurveType, uiPnlMod_CrvT_ComBx );
            UiControls.TrySetSelectedIndex( uiPnlMod_CrvT_ComBx, selectedIndex );
            uiPnlMod_CrvIdx_TxtBx.Text = names.Ui.Panel.ModifiedCurveIndex.GetString();

            if ( DataChart.IdealCurve.Points.Count != 0 ) {
                UiControls.TryRefreshOfProperty( uiPnlMod_CrvIdx_Num );
            }

            uiPnlMod_ShowDtSet_Btn.Text = names.Ui.Panel.ModifiedShowDataSet.GetString();
            uiPnlMod_GsNoise_TxtBx.Text = names.Ui.Panel.ModifiedGaussianNoise.GetString();
            uiPnlMod_CrvNo_TxtBx.Text = names.Ui.Panel.ModifiedCurvesNo.GetString();
            UiControls.TryRefreshOfProperty( uiPnlMod_CrvNo_Num );
            uiPnlMod_Surr_TxtBx.Text = names.Ui.Panel.ModifiedSurrounding.GetString();
            UiControls.TryRefreshOfProperty( uiPnlMod_Surr_Num );
            uiPnlMod_Malform_Btn.Text = names.Ui.Panel.ModifiedMalform.GetString();

            // Tab: Average
            uiPnlAvg_TbPg.Text = names.Ui.Panel.AverageTitle.GetString();
            uiPnlAvg_Avg_TxtBx.Text = names.Ui.Panel.AverageAveraging.GetString();
            uiPnlAvg_MeanT_TxtBx.Text = names.Ui.Panel.AverageMeanType.GetString();
            selectedIndex = UiControls.TryGetSelectedIndex( uiPnlAvg_MeanT_ComBx );
            EnumsLocalizer.Localize( LocalizableEnumerator.MeanType, uiPnlAvg_MeanT_ComBx );
            UiControls.TrySetSelectedIndex( uiPnlAvg_MeanT_ComBx, selectedIndex );
            uiPnlAvg_Crvs2No_TxtBx.Text = names.Ui.Panel.AverageCurves2No.GetString();
            UiControls.TryRefreshOfProperty( uiPnlAvg_Crvs2No_Num );
            uiPnlAvg_StdDev1_TxtBx.Text = names.Ui.Panel.AverageStandardDeviation1.GetString();
            uiPnlAvg_StdDev2_TxtBx.Text = double.NaN.ToString( Thread.CurrentThread.CurrentCulture );
            uiPnlAvg_Apply_Btn.Text = names.Ui.Panel.AverageApply.GetString();
        }

        #region Event handlers

        private void OnLoad( object sender, EventArgs e )
        {
            LanguageAssist.CurrentLanguage = Languages.English;
            LanguageAssist.TryLocalizeCulture( Languages.English );
            LocalizeWindow();
            UpdateUiBySettings();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ')' );
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            Settings = null;
            DataChart = null;
            Dispose();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ',' + e.CloseReason + ')' );
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
                signature = MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ')';

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

            log.Info( signature );
        }

        private void OnMenuExit( object sender, EventArgs e )
        {
            string signature = string.Empty;

            try {
                signature = MethodBase.GetCurrentMethod().Name + "()";
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

            log.Info( signature );
        }

        private void OnMenuSelectLanguage( object sender, EventArgs e )
        {
            using ( var dialog = new LanguageSelector() ) {
                dialog.Height = Settings.Dimensions.LanguageSelector.Height;
                dialog.Width = Settings.Dimensions.LanguageSelector.Width;
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    LanguageAssist.CurrentLanguage = dialog.GetSelectedLanguage();
                    LanguageAssist.TryLocalizeCulture( dialog.GetSelectedLanguage() );
                    LocalizeWindow();
                }
            }

            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        private void OnMenuStatisticalAnalysis( object sender, EventArgs e )
        {
            SavePresets();

            Thread window = new Thread( ShowStatisticalAnalysis ) {
                Name = nameof( StatisticalAnalysis ),
                IsBackground = true
            };

            bool result = Threads.TryStart( window );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + result + ',' + window.ThreadState + ')' );
        }

        private void OnMenuKeepProportions( object sender, EventArgs e )
        {
            Settings.MainWindow.Menu.Panel.KeepProportions = !Settings.MainWindow.Menu.Panel.KeepProportions;
            uiMenuPnl_KeepProp.Checked = Settings.MainWindow.Menu.Panel.KeepProportions;
            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        private void OnMenuPanelHide( object sender, EventArgs e )
        {
            Settings.MainWindow.Menu.Panel.Hide = !Settings.MainWindow.Menu.Panel.Hide;
            uiMenuPnl_Hide.Checked = Settings.MainWindow.Menu.Panel.Hide;
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + uiMenuPnl_Hide.Checked + ')' );

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
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + uiMenuPnl_Lock.Checked + ')' );
        }

        private void OnMenuMeansSettings( object sender, EventArgs e )
        {
            using ( var dialog = new MeansSettings() ) {
                dialog.MeansParams = DataChart.MeansParams;
                dialog.UpdateUiBySettings();
                dialog.Height = Settings.Dimensions.MeansSettings.Height;
                dialog.Width = Settings.Dimensions.MeansSettings.Width;
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    DataChart.MeansParams = dialog.MeansParams;
                }
            }

            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        private void OnMenuChartSettings( object sender, EventArgs e )
        {
            using ( var dialog = new ChartSettings( MainChartSettings.Get( Settings.Series, uiCharts_Crv ) ) ) {
                dialog.Height = Settings.Dimensions.ChartSettings.Height;
                dialog.Width = Settings.Dimensions.ChartSettings.Width;
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    MainChartSettings.Set( dialog.Settings, Settings.Series, uiCharts_Crv );
                    UpdateUiByShowingCurveOnChart( (DataSetCurveType) UiControls.TryGetSelectedIndex( uiPnlMod_CrvT_ComBx ) );
                }
            }

            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        private void OnMalformClick( object sender, EventArgs e )
        {
            if ( DataChart.IdealCurve.Points.Count == 0 ) {
                log.Info( MethodBase.GetCurrentMethod().Name + '(' + nameof( DataChart.IdealCurve ) + ')' );
                AppMessages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            int curvesNo = UiControls.TryGetValue<int>( uiPnlMod_CrvNo_Num );
            double surrounding = UiControls.TryGetValue<double>( uiPnlMod_Surr_Num );
            bool? result = DataChart.MakeNoiseOfGaussian( curvesNo, surrounding );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + curvesNo + ',' + surrounding + ',' + result.HasValue + ')' );

            if ( result == null ) {
                AppMessages.MainWindow.ExclamationOfSpecifiedCurveDoesNotExist();
                return;
            }

            if ( !result.Value ) {
                AppMessages.MainWindow.StopOfOperationMalformRejected();
                return;
            }

            UiControls.TrySetSelectedIndex( uiPnlMod_CrvT_ComBx, (int) DataSetCurveType.Modified );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified );
        }

        private void OnApplyClick( object sender, EventArgs e )
        {
            if ( DataChart.IdealCurve.Points.Count == 0 ) {
                log.Info( MethodBase.GetCurrentMethod().Name + '(' + nameof( DataChart.IdealCurve ) + ')' );
                AppMessages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            MeanType meanType = (MeanType) UiControls.TryGetSelectedIndex( uiPnlAvg_MeanT_ComBx );
            int curvesNo = UiControls.TryGetValue<int>( uiPnlAvg_Crvs2No_Num );
            bool? result = DataChart.TryMakeAverageCurve( meanType, curvesNo );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + meanType + ',' + curvesNo + ',' + result.HasValue + ')' );

            if ( !result.Value ) {
                DataChart.RemoveInvalidPoints( DataSetCurveType.Average );
                AppMessages.MainWindow.ExclamationOfPointsNotValidToChart();
            }

            UiControls.TrySetSelectedIndex( uiPnlMod_CrvT_ComBx, (int) DataSetCurveType.Average );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Average );
            double standardDeviation = Mathematics.GetRelativeStandardDeviation( SeriesAssist.GetValues( DataChart.AverageCurve ), SeriesAssist.GetValues( DataChart.IdealCurve ) );
            uiPnlAvg_StdDev2_TxtBx.Text = Strings.TryFormatAsNumeric( 8, standardDeviation );
        }

        private void OnShowDataSetClick( object sender, EventArgs e )
        {
            DataSetCurveType curveType = (DataSetCurveType) UiControls.TryGetSelectedIndex( uiPnlMod_CrvT_ComBx );
            int curveIndex = UiControls.TryGetValue<int>( uiPnlMod_CrvIdx_Num );
            string signature = MethodBase.GetCurrentMethod().Name + '(' + curveType + ',' + curveIndex + ')';

            switch ( curveType ) {
            case DataSetCurveType.Ideal:
            case DataSetCurveType.Modified:
            case DataSetCurveType.Average:
                break;
            default:
                log.Info( signature );
                AppMessages.MainWindow.AsteriskOfCurveTypeNotSelected();
                return;
            }

            Series curveSeries = SpecifyCurveSeries( curveType, curveIndex );

            if ( curveSeries == null || curveSeries.Points.Count == 0 ) {
                log.Info( MethodBase.GetCurrentMethod().Name + '(' + nameof( curveSeries ) + ')' );
                AppMessages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            using ( var dialog = new GridPreviewer( SeriesAssist.GetCopy( curveSeries ) ) ) {
                dialog.Height = Settings.Dimensions.GridPreviewer.Height;
                dialog.Width = Settings.Dimensions.GridPreviewer.Width;
                UiControls.TryShowDialog( dialog, this );

                try {
                    if ( dialog.DialogResult == DialogResult.OK ) {
                        DataChart.AlterCurve( dialog.Curve, curveType, curveIndex );
                        ChartAssist.Refresh( dialog.Curve, Color.Indigo, uiCharts_Crv );
                    }
                }
                catch ( InvalidOperationException ex ) {
                    log.Error( signature, ex );
                    AppMessages.MainWindow.ErrorOfChartRefreshing();
                }
                catch ( Exception ex ) {
                    log.Fatal( signature, ex );
                }
            }

            log.Info( signature );
        }

        private void OnCurveTypeSelection( object sender, EventArgs e )
        {
            DataSetCurveType curveType = (DataSetCurveType) UiControls.TryGetSelectedIndex( uiPnlMod_CrvT_ComBx );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + curveType + ')' );

            switch ( curveType ) {
            case DataSetCurveType.Modified:
                uiPnlMod_CrvIdx_Num.Enabled = true;
                uiPnlMod_CrvIdx_TrBr.Enabled = true;
                UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified, UiControls.TryGetValue<int>( uiPnlMod_CrvIdx_TrBr ) );
                break;
            case DataSetCurveType.Ideal:
                uiPnlMod_CrvIdx_Num.Enabled = false;
                uiPnlMod_CrvIdx_TrBr.Enabled = false;
                UpdateUiByShowingCurveOnChart( DataSetCurveType.Ideal );
                break;
            case DataSetCurveType.Average:
                uiPnlMod_CrvIdx_Num.Enabled = false;
                uiPnlMod_CrvIdx_TrBr.Enabled = false;
                UpdateUiByShowingCurveOnChart( DataSetCurveType.Average );
                break;
            }
        }

        private void OnCurveIndexAlteration( object sender, EventArgs e )
        {
            int curveIndex = UiControls.TryGetValue<int>( uiPnlMod_CrvIdx_Num );
            UiControls.TrySetValue( uiPnlMod_CrvIdx_TrBr, curveIndex );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified, curveIndex );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + curveIndex + ')' );
        }

        private void OnCurveIndexScroll( object sender, EventArgs e )
        {
            int curveIndex = UiControls.TryGetValue<int>( uiPnlMod_CrvIdx_TrBr );
            UiControls.TrySetValue( uiPnlMod_CrvIdx_Num, curveIndex );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified, curveIndex );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + curveIndex + ')' );
        }

        private void OnGenerateSetClick( object sender, EventArgs e )
        {
            if ( uiPnlId_Def_Btn.Text == new MainWindowStrings().Ui.Panel.IdealScaffoldNone.GetString() ) {
                log.Info( MethodBase.GetCurrentMethod().Name + '(' + uiPnlId_Def_Btn.Text + ')' );
                AppMessages.MainWindow.StopOfPatternCurveNotChosen();
                return;
            }

            SavePresets();

            bool result = DataChart.GenerateIdealCurve(
                Settings.Presets.Pcd.Scaffold,
                Settings.Presets.Pcd.Parameters,
                Settings.Presets.Ui.StartX,
                Settings.Presets.Ui.EndX,
                Settings.Presets.Ui.PointsNo
                );

            if ( !result ) {
                DataChart.RemoveInvalidPoints( DataSetCurveType.Ideal );
                AppMessages.MainWindow.ExclamationOfPointsNotValidToChart();
            }

            UpdateUiByShowingCurveOnChart( DataSetCurveType.Ideal );
            DataChart.PropagateIdealCurve( Settings.Presets.Ui.CurvesNo );
            UpdateUiByCurveControlsRanges();
            UiControls.TrySetSelectedIndex( uiPnlMod_CrvT_ComBx, (int) DataSetCurveType.Modified );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + Settings.Presets.Ui.ToString() + ')' );
        }

        private void OnDefineClick( object sender, EventArgs e )
        {
            string signature = MethodBase.GetCurrentMethod().Name + "()";

            using ( var dialog = new PatternCurveDefiner( Settings.Presets.Pcd ) ) {
                dialog.Width = Settings.Dimensions.PatternCurveDefiner.Width;
                dialog.Height = Settings.Dimensions.PatternCurveDefiner.Height;
                UiControls.TryShowDialog( dialog, this );

                try {
                    if ( dialog.DialogResult == DialogResult.OK ) {
                        Settings.Presets.Pcd = dialog.Settings;
                        UpdateUiByCurveScaffold();
                    }
                }
                catch ( Exception ex ) {
                    log.Fatal( signature, ex );
                }
            }

            log.Info( signature );
        }

        private void OnMenuAboutClick( object sender, EventArgs e )
        {
            UiControls.TryShowDialog( aboutDialog, this );
            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        #endregion
    }

}
