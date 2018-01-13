using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI.src.settings
{
    internal sealed class ChartCommonSettings
    {
        public AntiAliasingStyles AntiAliasing { get; set; }
        public bool SuppressExceptions { get; set; }
        public Color BackColor { get; set; }
    }
}
