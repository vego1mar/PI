namespace PI
{
    internal static class Enums
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
            Generated = 0,
            Pattern = 1,
            Average = 2
        }

        internal enum PatternCurveScaffold
        {
            Polynomial = 0,
            Hyperbolic = 1,
            Waveform = 2,
            WaveformSine = -21,
            WaveformSquare = -22,
            WaveformTriangle = -23,
            WaveformSawtooth = -24
        }

        internal enum MeanType
        {
            Mediana = 0,
            Maximum = 1,
            Minimum = 2,
            Arithmetic = 3,
            Geometric = 4,
            AGM = 5,
            Heronian = 6,
            Harmonic = 7,
            RMS = 8,
            Power = 9,
            Logarithmic = 10,
            EMA = 11,
            LnWages = 12,
            CustomDifferential = 13,
            CustomTolerance = 14
        }

    }
}
