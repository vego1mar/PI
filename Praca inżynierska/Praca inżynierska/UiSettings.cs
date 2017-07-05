namespace PI
{
    internal class UiSettings
    {
        internal MenuSettings Menu { get; set; } = new MenuSettings();
        internal ChartSettingsPool.CurveTypeSettings Series { get; set; } = new ChartSettingsPool.CurveTypeSettings();
        internal GenSettings Presets { get; set; } = new GenSettings();

        internal class MenuSettings
        {
            internal PanelSettings Panel { get; set; } = new PanelSettings();

            internal class PanelSettings
            {
                internal bool KeepProportions { get; set; } = true;
                internal bool Hide { get; set; } = false;
                internal int SplitterDistance { get; set; } = 248;
                internal bool Lock { get; set; } = false;
            }
        }

        internal UiSettings()
        {
            SetSeriesPropertyDefaults();
        }

        private void SetSeriesPropertyDefaults()
        {
            Series.Pattern.Color = System.Drawing.Color.Black;
            Series.Pattern.BorderWidth = 3;
            Series.Pattern.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            Series.Pattern.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            Series.Generated.Color = System.Drawing.Color.Crimson;
            Series.Generated.BorderWidth = 3;
            Series.Generated.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            Series.Generated.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            Series.Average.Color = System.Drawing.Color.ForestGreen;
            Series.Average.BorderWidth = 3;
            Series.Average.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            Series.Average.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

    }
}
