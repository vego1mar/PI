namespace PI.src.settings
{
    internal class PatternCurveDefinerGeneratorSettings
    {
        public Enums.PatternCurveScaffold Scaffold { get; set; } = Enums.PatternCurveScaffold.Hyperbolic;
        public CurvesParameters Parameters { get; set; } = new CurvesParameters();
    }
}
