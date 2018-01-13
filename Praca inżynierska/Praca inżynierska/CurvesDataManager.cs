using System;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using PI.src.general;
using PI.src.settings;

namespace PI
{

    class CurvesDataManager
    {

        public Series IdealCurve { get; private set; }
        public List<Series> ModifiedCurves { get; private set; }
        public Series AverageCurve { get; private set; }

        public MeansSettings.Params MeansParams { get; private set; }
        private CurvesParameters Parameters { get; set; }

        public CurvesDataManager( CurvesParameters parameters )
        {
            IdealCurve = new Series();
            ModifiedCurves = new List<Series>();
            AverageCurve = new Series();
            MeansParams = new MeansSettings.Params();
            Parameters = parameters;
            SeriesAssist.SetDefaultSettings( IdealCurve );
            SeriesAssist.SetDefaultSettings( AverageCurve );
        }

        public bool GenerateIdealCurve( Enums.PatternCurveScaffold type, double startX, double endX, int pointsDensity )
        {
            switch ( type ) {
            case Enums.PatternCurveScaffold.Polynomial:
                GeneratePolynomialPatternCurve( startX, endX, pointsDensity );
                return SeriesAssist.IsChartAcceptable( IdealCurve );
            case Enums.PatternCurveScaffold.Hyperbolic:
                GenerateHyperbolicPatternCurve( startX, endX, pointsDensity );
                return SeriesAssist.IsChartAcceptable( IdealCurve );
            case Enums.PatternCurveScaffold.WaveformSine:
            case Enums.PatternCurveScaffold.WaveformSquare:
            case Enums.PatternCurveScaffold.WaveformTriangle:
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                GenerateWaveformPatternCurve( startX, endX, pointsDensity, type );
                return SeriesAssist.IsChartAcceptable( IdealCurve );
            }

            return false;
        }

        private void GeneratePolynomialPatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( IdealCurve );
            ArgumentsMaker args = new ArgumentsMaker( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double leftFraction = (Parameters.Polynomial.A * Math.Pow( x, Parameters.Polynomial.B )) / Parameters.Polynomial.C;
                double rightFraction = (Parameters.Polynomial.D * Math.Pow( x, Parameters.Polynomial.E )) / Parameters.Polynomial.F;
                double polynomial = leftFraction + rightFraction + Parameters.Polynomial.I;
                IdealCurve.Points.AddXY( x, polynomial );
            }
        }

        private void GenerateHyperbolicPatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( IdealCurve );
            ArgumentsMaker args = new ArgumentsMaker( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double leftFactor = Parameters.Hyperbolic.A * Math.Pow( Math.E, Parameters.Hyperbolic.B * x );
                double rightFactor = Parameters.Hyperbolic.C * Math.Pow( Math.E, Parameters.Hyperbolic.D * (-x) );
                double fraction = (leftFactor - rightFactor) / Parameters.Hyperbolic.F;
                IdealCurve.Points.AddXY( x, fraction + Parameters.Hyperbolic.I );
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
            ClearPointsAndSetDefaults( IdealCurve );
            ArgumentsMaker args = new ArgumentsMaker( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double argument = (Parameters.Waveform.N * x) + Parameters.Waveform.O;
                double amplitude = Parameters.Waveform.M * Math.Sin( argument );
                IdealCurve.Points.AddXY( x, amplitude + Parameters.Waveform.K );
            }
        }

        private void GenerateSquareWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( IdealCurve );
            ArgumentsMaker args = new ArgumentsMaker( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double argument = (2.0 * Math.PI * x) / Parameters.Waveform.N;
                double absolute = Math.Abs( Math.Sin( argument ) );
                double cosecans = 1.0 / Math.Sin( argument );
                double modifier = Parameters.Waveform.M * cosecans;
                double expression = absolute * modifier;
                IdealCurve.Points.AddXY( x, expression + Parameters.Waveform.K );
            }
        }

        private void GenerateTriangleWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( IdealCurve );
            ArgumentsMaker args = new ArgumentsMaker( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double factor = (2.0 * Parameters.Waveform.M) / Math.PI;
                double argument = (2.0 * Math.PI * x) / Parameters.Waveform.N;
                double expression = factor * Math.Asin( Math.Sin( argument ) );
                IdealCurve.Points.AddXY( x, expression + Parameters.Waveform.K );
            }
        }

        private void GenerateSawtoothWavePatternCurve( double startX, double endX, int pointsDensity )
        {
            ClearPointsAndSetDefaults( IdealCurve );
            ArgumentsMaker args = new ArgumentsMaker( startX, endX, pointsDensity );

            while ( args.HasNextArgument() ) {
                double x = args.GetNextArgument();
                double factor = (-2.0 * Parameters.Waveform.M) / Math.PI;
                double argument = (Math.PI * x) / Parameters.Waveform.N;
                double cotangens = 1.0 / Math.Tan( argument );
                double expression = factor * Math.Atan( cotangens );
                IdealCurve.Points.AddXY( x, expression + Parameters.Waveform.K );
            }
        }

        public void AlterCurve( Series series, Enums.DataSetCurveType curveType, int curveIndex )
        {
            if ( series == null || curveIndex < 0 ) {
                return;
            }

            switch ( curveType ) {
            case Enums.DataSetCurveType.Ideal:
                IdealCurve.Points.Clear();
                SeriesAssist.CopyPoints( series, IdealCurve );
                break;
            case Enums.DataSetCurveType.Modified:
                curveIndex--;
                ModifiedCurves[curveIndex].Points.Clear();
                SeriesAssist.CopyPoints( ModifiedCurves, curveIndex, series );
                break;
            }
        }

        public void SpreadPatternCurveSetToGeneratedCurveSet( int numberOfCurves )
        {
            ModifiedCurves = new List<Series>();

            for ( int i = 0; i < numberOfCurves; i++ ) {
                Series series = new Series();
                SeriesAssist.SetDefaultSettings( series );
                SeriesAssist.CopyPoints( IdealCurve, series );
                ModifiedCurves.Add( series );
            }
        }

        public void RemoveInvalidPoints( Enums.DataSetCurveType curveType, int generatedCurveIndex = 0 )
        {
            switch ( curveType ) {
            case Enums.DataSetCurveType.Modified:
                RemoveInvalidPoints( ModifiedCurves[generatedCurveIndex] );
                break;
            case Enums.DataSetCurveType.Ideal:
                RemoveInvalidPoints( IdealCurve );
                break;
            case Enums.DataSetCurveType.Average:
                RemoveInvalidPoints( AverageCurve );
                break;
            }
        }

        private void RemoveInvalidPoints( Series series )
        {
            Series newSeries = new Series();
            SeriesAssist.SetDefaultSettings( newSeries );

            for ( int i = 0; i < series.Points.Count; i++ ) {
                double x = series.Points[i].XValue;
                double y = series.Points[i].YValues[0];

                if ( SeriesAssist.IsChartAcceptable( series.Points[i] ) ) {
                    newSeries.Points.AddXY( x, y );
                }
            }

            series.Points.Clear();
            SeriesAssist.CopyPoints( newSeries, series );
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
            SeriesAssist.SetDefaultSettings( chart.Series[seriesIndex] );
        }

        private void ClearPointsAndSetDefaults( Series series )
        {
            series.Points.Clear();
            SeriesAssist.SetDefaultSettings( series );
        }

        public bool? MakeGaussianNoiseForGeneratedCurves( int numberOfCurves, double surrounding )
        {
            if ( numberOfCurves < 0 || numberOfCurves > ModifiedCurves.Count ) {
                return null;
            }

            List<Series> curves = GetCopyOfGeneratedCurves( numberOfCurves );

            for ( int i = 0; i < curves.Count; i++ ) {
                MakeGaussianNoise( curves[i], surrounding );

                if ( !SeriesAssist.IsChartAcceptable( curves[i] ) ) {
                    return false;
                }
            }

            for ( int i = 0; i < curves.Count; i++ ) {
                ModifiedCurves[i].Points.Clear();
                SeriesAssist.CopyPoints( ModifiedCurves, i, curves[i] );
            }

            return true;
        }

        private List<Series> GetCopyOfGeneratedCurves( int numberOfCurves )
        {
            List<Series> curves = new List<Series>();

            for ( int i = 0; i < numberOfCurves; i++ ) {
                curves.Add( new Series() );
                SeriesAssist.CopyPoints( ModifiedCurves[i], curves[i] );
            }

            return curves;
        }

        private void MakeGaussianNoise( Series series, double surrounding )
        {
            for ( int i = 0; i < series.Points.Count; i++ ) {
                double y = series.Points[i].YValues[0];
                double newValue = Randoms.NextDouble( y - surrounding, y + surrounding );
                series.Points[i].YValues[0] = newValue;
            }
        }

        public bool? MakeAverageCurveFromGeneratedCurves( Enums.MeanType averageMethod, int numberOfCurves )
        {
            if ( numberOfCurves < 0 || numberOfCurves > ModifiedCurves.Count ) {
                return null;
            }

            try {
                switch ( averageMethod ) {
                case Enums.MeanType.Mediana:
                    MakeAverageCurveOfMediana( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.Maximum:
                case Enums.MeanType.Minimum:
                    MakeAverageCurveOfMaximumOrMinimum( averageMethod, numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.Arithmetic:
                    MakeAverageCurveOfArithmeticMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.Geometric:
                    MakeAverageCurveOfGeometricMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.AGM:
                    MakeAverageCurveOfArithmeticGeometricMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.Heronian:
                    MakeAverageCurveOfHeronianMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.Harmonic:
                    MakeAverageCurveOfHarmonicMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.RMS:
                    MakeAverageCurveOfRootMeanSquare( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.Power:
                    MakeAverageCurveOfPowerMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.Logarithmic:
                    MakeAverageCurveOfLogarithmicMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.EMA:
                    MakeAverageCurveOfExponentialMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.LnWages:
                    MakeAverageCurveOfLogarithmicallyWagedMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.CustomDifferential:
                    MakeAverageCurveOfCustomDifferentialMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.CustomTolerance:
                    MakeAverageCurveOfCustomToleranceMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
                case Enums.MeanType.CustomGeometric:
                    MakeAverageCurveOfCustomGeomericMean( numberOfCurves );
                    return SeriesAssist.IsChartAcceptable( AverageCurve );
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
            IList<double> maxYValues = SeriesAssist.GetValues( ModifiedCurves, 0 );

            for ( int i = 1; i < numberOfCurves; i++ ) {
                for ( int j = 0; j < ModifiedCurves[i].Points.Count; j++ ) {
                    double y = ModifiedCurves[i].Points[j].YValues[0];
                    maxYValues[j] = GetMaximumOrMinimum( type, y, maxYValues[j] ).Value;
                }
            }

            SetAverageCurveProperty( maxYValues );
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
            AverageCurve.Points.Clear();
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

            SetAverageCurveProperty( medianas );
        }

        private List<List<double>> GetGeneratedCurvesValuesReorderedIntoXByY( int numberOfCurves )
        {
            List<List<double>> argValues = new List<List<double>>();

            for ( int i = 0; i < IdealCurve.Points.Count; i++ ) {
                argValues.Add( new List<double>() );

                for ( int j = 0; j < numberOfCurves; j++ ) {
                    argValues[i].Add( 0.0 );
                }
            }

            for ( int i = 0; i < numberOfCurves; i++ ) {
                for ( int j = 0; j < ModifiedCurves[i].Points.Count; j++ ) {
                    double y = ModifiedCurves[i].Points[j].YValues[0];
                    argValues[j][i] = y;
                }
            }

            return argValues;
        }

        private void SetAverageCurveProperty( IList<double> newValues )
        {
            AverageCurve.Points.Clear();
            SeriesAssist.CopyPoints( AverageCurve, IdealCurve, newValues );
        }

        private void MakeAverageCurveOfArithmeticMean( int numberOfCurves )
        {
            List<List<double>> values = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> arithmetics = new List<double>();

            for ( int x = 0; x < values.Count; x++ ) {
                arithmetics.Add( GetArithmeticMeanFromSet( values[x] ) );
            }

            SetAverageCurveProperty( arithmetics );
        }

        private void MakeAverageCurveOfGeometricMean( int numberOfCurves )
        {
            List<List<double>> values = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> geometrics = new List<double>();

            for ( int x = 0; x < values.Count; x++ ) {
                geometrics.Add( GetGeometricMeanFromSet( values[x] ) );
            }

            SetAverageCurveProperty( geometrics );
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

            SetAverageCurveProperty( agm );
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

            SetAverageCurveProperty( heronians );
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

            SetAverageCurveProperty( harmonics );
        }

        private void MakeAverageCurveOfRootMeanSquare( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> rms = new List<double>();

            for ( int i = 0; i < argValues.Count; i++ ) {
                rms.Add( CalculatePowerMean( argValues[i], 2 ) );
            }

            SetAverageCurveProperty( rms );
        }

        private void MakeAverageCurveOfPowerMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> powers = new List<double>();

            for ( int i = 0; i < argValues.Count; i++ ) {
                powers.Add( CalculatePowerMean( argValues[i], MeansParams.PowerMean.Rank ) );
            }

            SetAverageCurveProperty( powers );
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

            SetAverageCurveProperty( logMeans );
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

            SetAverageCurveProperty( emas );
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

            SetAverageCurveProperty( wagedMeans );
        }

        private void MakeAverageCurveOfCustomDifferentialMean( int numberOfCurves )
        {
            List<List<double>> argValues = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> diffMeans = new List<double>();

            for ( int i = 0; i < argValues.Count; i++ ) {
                double minimum = GetMinimumFromSet( argValues[i] );
                double maximum = GetMaximumFromSet( argValues[i] );

                switch ( MeansParams.CustomDifferentialMean.Mode ) {
                case MeansSettings.CustomDifferentialMeanMode.Mediana:
                    diffMeans.Add( GetMedianaFromSet( argValues[i] ) / (maximum - minimum) );
                    break;
                case MeansSettings.CustomDifferentialMeanMode.Sum:
                    diffMeans.Add( GetSumFromSet( argValues[i] ) / (maximum - minimum) );
                    break;
                }
            }

            SetAverageCurveProperty( diffMeans );
        }

        private List<double> GetListCopy( List<double> source )
        {
            List<double> copy = new List<double>();

            for ( int i = 0; i < source.Count; i++ ) {
                copy.Add( source[i] );
            }

            return copy;
        }

        private double GetMedianaFromSet( List<double> set )
        {
            List<double> values = GetListCopy( set );
            values.Sort();
            int oddIndex = values.Count / 2;

            if ( values.Count % 2 == 0 ) {
                return (values[oddIndex] + values[oddIndex + 1]) / 2.0;
            }
            else {
                return values[oddIndex];
            }
        }

        private double GetSumFromSet( List<double> values )
        {
            double sum = 0.0;

            for ( int i = 0; i < values.Count; i++ ) {
                sum += values[i];
            }

            return sum;
        }

        private double GetMaximumFromSet( List<double> values )
        {
            double maximum = values[0];

            for ( int i = 1; i < values.Count; i++ ) {
                maximum = Math.Max( maximum, values[i] );
            }

            return maximum;
        }

        private double GetMinimumFromSet( List<double> values )
        {
            double minimum = values[0];

            for ( int i = 1; i < values.Count; i++ ) {
                minimum = Math.Min( minimum, values[i] );
            }

            return minimum;
        }

        private void MakeAverageCurveOfCustomToleranceMean( int numberOfCurves )
        {
            List<List<double>> values = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<List<double>> origins = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<List<double>> acceptables = new List<List<double>>();
            List<double> tolerants = new List<double>();
            List<double> maximums = new List<double>();
            List<double> minimums = new List<double>();
            double comparer = 0.0;

            for ( int x = 0; x < values.Count; x++ ) {
                acceptables.Add( new List<double>() );
                maximums.Add( GetMaximumFromSet( values[x] ) );
                minimums.Add( GetMinimumFromSet( values[x] ) );
            }

            switch ( MeansParams.CustomToleranceMean.Comparer ) {
            case MeansSettings.CustomToleranceComparerType.Mediana:
                comparer = GetMedianaFromSet( maximums ) - GetMedianaFromSet( minimums );
                break;
            case MeansSettings.CustomToleranceComparerType.ArithmeticMean:
                comparer = GetArithmeticMeanFromSet( maximums ) - GetArithmeticMeanFromSet( minimums );
                break;
            }

            for ( int x = 0; x < values.Count; x++ ) {
                SubtractFromSet( values[x], GetMedianaFromSet( values[x] ) );
            }

            for ( int x = 0; x < values.Count; x++ ) {
                for ( int y = 0; y < values[x].Count; y++ ) {
                    if ( Math.Abs( values[x][y] ) < Math.Abs( MeansParams.CustomToleranceMean.Tolerance * comparer ) ) {
                        acceptables[x].Add( origins[x][y] );
                    }
                }
            }

            for ( int x = 0; x < acceptables.Count; x++ ) {
                switch ( MeansParams.CustomToleranceMean.Finisher ) {
                case MeansSettings.CustomToleranceFinisherFunction.Mediana:
                    tolerants.Add( GetMedianaFromSet( acceptables[x] ) );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.ArithmeticMean:
                    tolerants.Add( GetArithmeticMeanFromSet( acceptables[x] ) );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.GeometricMean:
                    tolerants.Add( GetGeometricMeanFromSet( acceptables[x] ) );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.Maximum:
                    tolerants.Add( GetMaximumFromSet( acceptables[x] ) );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.Minimum:
                    tolerants.Add( GetMinimumFromSet( acceptables[x] ) );
                    break;
                }
            }

            SetAverageCurveProperty( tolerants );
        }

        private void SubtractFromSet( List<double> set, double subtrahend )
        {
            for ( int i = 0; i < set.Count; i++ ) {
                set[i] -= subtrahend;
            }
        }

        private double GetArithmeticMeanFromSet( List<double> set )
        {
            double sum = 0.0;

            for ( int i = 0; i < set.Count; i++ ) {
                sum += set[i];
            }

            return sum / Convert.ToDouble( set.Count );
        }

        private double GetGeometricMeanFromSet( List<double> set )
        {
            double product = 1.0;

            for ( int i = 0; i < set.Count; i++ ) {
                product *= set[i];
            }

            if ( product < 0.0 ) {
                return -GetNthRoot( Math.Abs( product ), set.Count );
            }

            return GetNthRoot( product, set.Count );
        }

        private void MakeAverageCurveOfCustomGeomericMean( int numberOfCurves )
        {
            List<List<double>> values = GetGeneratedCurvesValuesReorderedIntoXByY( numberOfCurves );
            List<double> geometrics = new List<double>();

            for ( int x = 0; x < values.Count; x++ ) {
                geometrics.Add( GetCustomGeometricMeanFromSet( values[x] ) );
            }

            SetAverageCurveProperty( geometrics );
        }

        private double GetCustomGeometricMeanFromSet( List<double> set )
        {
            double product = 1.0;

            for ( int i = 0; i < set.Count; i++ ) {
                product *= set[i];
            }

            if ( set.Count % 2 == 0 ) {
                return GetNthRoot( Math.Abs( product ), set.Count );
            }

            return GetNthRoot( product, set.Count );
        }


    }
}
