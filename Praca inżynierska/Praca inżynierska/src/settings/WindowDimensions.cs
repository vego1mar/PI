namespace PI.src.settings
{
    internal class WindowDimensions
    {
        public Dimensions About { get; set; }
        public Dimensions ChartSettings { get; set; }
        public Dimensions GridPreviewer { get; set; }
        public Dimensions LanguageSelector { get; set; }
        public Dimensions MainWindow { get; set; }
        public Dimensions MeansSettings { get; set; }
        public Dimensions PatternCurveDefiner { get; set; }
        public Dimensions StatisticalAnalysis { get; set; }

        public class Dimensions
        {
            public int Width { get; set; } 
            public int Height { get; set; } 
        }

        public WindowDimensions()
        {
            About               = new Dimensions() { Width = 16 * 42, Height =  9 * 42 };
            ChartSettings       = new Dimensions() { Width =  9 * 39, Height = 16 * 39 };
            GridPreviewer       = new Dimensions() { Width = 16 * 75, Height =  9 * 75 };
            LanguageSelector    = new Dimensions() { Width = 16 * 27, Height =  9 * 27 };
            MainWindow          = new Dimensions() { Width = 16 * 80, Height =  9 * 80 };
            MeansSettings       = new Dimensions() { Width = 16 * 23, Height =  9 * 23 };
            PatternCurveDefiner = new Dimensions() { Width = 16 * 45, Height =  9 * 45 };
            StatisticalAnalysis = new Dimensions() { Width = 19 * 86, Height =  8 * 86 };
        }
    }
}
