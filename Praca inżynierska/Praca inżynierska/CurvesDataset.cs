using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    class CurvesDataset
    {

        public const string PATTERN_CURVE_SERIES_NAME = "PatternCurveSeries";
        public const string GENERATED_CURVE_SERIES_NAME = "GeneratedCurveSeries";
        private const double ACCEPTABLE_MAX_VALUE = 9228162514264337593543950335.0;
        private const double ACCEPTABLE_MIN_VALUE = -ACCEPTABLE_MAX_VALUE;
        public Series PatternCurveSet { get; private set; }
        public List<Series> GeneratedCurvesSet { get; private set; }

        public CurvesDataset()
        {
            PatternCurveSet = new Series();
            GeneratedCurvesSet = new List<Series>();
            SetDefaultProperties( PatternCurveSet, PATTERN_CURVE_SERIES_NAME );
        }

        public static void SetDefaultProperties( Series series, string name )
        {
            series.Name = name;
            series.BorderWidth = 5;
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Black;
            series.IsVisibleInLegend = false;
            series.IsXValueIndexed = true;
            series.YValueType = ChartValueType.Double;
            series.YValueType = ChartValueType.Double;
            series.YValuesPerPoint = 1;
        }

        public bool GeneratePatternCurve( int curveScaffoldType, double startingXPoint, double endingXPoint, int pointsDensity )
        {
            switch ( curveScaffoldType ) {
            case Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL:
                GeneratePolynomialPatternCurve( startingXPoint, endingXPoint, pointsDensity );
                return IsPatternCurveSetPointsValid();
            case Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                GenerateHyperbolicPatternCurve( startingXPoint, endingXPoint, pointsDensity );
                return IsPatternCurveSetPointsValid();
            }

            return false;
        }

        private void GeneratePolynomialPatternCurve( double startingXPoint, double endingXPoint, int pointsDensity )
        {
            PatternCurveSet.Points.Clear();
            SetDefaultProperties( PatternCurveSet, PATTERN_CURVE_SERIES_NAME );
            double intervalLength = endingXPoint - startingXPoint;
            double densityUnit = intervalLength / pointsDensity;

            for ( int i = 0; i <= pointsDensity; i++ ) {
                double x = startingXPoint + (densityUnit * i);
                double leftFraction = (PreSets.Pcd.Parameters.A * Math.Pow( x, PreSets.Pcd.Parameters.B )) / PreSets.Pcd.Parameters.C;
                double rightFraction = (PreSets.Pcd.Parameters.D * Math.Pow( x, PreSets.Pcd.Parameters.E )) / PreSets.Pcd.Parameters.F;
                double polynomial = leftFraction + rightFraction + PreSets.Pcd.Parameters.I;
                PatternCurveSet.Points.AddXY( x, polynomial );
            }
        }

        private void GenerateHyperbolicPatternCurve( double startingXPoint, double endingXPoint, int pointsDensity )
        {
            PatternCurveSet.Points.Clear();
            SetDefaultProperties( PatternCurveSet, PATTERN_CURVE_SERIES_NAME );
            double intervalLength = endingXPoint - startingXPoint;
            double densityUnit = intervalLength / pointsDensity;

            for ( int i = 0; i <= pointsDensity; i++ ) {
                double x = startingXPoint + (densityUnit * i);
                double numerator = Math.Pow( Math.E, x ) - Math.Pow( Math.E, -x );
                double fraction = numerator / PreSets.Pcd.Parameters.G;
                PatternCurveSet.Points.AddXY( x, fraction + PreSets.Pcd.Parameters.J );
            }
        }

        public void AbsorbSeriesPoints( Series series, int curveType, int curveIndex )
        {
            if ( series == null ) {
                return;
            }

            switch ( curveType ) {
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_PATTERN:
                AbsorbSeriesForPatternCurve( series );
                break;
            case Constants.Ui.Panel.Datasheet.CURVE_TYPE_GENERATED:
                AbsorbSeriesForSpecifiedGeneratedCurve( series, curveIndex - 1 );
                break;
            }
        }

        private void AbsorbSeriesForPatternCurve( Series series )
        {
            for ( int i = 0; i < series.Points.Count; i++ ) {
                PatternCurveSet.Points[i].XValue = series.Points[i].XValue;
                PatternCurveSet.Points[i].YValues[0] = series.Points[i].YValues[0];
            }
        }

        private void AbsorbSeriesForSpecifiedGeneratedCurve( Series series, int collectionItemNumber )
        {
            for ( int i = 0; i < series.Points.Count; i++ ) {
                GeneratedCurvesSet[collectionItemNumber].Points[i].XValue = series.Points[i].XValue;
                GeneratedCurvesSet[collectionItemNumber].Points[i].YValues[0] = series.Points[i].YValues[0];
            }
        }

        public void SpreadPatternCurveSetToGeneratedCurveSet( int numberOfCurves )
        {
            GeneratedCurvesSet = new List<Series>();

            for ( int i = 0; i < numberOfCurves; i++ ) {
                Series series = new Series();
                SetDefaultProperties( series, GENERATED_CURVE_SERIES_NAME + i );
                RecopySeriesPoints( PatternCurveSet, series );
                GeneratedCurvesSet.Add( series );
            }
        }

        private void RecopySeriesPoints( Series source, Series target )
        {
            for ( int i = 0; i < source.Points.Count; i++ ) {
                target.Points.AddXY( source.Points[i].XValue, source.Points[i].YValues[0] );
            }
        }

        private bool IsPatternCurveSetPointsValid()
        {
            for ( int i = 0; i < PatternCurveSet.Points.Count; i++ ) {
                double x = PatternCurveSet.Points[i].XValue;
                double y = PatternCurveSet.Points[i].YValues[0];

                if ( x > ACCEPTABLE_MAX_VALUE || x < ACCEPTABLE_MIN_VALUE || y > ACCEPTABLE_MAX_VALUE || y < ACCEPTABLE_MIN_VALUE ) {
                    return false;
                }
            }

            return true;
        }

        public void RemoveInvalidPointsFromPatternCurveSet()
        {
            Series series = new Series();
            SetDefaultProperties( series, PATTERN_CURVE_SERIES_NAME + "Validated" );

            for ( int i = 0; i < PatternCurveSet.Points.Count; i++ ) {
                double x = PatternCurveSet.Points[i].XValue;
                double y = PatternCurveSet.Points[i].YValues[0];

                if ( (x < ACCEPTABLE_MAX_VALUE && x > ACCEPTABLE_MIN_VALUE) && (y < ACCEPTABLE_MAX_VALUE && y > ACCEPTABLE_MIN_VALUE) ) {
                    series.Points.AddXY( x, y );
                }
            }

            PatternCurveSet.Points.Clear();
            RecopySeriesPoints( series, PatternCurveSet );
        }

        public static void SetDefaultProperties( Chart chart, int chartAreaIndex = 0, int legendIndex = 0, int seriesIndex = 0 )
        {
            chart.ChartAreas[chartAreaIndex].AxisX.IntervalType = DateTimeIntervalType.Number;
            chart.ChartAreas[chartAreaIndex].AxisX.IsLabelAutoFit = false;
            chart.ChartAreas[chartAreaIndex].AxisX.IsMarginVisible = false;
            chart.ChartAreas[chartAreaIndex].AxisX.LabelAutoFitMaxFontSize = 8;
            chart.ChartAreas[chartAreaIndex].AxisX.LabelAutoFitMinFontSize = 8;
            chart.ChartAreas[chartAreaIndex].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
            chart.ChartAreas[chartAreaIndex].AxisX.LabelStyle.Font = new Font( "Consolas", 8F );
            chart.ChartAreas[chartAreaIndex].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart.ChartAreas[chartAreaIndex].AxisX.MajorTickMark.Enabled = false;
            chart.ChartAreas[chartAreaIndex].AxisX.TitleFont = new Font( "Consolas", 8F );
            chart.ChartAreas[chartAreaIndex].AxisY.IntervalType = DateTimeIntervalType.Number;
            chart.ChartAreas[chartAreaIndex].AxisY.IsLabelAutoFit = false;
            chart.ChartAreas[chartAreaIndex].AxisY.LabelAutoFitMaxFontSize = 8;
            chart.ChartAreas[chartAreaIndex].AxisY.LabelStyle.Font = new Font( "Consolas", 8F );
            chart.ChartAreas[chartAreaIndex].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart.ChartAreas[chartAreaIndex].AxisY.MajorTickMark.Enabled = false;
            chart.ChartAreas[chartAreaIndex].AxisY.TitleFont = new Font( "Consolas", 8F );
            chart.ChartAreas[chartAreaIndex].BackColor = Color.White;
            chart.ChartAreas[chartAreaIndex].IsSameFontSizeForAllAxes = true;
            chart.Legends[legendIndex].Enabled = false;
            chart.Series[seriesIndex].BorderWidth = 5;
            chart.Series[seriesIndex].ChartType = SeriesChartType.Line;
            chart.Series[seriesIndex].Color = Color.Black;
            chart.Series[seriesIndex].CustomProperties = "EmptyPointValue=Zero";
            chart.Series[seriesIndex].Font = new Font( "Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238 );
            chart.Series[seriesIndex].IsVisibleInLegend = false;
            chart.Series[seriesIndex].IsXValueIndexed = true;
            chart.Series[seriesIndex].XValueType = ChartValueType.Double;
            chart.Series[seriesIndex].YValueType = ChartValueType.Double;
        }

    }
}
