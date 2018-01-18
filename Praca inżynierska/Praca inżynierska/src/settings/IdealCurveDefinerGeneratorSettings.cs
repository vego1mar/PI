using PI.src.enumerators;

namespace PI.src.settings
{
    internal class IdealCurveDefinerGeneratorSettings
    {
        public IdealCurveScaffold Scaffold { get; set; } = IdealCurveScaffold.Hyperbolic;
        public CurvesParameters Parameters { get; set; } = new CurvesParameters();
    }
}
