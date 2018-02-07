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
                public LocalizedString ProgramExit { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program_Exit" ); } }
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
                public LocalizedString GenerateTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_Title" ); } }
                public LocalizedString GeneratePatternCurveScaffold { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_PatternCurveScaffold" ); } }
                public LocalizedString GenerateCurveScaffold1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold1" ); } }
                public LocalizedString GenerateScaffoldPolynomial { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_Polynomial" ); } }
                public LocalizedString GenerateScaffoldHyperbolic { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_Hyperbolic" ); } }
                public LocalizedString GenerateScaffoldWaveform { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_Waveform" ); } }
                public LocalizedString GenerateScaffoldNone { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_NotChosen" ); } }
                public LocalizedString GenerateCurvesSet { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurvesSet" ); } }
                public LocalizedString GenerateCurves1No { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_NumberOfCurves1" ); } }
                public LocalizedString GenerateStartX { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_StartingXPoint" ); } }
                public LocalizedString GenerateEndX { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_EndingXPoint" ); } }
                public LocalizedString GenerateDensity { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_Density" ); } }
                public LocalizedString GenerateGenerateSet { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_GenerateSet" ); } }
                public LocalizedString GenerateAveraging { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_Averaging" ); } }
                public LocalizedString GenerateMeanType { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_MeanType" ); } }
                public LocalizedString GenerateCurves2No { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_NumberOfCurves2" ); } }
                public LocalizedString GenerateApply { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_Apply" ); } }
                public LocalizedString GenerateStandardDeviation1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_StandardDeviation1" ); } }
                public LocalizedString DatasheetTitle { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_Title" ); } }
                public LocalizedString DatasheetDataSetControl { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_DatasetControl" ); } }
                public LocalizedString DatasheetCurveType { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_CurveType" ); } }
                public LocalizedString DatasheetCurveIndex { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_CurveIndex" ); } }
                public LocalizedString DatasheetShowDataSet { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_ShowDataSet" ); } }
                public LocalizedString DatasheetGaussianNoise { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_GaussianNoise" ); } }
                public LocalizedString DatasheetCurvesNo { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_CurvesNumber" ); } }
                public LocalizedString DatasheetSurrounding { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_Surrounding" ); } }
                public LocalizedString DatasheetMalform { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_Malform" ); } }
            }
        }
    }
}
