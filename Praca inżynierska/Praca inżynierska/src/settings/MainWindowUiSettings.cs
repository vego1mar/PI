namespace PI.src.settings
{
    internal class MainWindowUiSettings
    {
        public MainWindowDimensions Dimensions { get; set; } = new MainWindowDimensions();
        public MenuSettings Menu { get; set; } = new MenuSettings();

        internal class MainWindowDimensions
        {
            public int Height { get; set; } = 720;
            public int Width { get; set; } = 1300;
        }

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
