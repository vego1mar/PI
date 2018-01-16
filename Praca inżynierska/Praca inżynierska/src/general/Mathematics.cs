using System;

namespace PI.src.general
{
    public static class Mathematics
    {
        public static double Root( double value, double level )
        {
            return Math.Pow( value, 1.0 / level );
        }
    }
}
