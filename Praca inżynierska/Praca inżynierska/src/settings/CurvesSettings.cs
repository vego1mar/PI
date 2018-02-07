using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI.src.settings
{
    internal sealed class CurvesSettings
    {
        public ChartSeriesSettings Ideal { get; set; } = new ChartSeriesSettings();
        public ChartSeriesSettings Modified { get; set; } = new ChartSeriesSettings();
        public ChartSeriesSettings Average { get; set; } = new ChartSeriesSettings();

        internal sealed class ChartSeriesSettings
        {
            public Color Color { get; set; }
            public int BorderWidth { get; set; }
            public ChartDashStyle BorderDashStyle { get; set; }
            public SeriesChartType ChartType { get; set; }
        }

        public static void Set( Chart chart, ChartSeriesSettings settings, int seriesIndex = 0 )
        {
            if ( chart == null || settings == null || seriesIndex < 0 ) {
                return;
            }

            chart.Series[seriesIndex].Color = settings.Color;
            chart.Series[seriesIndex].BorderWidth = settings.BorderWidth;
            chart.Series[seriesIndex].BorderDashStyle = settings.BorderDashStyle;
            chart.Series[seriesIndex].ChartType = settings.ChartType;
        }
    }

}
