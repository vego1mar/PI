using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using PI.src.helpers;
using PI.src.messages;
using PI.src.settings;
using PI.src.general;
using PI.src.enumerators;
using log4net;
using System.Reflection;
using PI.src.localization.enums;

namespace PI
{
    public partial class StatAnalysis : Form
    {

        internal GeneratorSettings Settings { get; private set; }
        private List<List<CurvesDataManager>> Data { get; set; }
        private List<double> Surroundings { get; set; }
        private List<List<List<Series>>> Averages { get; set; }
        private List<List<List<double>>> StdDeviations { get; set; }
        private bool IsFormShown = false;
        private static uint WindowNo { get; set; } = 0;
        private string WindowThreadsName { get; set; }

        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        private struct DatasetControlsValues
        {
            public int PhenomNo { get; set; }
            public int NoiseNo { get; set; }
            public int CrvIdx { get; set; }
            public int MeanT { get; set; }
            public int CrvT { get; set; }
        }

        internal StatAnalysis( GeneratorSettings genSets, CurvesDataManager curvesData )
        {
            InitializeComponent();
            InitializePropertySettings( genSets );
            InitializeProperties();
            SetWindowDefaults();
            PerformStatisticalAnalysis();
            UpdateUiByPopulatingGridWithStandardDeviations();
            UpdateUiByColoringUiGrids();
            LocalizeWindow();
        }

        private void InitializeProperties()
        {
            InitializePropertySurroundings();
            InitializePropertyData();
            InitializePropertyAverages();
            InitializeDataGridViewUiFields();
            InitializePropertyStdDeviations();
            WindowNo++;
            WindowThreadsName = nameof( StatAnalysis ) + "::" + nameof( GridPreviewer ) + "::" + WindowNo;
        }

        private void InitializePropertySettings( GeneratorSettings genSets )
        {
            Settings = genSets;
        }

        private void InitializePropertySurroundings()
        {
            Surroundings = new List<double>() {
                0.1,
                0.5,
                1.0,
                2.0
            };
        }

        private void InitializePropertyData()
        {
            /*
             *  Data[a][b]
             *  a := { peek, deformation } - malformation type no.
             *  b := { 0.1, 0.5, 1.0, 2.0 } - noise surrounding no.
             */

            Data = new List<List<CurvesDataManager>>();

            foreach ( Phenomenon idx in Enum.GetValues( typeof( Phenomenon ) ) ) {
                Data.Add( new List<CurvesDataManager>() );                                      // [a][-] 

                for ( int i = 0; i < Surroundings.Count; i++ ) {
                    Data[(int) idx].Add( new CurvesDataManager() );    // [-][b] 
                }
            }

        }

        private void InitializePropertyAverages()
        {
            /*
             *  Averages[a][b][c]
             *  a := { peek, deformation } - malformation type no.
             *  b := { 0.1, 0.5, 1.0, 2.0 } - noise surrounding no.
             *  c := { arithmetic, geometric, mediana, ... } - mean type no.
             */

            Averages = new List<List<List<Series>>>();

            foreach ( Phenomenon idx in Enum.GetValues( typeof( Phenomenon ) ) ) {
                Averages.Add( new List<List<Series>>() );                                           // [a][-][-] 

                for ( int i = 0; i < Surroundings.Count; i++ ) {
                    Averages[(int) idx].Add( new List<Series>() );                                  // [-][b][-] 

                    for ( int j = 0; j < Enum.GetNames( typeof( MeanType ) ).Length; j++ ) {
                        Averages[(int) idx][i].Add( new Series() );                                 // [-][-][c]
                        SeriesAssist.SetDefaultSettings( Averages[(int) idx][i][j] );
                    }
                }
            }
        }

        private void InitializeDataGridViewUiFields()
        {
            List<string> peekColumns = GetDataGridColumnsNames( Phenomenon.Peek );
            List<string> deformColumns = GetDataGridColumnsNames( Phenomenon.Saturation );
            IList<string> meanNames = new MeanTypesStrings().ToList();

            for ( int i = 0; i < meanNames.Count; i++ ) {
                uiLPeek_Grid.Rows.Add();
                uiLPeek_Grid.Rows[i].HeaderCell.Value = meanNames[i];
                uiLDeform_Grid.Rows.Add();
                uiLDeform_Grid.Rows[i].HeaderCell.Value = meanNames[i];

                for ( int j = 0; j < peekColumns.Count; j++ ) {
                    uiLPeek_Grid.Rows[i].Cells[peekColumns[j]].ValueType = typeof( double );
                    uiLDeform_Grid.Rows[i].Cells[deformColumns[j]].ValueType = typeof( double );
                }
            }

            const int ROW_HEADERS_WIDTH = 23 * 8 + 11;
            uiLPeek_Grid.RowHeadersWidth = ROW_HEADERS_WIDTH;
            uiLPeek_Grid.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            uiLPeek_Grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            uiLDeform_Grid.RowHeadersWidth = ROW_HEADERS_WIDTH;
            uiLDeform_Grid.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            uiLDeform_Grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void InitializePropertyStdDeviations()
        {
            /*
             *  StdDeviations[a][b][c]
             *  a := { peek, deformation } - malformation type no.
             *  b := { 0.1, 0.5, 1.0, 2.0 } - noise surrounding no.
             *  c := { arithmetic, geometric, mediana, ... } - mean type no.
             */

            StdDeviations = new List<List<List<double>>>();

            foreach ( Phenomenon idx in Enum.GetValues( typeof( Phenomenon ) ) ) {
                StdDeviations.Add( new List<List<double>>() );                                           // [a][-][-] 

                for ( int i = 0; i < Surroundings.Count; i++ ) {
                    StdDeviations[(int) idx].Add( new List<double>() );                                  // [-][b][-] 

                    for ( int j = 0; j < Enum.GetNames( typeof( MeanType ) ).Length; j++ ) {
                        StdDeviations[(int) idx][i].Add( new double() );                                 // [-][-][c]
                    }
                }
            }
        }

        private void SetWindowDefaults()
        {
            UiControls.TrySelectTab( uiL_TbCtrl, (int) Phenomenon.Peek );
            UiControls.TrySelectTab( uiR_TbCtrl, 0 );
            ChartAssist.SetDefaultSettings( uiRChart_Chart );
            AddDataSetCurveTypes( uiRChartDown_CrvT_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_CrvT_ComBx, (int) DataSetCurveType.Ideal );
            uiRChartDown_CrvIdx_Num.Minimum = 0;
            uiRChartDown_CrvIdx_Num.Maximum = Settings.Ui.CurvesNo - 1;
            UiControls.TrySetValue( uiRChartDown_CrvIdx_Num, Settings.Ui.CurvesNo / 2 );
            AddPhenomenonsIndexNames( uiRChartDown_Phen_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_Phen_ComBx, (int) Phenomenon.Peek );
            AddSurroundings( uiRChartDown_Surr_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_Surr_ComBx, 0 );
            AddMeanTypes( uiRChartDown_MeanT_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_MeanT_ComBx, (int) MeanType.Tolerance );
            uiRFormulaDown_CrvsNo2_TxtBx.Text = Settings.Ui.CurvesNo.ToString();
            uiRFormulaDown_Dens2_TxtBx.Text = (Settings.Ui.PointsNo + 1).ToString();
        }

        private void UiStatAnalysis_Load( object sender, EventArgs e )
        {
            uiLPeek_Grid.Select();
        }

        private void AddDataSetCurveTypes( ComboBox comboBox )
        {
            foreach ( string type in Enum.GetNames( typeof( DataSetCurveType ) ) ) {
                comboBox.Items.Add( type );
            }
        }

        private void AddPhenomenonsIndexNames( ComboBox comboBox )
        {
            foreach ( string phenomenon in Enum.GetNames( typeof( Phenomenon ) ) ) {
                comboBox.Items.Add( phenomenon );
            }
        }

        private void AddSurroundings( ComboBox comboBox )
        {
            foreach ( double surrounding in Surroundings ) {
                comboBox.Items.Add( surrounding.ToString( System.Threading.Thread.CurrentThread.CurrentCulture ) );
            }
        }

        private void AddMeanTypes( ComboBox comboBox )
        {
            foreach ( string type in Enum.GetNames( typeof( MeanType ) ) ) {
                comboBox.Items.Add( type );
            }
        }

        private void PerformStatisticalAnalysis()
        {
            for ( int i = 0; i < Data.Count; i++ ) {
                for ( int j = 0; j < Surroundings.Count; j++ ) {
                    Data[i][j].GenerateIdealCurve( Settings.Pcd.Scaffold, Settings.Pcd.Parameters, Settings.Ui.StartX, Settings.Ui.EndX, Settings.Ui.PointsNo );
                    Data[i][j].PropagateIdealCurve( Settings.Ui.CurvesNo );
                    Data[i][j].MakeNoiseOfGaussian( Settings.Ui.CurvesNo, Surroundings[j] );
                    MakePeekOrDeformation( (Phenomenon) i, Data[i][j], Settings.Ui.CurvesNo / 2 );

                    foreach ( string type in Enum.GetNames( typeof( MeanType ) ) ) {
                        Enum.TryParse( type, out MeanType meanType );
                        Data[i][j].TryMakeAverageCurve( meanType, Settings.Ui.CurvesNo );
                        AddSeriesPoints( Averages[i][j][Convert.ToInt32( meanType )], Data[i][j].AverageCurve );
                        double stdDeviation = GetRelativeStandardDeviationFromSeriesValues( Averages[i][j][(int) meanType], Data[i][j].IdealCurve );
                        StdDeviations[i][j][Convert.ToInt32( meanType )] = stdDeviation;
                    }
                }
            }
        }

        private void UiRightChartDown_ShowDataset_Button_Click( object sender, EventArgs e )
        {
            if ( !IsCurveIndexProperValue() ) {
                return;
            }

            string signature = string.Empty;

            try {
                MethodBase @base = MethodBase.GetCurrentMethod();
                signature = @base.DeclaringType.Name + "." + @base.Name + "()";
                Series controlsSpecifiedSeries = GetSeriesSpecifiedByControls();

                Thread window = new Thread( () => DelegatorForGridPreviewer( controlsSpecifiedSeries ) ) {
                    Name = WindowThreadsName,
                    IsBackground = true
                };

                window.Start();
            }
            catch ( ThreadStateException ex ) {
                log.Error( signature, ex );
            }
            catch ( OutOfMemoryException ex ) {
                Messages.Application.StopOfOutOfMemoryException();
                log.Fatal( signature, ex );
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

        private void DelegatorForGridPreviewer( Series controlsSpecifiedSeries )
        {
            using ( var dialog = new GridPreviewer( controlsSpecifiedSeries ) ) {
                dialog.SetFastEditControls( false );
                dialog.ShowDialog();
            }
        }

        private void UiRightChartDown_CurveType_ComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            switch ( (DataSetCurveType) UiControls.TryGetSelectedIndex( uiRChartDown_CrvT_ComBx ) ) {
            case DataSetCurveType.Modified:
                uiRChartDown_CrvIdx_Num.Enabled = true;
                uiRChartDown_MeanT_ComBx.Enabled = false;
                break;
            case DataSetCurveType.Average:
                uiRChartDown_CrvIdx_Num.Enabled = false;
                uiRChartDown_MeanT_ComBx.Enabled = true;
                break;
            case DataSetCurveType.Ideal:
                uiRChartDown_CrvIdx_Num.Enabled = false;
                uiRChartDown_MeanT_ComBx.Enabled = false;
                break;
            }

            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }
        }

        private Series GetSeriesSpecifiedByControls()
        {
            DatasetControlsValues indices = GetDatasetControlsValues();

            switch ( (DataSetCurveType) indices.CrvT ) {
            case DataSetCurveType.Modified:
                return Data[indices.PhenomNo][indices.NoiseNo].ModifiedCurves[indices.CrvIdx];
            case DataSetCurveType.Average:
                return Averages[indices.PhenomNo][indices.NoiseNo][indices.MeanT];
            case DataSetCurveType.Ideal:
                return Data[indices.PhenomNo][indices.NoiseNo].IdealCurve;
            }

            return new Series();
        }

        private DatasetControlsValues GetDatasetControlsValues()
        {
            return new DatasetControlsValues() {
                PhenomNo = UiControls.TryGetSelectedIndex( uiRChartDown_Phen_ComBx ),
                CrvIdx = UiControls.TryGetValue<int>( uiRChartDown_CrvIdx_Num ),
                NoiseNo = UiControls.TryGetSelectedIndex( uiRChartDown_Surr_ComBx ),
                MeanT = UiControls.TryGetSelectedIndex( uiRChartDown_MeanT_ComBx ),
                CrvT = UiControls.TryGetSelectedIndex( uiRChartDown_CrvT_ComBx )
            };
        }

        private void MakePeekOrDeformation( Phenomenon idx, CurvesDataManager data, int curveIdx, int yValuesIdx = 0 )
        {
            Series newSeries = data.ModifiedCurves[curveIdx];
            int leftIntervalPoint = Convert.ToInt32( (3.0 / 7.0) * newSeries.Points.Count );
            int middlePoint = newSeries.Points.Count / 2;
            int rightIntervalPoint = Convert.ToInt32( (5.0 / 7.0) * newSeries.Points.Count );
            double maxValue = newSeries.Points.FindMaxByValue().YValues[yValuesIdx];
            double minValue = newSeries.Points.FindMinByValue().YValues[yValuesIdx];
            double malformationValue = 0.5 * (maxValue - minValue);

            switch ( idx ) {
            case Phenomenon.Peek:
                newSeries.Points[middlePoint].YValues[yValuesIdx] += malformationValue;
                break;
            case Phenomenon.Saturation:
                OverrideSeriesValuesWithinInterval( newSeries, leftIntervalPoint, rightIntervalPoint, malformationValue, yValuesIdx );
                break;
            }

            data.AlterCurve( newSeries, DataSetCurveType.Modified, curveIdx );
        }

        private void OverrideSeriesValuesWithinInterval( Series series, int leftPoint, int rightPoint, double value, int yValuesIdx = 0 )
        {
            for ( int i = leftPoint; i <= rightPoint; i++ ) {
                series.Points[i].YValues[yValuesIdx] = value;
            }
        }

        private void AddSeriesPoints( Series target, Series source, int yValuesIdx = 0 )
        {
            for ( int i = 0; i < source.Points.Count; i++ ) {
                target.Points.AddXY( source.Points[i].XValue, source.Points[i].YValues[yValuesIdx] );
            }
        }

        private void UiRightChartDown_CurveIndex_Numeric_ValueChanged( object sender, EventArgs e )
        {
            if ( !IsCurveIndexProperValue() ) {
                Messages.StatisticalAnalysis.ExclamationOfValueOutOfRange();
                return;
            }

            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }
        }

        private bool IsCurveIndexProperValue()
        {
            int value = UiControls.TryGetValue<int>( uiRChartDown_CrvIdx_Num );

            if ( value < 0 || value >= Settings.Ui.CurvesNo ) {
                return false;
            }

            return true;
        }

        private void UiRightChartDown_Phenomenon_ComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }

            UiControls.TrySelectTab( uiL_TbCtrl, UiControls.TryGetSelectedIndex( uiRChartDown_Phen_ComBx ) );
        }

        private void UiRightChartDown_Surroundings_ComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }
        }

        private void UiRightChartDown_MeanType_ComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }
        }

        private void UpdateUiByRefreshingChart()
        {
            string signature = string.Empty;

            try {
                MethodBase @base = MethodBase.GetCurrentMethod();
                signature = @base.DeclaringType.Name + "." + @base.Name + "()";
                uiRChart_Chart.Visible = false;
                uiRChart_Chart.Series.Clear();
                uiRChart_Chart.Series.Add( GetSeriesSpecifiedByControls() );

                if ( !SeriesAssist.IsChartAcceptable( uiRChart_Chart.Series[0] ) ) {
                    Messages.StatisticalAnalysis.ExclamationOfPointsNotValidToChart();
                    return;
                }

                ChooseChartSeriesColor( uiRChart_Chart );
                uiRChart_Chart.ChartAreas[0].RecalculateAxesScale();
                uiRChart_Chart.Visible = true;
                uiRChart_Chart.Invalidate();
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
                Messages.StatisticalAnalysis.ErrorOfUnrecognized();
            }
        }

        private void ChooseChartSeriesColor( Chart chart, int seriesIdx = 0 )
        {
            switch ( (DataSetCurveType) UiControls.TryGetSelectedIndex( uiRChartDown_CrvT_ComBx ) ) {
            case DataSetCurveType.Modified:
                chart.Series[seriesIdx].Color = System.Drawing.Color.Crimson;
                break;
            case DataSetCurveType.Ideal:
                chart.Series[seriesIdx].Color = System.Drawing.Color.Black;
                break;
            case DataSetCurveType.Average:
                chart.Series[seriesIdx].Color = System.Drawing.Color.ForestGreen;
                break;
            }
        }

        private void UiStatAnalysis_Shown( object sender, EventArgs e )
        {
            IsFormShown = true;
            UpdateUiByRefreshingChart();
        }

        private void UiStatAnalysis_FormClosed( object sender, FormClosedEventArgs e )
        {
            IsFormShown = false;
        }

        private List<string> GetDataGridColumnsNames( Phenomenon phenomenon )
        {
            switch ( phenomenon ) {
            case Phenomenon.Peek:
                return new List<string>() {
                    nameof( uiLPeekGrid_Noise01_Col ),
                    nameof( uiLPeekGrid_Noise05_Col ),
                    nameof( uiLPeekGrid_Noise1_Col ),
                    nameof( uiLPeekGrid_Noise2_Col )
                };
            case Phenomenon.Saturation:
                return new List<string>() {
                    nameof( uiLDeformGrid_Noise01_Col ),
                    nameof( uiLDeformGrid_Noise05_Col ),
                    nameof( uiLDeformGrid_Noise1_Col ),
                    nameof( uiLDeformGrid_Noise2_Col )
                };
            }

            return new List<string>();
        }

        public static double GetRelativeStandardDeviationFromSeriesValues( Series average, Series pattern, int yValuesIdx = 0 )
        {
            double sum = 0.0;
            double difference;

            for ( int i = 0; i < average.Points.Count; i++ ) {
                difference = average.Points[i].YValues[yValuesIdx] - pattern.Points[i].YValues[yValuesIdx];
                sum += difference * difference;
            }

            return Math.Sqrt( sum / Convert.ToDouble( average.Points.Count ) );
        }

        private void UpdateUiByPopulatingGridWithStandardDeviations()
        {
            DataGridView[] grids = { uiLPeek_Grid, uiLDeform_Grid };

            if ( grids.Count() != Enum.GetNames( typeof( Phenomenon ) ).Count() ) {
                return;
            }

            foreach ( string name in Enum.GetNames( typeof( Phenomenon ) ) ) {
                Enum.TryParse( name, out Phenomenon phenomenon );
                List<string> columns = GetDataGridColumnsNames( phenomenon );

                foreach ( string mean in Enum.GetNames( typeof( MeanType ) ) ) {
                    Enum.TryParse( mean, out MeanType type );

                    for ( int k = 0; k < Surroundings.Count; k++ ) {
                        string stdDeviation = Strings.TryFormatAsNumeric( 4, StdDeviations[(int) phenomenon][k][(int) type] );
                        grids[(int) phenomenon].Rows[(int) type].Cells[columns[k]].Value = stdDeviation;
                    }
                }
            }
        }

        private void UiLeft_TabControl_SelectedIndexChanged( object sender, EventArgs e )
        {
            UiControls.TrySetSelectedIndex( uiRChartDown_Phen_ComBx, UiControls.TryGetSelectedIndex( uiL_TbCtrl ) );
        }

        private void UpdateUiByColoringUiGrids()
        {
            DataGridView[] grids = { uiLPeek_Grid, uiLDeform_Grid };

            if ( grids.Count() != Enum.GetValues( typeof( Phenomenon ) ).Length ) {
                return;
            }

            for ( int i = 0; i < Enum.GetValues( typeof( Phenomenon ) ).Length; i++ ) {
                for ( int j = 0; j < Surroundings.Count; j++ ) {
                    int maxValueIdx = GetIndexOfMaximumValue( StdDeviations[i][j] );
                    int minValueIdx = GetIndexOfMinimumValue( StdDeviations[i][j] );
                    grids[i].Rows[maxValueIdx].Cells[j].Style.BackColor = System.Drawing.Color.OrangeRed;
                    grids[i].Rows[minValueIdx].Cells[j].Style.BackColor = System.Drawing.Color.SpringGreen;
                }
            }
        }

        private int GetIndexOfMaximumValue( List<double> list )
        {
            double maxValue = Double.MinValue;
            int idx = -1;

            for ( int i = 0; i < list.Count; i++ ) {
                if ( list[i] > maxValue ) {
                    maxValue = list[i];
                    idx = i;
                }
            }

            return idx;
        }

        private int GetIndexOfMinimumValue( List<double> list )
        {
            double minValue = Double.MaxValue;
            int idx = -1;

            for ( int i = 0; i < list.Count; i++ ) {
                if ( list[i] < minValue ) {
                    minValue = list[i];
                    idx = i;
                }
            }

            return idx;
        }

        private void UiStatAnalysis_FormClosing( object sender, FormClosingEventArgs e )
        {
            Settings = null;
            Data = null;
            Surroundings = null;
            Averages = null;
            StdDeviations = null;
            WindowThreadsName = null;
            Dispose();
        }

        private void LocalizeWindow()
        {
            LocalizeForm();
            LocalizeStandardDeviation();
            LocalizePreview();
        }

        private void LocalizeForm()
        {
            Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Form.Text.GetString();
        }

        private void LocalizeStandardDeviation()
        {
            uiL_StdDev_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.StdDev.GetString();
            uiL_Peek_TbPg.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Peek.GetString();
            uiL_Deform_TbPg.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Deform.GetString();
            uiLPeekGrid_Noise01_Col.HeaderText = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Noise01.GetString();
            uiLPeekGrid_Noise05_Col.HeaderText = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Noise05.GetString();
            uiLPeekGrid_Noise1_Col.HeaderText = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Noise1.GetString();
            uiLPeekGrid_Noise2_Col.HeaderText = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Noise2.GetString();
            uiLDeformGrid_Noise01_Col.HeaderText = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Noise01.GetString();
            uiLDeformGrid_Noise05_Col.HeaderText = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Noise05.GetString();
            uiLDeformGrid_Noise1_Col.HeaderText = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Noise1.GetString();
            uiLDeformGrid_Noise2_Col.HeaderText = Translator.GetInstance().Strings.StatAnalysis.Ui.StdDeviation.Noise2.GetString();
        }

        private void LocalizePreview()
        {
            uiR_Prv_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.Prv.GetString();
            uiR_Chart_TbPg.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.Chart.GetString();
            uiR_Formula_TbPg.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.Formula.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.DataSetCurveType, uiRChartDown_CrvT_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_CrvT_ComBx, (int) DataSetCurveType.Ideal );
            EnumsLocalizer.Localize( LocalizableEnumerator.Phenomenon, uiRChartDown_Phen_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_Phen_ComBx, (int) Phenomenon.Peek );
            EnumsLocalizer.Localize( LocalizableEnumerator.MeanType, uiRChartDown_MeanT_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_MeanT_ComBx, (int) MeanType.Tolerance );
            uiRChartDown_DtSet_Btn.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.DtSet.GetString();
            uiRFormulaDown_CrvsNo2_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.NotApplicable.GetString();
            uiRFormulaDown_Dens2_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.NotApplicable.GetString();
            uiRFormulaDown_CrvsNo2_TxtBx.Text = Settings.Ui.CurvesNo.ToString( Thread.CurrentThread.CurrentCulture );
            uiRFormulaDown_Dens2_TxtBx.Text = (Settings.Ui.PointsNo + 1).ToString( Thread.CurrentThread.CurrentCulture );
            uiRFormulaDown_CrvsNo1_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.CrvsNo1.GetString();
            uiRFormulaDown_Dens1_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.Dens1.GetString();
            uiRChartUp_CrvIdx_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.CrvIdx.GetString();
            uiRChartUp_CrvT_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.CrvT.GetString();
            uiRChartUp_Phen_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.Phen.GetString();
            uiRChartUp_Surr_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.Noise.GetString();
            uiRChartUp_MeanT_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.MeanT.GetString();
            uiRChartUp_DtSet_TxtBx.Text = Translator.GetInstance().Strings.StatAnalysis.Ui.Preview.DtSetSel.GetString();
        }

    }
}
