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

namespace PI
{
    public partial class MainWindow : Form
    {
        private static readonly MethodBase @base = MethodBase.GetCurrentMethod();
        private static readonly ILog log = LogManager.GetLogger( @base.DeclaringType );

        private Thread Timer { get; set; }
        private UiSettings Settings { get; set; }
        private CurvesDataManager DataChart { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitalizeFields();
            SetWindowDefaultDimensions();
        }

        private void InitalizeFields()
        {
            Timer = null;
            Settings = new UiSettings();
            DataChart = new CurvesDataManager();
        }

        private void UiMainWindow_Load( object sender, EventArgs e )
        {
            UpdateUiByDotNetFrameworkVersion();
            UpdateUiByOsVersionName();
            uiPnlPrg_LogPath2_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.InfoObtErrTxt.GetString();
            UpdateUiByDefaultSettings();
            DefineTimerThread();
            Threads.TryStart( Timer );
            UpdateUiByStatusOfTimerThread();
            Translator.GetInstance();
            Translator.SetLanguage( Languages.English );
            LocalizeCulture( Languages.English );
        }

        private void UiMenuProgram_Exit_Click( object sender, EventArgs e )
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

        private void DefineTimerThread()
        {
            string signature = string.Empty;

            Timer = new Thread( () => {
                try {
                    signature = @base.DeclaringType.Name + "." + @base.Name + "()";
                    Thread.CurrentThread.IsBackground = true;
                    System.Timers.Timer timer = new System.Timers.Timer();
                    InstallEventForTimer( ref timer );
                    timer.Interval = 1000;
                    timer.Start();
                    timer.Enabled = true;
                }
                catch ( ThreadStateException ex ) {
                    log.Error( signature, ex );
                }
                catch ( ObjectDisposedException ex ) {
                    log.Error( signature, ex );
                }
                catch ( ArgumentOutOfRangeException ex ) {
                    log.Error( signature, ex );
                }
                catch ( ArgumentException ex ) {
                    log.Error( signature, ex );
                }
                catch ( Exception ex ) {
                    log.Fatal( signature, ex );
                }
            } );

            try {
                Timer.Name = nameof( Timer );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }
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
            };
        }

        private void UpdateUiByTimerCounts( string text )
        {
            string signature = string.Empty;

            try {
                signature = @base.DeclaringType.Name + "." + @base.Name + "()";

                BeginInvoke( (MethodInvoker) delegate {
                    uiPnlPrg_Cnts2_TxtBx.Text = text;
                    uiPnlPrg_Cnts2_TxtBx.Refresh();
                } );
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

        private void UpdateUiByStatusOfTimerThread()
        {
            if ( Timer == null ) {
                uiPnlPrg_ActState2_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.StateFail.GetString();
            }
            else {
                uiPnlPrg_ActState2_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.StateSucc.GetString();
            }
        }

        private void UiPanelGenerate_Define_Click( object sender, EventArgs e )
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

        private void CopyDialogPropertiesIntoPreSetsArea( PatternCurveDefiner pcdDialog )
        {
            Settings.Presets.Pcd.Scaffold = pcdDialog.Settings.Scaffold;
            Settings.Presets.Pcd.Parameters = pcdDialog.Settings.Parameters;
        }

        private void UpdateUiByChosenScaffoldStatus()
        {
            switch ( Settings.Presets.Pcd.Scaffold ) {
            case IdealCurveScaffold.Polynomial:
                uiPnlGen_Def_Btn.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.ScaffPoly.GetString();
                break;
            case IdealCurveScaffold.Hyperbolic:
                uiPnlGen_Def_Btn.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.ScaffHyp.GetString();
                break;
            case IdealCurveScaffold.WaveformSine:
            case IdealCurveScaffold.WaveformSquare:
            case IdealCurveScaffold.WaveformTriangle:
            case IdealCurveScaffold.WaveformSawtooth:
                uiPnlGen_Def_Btn.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.ScaffWave.GetString();
                break;
            default:
                uiPnlGen_Def_Btn.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.ScaffNone.GetString();
                break;
            }
        }

        private void UiPanelDataSheet_CurveType_SelectedIndexChanged( object sender, EventArgs e )
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

        private void UiPanelDataSheet_CurveIndex_NumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            int curveIndex = UiControls.TryGetValue<int>( uiPnlDtSh_CrvIdx_Num );
            UiControls.TrySetValue( uiPnlDtSh_CrvIdx_TrBr, curveIndex );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified, curveIndex );
        }

        private void UiPanelDataSheet_CurveIndex_TrackBar_Scroll( object sender, EventArgs e )
        {
            int curveIndex = UiControls.TryGetValue<int>( uiPnlDtSh_CrvIdx_TrBr );
            UiControls.TrySetValue( uiPnlDtSh_CrvIdx_Num, curveIndex );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified, curveIndex );
        }

        private void UiPanelGenerate_GenerateSet_Click( object sender, EventArgs e )
        {
            if ( uiPnlGen_Def_Btn.Text == Translator.GetInstance().Strings.MainWindow.Panel.Generate.ScaffNone.GetString() ) {
                Messages.MainWindow.StopOfPatternCurveNotChosen();
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
                Messages.MainWindow.ExclamationOfPointsNotValidToChart();
            }

            if ( DataChart.IdealCurve.Points.Count > 0 ) {
                return UpdateUiByShowingCurveOnChart( DataSetCurveType.Ideal );
            }

            return false;
        }

        private bool UpdateUiByShowingCurveOnChart( DataSetCurveType curveType, int indexOfGeneratedCurve = 1 )
        {
            try {
                uiCharts_Crv.Series.Clear();

                switch ( curveType ) {
                case DataSetCurveType.Ideal:
                    uiCharts_Crv.Series.Add( DataChart.IdealCurve );
                    SetPatternCurveSeriesSettings( uiCharts_Crv );
                    break;
                case DataSetCurveType.Modified:
                    uiCharts_Crv.Series.Add( DataChart.ModifiedCurves[indexOfGeneratedCurve - 1] );
                    SetGeneratedCurveSeriesSettings( uiCharts_Crv );
                    break;
                case DataSetCurveType.Average:
                    uiCharts_Crv.Series.Add( DataChart.AverageCurve );
                    SetAverageCurveSeriesSettings( uiCharts_Crv );
                    break;
                }

                uiCharts_Crv.ChartAreas[0].RecalculateAxesScale();
                uiCharts_Crv.Visible = true;
                uiCharts_Crv.Invalidate();
            }
            catch ( InvalidOperationException ex ) {
                log.Error( ex.Message, ex );
                Messages.MainWindow.ErrorOfChartRefreshing();
                return false;
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
                Messages.MainWindow.ExclamationOfSeriesSelection();
                return false;
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
                return false;
            }

            return true;
        }

        private void SetPatternCurveSeriesSettings( Chart chart, int seriesNo = 0 )
        {
            chart.Series[seriesNo].Color = Settings.Series.Ideal.Color;
            chart.Series[seriesNo].BorderWidth = Settings.Series.Ideal.BorderWidth;
            chart.Series[seriesNo].BorderDashStyle = Settings.Series.Ideal.BorderDashStyle;
            chart.Series[seriesNo].ChartType = Settings.Series.Ideal.ChartType;
        }

        private void SetGeneratedCurveSeriesSettings( Chart chart, int seriesNo = 0 )
        {
            chart.Series[seriesNo].Color = Settings.Series.Modified.Color;
            chart.Series[seriesNo].BorderWidth = Settings.Series.Modified.BorderWidth;
            chart.Series[seriesNo].BorderDashStyle = Settings.Series.Modified.BorderDashStyle;
            chart.Series[seriesNo].ChartType = Settings.Series.Modified.ChartType;
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
            uiPnlGen_Crvs2No_Nm.Maximum = Settings.Presets.Ui.CurvesNo;
            uiPnlGen_Crvs2No_Nm.Value = uiPnlGen_Crvs2No_Nm.Maximum;
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
                uiPnlPrg_DotNetFr2_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.InfoObtErrTxt.GetString();
                return;
            }

            uiPnlPrg_DotNetFr2_TxtBx.Text = dotNetVersion;

        }

        private void UpdateUiByOsVersionName()
        {
            string osVersion = SystemInfo.TryGetOSVersion();

            if ( osVersion == null ) {
                uiPnlPrg_OsVer2_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.InfoObtErrTxt.GetString();
                return;
            }

            uiPnlPrg_OsVer2_TxtBx.Text = osVersion;
        }

        private void UiPanelDataSheet_ShowDataSet_Click( object sender, EventArgs e )
        {
            int selectedCurveType = UiControls.TryGetSelectedIndex( uiPnlDtSh_CrvT_ComBx );
            int selectedCurveIndex = UiControls.TryGetValue<int>( uiPnlDtSh_CrvIdx_Num );

            switch ( (DataSetCurveType) selectedCurveType ) {
            case DataSetCurveType.Ideal:
            case DataSetCurveType.Modified:
            case DataSetCurveType.Average:
                break;
            default:
                Messages.MainWindow.AsteriskOfCurveTypeNotSelected();
                return;
            }

            Series selectedCurveSeries = SpecifyCurveSeries( selectedCurveType, selectedCurveIndex );

            if ( selectedCurveSeries == null || DataChart.IdealCurve.Points.Count == 0 ) {
                Messages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            using ( var gprvDialog = new GridPreviewer( selectedCurveSeries ) ) {
                UiControls.TryShowDialog( gprvDialog, this );
                string signature = string.Empty;

                try {
                    signature = @base.DeclaringType.Name + "." + @base.Name + "(" + sender + ", " + e + ")";

                    if ( gprvDialog.DialogResult == DialogResult.OK ) {
                        DataChart.AlterCurve( gprvDialog.ChartDataSet, (DataSetCurveType) selectedCurveType, selectedCurveIndex );
                        uiCharts_Crv.Series.Clear();
                        uiCharts_Crv.Series.Add( gprvDialog.ChartDataSet );
                        uiCharts_Crv.Series[0].BorderWidth = 3;
                        uiCharts_Crv.Series[0].Color = System.Drawing.Color.Indigo;
                        uiCharts_Crv.ChartAreas[0].RecalculateAxesScale();
                        uiCharts_Crv.Visible = true;
                        uiCharts_Crv.Invalidate();
                    }
                }
                catch ( InvalidOperationException ex ) {
                    log.Error( signature, ex );
                    Messages.MainWindow.ErrorOfChartRefreshing();
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException ex ) {
                    log.Error( signature, ex );
                }
                catch ( Exception ex ) {
                    log.Fatal( signature, ex );
                }
            }
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

        private void UiPanelDataSheet_Malform_Click( object sender, EventArgs e )
        {
            if ( DataChart.IdealCurve.Points.Count == 0 ) {
                Messages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            int numberOfCurves = UiControls.TryGetValue<int>( uiPnlDtSh_CrvNo_Num );
            double surrounding = UiControls.TryGetValue<double>( uiPnlDtSh_Surr_Num );
            bool? result = DataChart.MakeNoiseOfGaussian( numberOfCurves, surrounding );

            if ( result == null ) {
                Messages.MainWindow.ExclamationOfSpecifiedCurveDoesNotExist();
                return;
            }

            if ( !result.Value ) {
                Messages.MainWindow.StopOfOperationMalformRejected();
                return;
            }

            UiControls.TrySetSelectedIndex( uiPnlDtSh_CrvT_ComBx, (int) DataSetCurveType.Modified );
            UpdateUiByShowingCurveOnChart( DataSetCurveType.Modified );
        }

        private void UiPanelGenerate_Apply_Click( object sender, EventArgs e )
        {
            if ( DataChart.IdealCurve.Points.Count == 0 ) {
                Messages.MainWindow.ExclamationOfSeriesSelection();
                return;
            }

            MeanType meanType = (MeanType) UiControls.TryGetSelectedIndex( uiPnlGen_MeanT_ComBx );
            int numberOfCurves = UiControls.TryGetValue<int>( uiPnlGen_Crvs2No_Nm );
            bool isNumberOfCurvesInsufficient = (meanType == MeanType.Median
                || meanType == MeanType.Tolerance)
                && numberOfCurves < 3;

            if ( isNumberOfCurvesInsufficient ) {
                Messages.MainWindow.ErrorOfNotEnoughCurvesForMediana();
                return;
            }

            bool? averageResult = DataChart.TryMakeAverageCurve( meanType, numberOfCurves );

            if ( !averageResult.Value ) {
                DataChart.RemoveInvalidPoints( DataSetCurveType.Average );
                Messages.MainWindow.ExclamationOfPointsNotValidToChart();
            }

            UiControls.TrySetSelectedIndex( uiPnlDtSh_CrvT_ComBx, (int) DataSetCurveType.Average );
            double standardDeviation = StatAnalysis.GetRelativeStandardDeviationFromSeriesValues( DataChart.AverageCurve, DataChart.IdealCurve );
            uiPnlGen_StdDev2_TxtBx.Text = Strings.TryFormatAsNumeric( 8, standardDeviation );
        }

        private void UpdateUiByDefaultSettings()
        {
            ChartAssist.SetDefaultSettings( uiCharts_Crv );
            UiControls.TrySetSelectedIndex( uiPnlGen_MeanT_ComBx, (int) MeanType.Geometric );
            UpdateUiByChosenScaffoldStatus();
        }

        private void UiMainWindow_Resize( object sender, EventArgs e )
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

        private void UiMenuPanel_KeepProportions_Click( object sender, EventArgs e )
        {
            Settings.MainWindow.Menu.Panel.KeepProportions = !Settings.MainWindow.Menu.Panel.KeepProportions;
            uiMenuPnl_KeepProp.Checked = Settings.MainWindow.Menu.Panel.KeepProportions;
        }

        private void UiMenuPanel_Hide_Click( object sender, EventArgs e )
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

        private void UiMenuPanel_Lock_Click( object sender, EventArgs e )
        {
            Settings.MainWindow.Menu.Panel.Lock = !Settings.MainWindow.Menu.Panel.Lock;
            uiMenuPnl_Lock.Checked = Settings.MainWindow.Menu.Panel.Lock;
            uiMw_SpCtn.Panel1.Enabled = !Settings.MainWindow.Menu.Panel.Lock;
        }

        private void UiMenuMeans_AveragingInfo_Click( object sender, EventArgs e )
        {
            using ( var msgBox = new AvgInfo() ) {
                UiControls.TryShowDialog( msgBox, this );
            }
        }

        private void UiMenuMeans_Settings_Click( object sender, EventArgs e )
        {
            using ( var dialog = new MeansSettings() ) {
                ProvideMeansSettings( dialog );
                dialog.UpdateUiBySettings();
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    GrabMeansSettings( dialog );
                }
            }
        }

        private void ProvideMeansSettings( MeansSettings dialog )
        {
            dialog.MeansParams.AGM = DataChart.MeansParams.AGM;
            dialog.MeansParams.Generalized = DataChart.MeansParams.Generalized;
            dialog.MeansParams.Geometric = DataChart.MeansParams.Geometric;
            dialog.MeansParams.Harmonic = DataChart.MeansParams.Harmonic;
            dialog.MeansParams.Heronian = DataChart.MeansParams.Heronian;
            dialog.MeansParams.Tolerance = DataChart.MeansParams.Tolerance;
        }

        private void GrabMeansSettings( MeansSettings dialog )
        {
            DataChart.MeansParams.AGM = dialog.MeansParams.AGM;
            DataChart.MeansParams.Generalized = dialog.MeansParams.Generalized;
            DataChart.MeansParams.Geometric = dialog.MeansParams.Geometric;
            DataChart.MeansParams.Harmonic = dialog.MeansParams.Harmonic;
            DataChart.MeansParams.Heronian = dialog.MeansParams.Heronian;
            DataChart.MeansParams.Tolerance = dialog.MeansParams.Tolerance;
        }

        private void UiMenuChart_Settings_Click( object sender, EventArgs e )
        {
            using ( var dialog = new ChartSettings( GetChartSettings( uiCharts_Crv ) ) ) {
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    SetChartSettings( dialog.Settings, uiCharts_Crv );
                    UpdateUiByInvalidatingChartSettings();
                }
            }
        }

        private MainChartSettings GetChartSettings( Chart chart, int areaNo = 0 )
        {
            MainChartSettings settings = new MainChartSettings();
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
            settings.Series.Ideal.Color = Settings.Series.Ideal.Color;
            settings.Series.Ideal.BorderWidth = Settings.Series.Ideal.BorderWidth;
            settings.Series.Ideal.BorderDashStyle = Settings.Series.Ideal.BorderDashStyle;
            settings.Series.Ideal.ChartType = Settings.Series.Ideal.ChartType;
            settings.Series.Modified.Color = Settings.Series.Modified.Color;
            settings.Series.Modified.BorderWidth = Settings.Series.Modified.BorderWidth;
            settings.Series.Modified.BorderDashStyle = Settings.Series.Modified.BorderDashStyle;
            settings.Series.Modified.ChartType = Settings.Series.Modified.ChartType;
            settings.Series.Average.Color = Settings.Series.Average.Color;
            settings.Series.Average.BorderWidth = Settings.Series.Average.BorderWidth;
            settings.Series.Average.BorderDashStyle = Settings.Series.Average.BorderDashStyle;
            settings.Series.Average.ChartType = Settings.Series.Average.ChartType;
            return settings;
        }

        private void SetChartSettings( MainChartSettings settings, Chart chart, int areaNo = 0 )
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

        private void SetCommonChartSettings( MainChartSettings settings, Chart chart, int areaNo = 0 )
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

        private void SetSeriesChartSettings( MainChartSettings settings )
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

        private void SetPatternSeriesChartSettings( MainChartSettings settings )
        {
            Settings.Series.Ideal.Color = settings.Series.Ideal.Color;
            Settings.Series.Ideal.BorderWidth = settings.Series.Ideal.BorderWidth;
            Settings.Series.Ideal.BorderDashStyle = settings.Series.Ideal.BorderDashStyle;
            Settings.Series.Ideal.ChartType = settings.Series.Ideal.ChartType;
        }

        private void SetGeneratedSeriesChartSettings( MainChartSettings settings )
        {
            Settings.Series.Modified.Color = settings.Series.Modified.Color;
            Settings.Series.Modified.BorderWidth = settings.Series.Modified.BorderWidth;
            Settings.Series.Modified.BorderDashStyle = settings.Series.Modified.BorderDashStyle;
            Settings.Series.Modified.ChartType = settings.Series.Modified.ChartType;
        }

        private void SetAverageSeriesChartSettings( MainChartSettings settings )
        {
            Settings.Series.Average.Color = settings.Series.Average.Color;
            Settings.Series.Average.BorderWidth = settings.Series.Average.BorderWidth;
            Settings.Series.Average.BorderDashStyle = settings.Series.Average.BorderDashStyle;
            Settings.Series.Average.ChartType = settings.Series.Average.ChartType;
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

        private void UiMenuProgram_StatisticalAnalysis_Click( object sender, EventArgs e )
        {
            string signature = string.Empty;

            try {
                signature = @base.DeclaringType.Name + "." + @base.Name + "(" + sender + ", " + e + ")";

                Thread window = new Thread( DelegatorForStatAnalysis ) {
                    Name = nameof( StatAnalysis ),
                    IsBackground = true
                };

                window.Start();
            }
            catch ( ThreadStateException ex ) {
                log.Error( signature, ex );
            }
            catch ( OutOfMemoryException ex ) {
                log.Fatal( signature, ex );
                Messages.Application.StopOfOutOfMemoryException();
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

        private void DelegatorForStatAnalysis()
        {
            if ( IsGeneratedCurvesSeriesEmpty() ) {
                Messages.StatisticalAnalysis.ErrorOfNoSavedPresets();
                return;
            }

            using ( var dialog = new StatAnalysis( Settings.Presets, DataChart ) ) {
                dialog.ShowDialog();
            }
        }

        private bool IsGeneratedCurvesSeriesEmpty()
        {
            if ( DataChart.ModifiedCurves.Count <= 0 ) {
                return true;
            }

            return false;
        }

        private void MainWindow_FormClosing( object sender, FormClosingEventArgs e )
        {
            Timer = null;
            Settings = null;
            DataChart = null;
            Dispose();
        }

        private void UiMenuProgram_SelectLanguage_Click( object sender, EventArgs e )
        {
            using ( var dialog = new LanguageSelector() ) {
                UiControls.TryShowDialog( dialog, this );

                if ( dialog.DialogResult == DialogResult.OK ) {
                    Translator.SetLanguage( dialog.GetSelectedLanguage() );
                    LocalizeCulture( dialog.GetSelectedLanguage() );
                    LocalizeWindow();
                }
            }
        }

        private void LocalizeWindow()
        {
            Text = Translator.GetInstance().Strings.MainWindow.Ui.Form.Text.GetString();
            LocalizeMenu();
            LocalizePanel();
        }

        private void LocalizeMenu()
        {
            LocalizeMenuProgram();
            LocalizeMenuPanel();
            LocalizeMenuMeans();
            LocalizeMenuChart();
        }

        private void LocalizeMenuProgram()
        {
            uiMenu_Prg.Text = Translator.GetInstance().Strings.MainWindow.Menu.Program.Title.GetString();
            uiMenuPrg_StatAnal.Text = Translator.GetInstance().Strings.MainWindow.Menu.Program.StatAnal.GetString();
            uiMenuPrg_Lang.Text = Translator.GetInstance().Strings.MainWindow.Menu.Program.Lang.GetString();
            uiMenuPrg_Exit.Text = Translator.GetInstance().Strings.MainWindow.Menu.Program.Exit.GetString();
        }

        private void LocalizeMenuPanel()
        {
            uiMenu_Pnl.Text = Translator.GetInstance().Strings.MainWindow.Menu.Panel.Title.GetString();
            uiMenuPnl_KeepProp.Text = Translator.GetInstance().Strings.MainWindow.Menu.Panel.KeepProp.GetString();
            uiMenuPnl_Hide.Text = Translator.GetInstance().Strings.MainWindow.Menu.Panel.Hide.GetString();
            uiMenuPnl_Lock.Text = Translator.GetInstance().Strings.MainWindow.Menu.Panel.Lock.GetString();
        }

        private void LocalizeMenuMeans()
        {
            uiMenu_Means.Text = Translator.GetInstance().Strings.MainWindow.Menu.Means.Title.GetString();
            uiMenuMeans_AvgInfo.Text = Translator.GetInstance().Strings.MainWindow.Menu.Means.AvgInfo.GetString();
            uiMenuMeans_Settings.Text = Translator.GetInstance().Strings.MainWindow.Menu.Means.Settings.GetString();
        }

        private void LocalizeMenuChart()
        {
            uiMenu_Chart.Text = Translator.GetInstance().Strings.MainWindow.Menu.Chart.Title.GetString();
            uiMenuChart_Settings.Text = Translator.GetInstance().Strings.MainWindow.Menu.Chart.Settings.GetString();
        }

        private void LocalizePanel()
        {
            LocalizePanelTabGenerate();
            LocalizePanelTabDatasheet();
            LocalizePanelTabProgram();
        }

        private void LocalizePanelTabGenerate()
        {
            uiPnlGen_TbPg.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.Title.GetString();
            uiPnlGen_PattCrvScaff_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.PattCrvScaff.GetString();
            uiPnlGen_CrvScaff1_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.CrvScaff1.GetString();
            uiPnlGen_Def_Btn.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.ScaffNone.GetString();
            UpdateUiByChosenScaffoldStatus();
            uiPnlGen_CrvsSet_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.CrvsSet.GetString();
            uiPnlGen_Crvs1No_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.Crvs1No.GetString();
            uiPnlGen_StartX_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.StartX.GetString();
            uiPnlGen_EndX_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.EndX.GetString();
            uiPnlGen_Dens_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.Dens.GetString();
            RefreshControlToLocalize( uiPnlGen_Crvs1No_Num );
            RefreshControlToLocalize( uiPnlGen_StartX_Num );
            RefreshControlToLocalize( uiPnlGen_EndX_Num );
            RefreshControlToLocalize( uiPnlGen_Dens_Num );
            uiPnlGen_GenSet_Btn.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.GenSet.GetString();
            uiPnlGen_Avg_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.Avg.GetString();
            uiPnlGen_MeanT_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.MeanT.GetString();
            uiPnlGen_Crvs2No_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.Crvs2No.GetString();
            Translator.AddLocalizedMeanTypes( uiPnlGen_MeanT_ComBx );
            RefreshControlToLocalize( uiPnlGen_Crvs2No_Nm );
            uiPnlGen_StdDev1_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.StdDev1.GetString();
            uiPnlGen_Apply_Btn.Text = Translator.GetInstance().Strings.MainWindow.Panel.Generate.Apply.GetString();
        }

        private void LocalizeCulture( Languages language )
        {
            string signature = string.Empty;

            try {
                signature = @base.DeclaringType.Name + "." + @base.Name + "(" + language + ")";

                switch ( language ) {
                case Languages.English:
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "en-US" );
                    break;
                case Languages.Polish:
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "pl-PL" );
                    break;
                }
            }
            catch ( System.Globalization.CultureNotFoundException ex ) {
                log.Error( signature, ex );
            }
            catch ( ArgumentNullException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }
        }

        private void RefreshControlToLocalize( Control control )
        {
            string signature = string.Empty;

            try {
                signature = @base.DeclaringType.Name + "." + @base.Name + "(" + control + ")";

                if ( control is NumericUpDown ) {
                    decimal originalValue = (control as NumericUpDown).Value;

                    if ( (control as NumericUpDown).Minimum == (control as NumericUpDown).Maximum ) {
                        (control as NumericUpDown).Maximum++;
                        UiControls.TrySetValue( control as NumericUpDown, (control as NumericUpDown).Maximum );
                        UiControls.TrySetValue( control as NumericUpDown, (control as NumericUpDown).Minimum );
                        (control as NumericUpDown).Maximum--;
                        return;
                    }

                    if ( originalValue == (control as NumericUpDown).Minimum ) {
                        UiControls.TrySetValue( control as NumericUpDown, originalValue + ((control as NumericUpDown).Increment) );
                        UiControls.TrySetValue( control as NumericUpDown, originalValue );
                        return;
                    }

                    decimal originalIncrement = (control as NumericUpDown).Increment;
                    (control as NumericUpDown).Increment = 0.0001M;
                    UiControls.TrySetValue( control as NumericUpDown, originalValue - ((control as NumericUpDown).Increment) );
                    UiControls.TrySetValue( control as NumericUpDown, originalValue );
                    (control as NumericUpDown).Increment = originalIncrement;
                }
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }
        }

        private void UiMainWindow_Shown( object sender, EventArgs e )
        {
            LocalizeWindow();
        }

        private void LocalizePanelTabDatasheet()
        {
            uiPnlDtSh_TbPg.Text = Translator.GetInstance().Strings.MainWindow.Panel.Datasheet.Title.GetString();
            uiPnlDtSh_DtSetCtrl_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Datasheet.DtSetCtrl.GetString();
            uiPnlDtSh_CrvT_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Datasheet.CrvT.GetString();
            Translator.AddLocalizedDataSetCurveTypes( uiPnlDtSh_CrvT_ComBx );
            uiPnlDtSh_CrvIdx_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Datasheet.CrvIdx.GetString();

            if ( DataChart.IdealCurve.Points.Count != 0 ) {
                RefreshControlToLocalize( uiPnlDtSh_CrvIdx_Num );
            }

            uiPnlDtSh_ShowDtSet_Btn.Text = Translator.GetInstance().Strings.MainWindow.Panel.Datasheet.ShowDtSet.GetString();
            uiPnlDtSh_GsNoise_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Datasheet.GsNoise.GetString();
            uiPnlDtSh_CrvNo_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Datasheet.CrvNo.GetString();
            RefreshControlToLocalize( uiPnlDtSh_CrvNo_Num );
            uiPnlDtSh_Surr_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Datasheet.Surr.GetString();
            RefreshControlToLocalize( uiPnlDtSh_Surr_Num );
            uiPnlDtSh_Malform_Btn.Text = Translator.GetInstance().Strings.MainWindow.Panel.Datasheet.Malform.GetString();
        }

        private void LocalizePanelTabProgram()
        {
            uiPnlPrg_TbPg.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.Title.GetString();
            uiPnlPrg_Timer_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.Timer.GetString();
            uiPnlPrg_ActState1_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.ActState1.GetString();
            UpdateUiByStatusOfTimerThread();
            uiPnlPrg_Cnts1_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.Cnts1.GetString();
            uiPnlPrg_Info_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.Info.GetString();
            uiPnlPrg_DotNetFr1_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.DotNetFr1.GetString();
            uiPnlPrg_OsVer1_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.OsVer1.GetString();
            uiPnlPrg_Log_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.Log.GetString();
            uiPnlPrg_LogPath1_TxtBx.Text = Translator.GetInstance().Strings.MainWindow.Panel.Program.LogPath1.GetString();
        }

        private void SetWindowDefaultDimensions()
        {
            Width = Settings.MainWindow.Dimensions.Width;
            Height = Settings.MainWindow.Dimensions.Height;
        }

    }

}
