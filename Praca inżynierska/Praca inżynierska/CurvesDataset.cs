using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    class CurvesDataset
    {

        public const string PATTERN_CURVE_SERIES_NAME = "PatternCurveSeries";
        public const string GENERATED_CURVE_SERIES_NAME = "GeneratedCurveSeries";

        public Series PatternCurveChartingSeries { get; private set; }
        public List<Series> GeneratedCurvesChartingSeriesCollection { get; private set; }

        public CurvesDataset()
        {
            PatternCurveChartingSeries = new Series();
            GeneratedCurvesChartingSeriesCollection = new List<Series>();
            SetDefaultPropertiesForChartingSeries( PatternCurveChartingSeries, PATTERN_CURVE_SERIES_NAME );
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
                return true;
            case Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                GenerateHyperbolicPatternCurve( numberOfPoints, startingXPoint );
                return true;
            }

            return false;
        }

        private void GeneratePolynomialPatternCurve( int numberOfPoints, int startingXPoint )
        {
            PatternCurveChartingSeries.Points.Clear();
            SetDefaultPropertiesForChartingSeries( PatternCurveChartingSeries, PATTERN_CURVE_SERIES_NAME );

            for ( int i = 1; i <= numberOfPoints; i++ ) {
                double offset = startingXPoint + i - 1;
                double leftFraction = (PreSets.Pcd.ParameterA * Math.Pow( offset, PreSets.Pcd.ParameterB )) / PreSets.Pcd.ParameterC;
                double rightFraction = (PreSets.Pcd.ParameterD * Math.Pow( offset, PreSets.Pcd.ParameterE )) / PreSets.Pcd.ParameterF;
                double polynomial = leftFraction + rightFraction;
                PatternCurveChartingSeries.Points.AddXY( offset, polynomial );
            }
        }

        private void GenerateHyperbolicPatternCurve( int numberOfPoints, int startingXPoint )
        {
            PatternCurveChartingSeries.Points.Clear();
            SetDefaultPropertiesForChartingSeries( PatternCurveChartingSeries, PATTERN_CURVE_SERIES_NAME );

            for ( int i = 1; i <= numberOfPoints; i++ ) {
                double offset = startingXPoint + i - 1;
                double numerator = Math.Pow( Math.E, offset ) - Math.Pow( Math.E, -offset );
                double fraction = numerator / PreSets.Pcd.ParameterC;
                PatternCurveChartingSeries.Points.AddXY( offset, fraction );
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
                PatternCurveChartingSeries.Points[i].XValue = series.Points[i].XValue;
                PatternCurveChartingSeries.Points[i].YValues[0] = series.Points[i].YValues[0];
            }
        }

        private void AbsorbSeriesForSpecifiedGeneratedCurve( Series series, int collectionItemNumber )
        {
            for ( int i = 0; i < series.Points.Count; i++ ) {
                GeneratedCurvesChartingSeriesCollection[collectionItemNumber].Points[i].XValue = series.Points[i].XValue;
                GeneratedCurvesChartingSeriesCollection[collectionItemNumber].Points[i].YValues[0] = series.Points[i].YValues[0];
            }
        }

        public void SpreadPatternCurveSeriesToGeneratedCurveSeriesCollection( int numberOfCurves )
        {
            GeneratedCurvesChartingSeriesCollection = new List<Series>();

            for ( int i = 0; i < numberOfCurves; i++ ) {
                Series series = new Series();
                SetDefaultPropertiesForChartingSeries( series, GENERATED_CURVE_SERIES_NAME + i );
                AddSeriesPointsFromSource( PatternCurveChartingSeries, series );
                GeneratedCurvesChartingSeriesCollection.Add( series );
            }
        }

        private void AddSeriesPointsFromSource( Series source, Series target )
        {
            for ( int i = 0; i < source.Points.Count; i++ ) {
                target.Points.AddXY( source.Points[i].XValue, source.Points[i].YValues[0] );
            }
        }

    }
}
