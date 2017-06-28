namespace PI
{
    internal class UiSettings
    {
        internal MenuSettings Menu { get; set; } = new MenuSettings();
        internal PanelSettings Panel { get; set; } = new PanelSettings();

        internal class MenuSettings
        {
            internal ProgramSettings Program { get; set; } = new ProgramSettings();

            internal class ProgramSettings
            {
                internal bool KeepPanelProportions { get; set; } = true;
            }
        }

        internal class PanelSettings
        {
            internal GenerateSettings Generate { get; set; } = new GenerateSettings();

            internal class GenerateSettings
            {
                internal bool GeometricMeanApplyingWarning { get; set; } = true;
                internal bool AgmMeanApplyingWarning { get; set; } = true;
            }
        }

    }
}
