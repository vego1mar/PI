using System;

namespace PI.src.general
{
    public static class Randoms
    {
        private static readonly Random generator = new Random( DateTime.Now.Millisecond );

        public static double NextDouble( double minimum, double maximum )
        {
            return (generator.NextDouble() * (maximum - minimum)) + minimum;
        }

        public static double GetGaussian( double mean, double standardDeviation )
        {
            double x1 = 1.0 - generator.NextDouble();
            double x2 = 1.0 - generator.NextDouble();
            double y1 = Math.Sqrt( -2.0 * Math.Log( x1 ) ) * Math.Cos( 2.0 * Math.PI * x2 );
            double gaussian = y1 * standardDeviation + mean;

            if ( gaussian < mean - standardDeviation || gaussian > standardDeviation + mean ) {
                gaussian = GetGaussian( mean, standardDeviation );
            }

            return gaussian;
        }

        public static double NextGaussian( double minimum, double maximum )
        {
            double gaussian = GetGaussian( 0.0, 1.0 );
            double mean = (maximum + minimum) / 2.0;
            double stdDeviation = (minimum - maximum) / 2.0;
            return (gaussian * stdDeviation) + mean;
        }
    }
}
