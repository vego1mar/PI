namespace PI
{
    internal class UiSettings
    {
        internal MenuSettings Menu { get; set; } = new MenuSettings();

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

    }
}
