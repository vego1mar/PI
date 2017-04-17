﻿using System.Windows.Forms.DataVisualization.Charting;
using System;

namespace PI
{
    class CurvesDataset
    {

        #region Members
        public Series PatternCurveChartingSeries { private set; get; }
        #endregion

        public CurvesDataset()
        {
            PatternCurveChartingSeries = new Series();
            SetDefaultPropertiesForChartingSeries( PatternCurveChartingSeries );
        }

        private void SetDefaultPropertiesForChartingSeries( Series series )
        {
            series.Name = "PatternCurveSeries";
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
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL:
                GeneratePolynomialPatternCurve( numberOfPoints, startingXPoint );
                return true;
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC:
                GenerateHyperbolicPatternCurve( numberOfPoints, startingXPoint );
                return true;
            }

            return false;
        }

        private void GeneratePolynomialPatternCurve( int numberOfPoints, int startingXPoint )
        {
            PatternCurveChartingSeries.Points.Clear();
            SetDefaultPropertiesForChartingSeries( PatternCurveChartingSeries );

            for ( int i = 1; i <= numberOfPoints; i++ ) {
                double offset = startingXPoint + i - 1;
                double leftFraction = (PreSets.ParameterA * Math.Pow( offset, PreSets.ParameterB )) / PreSets.ParameterC;
                double rightFraction = (PreSets.ParameterD * Math.Pow( offset, PreSets.ParameterE )) / PreSets.ParameterF;
                double polynomial = leftFraction + rightFraction;
                PatternCurveChartingSeries.Points.AddXY( offset, polynomial );
            }
        }

        private void GenerateHyperbolicPatternCurve( int numberOfPoints, int startingXPoint )
        {
            PatternCurveChartingSeries.Points.Clear();
            SetDefaultPropertiesForChartingSeries( PatternCurveChartingSeries );

            for ( int i = 1; i <= numberOfPoints; i++ ) {
                double offset = startingXPoint + i - 1;
                double numerator = Math.Pow( Math.E, offset ) - Math.Pow( Math.E, -offset );
                double fraction = numerator / PreSets.ParameterC;
                PatternCurveChartingSeries.Points.AddXY( offset, fraction );
            }
        }

        [Obsolete( "Unfinished", false )]
        public void AbsorbSeriesPoints( Series series, int numberOfGeneratedCurve = -1 )
        {
            if ( series == null ) {
                return;
            }

            if ( numberOfGeneratedCurve < 0 ) {
                for ( int i = 0; i < series.Points.Count; i++ ) {
                    PatternCurveChartingSeries.Points[i].XValue = series.Points[i].XValue;
                    PatternCurveChartingSeries.Points[i].YValues[0] = series.Points[i].YValues[0];
                }
            }
        }

    }
}
