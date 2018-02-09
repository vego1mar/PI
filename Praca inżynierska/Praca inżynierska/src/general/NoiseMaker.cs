using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI.src.general
{
    public static class NoiseMaker
    {
        public static double OfUniform( double value, double surrounding )
        {
            return Randoms.NextDouble( value - surrounding, value + surrounding );
        }

        public static DataPoint OfUniform( DataPoint point, double surrounding, int yValueIndex = 0 )
        {
            if ( point == null || yValueIndex < 0 ) {
                return new DataPoint();
            }

            double x = point.XValue;
            double y = point.YValues[yValueIndex];
            return new DataPoint( x, OfUniform( y, surrounding ) );
        }

        public static IList<DataPoint> OfUniform( IList<DataPoint> set, double surrounding, int yValueIndex = 0 )
        {
            if ( set == null ) {
                return new List<DataPoint>().AsReadOnly();
            }

            IList<DataPoint> result = new List<DataPoint>();

            for ( int i = 0; i < set.Count; i++ ) {
                result.Add( OfUniform( set[i], surrounding, yValueIndex ) );
            }

            return result;
        }

        public static double OfGaussian( double value, double surrounding )
        {
            return Randoms.NextGaussian( value - surrounding, value + surrounding );
        }

        public static DataPoint OfGaussian( DataPoint point, double surrounding, int yValueIndex = 0 )
        {
            if ( point == null || yValueIndex < 0 ) {
                return new DataPoint();
            }

            double x = point.XValue;
            double y = point.YValues[yValueIndex];
            return new DataPoint( x, OfGaussian( y, surrounding ) );
        }

        public static IList<DataPoint> OfGaussian( IList<DataPoint> set, double surrounding, int yValueIndex = 0 )
        {
            if ( set == null ) {
                return new List<DataPoint>().AsReadOnly();
            }

            IList<DataPoint> result = new List<DataPoint>();

            for ( int i = 0; i < set.Count; i++ ) {
                result.Add( OfGaussian( set[i], surrounding, yValueIndex ) );
            }

            return result;
        }
    }
}
