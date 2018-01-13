namespace PI.src.settings
{
    internal sealed class MainChartSettings
    {
        public ChartSettings.ApplyToCurve ApplyMode { get; set; }
        public ChartCommonSettings Common { get; set; } = new ChartCommonSettings();
        public ChartAreaSettings Areas { get; set; } = new ChartAreaSettings();
        public CurvesSettings Series { get; set; } = new CurvesSettings();
    }
}
