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
    }

}
