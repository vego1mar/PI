using System;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using PI.src.general;
using PI.src.settings;
using log4net;
using System.Reflection;
using System.Linq;
using PI.src.enumerators;

namespace PI
{
    // Change internal into public -> Enums
    class CurvesDataManager
    {
        public Series IdealCurve { get; private set; }
        public List<Series> ModifiedCurves { get; private set; }
        public Series AverageCurve { get; private set; }
        public MeansSettings.Params MeansParams { get; private set; }
        private readonly CurvesParameters curvesParams;
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public CurvesDataManager( CurvesParameters parameters )
        {
            IdealCurve = new Series();
            ModifiedCurves = new List<Series>();
            AverageCurve = new Series();
            MeansParams = new MeansSettings.Params();
            curvesParams = parameters;
            SeriesAssist.SetDefaultSettings( IdealCurve );
            SeriesAssist.SetDefaultSettings( AverageCurve );
        }

        public bool GenerateIdealCurve( Enums.IdealCurveScaffold type, double startX, double endX, int pointsDensity )
        {
            IdealCurve.Points.Clear();
            SeriesAssist.SetDefaultSettings( IdealCurve );
            ArgumentsMaker args = new ArgumentsMaker( startX, endX, pointsDensity );

            switch ( type ) {
            case Enums.IdealCurveScaffold.Polynomial:
                SeriesAssist.CopyPoints( CurveMaker.OfPolynomial( args, curvesParams.Polynomial ), IdealCurve );
                break;
            case Enums.IdealCurveScaffold.Hyperbolic:
                SeriesAssist.CopyPoints( CurveMaker.OfHyperbolic( args, curvesParams.Hyperbolic ), IdealCurve );
                break;
            case Enums.IdealCurveScaffold.WaveformSine:
                SeriesAssist.CopyPoints( CurveMaker.OfSineWave( args, curvesParams.Waveform ), IdealCurve );
                break;
            case Enums.IdealCurveScaffold.WaveformSquare:
                SeriesAssist.CopyPoints( CurveMaker.OfSquareWave( args, curvesParams.Waveform ), IdealCurve );
                break;
            case Enums.IdealCurveScaffold.WaveformTriangle:
                SeriesAssist.CopyPoints( CurveMaker.OfTriangleWave( args, curvesParams.Waveform ), IdealCurve );
                break;
            case Enums.IdealCurveScaffold.WaveformSawtooth:
                SeriesAssist.CopyPoints( CurveMaker.OfSawtoothWave( args, curvesParams.Waveform ), IdealCurve );
                break;
            }

            return SeriesAssist.IsChartAcceptable( IdealCurve );
        }

        // TODO: Replace Series with IList<DataPoint>
        public void AlterCurve( Series series, DataSetCurveType curveType, int curveIndex )
        {
            if ( series == null || curveIndex < 0 ) {
                return;
            }

            Series seriesCopy = new Series();
            SeriesAssist.CopyPoints( series, seriesCopy );

            switch ( curveType ) {
            case DataSetCurveType.Ideal:
                IdealCurve.Points.Clear();
                SeriesAssist.CopyPoints( seriesCopy, IdealCurve );
                break;
            case DataSetCurveType.Modified:
                curveIndex--;
                ModifiedCurves[curveIndex].Points.Clear();
                SeriesAssist.CopyPoints( ModifiedCurves, curveIndex, seriesCopy );
                break;
            }
        }

        public void PropagateIdealCurve( int numberOfCurves )
        {
            ModifiedCurves = new List<Series>();

            for ( int i = 0; i < numberOfCurves; i++ ) {
                Series series = new Series();
                SeriesAssist.SetDefaultSettings( series );
                SeriesAssist.CopyPoints( IdealCurve, series );
                ModifiedCurves.Add( series );
            }
        }

        public void RemoveInvalidPoints( DataSetCurveType curveType, int modifiedCurveIndex = 0 )
        {
            if ( modifiedCurveIndex < 0 || (modifiedCurveIndex >= ModifiedCurves.Count && ModifiedCurves.Count != 0) ) {
                return;
            }

            switch ( curveType ) {
            case DataSetCurveType.Ideal:
                IdealCurve.Points.Clear();
                SeriesAssist.CopyPoints( SeriesAssist.GetChartAcceptablePoints( IdealCurve ), IdealCurve );
                break;
            case DataSetCurveType.Modified:
                ModifiedCurves[modifiedCurveIndex].Points.Clear();
                SeriesAssist.CopyPoints( SeriesAssist.GetChartAcceptablePoints( ModifiedCurves[modifiedCurveIndex] ), ModifiedCurves[modifiedCurveIndex] );
                break;
            case DataSetCurveType.Average:
                AverageCurve.Points.Clear();
                SeriesAssist.CopyPoints( SeriesAssist.GetChartAcceptablePoints( AverageCurve ), AverageCurve );
                break;
            }
        }

        // TODO: Handle inconsistency between Gaussian and uniform noise
        public bool? MakeNoiseOfGaussian( int curvesNo, double surrounding )
        {
            if ( curvesNo < 0 ) {
                return null;
            }

            IList<IList<DataPoint>> curves = SeriesAssist.GetCopy( ModifiedCurves, curvesNo );

            for ( int i = 0; i < curves.Count; i++ ) {
                curves[i] = NoiseMaker.OfUniform( curves[i], surrounding );

                if ( !SeriesAssist.IsChartAcceptable( curves[i] ) ) {
                    return false;
                }
            }

            for ( int i = 0; i < curves.Count; i++ ) {
                ModifiedCurves[i].Points.Clear();
                SeriesAssist.CopyPoints( curves[i], ModifiedCurves[i] );
            }

            return true;
        }

        public bool? TryMakeAverageCurve( MeanType method, int curvesNo )
        {
            if ( curvesNo < 0 ) {
                return null;
            }

            string signature = string.Empty;
            IList<double> result = new List<double>();

            try {
                signature = MethodBase.GetCurrentMethod().Name + "(" + method + ", " + curvesNo + ")";
                IList<IList<double>> orderedSetOfCurves = SeriesAssist.GetOrderedCopy( ModifiedCurves, curvesNo );

                switch ( method ) {
                case MeanType.Mediana:
                    result = Averages.Median( orderedSetOfCurves );
                    break;
                case MeanType.Maximum:
                    result = Averages.Maximum( orderedSetOfCurves );
                    break;
                case MeanType.Minimum:
                    result = Averages.Minimum( orderedSetOfCurves );
                    break;
                case MeanType.Arithmetic:
                    result = Averages.Arithmetic( orderedSetOfCurves );
                    break;
                case MeanType.GeometricOfSign:
                    result = Averages.Geometric( orderedSetOfCurves, GeometricMeanVariant.Sign );
                    break;
                case MeanType.GeometricOfParity:
                    result = Averages.Geometric( orderedSetOfCurves, GeometricMeanVariant.Parity );
                    break;
                case MeanType.GeometricOfAbsolute:
                    result = Averages.Geometric( orderedSetOfCurves, GeometricMeanVariant.Absolute );
                    break;
                case MeanType.GeometricOfOffset:
                    result = Averages.Geometric( orderedSetOfCurves, GeometricMeanVariant.Offset );
                    break;
                case MeanType.AGM:
                    //MakeAverageCurveOfArithmeticGeometricMean( curvesNo );
                    break;
                case MeanType.Heronian:
                    //MakeAverageCurveOfHeronianMean( curvesNo );
                    break;
                case MeanType.Harmonic:
                    //MakeAverageCurveOfHarmonicMean( curvesNo );
                    break;
                case MeanType.RMS:
                    //MakeAverageCurveOfRootMeanSquare( curvesNo );
                    break;
                case MeanType.Power:
                    //MakeAverageCurveOfPowerMean( curvesNo );
                    break;
                case MeanType.Logarithmic:
                    //MakeAverageCurveOfLogarithmicMean( curvesNo );
                    break;
                case MeanType.EMA:
                    //MakeAverageCurveOfExponentialMean( curvesNo );
                    break;
                case MeanType.LnWages:
                    //MakeAverageCurveOfLogarithmicallyWagedMean( curvesNo );
                    break;
                case MeanType.CustomDifferential:
                    //MakeAverageCurveOfCustomDifferentialMean( curvesNo );
                    break;
                case MeanType.CustomTolerance:
                    //MakeAverageCurveOfCustomToleranceMean( curvesNo );
                    break;
                }
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( signature, ex );
                return false;
            }
            catch ( OverflowException ex ) {
                log.Error( signature, ex );
                return false;
            }
            catch ( InvalidOperationException ex ) {
                log.Error( signature, ex );
                return false;
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
                return false;
            }

            AverageCurve.Points.Clear();
            SeriesAssist.CopyPoints( AverageCurve, IdealCurve, result );
            return SeriesAssist.IsChartAcceptable( AverageCurve );
        }



        [Obsolete]
        private List<List<double>> GetGeneratedCurvesValuesReorderedIntoXByY( int curvesNo )
        {
            //List<List<double>> argValues = new List<List<double>>();
            List<List<double>> argValues = Lists.Get<double>( IdealCurve.Points.Count, curvesNo ).Cast<List<double>>().ToList();

            for ( int i = 0; i < curvesNo; i++ ) {
                for ( int j = 0; j < ModifiedCurves[i].Points.Count; j++ ) {
                    double y = ModifiedCurves[i].Points[j].YValues[0];
                    argValues[j][i] = y;
                }
            }

            return argValues;
        }

        [Obsolete]
        private void SetAverageCurveProperty( IList<double> newValues )
        {
            AverageCurve.Points.Clear();
            SeriesAssist.CopyPoints( AverageCurve, IdealCurve, newValues );
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
                Mathematics.Root( Math.Abs( product ), values.Count )
            };
        }

        private List<double> CalculateNextAgmValues( List<double> previousArgs )
        {
            return new List<double>() {
                Math.Abs( (previousArgs[0] + previousArgs[1]) / 2.0 ),
                Mathematics.Root( Math.Abs( previousArgs[0] * previousArgs[1] ), 2.0 )
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
                    heronians.Add( (sum + Mathematics.Root( product, argValues[i].Count )) / (argValues[i].Count + 1) );
                    continue;
                }

                heronians.Add( (sum - Mathematics.Root( Math.Abs( product ), argValues[i].Count )) / (argValues[i].Count + 1) );
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

            return Mathematics.Root( powerSum / args.Count, rank );
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
                double minimum = Averages.Minimum( argValues[i] ).Value;
                double maximum = Averages.Maximum( argValues[i] ).Value;

                switch ( MeansParams.CustomDifferentialMean.Mode ) {
                case MeansSettings.CustomDifferentialMeanMode.Mediana:
                    diffMeans.Add( Averages.Median( argValues[i] ).Value / (maximum - minimum) );
                    break;
                case MeansSettings.CustomDifferentialMeanMode.Sum:
                    diffMeans.Add( Lists.Sum( argValues[i] ) / (maximum - minimum) );
                    break;
                }
            }

            SetAverageCurveProperty( diffMeans );
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
                maximums.Add( Averages.Maximum( values[x] ).Value );
                minimums.Add( Averages.Minimum( values[x] ).Value );
            }

            switch ( MeansParams.CustomToleranceMean.Comparer ) {
            case MeansSettings.CustomToleranceComparerType.Mediana:
                comparer = Averages.Median( maximums ).Value - Averages.Median( minimums ).Value;
                break;
            case MeansSettings.CustomToleranceComparerType.ArithmeticMean:
                comparer = Averages.Arithmetic( maximums ).Value - Averages.Arithmetic( minimums ).Value;
                break;
            }

            for ( int x = 0; x < values.Count; x++ ) {
                Lists.Subtract( values[x], Averages.Median( values[x] ).Value );
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
                    tolerants.Add( Averages.Median( acceptables[x] ).Value );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.ArithmeticMean:
                    tolerants.Add( Averages.Arithmetic( acceptables[x] ).Value );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.GeometricMean:
                    tolerants.Add( Averages.Geometric(acceptables[x], GeometricMeanVariant.Sign).Value );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.Maximum:
                    tolerants.Add( Averages.Maximum( acceptables[x] ).Value );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.Minimum:
                    tolerants.Add( Averages.Minimum( acceptables[x] ).Value );
                    break;
                }
            }

            SetAverageCurveProperty( tolerants );
        }
    }
}
