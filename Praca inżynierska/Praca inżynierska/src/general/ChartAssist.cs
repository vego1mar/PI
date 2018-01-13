using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI.src.general
{
    public static class ChartAssist
    {
        public static void SetDefaultSettings( Chart chart, int chartAreaIndex = 0, int legendIndex = 0, int seriesIndex = 0 )
        {
            chart.ChartAreas[chartAreaIndex].AxisX.IntervalType = DateTimeIntervalType.Number;
            chart.ChartAreas[chartAreaIndex].AxisX.IsLabelAutoFit = false;
            chart.ChartAreas[chartAreaIndex].AxisX.IsMarginVisible = false;
            chart.ChartAreas[chartAreaIndex].AxisX.LabelAutoFitMaxFontSize = 8;
            chart.ChartAreas[chartAreaIndex].AxisX.LabelAutoFitMinFontSize = 8;
            chart.ChartAreas[chartAreaIndex].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
            chart.ChartAreas[chartAreaIndex].AxisX.LabelStyle.Font = new Font( "Consolas", 8F );
            chart.ChartAreas[chartAreaIndex].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart.ChartAreas[chartAreaIndex].AxisX.MajorTickMark.Enabled = false;
            chart.ChartAreas[chartAreaIndex].AxisX.TitleFont = new Font( "Consolas", 8F );
            chart.ChartAreas[chartAreaIndex].AxisY.IntervalType = DateTimeIntervalType.Number;
            chart.ChartAreas[chartAreaIndex].AxisY.IsLabelAutoFit = false;
            chart.ChartAreas[chartAreaIndex].AxisY.LabelAutoFitMaxFontSize = 8;
            chart.ChartAreas[chartAreaIndex].AxisY.LabelStyle.Font = new Font( "Consolas", 8F );
            chart.ChartAreas[chartAreaIndex].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart.ChartAreas[chartAreaIndex].AxisY.MajorTickMark.Enabled = false;
            chart.ChartAreas[chartAreaIndex].AxisY.TitleFont = new Font( "Consolas", 8F );
            chart.ChartAreas[chartAreaIndex].BackColor = Color.White;
            chart.ChartAreas[chartAreaIndex].IsSameFontSizeForAllAxes = true;
            chart.Legends[legendIndex].Enabled = false;
            SeriesAssist.SetDefaultSettings( chart.Series[seriesIndex] );
        }
    }
}
