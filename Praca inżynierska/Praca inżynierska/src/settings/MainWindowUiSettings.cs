namespace PI.src.settings
{
    internal class MainWindowUiSettings
    {
        public MenuSettings Menu { get; set; } = new MenuSettings();

        internal class MenuSettings
        {
            public PanelSettings Panel { get; set; } = new PanelSettings();

            internal class PanelSettings
            {
                public bool KeepProportions { get; set; } = true;
                public bool Hide { get; set; } = false;
                public int SplitterDistance { get; set; } = 300;
                public bool Lock { get; set; } = false;
            }
        }
    }
}
