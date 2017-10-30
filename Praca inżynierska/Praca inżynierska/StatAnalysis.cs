﻿using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    public partial class StatAnalysis : Form
    {

        internal GenSettings Settings { get; private set; }
        private List<List<CurvesDataManager>> Data { get; set; }
        private List<double> Surroundings { get; set; }
        private List<List<List<Series>>> Averages { get; set; }
        private bool IsFormShown = false;
        private List<List<List<double>>> StdDeviations { get; set; }

        public enum PhenomenonIndex
        {
            Peek = 0,
            Deformation = 1
        }

        private struct DatasetControlsValues
        {
            public int PhenomNo { get; set; }
            public int NoiseNo { get; set; }
            public int CrvIdx { get; set; }
            public int MeanT { get; set; }
            public int CrvT { get; set; }
        }

        internal StatAnalysis( GenSettings.PcdGenSettings genSets )
        {
            InitializeComponent();
            InitializePropertySettings( genSets );
            InitializeProperties();
            SetWindowDefaults();
            PerformStatisticalAnalysis();
            UpdateUiByPopulatingGridWithStandardDeviations();
            UpdateUiByColoringUiGrids();
        }

        private void InitializeProperties()
        {
            InitializePropertySurroundings();
            InitializePropertyData();
            InitializePropertyAverages();
            InitializeDataGridViewUiFields();
            InitializePropertyStdDeviations();
        }

        private void InitializePropertySettings( GenSettings.PcdGenSettings genSets )
        {
            Settings = new GenSettings();

            if ( genSets != null ) {
                Settings.Pcd.Scaffold = genSets.Scaffold;
                Settings.Pcd.Parameters = genSets.Parameters;
            }

            Settings.Ui.NumberOfCurves = 100;
            Settings.Ui.StartingXPoint = -2;
            Settings.Ui.EndingXPoint = 2;
            Settings.Ui.PointsDensity = 400;
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

            foreach ( PhenomenonIndex idx in Enum.GetValues( typeof( PhenomenonIndex ) ) ) {
                Data.Add( new List<CurvesDataManager>() );                                      // [a][-] 

                for ( int i = 0; i < Surroundings.Count; i++ ) {
                    Data[(int) idx].Add( new CurvesDataManager( Settings.Pcd.Parameters ) );    // [-][b] 
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

            foreach ( PhenomenonIndex idx in Enum.GetValues( typeof( PhenomenonIndex ) ) ) {
                Averages.Add( new List<List<Series>>() );                                           // [a][-][-] 

                for ( int i = 0; i < Surroundings.Count; i++ ) {
                    Averages[(int) idx].Add( new List<Series>() );                                  // [-][b][-] 

                    for ( int j = 0; j < Enum.GetNames( typeof( Enums.MeanType ) ).Length; j++ ) {
                        Averages[(int) idx][i].Add( new Series() );                                 // [-][-][c]
                        string seriesName = nameof( Averages ) + "[" + (int) idx + "][" + i + "][" + j + "]";
                        CurvesDataManager.SetDefaultProperties( Averages[(int) idx][i][j], seriesName );
                    }
                }
            }
        }

        private void InitializeDataGridViewUiFields()
        {
            List<string> peekColumns = GetDataGridColumnsNames( PhenomenonIndex.Peek );
            List<string> deformColumns = GetDataGridColumnsNames( PhenomenonIndex.Deformation );
            List<string> meanNames = Enum.GetNames( typeof( Enums.MeanType ) ).ToList();

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

            foreach ( PhenomenonIndex idx in Enum.GetValues( typeof( PhenomenonIndex ) ) ) {
                StdDeviations.Add( new List<List<double>>() );                                           // [a][-][-] 

                for ( int i = 0; i < Surroundings.Count; i++ ) {
                    StdDeviations[(int) idx].Add( new List<double>() );                                  // [-][b][-] 

                    for ( int j = 0; j < Enum.GetNames( typeof( Enums.MeanType ) ).Length; j++ ) {
                        StdDeviations[(int) idx][i].Add( new double() );                                 // [-][-][c]
                    }
                }
            }
        }

        private void SetWindowDefaults()
        {
            WinFormsHelper.SelectTabSafe( uiL_TbCtrl, (int) PhenomenonIndex.Peek );
            WinFormsHelper.SelectTabSafe( uiR_TbCtrl, 0 );
            CurvesDataManager.SetDefaultProperties( uiRChart_Chart );
            AddDataSetCurveTypes( uiRChartDown_CrvT_ComBx );
            WinFormsHelper.SetSelectedIndexSafe( uiRChartDown_CrvT_ComBx, (int) Enums.DataSetCurveType.Pattern );
            uiRChartDown_CrvIdx_Num.Minimum = 0;
            uiRChartDown_CrvIdx_Num.Maximum = Settings.Ui.NumberOfCurves - 1;
            WinFormsHelper.SetValue( uiRChartDown_CrvIdx_Num, Settings.Ui.NumberOfCurves / 2 );
            AddPhenomenonsIndexNames( uiRChartDown_Phen_ComBx );
            WinFormsHelper.SetSelectedIndexSafe( uiRChartDown_Phen_ComBx, (int) PhenomenonIndex.Peek );
            AddSurroundings( uiRChartDown_Surr_ComBx );
            WinFormsHelper.SetSelectedIndexSafe( uiRChartDown_Surr_ComBx, 0 );
            AddMeanTypes( uiRChartDown_MeanT_ComBx );
            WinFormsHelper.SetSelectedIndexSafe( uiRChartDown_MeanT_ComBx, (int) Enums.MeanType.CustomTolerance );
            uiRFormulaDown_CrvsNo2_TxtBx.Text = Settings.Ui.NumberOfCurves.ToString();
            uiRFormulaDown_Dens2_TxtBx.Text = (Settings.Ui.PointsDensity + 1).ToString();
        }

        private void UiStatAnalysis_Load( object sender, EventArgs e )
        {
            uiLPeek_Grid.Select();
        }

        private void AddDataSetCurveTypes( ComboBox comboBox )
        {
            foreach ( string type in Enum.GetNames( typeof( Enums.DataSetCurveType ) ) ) {
                comboBox.Items.Add( type );
            }
        }

        private void AddPhenomenonsIndexNames( ComboBox comboBox )
        {
            foreach ( string phenomenon in Enum.GetNames( typeof( PhenomenonIndex ) ) ) {
                comboBox.Items.Add( phenomenon );
            }
        }

        private void AddSurroundings( ComboBox comboBox )
        {
            foreach ( double surrounding in Surroundings ) {
                comboBox.Items.Add( surrounding.ToString( System.Globalization.CultureInfo.InvariantCulture ) );
            }
        }

        private void AddMeanTypes( ComboBox comboBox )
        {
            foreach ( string type in Enum.GetNames( typeof( Enums.MeanType ) ) ) {
                comboBox.Items.Add( type );
            }
        }

        private void PerformStatisticalAnalysis()
        {
            for ( int i = 0; i < Data.Count; i++ ) {
                for ( int j = 0; j < Surroundings.Count; j++ ) {
                    Data[i][j].GeneratePatternCurve( Settings.Pcd.Scaffold, Settings.Ui.StartingXPoint, Settings.Ui.EndingXPoint, Settings.Ui.PointsDensity );
                    Data[i][j].SpreadPatternCurveSetToGeneratedCurveSet( Settings.Ui.NumberOfCurves );
                    Data[i][j].MakeGaussianNoiseForGeneratedCurves( Settings.Ui.NumberOfCurves, Surroundings[j] );
                    MakePeekOrDeformation( (PhenomenonIndex) i, Data[i][j], Settings.Ui.NumberOfCurves / 2 );
                    double patternCurveMean = GetArithmeticMeanFromSeriesValues( Data[i][j].PatternCurveSet );

                    foreach ( string type in Enum.GetNames( typeof( Enums.MeanType ) ) ) {
                        Enum.TryParse( type, out Enums.MeanType meanType );
                        Data[i][j].MakeAverageCurveFromGeneratedCurves( meanType, Settings.Ui.NumberOfCurves );
                        AddSeriesPoints( Averages[i][j][Convert.ToInt32( meanType )], Data[i][j].AverageCurveSet );
                        double stdDeviation = GetRelativeStandardDeviationFromSeriesValues( Averages[i][j][(int) meanType], patternCurveMean );
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

            using ( var dialog = new GridPreviewer( GetSeriesSpecifiedByControls() ) ) {
                WinFormsHelper.ShowDialogSafe( dialog, this );
            }
        }

        private void UiRightChartDown_CurveType_ComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            switch ( (Enums.DataSetCurveType) WinFormsHelper.GetSelectedIndexSafe( uiRChartDown_CrvT_ComBx ) ) {
            case Enums.DataSetCurveType.Generated:
                uiRChartDown_CrvIdx_Num.Enabled = true;
                uiRChartDown_MeanT_ComBx.Enabled = false;
                break;
            case Enums.DataSetCurveType.Average:
                uiRChartDown_CrvIdx_Num.Enabled = false;
                uiRChartDown_MeanT_ComBx.Enabled = true;
                break;
            case Enums.DataSetCurveType.Pattern:
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

            switch ( (Enums.DataSetCurveType) indices.CrvT ) {
            case Enums.DataSetCurveType.Generated:
                return Data[indices.PhenomNo][indices.NoiseNo].GeneratedCurvesSet[indices.CrvIdx];
            case Enums.DataSetCurveType.Average:
                return Averages[indices.PhenomNo][indices.NoiseNo][indices.MeanT];
            case Enums.DataSetCurveType.Pattern:
                return Data[indices.PhenomNo][indices.NoiseNo].PatternCurveSet;
            }

            return new Series();
        }

        private DatasetControlsValues GetDatasetControlsValues()
        {
            return new DatasetControlsValues() {
                PhenomNo = WinFormsHelper.GetSelectedIndexSafe( uiRChartDown_Phen_ComBx ),
                CrvIdx = WinFormsHelper.GetValue<int>( uiRChartDown_CrvIdx_Num ),
                NoiseNo = WinFormsHelper.GetSelectedIndexSafe( uiRChartDown_Surr_ComBx ),
                MeanT = WinFormsHelper.GetSelectedIndexSafe( uiRChartDown_MeanT_ComBx ),
                CrvT = WinFormsHelper.GetSelectedIndexSafe( uiRChartDown_CrvT_ComBx )
            };
        }

        private void MakePeekOrDeformation( PhenomenonIndex idx, CurvesDataManager data, int curveIdx, int yValuesIdx = 0 )
        {
            Series newSeries = data.GeneratedCurvesSet[curveIdx];
            int leftIntervalPoint = Convert.ToInt32( (3.0 / 7.0) * newSeries.Points.Count );
            int middlePoint = newSeries.Points.Count / 2;
            int rightIntervalPoint = Convert.ToInt32( (5.0 / 7.0) * newSeries.Points.Count );
            double maxValue = newSeries.Points.FindMaxByValue().YValues[yValuesIdx];
            double minValue = newSeries.Points.FindMinByValue().YValues[yValuesIdx];
            double malformationValue = 0.5 * (maxValue - minValue);

            switch ( idx ) {
            case PhenomenonIndex.Peek:
                newSeries.Points[middlePoint].YValues[yValuesIdx] += malformationValue;
                break;
            case PhenomenonIndex.Deformation:
                OverrideSeriesValuesWithinInterval( newSeries, leftIntervalPoint, rightIntervalPoint, malformationValue, yValuesIdx );
                break;
            }

            data.AbsorbSeriesPoints( newSeries, (int) Enums.DataSetCurveType.Generated, curveIdx );
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
                MsgBxShower.Stat.Preview.Chart.ValueOutOfRangeProblem();
                return;
            }

            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }
        }

        private bool IsCurveIndexProperValue()
        {
            int value = WinFormsHelper.GetValue<int>( uiRChartDown_CrvIdx_Num );

            if ( value < 0 || value >= Settings.Ui.NumberOfCurves ) {
                return false;
            }

            return true;
        }

        private void UiRightChartDown_Phenomenon_ComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }

            WinFormsHelper.SelectTabSafe( uiL_TbCtrl, WinFormsHelper.GetSelectedIndexSafe( uiRChartDown_Phen_ComBx ) );
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
            try {
                uiRChart_Chart.Visible = false;
                uiRChart_Chart.Series.Clear();
                uiRChart_Chart.Series.Add( GetSeriesSpecifiedByControls() );

                if ( !CurvesDataManager.IsCurvePointsSetValid( uiRChart_Chart.Series[0] ) ) {
                    MsgBxShower.Stat.Preview.Chart.PointsNotValidToChartProblem();
                    return;
                }

                ChooseChartSeriesColor( uiRChart_Chart );
                uiRChart_Chart.ChartAreas[0].RecalculateAxesScale();
                uiRChart_Chart.Visible = true;
                uiRChart_Chart.Invalidate();
            }
            catch ( Exception ex ) {
                MsgBxShower.Stat.Preview.Chart.UnrecognizedError();
                Logger.WriteException( ex );
            }
        }

        private void ChooseChartSeriesColor( Chart chart, int seriesIdx = 0 )
        {
            switch ( (Enums.DataSetCurveType) WinFormsHelper.GetSelectedIndexSafe( uiRChartDown_CrvT_ComBx ) ) {
            case Enums.DataSetCurveType.Generated:
                chart.Series[seriesIdx].Color = System.Drawing.Color.Crimson;
                break;
            case Enums.DataSetCurveType.Pattern:
                chart.Series[seriesIdx].Color = System.Drawing.Color.Black;
                break;
            case Enums.DataSetCurveType.Average:
                chart.Series[seriesIdx].Color = System.Drawing.Color.ForestGreen;
                break;
            }
        }

        private void StatAnalysis_Shown( object sender, EventArgs e )
        {
            IsFormShown = true;
            UpdateUiByRefreshingChart();
        }

        private void StatAnalysis_FormClosed( object sender, FormClosedEventArgs e )
        {
            IsFormShown = false;
        }

        private List<string> GetDataGridColumnsNames( PhenomenonIndex phenomenon )
        {
            switch ( phenomenon ) {
            case PhenomenonIndex.Peek:
                return new List<string>() {
                    nameof( uiLPeekGrid_Noise01_Col ),
                    nameof( uiLPeekGrid_Noise05_Col ),
                    nameof( uiLPeekGrid_Noise1_Col ),
                    nameof( uiLPeekGrid_Noise2_Col )
                };
            case PhenomenonIndex.Deformation:
                return new List<string>() {
                    nameof( uiLDeformGrid_Noise01_Col ),
                    nameof( uiLDeformGrid_Noise05_Col ),
                    nameof( uiLDeformGrid_Noise1_Col ),
                    nameof( uiLDeformGrid_Noise2_Col )
                };
            }

            return new List<string>();
        }

        private double GetArithmeticMeanFromSeriesValues( Series series, int yValuesIdx = 0 )
        {
            double sum = 0.0;

            foreach ( DataPoint point in series.Points ) {
                sum += point.YValues[yValuesIdx];
            }

            return sum / Convert.ToDouble( series.Points.Count );
        }

        private double GetRelativeStandardDeviationFromSeriesValues( Series series, double patternMean, int yValuesIdx = 0 )
        {
            double sum = 0.0;
            double difference;

            foreach ( DataPoint point in series.Points ) {
                difference = point.YValues[yValuesIdx] - patternMean;
                sum += difference * difference;
            }

            return Math.Sqrt( sum / Convert.ToDouble( series.Points.Count ) );
        }

        private void UpdateUiByPopulatingGridWithStandardDeviations()
        {
            DataGridView[] grids = { uiLPeek_Grid, uiLDeform_Grid };

            if ( grids.Count() != Enum.GetNames( typeof( PhenomenonIndex ) ).Count() ) {
                return;
            }

            foreach ( string name in Enum.GetNames( typeof( PhenomenonIndex ) ) ) {
                Enum.TryParse( name, out PhenomenonIndex phenomenon );
                List<string> columns = GetDataGridColumnsNames( phenomenon );

                foreach ( string mean in Enum.GetNames( typeof( Enums.MeanType ) ) ) {
                    Enum.TryParse( mean, out Enums.MeanType type );

                    for ( int k = 0; k < Surroundings.Count; k++ ) {
                        string stdDeviation = StringFormatter.FormatAsNumeric( 4, StdDeviations[(int) phenomenon][k][(int) type] );
                        grids[(int) phenomenon].Rows[(int) type].Cells[columns[k]].Value = stdDeviation;
                    }
                }
            }
        }

        private void UiLeft_TabControl_SelectedIndexChanged( object sender, EventArgs e )
        {
            WinFormsHelper.SetSelectedIndexSafe( uiRChartDown_Phen_ComBx, WinFormsHelper.GetSelectedTab( uiL_TbCtrl ) );
        }

        private void UpdateUiByColoringUiGrids()
        {
            DataGridView[] grids = { uiLPeek_Grid, uiLDeform_Grid };

            if ( grids.Count() != Enum.GetValues( typeof( PhenomenonIndex ) ).Length ) {
                return;
            }

            for ( int i = 0; i < Enum.GetValues( typeof( PhenomenonIndex ) ).Length; i++ ) {
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

        private void StatAnalysis_FormClosing( object sender, FormClosingEventArgs e )
        {
            Settings = null;
            Data = null;
            Surroundings = null;
            Averages = null;
            StdDeviations = null;
            Dispose();
        }

    }
}
