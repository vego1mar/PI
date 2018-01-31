using System.Runtime.CompilerServices;

namespace PI.src.localization.messages
{
    public class Messages
    {
        private static volatile Messages singleton;
        public MeansSettingsTexts MeansSettings { get; private set; } = new MeansSettingsTexts();
        public GeneralTexts General { get; private set; } = new GeneralTexts();
        public StatisticalAnalysisTexts StatisticalAnalysis { get; private set; } = new StatisticalAnalysisTexts();
        public GridPreviewerTexts GridPreviewer { get; private set; } = new GridPreviewerTexts();
        public MainWindowTexts MainWindow { get; private set; } = new MainWindowTexts();

        private Messages()
        {
            // This should be a singleton.
        }

        [MethodImpl( MethodImplOptions.Synchronized )]
        public static Messages GetInstance()
        {
            if ( singleton == null ) {
                singleton = new Messages();
            }

            return singleton;
        }
    }
}
