using System;
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
            SetDefaultPropertiesForChartingSeries( PatternCurveSet, PATTERN_CURVE_SERIES_NAME );
        }

        public static void SetDefaultPropertiesForChartingSeries( Series series, string name )
        {
            series.Name = name;
            series.Color = System.Drawing.Color.Black;
            series.IsVisibleInLegend = false;
            series.IsXValueIndexed = true;
            series.ChartType = SeriesChartType.Line;
            series.YValueType = ChartValueType.Double;
            series.YValuesPerPoint = 1;
        }

        public bool GeneratePatternCurve( int curveScaffoldType, int numberOfPoints, int startingXPoint )
        {
            switch ( curveScaffoldType ) {
            case Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL:
                GeneratePolynomialPatternCurve( numberOfPoints, startingXPoint );
                return IsPatternCurveSetPointsValid();
            case Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                GenerateHyperbolicPatternCurve( numberOfPoints, startingXPoint );
                return IsPatternCurveSetPointsValid();
            }

            return false;
        }

        private void GeneratePolynomialPatternCurve( int numberOfPoints, int startingXPoint )
        {
            PatternCurveSet.Points.Clear();
            SetDefaultPropertiesForChartingSeries( PatternCurveSet, PATTERN_CURVE_SERIES_NAME );

            for ( int i = 1; i <= numberOfPoints; i++ ) {
                double offset = startingXPoint + i - 1;
                double leftFraction = (PreSets.Pcd.Parameters.A * Math.Pow( offset, PreSets.Pcd.Parameters.B )) / PreSets.Pcd.Parameters.C;
                double rightFraction = (PreSets.Pcd.Parameters.D * Math.Pow( offset, PreSets.Pcd.Parameters.E )) / PreSets.Pcd.Parameters.F;
                double polynomial = leftFraction + rightFraction;
                PatternCurveSet.Points.AddXY( offset, polynomial );
            }
        }

        private void GenerateHyperbolicPatternCurve( int numberOfPoints, int startingXPoint )
        {
            PatternCurveSet.Points.Clear();
            SetDefaultPropertiesForChartingSeries( PatternCurveSet, PATTERN_CURVE_SERIES_NAME );

            for ( int i = 1; i <= numberOfPoints; i++ ) {
                double offset = startingXPoint + i - 1;
                double numerator = Math.Pow( Math.E, offset ) - Math.Pow( Math.E, -offset );
                double fraction = numerator / PreSets.Pcd.Parameters.C;
                PatternCurveSet.Points.AddXY( offset, fraction );
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
                SetDefaultPropertiesForChartingSeries( series, GENERATED_CURVE_SERIES_NAME + i );
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
            SetDefaultPropertiesForChartingSeries( series, PATTERN_CURVE_SERIES_NAME + "Validated" );

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

    }
}
