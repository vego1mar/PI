using System;

namespace PI
{
    public static class Randomizer
    {
        private static Random Generator { get; } = new Random( DateTime.Now.Millisecond );

        public static double NextDouble( double minimum, double maximum )
        {
            return Generator.NextDouble() * (maximum - minimum) + minimum;
        }

    }
}
