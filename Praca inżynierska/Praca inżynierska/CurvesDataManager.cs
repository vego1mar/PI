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
    public class CurvesDataManager
    {
        public Series IdealCurve { get; private set; }
        public List<Series> ModifiedCurves { get; private set; }
        public Series AverageCurve { get; private set; }
        public MeansParameters MeansParams { get; private set; }
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public CurvesDataManager()
        {
            IdealCurve = new Series();
            ModifiedCurves = new List<Series>();
            AverageCurve = new Series();
            MeansParams = new MeansParameters();
            SeriesAssist.SetDefaultSettings( IdealCurve );
            SeriesAssist.SetDefaultSettings( AverageCurve );
        }

        public bool GenerateIdealCurve( IdealCurveScaffold type, CurvesParameters @params, double startX, double endX, int pointsDensity )
        {
            IdealCurve.Points.Clear();
            SeriesAssist.SetDefaultSettings( IdealCurve );
            ArgumentsMaker args = new ArgumentsMaker( startX, endX, pointsDensity );

            switch ( type ) {
            case IdealCurveScaffold.Polynomial:
                SeriesAssist.CopyPoints( CurveMaker.OfPolynomial( args, @params.Polynomial ), IdealCurve );
                break;
            case IdealCurveScaffold.Hyperbolic:
                SeriesAssist.CopyPoints( CurveMaker.OfHyperbolic( args, @params.Hyperbolic ), IdealCurve );
                break;
            case IdealCurveScaffold.WaveformSine:
                SeriesAssist.CopyPoints( CurveMaker.OfSineWave( args, @params.Waveform ), IdealCurve );
                break;
            case IdealCurveScaffold.WaveformSquare:
                SeriesAssist.CopyPoints( CurveMaker.OfSquareWave( args, @params.Waveform ), IdealCurve );
                break;
            case IdealCurveScaffold.WaveformTriangle:
                SeriesAssist.CopyPoints( CurveMaker.OfTriangleWave( args, @params.Waveform ), IdealCurve );
                break;
            case IdealCurveScaffold.WaveformSawtooth:
                SeriesAssist.CopyPoints( CurveMaker.OfSawtoothWave( args, @params.Waveform ), IdealCurve );
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

        // TODO: make GM variant UI available through MeansParams usage
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
                case MeanType.Median:
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
                case MeanType.Geometric:
                    result = Averages.Geometric( orderedSetOfCurves, MeansParams.Geometric.Variant );
                    break;
                case MeanType.AGM:
                    result = Averages.AGM( orderedSetOfCurves, MeansParams.AGM.Variant );
                    break;
                case MeanType.Heronian:
                    result = Averages.Heronian( orderedSetOfCurves, MeansParams.Heronian.Variant );
                    break;
                case MeanType.Harmonic:
                    result = Averages.Harmonic( orderedSetOfCurves, MeansParams.Harmonic.Variant );
                    break;
                case MeanType.Generalized:
                    result = Averages.Generalized( orderedSetOfCurves, MeansParams.Generalized.Variant, MeansParams.Generalized.Rank );
                    break;
                case MeanType.Moving:
                    // TODO: refactor this averaging method
                    break;
                case MeanType.Tolerance:
                    // TODO: refactor this averaging method
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



        [Obsolete( "Pending to remove.", false )]
        private List<List<double>> GetGeneratedCurvesValuesReorderedIntoXByY( int curvesNo )
        {
            List<List<double>> argValues = Lists.Get<double>( IdealCurve.Points.Count, curvesNo ).Cast<List<double>>().ToList();

            for ( int i = 0; i < curvesNo; i++ ) {
                for ( int j = 0; j < ModifiedCurves[i].Points.Count; j++ ) {
                    double y = ModifiedCurves[i].Points[j].YValues[0];
                    argValues[j][i] = y;
                }
            }

            return argValues;
        }

        [Obsolete( "Pending to refactor.", true )]
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

        [Obsolete( "Pending to refactor.", true )]
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

            comparer = Averages.Median( maximums ).Value - Averages.Median( minimums ).Value;

            for ( int x = 0; x < values.Count; x++ ) {
                Lists.Subtract( values[x], Averages.Median( values[x] ).Value );
            }

            for ( int x = 0; x < values.Count; x++ ) {
                for ( int y = 0; y < values[x].Count; y++ ) {
                    if ( Math.Abs( values[x][y] ) < Math.Abs( MeansParams.Tolerance.Tolerance * comparer ) ) {
                        acceptables[x].Add( origins[x][y] );
                    }
                }
            }

            for ( int x = 0; x < acceptables.Count; x++ ) {
                tolerants.Add( Averages.Median( acceptables[x] ).Value );
                tolerants.Add( Averages.Arithmetic( acceptables[x] ).Value );
                tolerants.Add( Averages.Geometric( acceptables[x], GeometricMeanVariant.Sign ).Value );
            }
        }
    }
}
