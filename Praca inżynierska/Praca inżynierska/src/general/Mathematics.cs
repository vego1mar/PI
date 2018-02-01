using System;

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
    }
}
