using System;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{

    class CurvesDataManager
    {

        public const string PATTERN_CURVE_SERIES_NAME = "PatternCurveSeries";
        public const string GENERATED_CURVE_SERIES_NAME = "GeneratedCurveSeries";
        public const string AVERAGE_CURVE_SERIES_NAME = "AverageCurveSeries";
        private const double ACCEPTABLE_MAX_VALUE = 9228162514264337593543950335.0;
        private const double ACCEPTABLE_MIN_VALUE = -ACCEPTABLE_MAX_VALUE;
        public Series PatternCurveSet { get; private set; }
        public List<Series> GeneratedCurvesSet { get; private set; }
        public Series AverageCurveSet { get; private set; }

        public CurvesDataManager()
        {
            PatternCurveSet = new Series();
            GeneratedCurvesSet = new List<Series>();
            AverageCurveSet = new Series();
            SetDefaultProperties( PatternCurveSet, PATTERN_CURVE_SERIES_NAME );
            SetDefaultProperties( AverageCurveSet, AVERAGE_CURVE_SERIES_NAME );
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

        private void GeneratePolynomialPatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double leftFraction = (Presets.Pcd.Parameters.Polynomial.A * Math.Pow( x, Presets.Pcd.Parameters.Polynomial.B )) / Presets.Pcd.Parameters.Polynomial.C;
                double rightFraction = (Presets.Pcd.Parameters.Polynomial.D * Math.Pow( x, Presets.Pcd.Parameters.Polynomial.E )) / Presets.Pcd.Parameters.Polynomial.F;
                double polynomial = leftFraction + rightFraction + Presets.Pcd.Parameters.Polynomial.I;
                PatternCurveSet.Points.AddXY( x, polynomial );
            }
        }

        private void GenerateHyperbolicPatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double numerator = Math.Pow( Math.E, x ) - Math.Pow( Math.E, -x );
                double fraction = numerator / Presets.Pcd.Parameters.Hyperbolic.G;
                PatternCurveSet.Points.AddXY( x, fraction + Presets.Pcd.Parameters.Hyperbolic.J );
            }
        }

        private void GenerateWaveformPatternCurve( double startX, double endX, int pointsDensity, Enums.PatternCurveScaffold wavetype )
        {
            switch ( wavetype ) {
            case Enums.PatternCurveScaffold.WaveformSine:
                GenerateSineWavePatternCurve( startX, endX, pointsDensity );
                break;
            case Enums.PatternCurveScaffold.WaveformSquare:
                GenerateSquareWavePatternCurve( startX, endX, pointsDensity );
                break;
            case Enums.PatternCurveScaffold.WaveformTriangle:
                GenerateTriangleWavePatternCurve( startX, endX, pointsDensity );
                break;
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                GenerateSawtoothWavePatternCurve( startX, endX, pointsDensity );
                break;
            }
        }

        private void GenerateSineWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double argument = (Presets.Pcd.Parameters.Waveform.N * x) + Presets.Pcd.Parameters.Waveform.O;
                double amplitude = Presets.Pcd.Parameters.Waveform.M * Math.Sin( argument );
                PatternCurveSet.Points.AddXY( x, amplitude + Presets.Pcd.Parameters.Waveform.K );
            }
        }

        private void GenerateSquareWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

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

        private void GenerateTriangleWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double factor = (2.0 * Presets.Pcd.Parameters.Waveform.M) / Math.PI;
                double argument = (ArgsGenerator.PI_2 * x) / Presets.Pcd.Parameters.Waveform.N;
                double expression = factor * Math.Asin( Math.Sin( argument ) );
                PatternCurveSet.Points.AddXY( x, expression + Presets.Pcd.Parameters.Waveform.K );
            }
        }

        private void GenerateSawtoothWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

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

        private List<double> GetCopyOfGeneratedCurvePointsValues( int numberOfCurve = 0 )
        {
            if ( numberOfCurve < 0 || numberOfCurve >= GeneratedCurvesSet.Count ) {
                return null;
            }

            List<double> yValues = new List<double>();

            for ( int i = 0; i < GeneratedCurvesSet[numberOfCurve].Points.Count; i++ ) {
                yValues.Add( GeneratedCurvesSet[numberOfCurve].Points[i].YValues[0] );
            }

            return yValues;
        }

        private void MakeGaussianNoise( Series series, double surrounding )
        {
            for ( int i = 0; i < series.Points.Count; i++ ) {
                double y = series.Points[i].YValues[0];
                double newValue = Randomizer.NextDouble( y - surrounding, y + surrounding );
                series.Points[i].YValues[0] = newValue;
            }
        }

        public bool? MakeAverageCurveFromGeneratedCurves( Enums.MeanType averageMethod, int numberOfCurves )
        {
            if ( numberOfCurves < 0 || numberOfCurves > GeneratedCurvesSet.Count ) {
                return null;
            }

            switch ( averageMethod ) {
            case Enums.MeanType.Mediana:
                MakeAverageCurveOfMediana( numberOfCurves );
                break;
            case Enums.MeanType.Maximum:
            case Enums.MeanType.Minimum:
                MakeAverageCurveOfMaximumOrMinimum( averageMethod, numberOfCurves );
                return true;
            case Enums.MeanType.Arithmetic:
                MakeAverageCurveOfArithmeticMean( numberOfCurves );
                return true;
            case Enums.MeanType.Geometric:
                MakeAverageCurveOfGeometricMean( numberOfCurves );
                return true;
            case Enums.MeanType.ArithmeticGeometric:
                MakeAverageCurveOfArithmeticGeometricMean( numberOfCurves );
                return true;
            case Enums.MeanType.Heronian:
            case Enums.MeanType.Harmonic:
            case Enums.MeanType.Quadrature:
            case Enums.MeanType.Power:
            case Enums.MeanType.Logarithmic:
            case Enums.MeanType.Exponential:
                break;
            }

            return false;
        }

        private void MakeAverageCurveOfMaximumOrMinimum( Enums.MeanType type, int numberOfCurves )
        {
            List<double> maxYValues = GetCopyOfGeneratedCurvePointsValues();

            for ( int i = 1; i < numberOfCurves; i++ ) {
                for ( int j = 0; j < GeneratedCurvesSet[i].Points.Count; j++ ) {
                    double y = GeneratedCurvesSet[i].Points[j].YValues[0];
                    maxYValues[j] = GetMaximumOrMinimum( type, y, maxYValues[j] ).Value;
                }
            }

            DefineAverageCurveSetValues( maxYValues );
        }

        private double? GetMaximumOrMinimum( Enums.MeanType type, double leftValue, double rightValue )
        {
            switch ( type ) {
            case Enums.MeanType.Maximum:
                return ((leftValue > rightValue) ? leftValue : rightValue);
            case Enums.MeanType.Minimum:
                return ((leftValue < rightValue) ? leftValue : rightValue);
            }

            return null;
        }

        public void ClearAverageCurveSetPoints()
        {
            AverageCurveSet.Points.Clear();
        }

        private void MakeAverageCurveOfMediana( int numberOfCurves = 3 )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> medianas = new List<double>();

            for ( int i = 0; i < argValues.Count; i++ ) {
                argValues[i].Sort();
            }

            for ( int i = 0; i < argValues.Count; i++ ) {
                int oddIndex = argValues[i].Count / 2;

                if ( argValues[i].Count % 2 == 0 ) {
                    double value = (argValues[i][oddIndex] + argValues[i][oddIndex + 1]) / 2.0;
                    medianas.Add( value );
                }
                else {
                    medianas.Add( argValues[i][oddIndex] );
                }
            }

            DefineAverageCurveSetValues( medianas );
        }

        private List<List<double>> GetGeneratedCurvesValuesReorderedIntoXByY( int numberOfCurves )
        {
            List<List<double>> argValues = new List<List<double>>();

            for ( int i = 0; i < PatternCurveSet.Points.Count; i++ ) {
                argValues.Add( new List<double>() );

                for ( int j = 0; j < numberOfCurves; j++ ) {
                    argValues[i].Add( 0.0 );
                }
            }

            for ( int i = 0; i < numberOfCurves; i++ ) {
                for ( int j = 0; j < GeneratedCurvesSet[i].Points.Count; j++ ) {
                    double y = GeneratedCurvesSet[i].Points[j].YValues[0];
                    argValues[j][i] = y;
                }
            }

            return argValues;
        }

        private void DefineAverageCurveSetValues( List<double> newValues )
        {
            AverageCurveSet.Points.Clear();

            for ( int i = 0; i < PatternCurveSet.Points.Count; i++ ) {
                AverageCurveSet.Points.AddXY( PatternCurveSet.Points[i].XValue, newValues[i] );
            }
        }

        private void MakeAverageCurveOfArithmeticMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> arithmeticMeans = new List<double>();
            double sum;

            for ( int i = 0; i < argValues.Count; i++ ) {
                sum = 0.0;

                for ( int j = 0; j < argValues[i].Count; j++ ) {
                    sum += argValues[i][j];
                }

                arithmeticMeans.Add( sum / Convert.ToDouble( argValues[i].Count ) );
            }

            DefineAverageCurveSetValues( arithmeticMeans );
        }

        private void MakeAverageCurveOfGeometricMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> geometricMeans = new List<double>();
            double product;

            for ( int i = 0; i < argValues.Count; i++ ) {
                product = 1.0;

                for ( int j = 0; j < argValues[i].Count; j++ ) {
                    product *= argValues[i][j];
                }

                geometricMeans.Add( GetSquareRoot( Math.Abs( product ), argValues[i].Count ) );
            }

            DefineAverageCurveSetValues( geometricMeans );
        }

        private double GetSquareRoot( double value, double basis )
        {
            return Math.Pow( value, 1.0 / basis );
        }

        private void MakeAverageCurveOfArithmeticGeometricMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> agm = new List<double>();

            for ( int i = 0; i < argValues.Count; i++ ) {
                List<double> args = CalculateFirstAgmValues( argValues[i] );

                for ( int j = 0; j < argValues[i].Count; j++ ) {
                    args = CalculateNextAgmValues( args );
                }

                agm.Add( GetFinalAgmValue( args[0], args[1] ) );
            }

            DefineAverageCurveSetValues( agm );
        }

        private List<double> CalculateFirstAgmValues( List<double> values )
        {
            double sum = 0.0;
            double product = 1.0;

            for ( int i = 0; i < values.Count; i++ ) {
                sum += values[i];
                product *= values[i];
            }

            return new List<double>() {
                Math.Abs( sum / Convert.ToDouble( values.Count ) ),
                GetSquareRoot( Math.Abs( product ), values.Count )
            };
        }

        private List<double> CalculateNextAgmValues( List<double> previousArgs )
        {
            return new List<double>() {
                Math.Abs( (previousArgs[0] + previousArgs[1]) / 2.0 ),
                GetSquareRoot( Math.Abs( previousArgs[0] * previousArgs[1] ), 2.0 )
            };
        }

        private double GetFinalAgmValue( double arithmeticMean, double geometricMean )
        {
            string a = arithmeticMean.ToString( CultureInfo.InvariantCulture );
            string g = geometricMean.ToString( CultureInfo.InvariantCulture );

            if ( a == g ) {
                return Convert.ToDouble( a, CultureInfo.InvariantCulture );
            }

            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            int commonLength = Math.Min( a.Length, g.Length );

            for ( int i = 0; i < commonLength; i++ ) {
                if ( a[i] == g[i] ) {
                    builder.Append( g[i] );
                }
                else {
                    break;
                }
            }

            return Convert.ToDouble( builder.ToString(), CultureInfo.InvariantCulture );
        }

    }
}
