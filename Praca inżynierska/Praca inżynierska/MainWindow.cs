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
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
        private UiSettings Settings { get; set; }
        private CurvesDataManager DataChart { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Settings = new UiSettings();
            DataChart = new CurvesDataManager();
        }

        private void UpdateUiBySettings()
        {
            // Form
            Width = Settings.MainWindow.Dimensions.Width;
            Height = Settings.MainWindow.Dimensions.Height;

            // Ui
            ChartAssist.SetDefaultSettings( uiCharts_Crv );
            UiControls.TrySetSelectedIndex( uiPnlGen_MeanT_ComBx, (int) MeanType.NN );
            UpdateUiByCurveScaffold();
        }

        private void UpdateUiByCurveScaffold()
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

        private void SavePresets()
        {
            Settings.Presets.Ui.CurvesNo = UiControls.TryGetValue<int>( uiPnlGen_Crvs1No_Num );
            Settings.Presets.Ui.StartX = UiControls.TryGetValue<double>( uiPnlGen_StartX_Num );
            Settings.Presets.Ui.EndX = UiControls.TryGetValue<double>( uiPnlGen_EndX_Num );
            Settings.Presets.Ui.PointsNo = UiControls.TryGetValue<int>( uiPnlGen_Dens_Num );
        }

        private void UpdateUiByCurveControlsRanges()
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
            UpdateUiByCurveScaffold();
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
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    MainChartSettings.Set( dialog.Settings, Settings.Series, uiCharts_Crv );
                    UpdateUiByShowingCurveOnChart( (DataSetCurveType) UiControls.TryGetSelectedIndex( uiPnlDtSh_CrvT_ComBx ) );
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

            int curvesNo = UiControls.TryGetValue<int>( uiPnlDtSh_CrvNo_Num );
            double surrounding = UiControls.TryGetValue<double>( uiPnlDtSh_Surr_Num );
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

            UiControls.TrySetSelectedIndex( uiPnlDtSh_CrvT_ComBx, (int) DataSetCurveType.Modified );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified );
        }

        private void OnApplyClick( object sender, EventArgs e )
        {
            if ( DataChart.IdealCurve.Points.Count == 0 ) {
                log.Info( MethodBase.GetCurrentMethod().Name + '(' + nameof( DataChart.IdealCurve ) + ')' );
                AppMessages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            MeanType meanType = (MeanType) UiControls.TryGetSelectedIndex( uiPnlGen_MeanT_ComBx );
            int curvesNo = UiControls.TryGetValue<int>( uiPnlGen_Crvs2No_Num );
            bool? result = DataChart.TryMakeAverageCurve( meanType, curvesNo );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + meanType + ',' + curvesNo + ',' + result.HasValue + ')' );

            if ( !result.Value ) {
                DataChart.RemoveInvalidPoints( DataSetCurveType.Average );
                AppMessages.MainWindow.ExclamationOfPointsNotValidToChart();
            }

            UiControls.TrySetSelectedIndex( uiPnlDtSh_CrvT_ComBx, (int) DataSetCurveType.Average );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Average );
            double standardDeviation = Mathematics.GetRelativeStandardDeviation( SeriesAssist.GetValues( DataChart.AverageCurve ), SeriesAssist.GetValues( DataChart.IdealCurve ) );
            uiPnlGen_StdDev2_TxtBx.Text = Strings.TryFormatAsNumeric( 8, standardDeviation );
        }

        private void OnShowDataSetClick( object sender, EventArgs e )
        {
            DataSetCurveType curveType = (DataSetCurveType) UiControls.TryGetSelectedIndex( uiPnlDtSh_CrvT_ComBx );
            int curveIndex = UiControls.TryGetValue<int>( uiPnlDtSh_CrvIdx_Num );
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

            using ( var gprvDialog = new GridPreviewer( SeriesAssist.GetCopy( curveSeries ) ) ) {
                UiControls.TryShowDialog( gprvDialog, this );

                try {
                    if ( gprvDialog.DialogResult == DialogResult.OK ) {
                        DataChart.AlterCurve( gprvDialog.Curve, curveType, curveIndex );
                        ChartAssist.Refresh( gprvDialog.Curve, Color.Indigo, uiCharts_Crv );
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
            DataSetCurveType curveType = (DataSetCurveType) UiControls.TryGetSelectedIndex( uiPnlDtSh_CrvT_ComBx );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + curveType + ')' );

            switch ( curveType ) {
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
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + curveIndex + ')' );
        }

        private void OnCurveIndexScroll( object sender, EventArgs e )
        {
            int curveIndex = UiControls.TryGetValue<int>( uiPnlDtSh_CrvIdx_TrBr );
            UiControls.TrySetValue( uiPnlDtSh_CrvIdx_Num, curveIndex );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified, curveIndex );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + curveIndex + ')' );
        }

        private void OnGenerateSetClick( object sender, EventArgs e )
        {
            if ( uiPnlGen_Def_Btn.Text == new MainWindowStrings().Ui.Panel.GenerateScaffoldNone.GetString() ) {
                log.Info( MethodBase.GetCurrentMethod().Name + '(' + uiPnlGen_Def_Btn.Text + ')' );
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
            UiControls.TrySetSelectedIndex( uiPnlDtSh_CrvT_ComBx, (int) DataSetCurveType.Modified );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + Settings.Presets.Ui.ToString() + ')' );
        }

        private void OnDefineClick( object sender, EventArgs e )
        {
            string signature = MethodBase.GetCurrentMethod().Name + "()";

            using ( var pcdDialog = new PatternCurveDefiner( Settings.Presets.Pcd ) ) {
                UiControls.TryShowDialog( pcdDialog, this );

                try {
                    if ( pcdDialog.DialogResult == DialogResult.OK ) {
                        Settings.Presets.Pcd = pcdDialog.Settings;
                        UpdateUiByCurveScaffold();
                    }
                }
                catch ( Exception ex ) {
                    log.Fatal( signature, ex );
                }
            }

            log.Info( signature );
        }

        #endregion
    }

}
