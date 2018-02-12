using System;
using System.Collections.Generic;

namespace PI.src.general
{
    public static class Mathematics
    {
        public const double IBM_FLOAT_SURROUNDING = 5.96E-8;

        public static double Root( double value, double level )
        {
            return Math.Pow( value, 1.0 / level );
        }

        public static double Reciprocal( double value )
        {
            return 1.0 / value;
        }

        public static bool IsZero( double value )
        {
            return value >= -IBM_FLOAT_SURROUNDING && value <= IBM_FLOAT_SURROUNDING;
        }

        public static double GetRelativeStandardDeviation( IList<double> average, IList<double> ideal )
        {
            if ( average.Count != ideal.Count ) {
                return double.NaN;
            }

            double sum = 0.0;
            double difference;

            for ( int i = 0; i < average.Count; i++ ) {
                difference = average[i] - ideal[i];
                sum += difference * difference;
            }

            return Math.Sqrt( sum / Convert.ToDouble( average.Count ) );
        }

        public static double GetKernelDistanceArgument( IList<double> selector, IList<double> comparator, double kernelSize )
        {
            IList<double> x = Lists.GetCopy( comparator );
            Lists.Subtract( x, selector );
            return Lists.GetNorm( x, 2 ) / kernelSize;
        }
    }
}
