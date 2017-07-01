namespace PI
{
    internal class CurvesDataManagerConsts
    {
        internal SeriesNamesConsts SeriesNames { get; } = new SeriesNamesConsts();
        internal ChartValuesConsts ChartValues { get; } = new ChartValuesConsts();

        internal class SeriesNamesConsts
        {
            internal string PatternCurve { get; } = "PatternCurveSeries";
            internal string GeneratedCurve { get; } = "GeneratedCurveSeries";
            internal string AverageCurve { get; } = "AverageCurveSeries";
            internal string ValidatedCurve { get; } = "ValidatedCurveSeries";
        }

        internal class ChartValuesConsts
        {
            internal double AcceptableMax { get; } =  9228162514264337593543950335.0;
            internal double AcceptableMin { get; } = -9228162514264337593543950335.0;
        }

    }
}
