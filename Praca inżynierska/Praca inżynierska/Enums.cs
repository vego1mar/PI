namespace PI
{
    public static class Enums
    {

        internal enum AutoSizeColumnsMode
        {
            None = 0,
            AllCells = 1,
            DisplayedCells = 2,
            Fill = 3
        }

        internal enum Operation
        {
            Addition = 0,
            Substraction = 1,
            Multiplication = 2,
            Division = 3,
            Exponentiation = 4,
            Logarithmic = 5,
            Rooting = 6,
            Constant = 7,
            Positive = 8,
            Negative = 9
        }

        internal enum Exceptions
        {
            ArgumentException,
            ArgumentNullException,
            ArgumentOutOfRangeException,
            DirectoryNotFoundException,
            Exception,
            IOException,
            InvalidEnumArgumentException,
            InvalidOperationException,
            None,
            NotSupportedException,
            ObjectDisposedException,
            OutOfMemoryException,
            PathTooLongException,
            SecurityException,
            ThreadStateException,
            UnauthorizedAccessException,
        }

        internal enum DataSetCurveType
        {
            Modified = 0,
            Ideal = 1,
            Average = 2
        }

        internal enum IdealCurveScaffold
        {
            Polynomial = 0,
            Hyperbolic = 1,
            Waveform = 2,
            WaveformSine = -21,
            WaveformSquare = -22,
            WaveformTriangle = -23,
            WaveformSawtooth = -24
        }

    }
}
