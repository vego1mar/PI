using System;

namespace PI.src.general
{
    public sealed class ArgumentsMaker
    {
        private readonly double densityUnit;
        private readonly double xMinimum;
        private readonly int densityPoints;
        private int successor;

        public ArgumentsMaker( double xMin, double xMax, int pointsDensity )
        {
            densityUnit = Math.Abs( xMax - xMin ) / Math.Abs( pointsDensity );
            xMinimum = xMin;
            densityPoints = pointsDensity;
            successor = 0;
        }

        public double GetNextArgument()
        {
            successor++;
            return xMinimum + (densityUnit * (successor - 1));
        }

        public bool HasNextArgument()
        {
            return successor < densityPoints + 1;
        }
    }
}
