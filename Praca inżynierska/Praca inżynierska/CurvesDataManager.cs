using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{

    class CurvesDataManager
    {

        public const string PATTERN_CURVE_SERIES_NAME = "PatternCurveSeries";
        public const string GENERATED_CURVE_SERIES_NAME = "GeneratedCurveSeries";
        private const double ACCEPTABLE_MAX_VALUE = 9228162514264337593543950335.0;
        private const double ACCEPTABLE_MIN_VALUE = -ACCEPTABLE_MAX_VALUE;
        public Series PatternCurveSet { get; private set; }
        public List<Series> GeneratedCurvesSet { get; private set; }

        public CurvesDataManager()
        {
            PatternCurveSet = new Series();
            GeneratedCurvesSet = new List<Series>();
            SetDefaultProperties( PatternCurveSet, PATTERN_CURVE_SERIES_NAME );
        }

        public static void SetDefaultProperties( Series series, string name )
        {
            series.Name = name;
            series.BorderWidth = 3;
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Black;
            series.Font = new Font( "Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238 );
            series.IsVisibleInLegend = false;
            series.IsXValueIndexed = true;
            series.YValueType = ChartValueType.Double;
            series.YValueType = ChartValueType.Double;
            series.YValuesPerPoint = 1;
        }

        public bool GeneratePatternCurve( Enums.PatternCurveScaffold type, double startingXPoint, double endingXPoint, int pointsDensity )
        {
            switch ( type ) {
            case Enums.PatternCurveScaffold.Polynomial:
                GeneratePolynomialPatternCurve( startingXPoint, endingXPoint, pointsDensity );
                return IsCurvePointsSetValid( PatternCurveSet );
            case Enums.PatternCurveScaffold.Hyperbolic:
                GenerateHyperbolicPatternCurve( startingXPoint, endingXPoint, pointsDensity );
                return IsCurvePointsSetValid( PatternCurveSet );
            case Enums.PatternCurveScaffold.WaveformSine:
            case Enums.PatternCurveScaffold.WaveformSquare:
            case Enums.PatternCurveScaffold.WaveformTriangle:
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                GenerateWaveformPatternCurve( startingXPoint, endingXPoint, pointsDensity, type );
                return IsCurvePointsSetValid( PatternCurveSet );
            }

            return false;
        }

        private void GeneratePolynomialPatternCurve( double startingXPoint, double endingXPoint, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startingXPoint, endingXPoint, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double leftFraction = (Presets.Pcd.Parameters.Polynomial.A * Math.Pow( x, Presets.Pcd.Parameters.Polynomial.B )) / Presets.Pcd.Parameters.Polynomial.C;
                double rightFraction = (Presets.Pcd.Parameters.Polynomial.D * Math.Pow( x, Presets.Pcd.Parameters.Polynomial.E )) / Presets.Pcd.Parameters.Polynomial.F;
                double polynomial = leftFraction + rightFraction + Presets.Pcd.Parameters.Polynomial.I;
                PatternCurveSet.Points.AddXY( x, polynomial );
            }
        }

        private void GenerateHyperbolicPatternCurve( double startingXPoint, double endingXPoint, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startingXPoint, endingXPoint, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double numerator = Math.Pow( Math.E, x ) - Math.Pow( Math.E, -x );
                double fraction = numerator / Presets.Pcd.Parameters.Hyperbolic.G;
                PatternCurveSet.Points.AddXY( x, fraction + Presets.Pcd.Parameters.Hyperbolic.J );
            }
        }

        private void GenerateWaveformPatternCurve( double startingXPoint, double endingXPoint, int pointsDensity, Enums.PatternCurveScaffold wavetype )
        {
            switch ( wavetype ) {
            case Enums.PatternCurveScaffold.WaveformSine:
                GenerateSineWavePatternCurve( startingXPoint, endingXPoint, pointsDensity );
                break;
            case Enums.PatternCurveScaffold.WaveformSquare:
                GenerateSquareWavePatternCurve( startingXPoint, endingXPoint, pointsDensity );
                break;
            case Enums.PatternCurveScaffold.WaveformTriangle:
                GenerateTriangleWavePatternCurve( startingXPoint, endingXPoint, pointsDensity );
                break;
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                GenerateSawtoothWavePatternCurve( startingXPoint, endingXPoint, pointsDensity );
                break;
            }
        }

        private void GenerateSineWavePatternCurve( double startingXPoint, double endingXPoint, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startingXPoint, endingXPoint, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double argument = (Presets.Pcd.Parameters.Waveform.N * x) + Presets.Pcd.Parameters.Waveform.O;
                double amplitude = Presets.Pcd.Parameters.Waveform.M * Math.Sin( argument );
                PatternCurveSet.Points.AddXY( x, amplitude + Presets.Pcd.Parameters.Waveform.K );
            }
        }

        private void GenerateSquareWavePatternCurve( double startingXPoint, double endingXPoint, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startingXPoint, endingXPoint, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double argument = (ArgsGenerator.PI_2 * x) / Presets.Pcd.Parameters.Waveform.N;
                double absolute = Math.Abs( Math.Sin( argument ) );
                double cosecans = 1.0 / Math.Sin( argument );
                double modifier = Presets.Pcd.Parameters.Waveform.M * cosecans;
                double expression = absolute * modifier;
                PatternCurveSet.Points.AddXY( x, expression + Presets.Pcd.Parameters.Waveform.K );
            }
        }

        private void GenerateTriangleWavePatternCurve( double startingXPoint, double endingXPoint, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startingXPoint, endingXPoint, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double factor = (2.0 * Presets.Pcd.Parameters.Waveform.M) / Math.PI;
                double argument = (ArgsGenerator.PI_2 * x) / Presets.Pcd.Parameters.Waveform.N;
                double expression = factor * Math.Asin( Math.Sin( argument ) );
                PatternCurveSet.Points.AddXY( x, expression + Presets.Pcd.Parameters.Waveform.K );
            }
        }

        private void GenerateSawtoothWavePatternCurve( double startingXPoint, double endingXPoint, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startingXPoint, endingXPoint, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double factor = (-2.0 * Presets.Pcd.Parameters.Waveform.M) / Math.PI;
                double argument = (Math.PI * x) / Presets.Pcd.Parameters.Waveform.N;
                double cotangens = 1.0 / Math.Tan( argument );
                double expression = factor * Math.Atan( cotangens );
                PatternCurveSet.Points.AddXY( x, expression + Presets.Pcd.Parameters.Waveform.K );
            }
        }

        public void AbsorbSeriesPoints( Series series, int curveType, int curveIndex )
        {
            if ( series == null ) {
                return;
            }

            switch ( (Enums.DataSetCurveType) curveType ) {
            case Enums.DataSetCurveType.Pattern:
                AbsorbSeriesForPatternCurve( series );
                break;
            case Enums.DataSetCurveType.Generated:
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

        public static bool IsCurvePointsSetValid( Series series )
        {
            for ( int i = 0; i < series.Points.Count; i++ ) {
                double x = series.Points[i].XValue;
                double y = series.Points[i].YValues[0];

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
            chart.Series[seriesIndex].Font = new Font( "Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238 );
            chart.Series[seriesIndex].IsVisibleInLegend = false;
            chart.Series[seriesIndex].IsXValueIndexed = true;
            chart.Series[seriesIndex].XValueType = ChartValueType.Double;
            chart.Series[seriesIndex].YValueType = ChartValueType.Double;
        }

        private void ClearPointsAndSetDefaults( Series series )
        {
            series.Points.Clear();
            SetDefaultProperties( series, PATTERN_CURVE_SERIES_NAME );
        }

        public bool? MakeGaussianNoiseForGeneratedCurves( int numberOfCurves, double surrounding )
        {
            if ( numberOfCurves < 0 || numberOfCurves > GeneratedCurvesSet.Count ) {
                return null;
            }

            List<Series> curves = GetCopyOfGeneratedCurves( numberOfCurves );

            for ( int i = 0; i < curves.Count; i++ ) {
                MakeGaussianNoise( curves[i], surrounding );

                if ( !IsCurvePointsSetValid( curves[i] ) ) {
                    return false;
                }
            }

            for ( int i = 0; i < curves.Count; i++ ) {
                AbsorbSeriesForSpecifiedGeneratedCurve( curves[i], i );
            }

            return true;
        }

        private List<Series> GetCopyOfGeneratedCurves( int numberOfCurves )
        {
            List<Series> curves = new List<Series>();

            for ( int i = 0; i < numberOfCurves; i++ ) {
                curves.Add( new Series() );
                RecopySeriesPoints( GeneratedCurvesSet[i], curves[i] );
            }

            return curves;
        }

        private void MakeGaussianNoise( Series series, double surrounding )
        {
            for ( int i = 0; i < series.Points.Count; i++ ) {
                double y = series.Points[i].YValues[0];
                double newValue = Randomizer.NextDouble( y - surrounding, y + surrounding );
                series.Points[i].YValues[0] = newValue;
            }
        }

    }
}
