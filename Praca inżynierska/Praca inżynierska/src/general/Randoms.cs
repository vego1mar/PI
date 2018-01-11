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
    }
}
