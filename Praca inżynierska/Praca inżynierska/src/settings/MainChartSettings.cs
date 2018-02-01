using PI.src.enumerators;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI.src.settings
{
    internal sealed class MainChartSettings
    {
        public CurveApply ApplyMode { get; set; }
        public ChartCommonSettings Common { get; set; } = new ChartCommonSettings();
        public ChartAreaSettings Areas { get; set; } = new ChartAreaSettings();
        public CurvesSettings Series { get; set; } = new CurvesSettings();

        public static MainChartSettings Get( CurvesSettings curves, Chart chart, int chartAreaNo = 0 )
        {
            MainChartSettings settings = new MainChartSettings();
            settings.Common.AntiAliasing = chart.AntiAliasing;
            settings.Common.SuppressExceptions = chart.SuppressExceptions;
            settings.Common.BackColor = chart.BackColor;
            settings.Areas.Common.Area3dStyle = chart.ChartAreas[chartAreaNo].Area3DStyle.Enable3D;
            settings.Areas.Common.BackColor = chart.ChartAreas[chartAreaNo].BackColor;
            settings.Areas.X.MajorGrid.Enabled = chart.ChartAreas[chartAreaNo].AxisX.MajorGrid.Enabled;
            settings.Areas.X.MajorGrid.LineColor = chart.ChartAreas[chartAreaNo].AxisX.MajorGrid.LineColor;
            settings.Areas.X.MajorGrid.LineDashStyle = chart.ChartAreas[chartAreaNo].AxisX.MajorGrid.LineDashStyle;
            settings.Areas.X.MajorGrid.LineWidth = chart.ChartAreas[chartAreaNo].AxisX.MajorGrid.LineWidth;
            settings.Areas.X.MinorGrid.Enabled = chart.ChartAreas[chartAreaNo].AxisX.MinorGrid.Enabled;
            settings.Areas.X.MinorGrid.LineColor = chart.ChartAreas[chartAreaNo].AxisX.MinorGrid.LineColor;
            settings.Areas.X.MinorGrid.LineDashStyle = chart.ChartAreas[chartAreaNo].AxisX.MinorGrid.LineDashStyle;
            settings.Areas.X.MinorGrid.LineWidth = chart.ChartAreas[chartAreaNo].AxisX.MinorGrid.LineWidth;
            settings.Areas.Y.MajorGrid.Enabled = chart.ChartAreas[chartAreaNo].AxisY.MajorGrid.Enabled;
            settings.Areas.Y.MajorGrid.LineColor = chart.ChartAreas[chartAreaNo].AxisY.MajorGrid.LineColor;
            settings.Areas.Y.MajorGrid.LineDashStyle = chart.ChartAreas[chartAreaNo].AxisY.MajorGrid.LineDashStyle;
            settings.Areas.Y.MajorGrid.LineWidth = chart.ChartAreas[chartAreaNo].AxisY.MajorGrid.LineWidth;
            settings.Areas.Y.MinorGrid.Enabled = chart.ChartAreas[chartAreaNo].AxisY.MinorGrid.Enabled;
            settings.Areas.Y.MinorGrid.LineColor = chart.ChartAreas[chartAreaNo].AxisY.MinorGrid.LineColor;
            settings.Areas.Y.MinorGrid.LineDashStyle = chart.ChartAreas[chartAreaNo].AxisY.MinorGrid.LineDashStyle;
            settings.Areas.Y.MinorGrid.LineWidth = chart.ChartAreas[chartAreaNo].AxisY.MinorGrid.LineWidth;
            settings.Series = curves;
            return settings;
        }

        public static void Set( MainChartSettings source1, CurvesSettings target1, Chart target2, int chartAreaNo = 0 )
        {
            switch ( source1.ApplyMode ) {
            case CurveApply.All:
                target2.AntiAliasing = source1.Common.AntiAliasing;
                target2.SuppressExceptions = source1.Common.SuppressExceptions;
                target2.BackColor = source1.Common.BackColor;
                target2.ChartAreas[chartAreaNo].Area3DStyle.Enable3D = source1.Areas.Common.Area3dStyle;
                target2.ChartAreas[chartAreaNo].BackColor = source1.Areas.Common.BackColor;
                target2.ChartAreas[chartAreaNo].AxisX.MajorGrid.Enabled = source1.Areas.X.MajorGrid.Enabled;
                target2.ChartAreas[chartAreaNo].AxisX.MajorGrid.LineColor = source1.Areas.X.MajorGrid.LineColor;
                target2.ChartAreas[chartAreaNo].AxisX.MajorGrid.LineDashStyle = source1.Areas.X.MajorGrid.LineDashStyle;
                target2.ChartAreas[chartAreaNo].AxisX.MajorGrid.LineWidth = source1.Areas.X.MajorGrid.LineWidth;
                target2.ChartAreas[chartAreaNo].AxisX.MinorGrid.Enabled = source1.Areas.X.MinorGrid.Enabled;
                target2.ChartAreas[chartAreaNo].AxisX.MinorGrid.LineColor = source1.Areas.X.MinorGrid.LineColor;
                target2.ChartAreas[chartAreaNo].AxisX.MinorGrid.LineDashStyle = source1.Areas.X.MinorGrid.LineDashStyle;
                target2.ChartAreas[chartAreaNo].AxisX.MinorGrid.LineWidth = source1.Areas.X.MinorGrid.LineWidth;
                target2.ChartAreas[chartAreaNo].AxisY.MajorGrid.Enabled = source1.Areas.Y.MajorGrid.Enabled;
                target2.ChartAreas[chartAreaNo].AxisY.MajorGrid.LineColor = source1.Areas.Y.MajorGrid.LineColor;
                target2.ChartAreas[chartAreaNo].AxisY.MajorGrid.LineDashStyle = source1.Areas.Y.MajorGrid.LineDashStyle;
                target2.ChartAreas[chartAreaNo].AxisY.MajorGrid.LineWidth = source1.Areas.Y.MajorGrid.LineWidth;
                target2.ChartAreas[chartAreaNo].AxisY.MinorGrid.Enabled = source1.Areas.Y.MinorGrid.Enabled;
                target2.ChartAreas[chartAreaNo].AxisY.MinorGrid.LineColor = source1.Areas.Y.MinorGrid.LineColor;
                target2.ChartAreas[chartAreaNo].AxisY.MinorGrid.LineDashStyle = source1.Areas.Y.MinorGrid.LineDashStyle;
                target2.ChartAreas[chartAreaNo].AxisY.MinorGrid.LineWidth = source1.Areas.Y.MinorGrid.LineWidth;
                break;
            case CurveApply.Average:
            case CurveApply.Modified:
            case CurveApply.Ideal:
                target1 = source1.Series;
                break;
            }
        }
    }
}
