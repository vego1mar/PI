using System;
using System.Linq;
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
using PI.src.localization.windows;
using PI.src.windows;

namespace PI
{
    public partial class StatisticalAnalysis : Form
    {
        internal GeneratorSettings Settings { get; private set; }
        private IList<IList<CurvesDataManager>> Data { get; set; }
        private IList<double> Noises { get; set; }
        private IList<IList<IList<Series>>> Averages { get; set; }
        private IList<IList<IList<double>>> StdDeviations { get; set; }
        private bool IsFormShown = false;
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        internal StatisticalAnalysis( GeneratorSettings genSets )
        {
            InitializeComponent();
            Settings = genSets;
            Noises = new List<double>() { 0.1, 0.5, 1.0, 11.0 };
            int dimension1 = Enum.GetValues( typeof( Phenomenon ) ).Length;
            int dimension2 = Noises.Count;
            int dimension3 = Enum.GetValues( typeof( MeanType ) ).Length;
            Data = Lists.GetCloneable( dimension1, dimension2, new CurvesDataManager() );
            Averages = Lists.GetNew<Series>( dimension1, dimension2, dimension3 );
            Averages.ToList().ForEach( l2 => l2.ToList().ForEach( l1 => l1.ToList().ForEach( s => SeriesAssist.SetDefaultSettings( s ) ) ) );
            StdDeviations = Lists.GetNew<double>( dimension1, dimension2, dimension3 );
            UpdateUiByGridPresentation();
            UpdateUiBySettings();

            PerformStatisticalAnalysis();
            UpdateUiByPopulatingGridWithStandardDeviations();
            UpdateUiByColoringUiGrids();
            LocalizeWindow();
        }

        private void UpdateUiByGridPresentation()
        {
            const int FIRST_ROW_HEADER_WIDTH = 23 * 6 + 23;
            IList<string> meanNames = new MeanTypesStrings().ToList();
            IList<DataGridView> grids = new List<DataGridView>() { uiLPeek_Grid, uiLSat_Grid };

            grids.ToList().ForEach( grid => {
                GridAssist.AddRows( grid, meanNames.Count );
                grid.RowHeadersWidth = FIRST_ROW_HEADER_WIDTH;
                grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            } );

            for ( int i = 0; i < meanNames.Count; i++ ) {
                grids.ToList().ForEach( grid => GridAssist.AlterRowHeader( grid.Rows[i], meanNames[i] ) );
            }
        }

        private void UpdateUiBySettings()
        {
            UiControls.TrySelectTab( uiL_TbCtrl, (int) Phenomenon.Peek );
            UiControls.TrySelectTab( uiR_TbCtrl, 0 );
            ChartAssist.SetDefaultSettings( uiRChart_Chart );
            EnumsLocalizer.Localize( LocalizableEnumerator.DataSetCurveType, uiRChartDown_CrvT_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_CrvT_ComBx, (int) DataSetCurveType.Ideal );
            uiRChartDown_CrvIdx_Num.Minimum = 0;
            uiRChartDown_CrvIdx_Num.Maximum = Settings.Ui.CurvesNo - 1;
            UiControls.TrySetValue( uiRChartDown_CrvIdx_Num, Settings.Ui.CurvesNo / 2 );
            EnumsLocalizer.Localize( LocalizableEnumerator.Phenomenon, uiRChartDown_Phen_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_Phen_ComBx, (int) Phenomenon.Peek );
            EnumsLocalizer.PopulateComboBox( Lists.Cast<double, string>( Noises ), uiRChartDown_Noises_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_Noises_ComBx, 0 );
            EnumsLocalizer.Localize( LocalizableEnumerator.MeanType, uiRChartDown_MeanT_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_MeanT_ComBx, (int) MeanType.Tolerance );
            GridAssist.SetDefaultSettings( uiLPeek_Grid );
            GridAssist.SetDefaultSettings( uiLSat_Grid );
        }

        private void PerformStatisticalAnalysis()
        {
            double stdDeviation;
            IList<double> averagesValues;
            IList<double> idealValues;

            for ( int i = 0; i < Data.Count; i++ ) {
                for ( int j = 0; j < Noises.Count; j++ ) {
                    Data[i][j].GenerateIdealCurve( Settings.Pcd.Scaffold, Settings.Pcd.Parameters, Settings.Ui.StartX, Settings.Ui.EndX, Settings.Ui.PointsNo );
                    Data[i][j].PropagateIdealCurve( Settings.Ui.CurvesNo );
                    Data[i][j].MakeNoiseOfGaussian( Settings.Ui.CurvesNo, Noises[j] );
                    MakeMalformation( (Phenomenon) i, Data[i][j], Settings.Ui.CurvesNo / 2 );

                    foreach ( string type in Enum.GetNames( typeof( MeanType ) ) ) {
                        Enum.TryParse( type, out MeanType meanType );
                        Data[i][j].TryMakeAverageCurve( meanType, Settings.Ui.CurvesNo );
                        SeriesAssist.CopyPoints( Data[i][j].AverageCurve, Averages[i][j][(int) meanType] );

                        averagesValues = SeriesAssist.GetValues( Averages[i][j][(int) meanType] );
                        idealValues = SeriesAssist.GetValues( Data[i][j].IdealCurve );
                        stdDeviation = Mathematics.GetRelativeStandardDeviation( averagesValues, idealValues );
                        StdDeviations[i][j][(int) meanType] = stdDeviation;
                    }
                }
            }
        }

        private Series GetUiSpecifiedSeries()
        {
            int phenomenonNo = UiControls.TryGetSelectedIndex( uiRChartDown_Phen_ComBx );
            int curveIndex = UiControls.TryGetValue<int>( uiRChartDown_CrvIdx_Num );
            int noiseNo = UiControls.TryGetSelectedIndex( uiRChartDown_Noises_ComBx );
            int meanType = UiControls.TryGetSelectedIndex( uiRChartDown_MeanT_ComBx );
            int curveType = UiControls.TryGetSelectedIndex( uiRChartDown_CrvT_ComBx );

            switch ( (DataSetCurveType) curveType ) {
            case DataSetCurveType.Modified:
                return Data[phenomenonNo][noiseNo].ModifiedCurves[curveIndex];
            case DataSetCurveType.Average:
                return Averages[phenomenonNo][noiseNo][meanType];
            case DataSetCurveType.Ideal:
                return Data[phenomenonNo][noiseNo].IdealCurve;
            }

            return new Series();
        }

        // TODO: provide random alterations
        private void MakeMalformation( Phenomenon idx, CurvesDataManager data, int curveIdx, int yValuesIdx = 0 )
        {
            Series newSeries = data.ModifiedCurves[curveIdx];
            int leftIntervalPoint = Convert.ToInt32( (4.0 / 11.0) * newSeries.Points.Count );
            int middlePoint = newSeries.Points.Count / 2;
            int rightIntervalPoint = Convert.ToInt32( (8.0 / 11.0) * newSeries.Points.Count );
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

        // TODO: Use ChartAssist
        private void UpdateUiByRefreshingChart()
        {
            string signature = string.Empty;

            try {
                MethodBase @base = MethodBase.GetCurrentMethod();
                signature = @base.DeclaringType.Name + "." + @base.Name + "()";
                uiRChart_Chart.Visible = false;
                uiRChart_Chart.Series.Clear();
                uiRChart_Chart.Series.Add( GetUiSpecifiedSeries() );

                if ( !SeriesAssist.IsChartAcceptable( uiRChart_Chart.Series[0] ) ) {
                    AppMessages.StatisticalAnalysis.ExclamationOfPointsNotValidToChart();
                    return;
                }

                ChooseChartSeriesColor( uiRChart_Chart );
                uiRChart_Chart.ChartAreas[0].RecalculateAxesScale();
                uiRChart_Chart.Visible = true;
                uiRChart_Chart.Invalidate();
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
                AppMessages.StatisticalAnalysis.ErrorOfUnrecognized();
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

        private IList<string> GetDataGridColumnsNames( Phenomenon phenomenon )
        {
            switch ( phenomenon ) {
            case Phenomenon.Peek:
                return new List<string>() { uiLPeekGrid_NoiseA_Col.Name, uiLPeekGrid_NoiseB_Col.Name, uiLPeekGrid_NoiseC_Col.Name, uiLPeekGrid_NoiseD_Col.Name };
            case Phenomenon.Saturation:
                return new List<string>() { uiLSatGrid_NoiseA_Col.Name, uiLSatGrid_NoiseB_Col.Name, uiLSatGrid_NoiseC_Col.Name, uiLSatGrid_NoiseD_Col.Name };
            }

            return new List<string>().AsReadOnly();
        }

        // TODO: Use GridAssist.PopulateColumn<T>()
        private void UpdateUiByPopulatingGridWithStandardDeviations()
        {
            DataGridView[] grids = { uiLPeek_Grid, uiLSat_Grid };

            if ( grids.Count() != Enum.GetNames( typeof( Phenomenon ) ).Count() ) {
                return;
            }

            foreach ( string name in Enum.GetNames( typeof( Phenomenon ) ) ) {
                Enum.TryParse( name, out Phenomenon phenomenon );
                IList<string> columns = GetDataGridColumnsNames( phenomenon );

                foreach ( string mean in Enum.GetNames( typeof( MeanType ) ) ) {
                    Enum.TryParse( mean, out MeanType type );

                    for ( int k = 0; k < Noises.Count; k++ ) {
                        string stdDeviation = Strings.TryFormatAsNumeric( 4, StdDeviations[(int) phenomenon][k][(int) type] );
                        grids[(int) phenomenon].Rows[(int) type].Cells[columns[k]].Value = stdDeviation;
                    }
                }
            }
        }

        private void UpdateUiByColoringUiGrids()
        {
            DataGridView[] grids = { uiLPeek_Grid, uiLSat_Grid };

            if ( grids.Count() != Enum.GetValues( typeof( Phenomenon ) ).Length ) {
                return;
            }

            for ( int i = 0; i < Enum.GetValues( typeof( Phenomenon ) ).Length; i++ ) {
                for ( int j = 0; j < Noises.Count; j++ ) {
                    int maxValueIdx = StdDeviations[i][j].IndexOf( StdDeviations[i][j].Max() );
                    int minValueIdx = StdDeviations[i][j].IndexOf( StdDeviations[i][j].Min() );
                    grids[i].Rows[maxValueIdx].Cells[j].Style.BackColor = System.Drawing.Color.OrangeRed;
                    grids[i].Rows[minValueIdx].Cells[j].Style.BackColor = System.Drawing.Color.SpringGreen;
                }
            }
        }

        private void LocalizeWindow()
        {
            StatisticalAnalysisStrings names = new StatisticalAnalysisStrings();
            Text = names.Form.Text.GetString();

            // Standard deviation
            uiL_StdDev_TxtBx.Text = names.Ui.StandardDeviationTitle.GetString();
            uiL_Peek_TbPg.Text = names.Ui.StandardDeviationPeek.GetString();
            uiL_Sat_TbPg.Text = names.Ui.StandardDeviationSaturation.GetString();
            GridAssist.AlterColumnHeader( uiLPeekGrid_NoiseA_Col, names.Ui.StandardDeviationNoise.GetString() + ' ' + Noises[0], true );
            GridAssist.AlterColumnHeader( uiLPeekGrid_NoiseB_Col, names.Ui.StandardDeviationNoise.GetString() + ' ' + Noises[1], true );
            GridAssist.AlterColumnHeader( uiLPeekGrid_NoiseC_Col, names.Ui.StandardDeviationNoise.GetString() + ' ' + Noises[2], true );
            GridAssist.AlterColumnHeader( uiLPeekGrid_NoiseD_Col, names.Ui.StandardDeviationNoise.GetString() + ' ' + Noises[3], true );
            GridAssist.AlterColumnHeader( uiLSatGrid_NoiseA_Col, names.Ui.StandardDeviationNoise.GetString() + ' ' + Noises[0], true );
            GridAssist.AlterColumnHeader( uiLSatGrid_NoiseB_Col, names.Ui.StandardDeviationNoise.GetString() + ' ' + Noises[1], true );
            GridAssist.AlterColumnHeader( uiLSatGrid_NoiseC_Col, names.Ui.StandardDeviationNoise.GetString() + ' ' + Noises[2], true );
            GridAssist.AlterColumnHeader( uiLSatGrid_NoiseD_Col, names.Ui.StandardDeviationNoise.GetString() + ' ' + Noises[3], true );

            // Preview
            uiR_Prv_TxtBx.Text = names.Ui.PreviewTitle.GetString();
            uiR_Chart_TbPg.Text = names.Ui.PreviewChart.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.DataSetCurveType, uiRChartDown_CrvT_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_CrvT_ComBx, (int) DataSetCurveType.Ideal );
            EnumsLocalizer.Localize( LocalizableEnumerator.Phenomenon, uiRChartDown_Phen_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_Phen_ComBx, (int) Phenomenon.Peek );
            EnumsLocalizer.Localize( LocalizableEnumerator.MeanType, uiRChartDown_MeanT_ComBx );
            UiControls.TrySetSelectedIndex( uiRChartDown_MeanT_ComBx, (int) MeanType.NN );
            uiRChartDown_DtSet_Btn.Text = names.Ui.PreviewDataSet.GetString();
            uiRChartUp_CrvIdx_TxtBx.Text = names.Ui.PreviewCurveIndex.GetString();
            uiRChartUp_CrvT_TxtBx.Text = names.Ui.PreviewCurveType.GetString();
            uiRChartUp_Phen_TxtBx.Text = names.Ui.PreviewPhenomenon.GetString();
            uiRChartUp_Surr_TxtBx.Text = names.Ui.PreviewNoise.GetString();
            uiRChartUp_MeanT_TxtBx.Text = names.Ui.PreviewMeanType.GetString();
            uiRChartUp_DtSet_TxtBx.Text = names.Ui.PreviewDataSetSelection.GetString();
        }

        #region Event handlers

        private void OnShown( object sender, EventArgs e )
        {
            IsFormShown = true;
            UpdateUiByRefreshingChart();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ')' );
        }

        private void OnLoad( object sender, EventArgs e )
        {
            uiLPeek_Grid.Select();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ')' );
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            Settings = null;
            Data = null;
            Noises = null;
            Averages = null;
            StdDeviations = null;
            Dispose();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ',' + e.CloseReason + ')' );
        }

        private void OnFormClosed( object sender, FormClosedEventArgs e )
        {
            IsFormShown = false;
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ',' + e.CloseReason + ')' );
        }

        private void OnPhenomenonSelection( object sender, EventArgs e )
        {
            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }

            int selection = UiControls.TryGetSelectedIndex( uiRChartDown_Phen_ComBx );
            UiControls.TrySelectTab( uiL_TbCtrl, selection );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + IsFormShown + ',' + ((Phenomenon) selection) + ')' );
        }

        private void OnSurroundingsSelection( object sender, EventArgs e )
        {
            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + IsFormShown + ',' + Noises[UiControls.TryGetSelectedIndex( uiRChartDown_Noises_ComBx )] + ')' );
        }

        private void OnMeanTypeSelection( object sender, EventArgs e )
        {
            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + IsFormShown + ',' + (MeanType) UiControls.TryGetSelectedIndex( uiRChartDown_MeanT_ComBx ) + ')' );
        }

        private void OnTabSelection( object sender, EventArgs e )
        {
            int selection = UiControls.TryGetSelectedIndex( uiL_TbCtrl );
            UiControls.TrySetSelectedIndex( uiRChartDown_Phen_ComBx, selection );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (Phenomenon) selection + ')' );
        }

        private void OnCurveIndexAlteration( object sender, EventArgs e )
        {
            int curveIndex = UiControls.TryGetValue<int>( uiRChartDown_CrvIdx_Num );

            if ( curveIndex < 0 || curveIndex >= Settings.Ui.CurvesNo ) {
                log.Info( MethodBase.GetCurrentMethod().Name + '(' + curveIndex + ')' );
                AppMessages.StatisticalAnalysis.ExclamationOfValueOutOfRange();
                return;
            }

            if ( IsFormShown ) {
                UpdateUiByRefreshingChart();
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + IsFormShown + ',' + curveIndex + ')' );
        }

        private void OnCurveTypeSelection( object sender, EventArgs e )
        {
            DataSetCurveType curveType = (DataSetCurveType) UiControls.TryGetSelectedIndex( uiRChartDown_CrvT_ComBx );

            switch ( curveType ) {
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

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + IsFormShown + ',' + curveType + ')' );
        }

        private void OnShowDatasetClick( object sender, EventArgs e )
        {
            int curveIndex = UiControls.TryGetValue<int>( uiRChartDown_CrvIdx_Num );
            string signature = MethodBase.GetCurrentMethod().Name + '(' + curveIndex + ')';
            log.Info( signature );

            if ( curveIndex < 0 || curveIndex >= Settings.Ui.CurvesNo ) {
                return;
            }

            try {
                Series controlsSpecifiedSeries = GetUiSpecifiedSeries();

                using ( var dialog = new GridPreviewer( controlsSpecifiedSeries ) ) {
                    UiControls.TryShowDialog( dialog, this );
                }
            }
            catch ( OutOfMemoryException ex ) {
                AppMessages.General.StopOfOutOfMemoryException();
                log.Fatal( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }
        }

        #endregion
    }
}
