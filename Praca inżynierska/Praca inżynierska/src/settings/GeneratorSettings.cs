namespace PI.src.settings
{
    internal class GeneratorSettings
    {
        public IdealCurveDefinerGeneratorSettings Pcd { get; } = new IdealCurveDefinerGeneratorSettings();
        public MainWindowGeneratorSettings Ui { get; } = new MainWindowGeneratorSettings();
    }
}
