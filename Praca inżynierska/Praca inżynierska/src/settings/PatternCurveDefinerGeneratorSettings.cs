namespace PI.src.settings
{
    internal class PatternCurveDefinerGeneratorSettings
    {
        public Enums.IdealCurveScaffold Scaffold { get; set; } = Enums.IdealCurveScaffold.Hyperbolic;
        public CurvesParameters Parameters { get; set; } = new CurvesParameters();
    }
}
