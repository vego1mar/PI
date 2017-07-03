using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    internal class ChartSettingsPool
    {
        internal ChartSettings.ApplyToCurve ApplyMode { get; set; }
        internal ChartCommonSettings Common { get; set; } = new ChartCommonSettings();
        internal ChartAreasSettings Areas { get; set; } = new ChartAreasSettings();
        internal CurveTypeSettings Series { get; set; } = new CurveTypeSettings();

        internal class ChartCommonSettings
        {
            internal AntiAliasingStyles AntiAliasing { get; set; }
            internal bool SuppressExceptions { get; set; }
            internal Color BackColor { get; set; }
        }

        internal class ChartAreasSettings
        {
            internal CommonSettings Common { get; set; } = new CommonSettings();
            internal AxisSettings X { get; set; } = new AxisSettings();
            internal AxisSettings Y { get; set; } = new AxisSettings();

            internal class CommonSettings
            {
                internal bool Area3dStyle { get; set; }
                internal Color BackColor { get; set; }
            }

            internal class AxisSettings
            {
                internal GridSettings MajorGrid { get; set; } = new GridSettings();
                internal GridSettings MinorGrid { get; set; } = new GridSettings();

                internal class GridSettings
                {
                    internal bool Enabled { get; set; }
                    internal Color LineColor { get; set; }
                    internal ChartDashStyle LineDashStyle { get; set; }
                    internal int LineWidth { get; set; }
                }
            }
        }

        internal class CurveTypeSettings
        {
            internal ChartSeriesSettings Pattern { get; set; } = new ChartSeriesSettings();
            internal ChartSeriesSettings Generated { get; set; } = new ChartSeriesSettings();
            internal ChartSeriesSettings Average { get; set; } = new ChartSeriesSettings();

            internal class ChartSeriesSettings
            {
                internal Color Color { get; set; }
                internal int BorderWidth { get; set; }
                internal ChartDashStyle BorderDashStyle { get; set; }
                internal SeriesChartType ChartType { get; set; }
            }
        }

    }
}
