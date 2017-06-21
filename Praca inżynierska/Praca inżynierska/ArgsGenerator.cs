namespace PI
{
    public sealed class ArgsGenerator
    {

        public double DensityUnit { get; private set; }
        private double StartingX { get; set; }
        private int DensityPoints { get; set; }
        public int Successor { get; private set; } = 0;
        public const double PI_2 = 2.0 * System.Math.PI;

        public ArgsGenerator( double startingXPoint, double endingXPoint, int pointsDensity )
        {
            DensityUnit = (endingXPoint - startingXPoint) / pointsDensity;
            StartingX = startingXPoint;
            DensityPoints = pointsDensity;
        }

        public double GetNextArgument()
        {
            Successor++;
            return StartingX + (DensityUnit * (Successor - 1));
        }

        public bool HasNextArgument()
        {
            if ( Successor < DensityPoints + 1 ) {
                return true;
            }

            return false;
        }

    }
}
