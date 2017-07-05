namespace PI
{

    internal class GenSettings
    {
        internal PcdGenSettings Pcd { get; } = new PcdGenSettings();
        internal UiGenSettings Ui { get; } = new UiGenSettings();

        internal class PcdGenSettings
        {
            internal Enums.PatternCurveScaffold Scaffold { get; set; } = Enums.PatternCurveScaffold.Hyperbolic;
            internal Params Parameters { get; set; } = new Params();
        }

        internal class UiGenSettings
        {
            internal int NumberOfCurves { get; set; } = 5;
            internal double StartingXPoint { get; set; } = -2.0;
            internal double EndingXPoint { get; set; } = 2.0;
            internal int PointsDensity { get; set; } = 200;
        }

    }
}
