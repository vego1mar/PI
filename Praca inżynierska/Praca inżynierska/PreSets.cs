namespace PI
{

    internal static class Presets
    {
        internal static PcdPresets Pcd { get; } = new PcdPresets();
        internal static UiPresets Ui { get; } = new UiPresets();

        internal class PcdPresets
        {
            internal Enums.PatternCurveScaffold ChosenScaffold { get; set; } = Enums.PatternCurveScaffold.WaveformSine;
            internal Params Parameters { get; set; } = new Params();
        }

        internal class UiPresets
        {
            internal int NumberOfCurves { get; set; } = 5;
            internal double StartingXPoint { get; set; } = -2.0;
            internal double EndingXPoint { get; set; } = 2.0;
            internal int PointsDensity { get; set; } = 200;
        }

    }
}
