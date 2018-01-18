using System;
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

        // TODO: make GM variant UI available
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
                    result = Averages.AGM( orderedSetOfCurves, GeometricMeanVariant.Offset );
                    break;
                case MeanType.Heronian:
                    result = Averages.Heronian( orderedSetOfCurves, GeometricMeanVariant.Offset );
                    break;
                case MeanType.Harmonic:
                    result = Averages.Harmonic( orderedSetOfCurves, StandardMeanVariants.Offset );
                    break;
                case MeanType.RMS:
                    // TODO: remove this enum value
                    break;
                case MeanType.Power:
                    result = Averages.Generalized( orderedSetOfCurves, StandardMeanVariants.Offset, MeansParams.PowerMean.Rank );
                    break;
                case MeanType.Logarithmic:
                    // TODO: remove this enum value
                    break;
                case MeanType.EMA:
                    //MakeAverageCurveOfExponentialMean( curvesNo );
                    break;
                case MeanType.LnWages:
                    // TODO: remove this enum value
                    break;
                case MeanType.CustomDifferential:
                    // TODO: remove this enum value
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



        [Obsolete( "Waiting to remove after refactorization of methods below.", false )]
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
                    tolerants.Add( Averages.Geometric( acceptables[x], GeometricMeanVariant.Sign ).Value );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.Maximum:
                    tolerants.Add( Averages.Maximum( acceptables[x] ).Value );
                    break;
                case MeansSettings.CustomToleranceFinisherFunction.Minimum:
                    tolerants.Add( Averages.Minimum( acceptables[x] ).Value );
                    break;
                }
            }
        }
    }
}
