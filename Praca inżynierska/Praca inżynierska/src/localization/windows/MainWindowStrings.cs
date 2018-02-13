using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.windows
{
    public class MainWindowStrings
    {
        public MainWindowFormStrings Form { get; private set; } = new MainWindowFormStrings();
        public MainWindowUiStrings Ui { get; private set; } = new MainWindowUiStrings();

        public class MainWindowFormStrings
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MainWindow_Ui_Form_Text" ); } }
        }

        public class MainWindowUiStrings
        {
            public MainWindowMenuStrings Menu { get; private set; } = new MainWindowMenuStrings();
            public MainWindowPanelStrings Panel { get; private set; } = new MainWindowPanelStrings();

            public class MainWindowMenuStrings
            {
                public LocalizedString ProgramTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program_Title" ); } }
                public LocalizedString ProgramStatisticalAnalysis { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program_StatisticalAnalysis" ); } }
                public LocalizedString ProgramSelectLanguage { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program_SelectLanguage" ); } }
                public LocalizedString ProgramAbout { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program_About" ); } }
                public LocalizedString ProgramExit { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program_Exit" ); } }
                public LocalizedString FileTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_File_Title" ); } }
                public LocalizedString FileImport { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_File_Import" ); } }
                public LocalizedString PanelTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Panel_Title" ); } }
                public LocalizedString PanelKeepProportions { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Panel_KeepProportions" ); } }
                public LocalizedString PanelHide { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Panel_Hide" ); } }
                public LocalizedString PanelLock { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Panel_Lock" ); } }
                public LocalizedString MeansTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Means_Title" ); } }
                public LocalizedString MeansSettings { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Means_Settings" ); } }
                public LocalizedString ChartTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Chart_Title" ); } }
                public LocalizedString ChartSettings { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Chart_Settings" ); } }
            }

            public class MainWindowPanelStrings
            {
                public LocalizedString IdealTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_Title" ); } }
                public LocalizedString IdealPatternCurveScaffold { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_IdealCurveScaffold" ); } }
                public LocalizedString IdealCurveScaffold1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_CurveScaffold1" ); } }
                public LocalizedString IdealScaffoldPolynomial { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_CurveScaffold2_Polynomial" ); } }
                public LocalizedString IdealScaffoldHyperbolic { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_CurveScaffold2_Hyperbolic" ); } }
                public LocalizedString IdealScaffoldWaveform { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_CurveScaffold2_Waveform" ); } }
                public LocalizedString IdealScaffoldNone { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_CurveScaffold2_NotChosen" ); } }
                public LocalizedString IdealCurvesSet { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_CurvesSet" ); } }
                public LocalizedString IdealCurves1No { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_NumberOfCurves1" ); } }
                public LocalizedString IdealStartX { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_StartingXPoint" ); } }
                public LocalizedString IdealEndX { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_EndingXPoint" ); } }
                public LocalizedString IdealDensity { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_Density" ); } }
                public LocalizedString IdealGenerateSet { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Ideal_GenerateSet" ); } }
                public LocalizedString ModifiedTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Modified_Title" ); } }
                public LocalizedString ModifiedDataSetControl { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Modified_DatasetControl" ); } }
                public LocalizedString ModifiedCurveType { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Modified_CurveType" ); } }
                public LocalizedString ModifiedCurveIndex { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Modified_CurveIndex" ); } }
                public LocalizedString ModifiedShowDataSet { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Modified_ShowDataSet" ); } }
                public LocalizedString ModifiedGaussianNoise { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Modified_GaussianNoise" ); } }
                public LocalizedString ModifiedCurvesNo { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Modified_CurvesNumber" ); } }
                public LocalizedString ModifiedSurrounding { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Modified_Surrounding" ); } }
                public LocalizedString ModifiedMalform { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Modified_Malform" ); } }
                public LocalizedString AverageTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Average_Title" ); } }
                public LocalizedString AverageAveraging { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Average_Averaging" ); } }
                public LocalizedString AverageMeanType { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Average_MeanType" ); } }
                public LocalizedString AverageCurves2No { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Average_NumberOfCurves2" ); } }
                public LocalizedString AverageApply { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Average_Apply" ); } }
                public LocalizedString AverageStandardDeviation1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Average_StandardDeviation1" ); } }
            }
        }
    }
}
