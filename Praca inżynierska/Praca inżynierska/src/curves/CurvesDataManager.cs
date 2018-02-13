using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using log4net;
using System.Reflection;
using PI.src.enumerators;
using PI.src.parameters;
using PI.src.averaging.means;
using System.Linq;
using PI.src.general;

namespace PI.src.curves
{
    public class CurvesDataManager : ICloneable
    {
        public Series IdealCurve { get; private set; }
        public IList<Series> ModifiedCurves { get; private set; }
        public Series AverageCurve { get; private set; }
        public MeansParameters MeansParams { get; set; }
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public CurvesDataManager()
        {
            IdealCurve = new Series();
            ModifiedCurves = new List<Series>();
            AverageCurve = new Series();
            MeansParams = new MeansParameters();
            SeriesAssist.SetDefaultSettings( IdealCurve );
            SeriesAssist.SetDefaultSettings( AverageCurve );
            ModifiedCurves.ToList().ForEach( s => SeriesAssist.SetDefaultSettings( s ) );
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

        public void PropagateIdealCurve( int curvesNo )
        {
            ModifiedCurves = new List<Series>();

            for ( int i = 0; i < curvesNo; i++ ) {
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

        public bool? MakeNoiseOfGaussian( int curvesNo, double surrounding )
        {
            if ( curvesNo < 0 ) {
                return null;
            }

            IList<IList<DataPoint>> curves = SeriesAssist.GetCopy( ModifiedCurves, curvesNo, 0 );

            for ( int i = 0; i < curves.Count; i++ ) {
                curves[i] = NoiseMaker.OfGaussian( curves[i], surrounding );

                if ( !SeriesAssist.IsChartAcceptable( curves[i], 0 ) ) {
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
                signature = MethodBase.GetCurrentMethod().Name + '(' + method + ',' + curvesNo + ')';
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
                case MeanType.SMA:
                    result = Averages.SMA( orderedSetOfCurves );
                    break;
                case MeanType.Tolerance:
                    result = Averages.Tolerance( orderedSetOfCurves, MeansParams.Tolerance.Tolerance, MeansParams.Tolerance.Finisher );
                    break;
                case MeanType.Central:
                    result = Averages.Central( orderedSetOfCurves, MeansParams.Central.MassPercent );
                    break;
                case MeanType.NN:
                    result = Smoothers.NearestNeighbors( orderedSetOfCurves, MeansParams.NN.Amount );
                    break;
                case MeanType.NadarayaWatson:
                    result = Smoothers.NadarayaWatson( orderedSetOfCurves, MeansParams.NadarayaWatson.Variant, MeansParams.NadarayaWatson.KernelType, MeansParams.NadarayaWatson.KernelSize );
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

        public object Clone()
        {
            return new CurvesDataManager() {
                IdealCurve = SeriesAssist.GetCopy( IdealCurve, 0, false ),
                ModifiedCurves = SeriesAssist.GetCopy( ModifiedCurves, 0, false ),
                AverageCurve = SeriesAssist.GetCopy( AverageCurve, 0, false ),
                MeansParams = MeansParams
            };
        }

        public void Import( CurvesDataImporter importer )
        {
            if ( importer == null || importer.Values == null ) {
                return;
            }

            IdealCurve.Points.Clear();
            SeriesAssist.CopyPoints( IdealCurve, importer.Arguments, Smoothers.NearestNeighbors( importer.Values ) );
            ModifiedCurves = SeriesAssist.GetSeries( importer.Arguments, importer.Values );
            AverageCurve.Points.Clear();
        }
    }
}
