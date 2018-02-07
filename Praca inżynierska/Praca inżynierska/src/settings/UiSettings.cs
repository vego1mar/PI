using PI.src.general;
using System.Collections.Generic;
using System.Drawing;
using static PI.src.settings.CurvesSettings;

namespace PI.src.settings
{
    internal class UiSettings
    {
        public MainWindowUiSettings MainWindow { get; set; } = new MainWindowUiSettings();
        public CurvesSettings Series { get; set; } = new CurvesSettings();
        public GeneratorSettings Presets { get; set; } = new GeneratorSettings();

        public UiSettings()
        {
            SetSeriesPropertyDefaults();
        }

        private void SetSeriesPropertyDefaults()
        {
            IList<ChartSeriesSettings> list = new List<ChartSeriesSettings>() {
                Series.Ideal,
                Series.Modified,
                Series.Average
            };

            foreach ( ChartSeriesSettings setting in list ) {
                setting.BorderWidth = SeriesAssist.DEFAULT_BORDER_WIDTH;
                setting.BorderDashStyle = SeriesAssist.DEFAULT_BORDER_DASH_STYLE;
                setting.ChartType = SeriesAssist.DEFAULT_CHART_TYPE;
            }

            Series.Ideal.Color = Color.Black;
            Series.Modified.Color = Color.Crimson;
            Series.Average.Color = Color.ForestGreen;
        }
    }
}
