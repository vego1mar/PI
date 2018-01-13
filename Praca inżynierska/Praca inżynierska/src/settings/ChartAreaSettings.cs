using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI.src.settings
{
    internal sealed class ChartAreaSettings
    {
        public CommonSettings Common { get; set; } = new CommonSettings();
        public AxisSettings X { get; set; } = new AxisSettings();
        public AxisSettings Y { get; set; } = new AxisSettings();

        internal sealed class CommonSettings
        {
            public bool Area3dStyle { get; set; }
            public Color BackColor { get; set; }
        }

        internal sealed class AxisSettings
        {
            public GridSettings MajorGrid { get; set; } = new GridSettings();
            public GridSettings MinorGrid { get; set; } = new GridSettings();

            internal sealed class GridSettings
            {
                public bool Enabled { get; set; }
                public Color LineColor { get; set; }
                public ChartDashStyle LineDashStyle { get; set; }
                public int LineWidth { get; set; }
            }
        }
    }
}
