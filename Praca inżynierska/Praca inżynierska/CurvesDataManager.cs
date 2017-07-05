using System;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{

    class CurvesDataManager
    {

        public Series PatternCurveSet { get; private set; }
        public List<Series> GeneratedCurvesSet { get; private set; }
        public Series AverageCurveSet { get; private set; }

        public double PowerMeanRank { get; set; } 
        internal CurvesDataManagerConsts Consts { get; } 
        private Params Parameters { get; set; } 

        public CurvesDataManager( Params parameters )
        {
            PatternCurveSet = new Series();
            GeneratedCurvesSet = new List<Series>();
            AverageCurveSet = new Series();
            PowerMeanRank = 0.5;
            Consts = new CurvesDataManagerConsts();
            Parameters = parameters;
            SetDefaultProperties( PatternCurveSet, Consts.SeriesNames.PatternCurve );
            SetDefaultProperties( AverageCurveSet, Consts.SeriesNames.AverageCurve );
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

        public bool GeneratePatternCurve( Enums.PatternCurveScaffold type, double startX, double endX, int pointsDensity )
        {
            switch ( type ) {
            case Enums.PatternCurveScaffold.Polynomial:
                GeneratePolynomialPatternCurve( startX, endX, pointsDensity );
                return IsCurvePointsSetValid( PatternCurveSet );
            case Enums.PatternCurveScaffold.Hyperbolic:
                GenerateHyperbolicPatternCurve( startX, endX, pointsDensity );
                return IsCurvePointsSetValid( PatternCurveSet );
            case Enums.PatternCurveScaffold.WaveformSine:
            case Enums.PatternCurveScaffold.WaveformSquare:
            case Enums.PatternCurveScaffold.WaveformTriangle:
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                GenerateWaveformPatternCurve( startX, endX, pointsDensity, type );
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
                double leftFraction = (Parameters.Polynomial.A * Math.Pow( x, Parameters.Polynomial.B )) / Parameters.Polynomial.C;
                double rightFraction = (Parameters.Polynomial.D * Math.Pow( x, Parameters.Polynomial.E )) / Parameters.Polynomial.F;
                double polynomial = leftFraction + rightFraction + Parameters.Polynomial.I;
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
                double fraction = numerator / Parameters.Hyperbolic.G;
                PatternCurveSet.Points.AddXY( x, fraction + Parameters.Hyperbolic.J );
            }
        }

        private void GenerateWaveformPatternCurve( double startX, double endX, int density, Enums.PatternCurveScaffold wavetype )
        {
            switch ( wavetype ) {
            case Enums.PatternCurveScaffold.WaveformSine:
                GenerateSineWavePatternCurve( startX, endX, density );
                break;
            case Enums.PatternCurveScaffold.WaveformSquare:
                GenerateSquareWavePatternCurve( startX, endX, density );
                break;
            case Enums.PatternCurveScaffold.WaveformTriangle:
                GenerateTriangleWavePatternCurve( startX, endX, density );
                break;
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                GenerateSawtoothWavePatternCurve( startX, endX, density );
                break;
            }
        }

        private void GenerateSineWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double argument = (Parameters.Waveform.N * x) + Parameters.Waveform.O;
                double amplitude = Parameters.Waveform.M * Math.Sin( argument );
                PatternCurveSet.Points.AddXY( x, amplitude + Parameters.Waveform.K );
            }
        }

        private void GenerateSquareWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double argument = (ArgsGenerator.PI_2 * x) / Parameters.Waveform.N;
                double absolute = Math.Abs( Math.Sin( argument ) );
                double cosecans = 1.0 / Math.Sin( argument );
                double modifier = Parameters.Waveform.M * cosecans;
                double expression = absolute * modifier;
                PatternCurveSet.Points.AddXY( x, expression + Parameters.Waveform.K );
            }
        }

        private void GenerateTriangleWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double factor = (2.0 * Parameters.Waveform.M) / Math.PI;
                double argument = (ArgsGenerator.PI_2 * x) / Parameters.Waveform.N;
                double expression = factor * Math.Asin( Math.Sin( argument ) );
                PatternCurveSet.Points.AddXY( x, expression + Parameters.Waveform.K );
            }
        }

        private void GenerateSawtoothWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( PatternCurveSet );
            ArgsGenerator args = new ArgsGenerator( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double factor = (-2.0 * Parameters.Waveform.M) / Math.PI;
                double argument = (Math.PI * x) / Parameters.Waveform.N;
                double cotangens = 1.0 / Math.Tan( argument );
                double expression = factor * Math.Atan( cotangens );
                PatternCurveSet.Points.AddXY( x, expression + Parameters.Waveform.K );
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
                SetDefaultProperties( series, Consts.SeriesNames.GeneratedCurve + i );
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
            CurvesDataManagerConsts.ChartValuesConsts consts = new CurvesDataManagerConsts.ChartValuesConsts();

            for ( int i = 0; i < series.Points.Count; i++ ) {
                double x = series.Points[i].XValue;
                double y = series.Points[i].YValues[0];

                if ( x > consts.AcceptableMax || x < consts.AcceptableMin || y > consts.AcceptableMax || y < consts.AcceptableMin ) {
                    return false;
                }
            }

            return true;
        }

        public void RemoveInvalidPoints( Enums.DataSetCurveType curveType, int generatedCurveIndex = 0 )
        {
            switch ( curveType ) {
            case Enums.DataSetCurveType.Generated:
                RemoveInvalidPoints( GeneratedCurvesSet[generatedCurveIndex] );
                break;
            case Enums.DataSetCurveType.Pattern:
                RemoveInvalidPoints( PatternCurveSet );
                break;
            case Enums.DataSetCurveType.Average:
                RemoveInvalidPoints( AverageCurveSet );
                break;
            }
        }

        private void RemoveInvalidPoints( Series series )
        {
            Series newSeries = new Series();
            SetDefaultProperties( newSeries, Consts.SeriesNames.ValidatedCurve );
            double maxValue = Consts.ChartValues.AcceptableMax;
            double minValue = Consts.ChartValues.AcceptableMin;

            for ( int i = 0; i < series.Points.Count; i++ ) {
                double x = series.Points[i].XValue;
                double y = series.Points[i].YValues[0];

                if ( (x < maxValue && x > minValue) && (y < maxValue && y > minValue) ) {
                    newSeries.Points.AddXY( x, y );
                }
            }

            series.Points.Clear();
            RecopySeriesPoints( newSeries, series );
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
            SetDefaultProperties( series, Consts.SeriesNames.PatternCurve );
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

            try {
                switch ( averageMethod ) {
                case Enums.MeanType.Mediana:
                    MakeAverageCurveOfMediana( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.Maximum:
                case Enums.MeanType.Minimum:
                    MakeAverageCurveOfMaximumOrMinimum( averageMethod, numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.Arithmetic:
                    MakeAverageCurveOfArithmeticMean( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.Geometric:
                    MakeAverageCurveOfGeometricMean( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.AGM:
                    MakeAverageCurveOfArithmeticGeometricMean( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.Heronian:
                    MakeAverageCurveOfHeronianMean( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.Harmonic:
                    MakeAverageCurveOfHarmonicMean( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.RMS:
                    MakeAverageCurveOfRootMeanSquare( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.Power:
                    MakeAverageCurveOfPowerMean( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.Logarithmic:
                    MakeAverageCurveOfLogarithmicMean( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.EMA:
                    MakeAverageCurveOfExponentialMean( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                case Enums.MeanType.LnWages:
                    MakeAverageCurveOfLogarithmicallyWagedMean( numberOfCurves );
                    return IsCurvePointsSetValid( AverageCurveSet );
                }
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
                return false;
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return false;
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

                if ( product < 0.0 ) {
                    geometricMeans.Add( -GetNthRoot( Math.Abs( product ), argValues[i].Count ) );
                }
                else {
                    geometricMeans.Add( GetNthRoot( product, argValues[i].Count ) );
                }
            }

            DefineAverageCurveSetValues( geometricMeans );
        }

        private double GetNthRoot( double value, double basis )
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
                GetNthRoot( Math.Abs( product ), values.Count )
            };
        }

        private List<double> CalculateNextAgmValues( List<double> previousArgs )
        {
            return new List<double>() {
                Math.Abs( (previousArgs[0] + previousArgs[1]) / 2.0 ),
                GetNthRoot( Math.Abs( previousArgs[0] * previousArgs[1] ), 2.0 )
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

        private void MakeAverageCurveOfHeronianMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> heronians = new List<double>();
            double sum;
            double product;

            for ( int i = 0; i < argValues.Count; i++ ) {
                sum = 0.0;
                product = 1.0;

                for ( int j = 0; j < argValues[i].Count; j++ ) {
                    sum += argValues[i][j];
                    product *= argValues[i][j];
                }

                if ( product > 0.0 ) {
                    heronians.Add( (sum + GetNthRoot( product, argValues[i].Count )) / (argValues[i].Count + 1) );
                    continue;
                }

                heronians.Add( (sum - GetNthRoot( Math.Abs( product ), argValues[i].Count )) / (argValues[i].Count + 1) );
            }

            DefineAverageCurveSetValues( heronians );
        }

        private void MakeAverageCurveOfHarmonicMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> harmonics = new List<double>();
            double sumOfReciprocals;

            for ( int i = 0; i < argValues.Count; i++ ) {
                sumOfReciprocals = 0.0;

                for ( int j = 0; j < argValues[i].Count; j++ ) {
                    sumOfReciprocals += 1.0 / argValues[i][j];
                }

                harmonics.Add( Convert.ToDouble( argValues[i].Count ) / sumOfReciprocals );
            }

            DefineAverageCurveSetValues( harmonics );
        }

        private void MakeAverageCurveOfRootMeanSquare( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> rms = new List<double>();

            for ( int i = 0; i < argValues.Count; i++ ) {
                rms.Add( CalculatePowerMean( argValues[i], 2 ) );
            }

            DefineAverageCurveSetValues( rms );
        }

        private void MakeAverageCurveOfPowerMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> powers = new List<double>();

            for ( int i = 0; i < argValues.Count; i++ ) {
                powers.Add( CalculatePowerMean( argValues[i], PowerMeanRank ) );
            }

            DefineAverageCurveSetValues( powers );
        }

        private double CalculatePowerMean( List<double> args, double rank )
        {
            double powerSum = 0.0;

            for ( int i = 0; i < args.Count; i++ ) {
                powerSum += Math.Pow( args[i], rank );
            }

            return GetNthRoot( powerSum / args.Count, rank );
        }

        private void MakeAverageCurveOfLogarithmicMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> logMeans = new List<double>();
            double distinction;
            double distinctionOfLogs;

            for ( int i = 0; i < argValues.Count; i++ ) {
                distinction = Math.Abs( argValues[i][0] );
                distinctionOfLogs = Math.Log( Math.Abs( argValues[i][0] ), Math.E );

                for ( int j = 1; j < argValues[i].Count; j++ ) {
                    distinction -= Math.Abs( argValues[i][j] );
                    distinctionOfLogs -= Math.Log( Math.Abs( argValues[i][j] ), Math.E );
                }

                logMeans.Add( distinction / distinctionOfLogs );
            }

            DefineAverageCurveSetValues( logMeans );
        }

        private void MakeAverageCurveOfExponentialMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> emas = new List<double>();
            double sum = 0.0;
            double alfa;

            for ( int i = 0; i < argValues[0].Count; i++ ) {
                sum += argValues[0][i];
            }

            emas.Add( sum );

            for ( int i = 1; i < argValues.Count; i++ ) {
                sum = 0.0;
                alfa = 2.0 / (i + 2);

                for ( int j = 0; j < argValues[i].Count; j++ ) {
                    sum += argValues[i][j];
                }

                emas.Add( (alfa * sum) + ((1.0 - alfa) * emas[i - 1]) );
            }

            DefineAverageCurveSetValues( emas );
        }

        private void MakeAverageCurveOfLogarithmicallyWagedMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> wagedMeans = new List<double>();
            double nominator;
            double denominator;
            double logWage;

            for ( int i = 0; i < argValues.Count; i++ ) {
                nominator = 0.0;
                denominator = 0.0;

                for ( int j = 0; j < argValues[i].Count; j++ ) {
                    logWage = Math.Log( Math.Abs( argValues[i][j] ), Math.E );
                    nominator += logWage * argValues[i][j];
                    denominator += logWage;
                }

                wagedMeans.Add( nominator / denominator );
            }

            DefineAverageCurveSetValues( wagedMeans );
        }

    }
}
