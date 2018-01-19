using PI.src.enumerators;
using PI.src.parameters;

namespace PI.src.settings
{
    internal class IdealCurveDefinerGeneratorSettings
    {
        public IdealCurveScaffold Scaffold { get; set; } = IdealCurveScaffold.Hyperbolic;
        public CurvesParameters Parameters { get; set; } = new CurvesParameters();
    }
}
