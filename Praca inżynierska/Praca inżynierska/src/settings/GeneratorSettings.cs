namespace PI.src.settings
{
    internal class GeneratorSettings
    {
        public PatternCurveDefinerGeneratorSettings Pcd { get; } = new PatternCurveDefinerGeneratorSettings();
        public MainWindowGeneratorSettings Ui { get; } = new MainWindowGeneratorSettings();
    }
}
