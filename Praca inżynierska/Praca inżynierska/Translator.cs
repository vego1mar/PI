using System.Runtime.CompilerServices;
using PI.src.enumerators;
using PI.src.localization.general;

namespace PI
{
    public class Translator
    {

        private static volatile Translator instance = null;
        public static Languages CurrentLanguage { get; private set; } = Languages.English;
        public StringsHierarchy Strings { get; private set; } = new StringsHierarchy();

        private Translator()
        {
            // This should be a singleton.
        }

        [MethodImpl( MethodImplOptions.Synchronized )]
        public static Translator GetInstance()
        {
            if ( instance == null ) {
                instance = new Translator();
            }

            return instance;
        }

        public static void SetLanguage( Languages language )
        {
            CurrentLanguage = language;
        }

        public class StringsHierarchy
        {
            public LangSelectorStrings LangSelector { get; private set; } = new LangSelectorStrings();
            public MainWindowStrings MainWindow { get; private set; } = new MainWindowStrings();
            public MessageBoxShowerStrings MsgBxShower { get; private set; } = new MessageBoxShowerStrings();
            public GridPreviewerStrings GridPreviewer { get; private set; } = new GridPreviewerStrings();
            public StatAnalysisStrings StatAnalysis { get; private set; } = new StatAnalysisStrings();
            public PatternCurveDefinerStrings PatternCurveDefiner { get; private set; } = new PatternCurveDefinerStrings();
            public ChartSettingsStrings ChartSettings { get; private set; } = new ChartSettingsStrings();

            public class LangSelectorStrings
            {
                public LsUiStrings Ui { get; private set; } = new LsUiStrings();

                public class LsUiStrings
                {
                    public LsUiDownStrings Up { get; private set; } = new LsUiDownStrings();
                    public LsUiFormStrings Form { get; private set; } = new LsUiFormStrings();

                    public class LsUiDownStrings
                    {
                        public LocalizedString OkBtn { get { return new LocalizedString( CurrentLanguage, "LangSelector_Ui_Down_OkBtn" ); } }
                    }

                    public class LsUiFormStrings
                    {
                        public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "LangSelector_Ui_Form_Text" ); } }
                    }
                }
            }

            public class MainWindowStrings
            {
                public MwMenuStrings Menu { get; private set; } = new MwMenuStrings();
                public MwUiStrings Ui { get; private set; } = new MwUiStrings();
                public MwPanelStrings Panel { get; private set; } = new MwPanelStrings();

                public class MwMenuStrings
                {
                    public MwProgramMenuStrings Program { get; private set; } = new MwProgramMenuStrings();
                    public MwPanelMenuStrings Panel { get; private set; } = new MwPanelMenuStrings();
                    public MwMeansMenuStrings Means { get; private set; } = new MwMeansMenuStrings();
                    public MwChartMenuStrings Chart { get; private set; } = new MwChartMenuStrings();

                    public class MwProgramMenuStrings
                    {
                        public LocalizedString Title { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program" ); } }
                        public LocalizedString StatAnal { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program_StatisticalAnalysis" ); } }
                        public LocalizedString Lang { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program_SelectLanguage" ); } }
                        public LocalizedString Exit { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Program_Exit" ); } }
                    }

                    public class MwPanelMenuStrings
                    {
                        public LocalizedString Title { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Panel" ); } }
                        public LocalizedString KeepProp { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Panel_KeepProportions" ); } }
                        public LocalizedString Hide { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Panel_Hide" ); } }
                        public LocalizedString Lock { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Panel_Lock" ); } }
                    }

                    public class MwMeansMenuStrings
                    {
                        public LocalizedString Title { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Means" ); } }
                        public LocalizedString AvgInfo { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Means_AveragingInfo" ); } }
                        public LocalizedString Settings { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Means_Settings" ); } }
                    }

                    public class MwChartMenuStrings
                    {
                        public LocalizedString Title { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Chart" ); } }
                        public LocalizedString Settings { get { return new LocalizedString( CurrentLanguage, "MainWindow_Menu_Chart_Settings" ); } }
                    }
                }

                public class MwUiStrings
                {
                    public MwFormStrings Form { get; private set; } = new MwFormStrings();

                    public class MwFormStrings
                    {
                        public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MainWindow_Ui_Form_Text" ); } }
                    }
                }

                public class MwPanelStrings
                {
                    public MwGenerateTabStrings Generate { get; private set; } = new MwGenerateTabStrings();
                    public MwDatasheetTabStrings Datasheet { get; private set; } = new MwDatasheetTabStrings();
                    public MwProgramTabStrings Program { get; private set; } = new MwProgramTabStrings();

                    public class MwGenerateTabStrings
                    {
                        public LocalizedString Title { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate" ); } }
                        public LocalizedString PattCrvScaff { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_PatternCurveScaffold" ); } }
                        public LocalizedString CrvScaff1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold1" ); } }
                        public LocalizedString ScaffPoly { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_Polynomial" ); } }
                        public LocalizedString ScaffHyp { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_Hyperbolic" ); } }
                        public LocalizedString ScaffWave { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_Waveform" ); } }
                        public LocalizedString ScaffNone { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_NotChosen" ); } }
                        public LocalizedString CrvsSet { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_CurvesSet" ); } }
                        public LocalizedString Crvs1No { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_NumberOfCurves1" ); } }
                        public LocalizedString StartX { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_StartingXPoint" ); } }
                        public LocalizedString EndX { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_EndingXPoint" ); } }
                        public LocalizedString Dens { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_Density" ); } }
                        public LocalizedString GenSet { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_GenerateSet" ); } }
                        public LocalizedString Avg { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_Averaging" ); } }
                        public LocalizedString MeanT { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_MeanType" ); } }
                        public LocalizedString Crvs2No { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_NumberOfCurves2" ); } }
                        public LocalizedString Apply { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_Apply" ); } }
                        public LocalizedString StdDev1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Generate_StandardDeviation1" ); } }
                    }

                    public class MwDatasheetTabStrings
                    {
                        public LocalizedString Title { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet" ); } }
                        public LocalizedString DtSetCtrl { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_DatasetControl" ); } }
                        public LocalizedString CrvT { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_CurveType" ); } }
                        public LocalizedString CrvIdx { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_CurveIndex" ); } }
                        public LocalizedString ShowDtSet { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_ShowDataSet" ); } }
                        public LocalizedString GsNoise { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_GaussianNoise" ); } }
                        public LocalizedString CrvNo { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_CurvesNumber" ); } }
                        public LocalizedString Surr { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_Surrounding" ); } }
                        public LocalizedString Malform { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Datasheet_Malform" ); } }
                    }

                    public class MwProgramTabStrings
                    {
                        public LocalizedString Title { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program" ); } }
                        public LocalizedString Timer { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_Timer" ); } }
                        public LocalizedString ActState1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState1" ); } }
                        public LocalizedString StateFail { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Failure" ); } }
                        public LocalizedString StateSucc { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Success" ); } }
                        public LocalizedString Cnts1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Counts1" ); } }
                        public LocalizedString Info { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Informations" ); } }
                        public LocalizedString DotNetFr1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_DotNetFramework" ); } }
                        public LocalizedString InfoObtErrTxt { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_InfoObtainingErrorText" ); } }
                        public LocalizedString OsVer1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_OsVersion1" ); } }
                        public LocalizedString Log { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Logging" ); } }
                        public LocalizedString LogPath1 { get { return new LocalizedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_LogPath1" ); } }
                    }

                }

            }

            public class MessageBoxShowerStrings
            {
                public MbsMainWindowStrings MainWindow { get; private set; } = new MbsMainWindowStrings();
                public MbsGridPreviewerStrings GridPreviewer { get; private set; } = new MbsGridPreviewerStrings();
                public MbsStatisticalAnalysisStrings StatAnalysis { get; private set; } = new MbsStatisticalAnalysisStrings();
                public MbsGeneralStrings General { get; private set; } = new MbsGeneralStrings();

                public class MbsMainWindowStrings
                {
                    public MbsUiStrings Ui { get; private set; } = new MbsUiStrings();

                    public class MbsUiStrings
                    {
                        public MbsSeriesSelectionProblemStrings ProblemSeriesSelection { get; private set; } = new MbsSeriesSelectionProblemStrings();
                        public MbsChartRefreshingErrorStrings ErrorChartRefreshing { get; private set; } = new MbsChartRefreshingErrorStrings();
                        public MbsCurveTypeNotSelectedInfoStrings InfoCurveTypeNotSelected { get; private set; } = new MbsCurveTypeNotSelectedInfoStrings();
                        public MbsPatternCurveNotChosenPrerequisiteStrings PrerequisitePatternCurveNotChosen { get; private set; } = new MbsPatternCurveNotChosenPrerequisiteStrings();
                        public MbsPointsNotValidToChartProblemStrings ProblemPointsNotValidToChart { get; private set; } = new MbsPointsNotValidToChartProblemStrings();
                        public MbsSpecifiedCurveDoesntExistProblemStrings ProblemSpecifiedCurveDoesntExist { get; private set; } = new MbsSpecifiedCurveDoesntExistProblemStrings();
                        public MbsOperationMalformRejectedStopStrings StopOperationMalformRejected { get; private set; } = new MbsOperationMalformRejectedStopStrings();
                        public MbsNotEnoughCurvesForMedianaStopStrings StopNotEnoughCurvesForMediana { get; private set; } = new MbsNotEnoughCurvesForMedianaStopStrings();

                        public class MbsSeriesSelectionProblemStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_SeriesSelectionProblem_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_SeriesSelectionProblem_Caption" ); } }
                        }

                        public class MbsChartRefreshingErrorStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_ChartRefreshingError_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_ChartRefreshingError_Caption" ); } }
                        }

                        public class MbsCurveTypeNotSelectedInfoStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_CurveTypeNotSelectedInfo_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_CurveTypeNotSelectedInfo_Caption" ); } }
                        }

                        public class MbsPatternCurveNotChosenPrerequisiteStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_PatternCurveNotChosenPrerequisite_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_PatternCurveNotChosenPrerequisite_Caption" ); } }
                        }

                        public class MbsPointsNotValidToChartProblemStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_PointsNotValidToChartProblem_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_PointsNotValidToChartProblem_Caption" ); } }
                        }

                        public class MbsSpecifiedCurveDoesntExistProblemStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_SpecifiedCurveDoesntExistProblem_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_SpecifiedCurveDoesntExistProblem_Caption" ); } }
                        }

                        public class MbsOperationMalformRejectedStopStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_OperationMalformRejectedStop_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_OperationMalformRejectedStop_Caption" ); } }
                        }

                        public class MbsNotEnoughCurvesForMedianaStopStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_NotEnoughCurvesForMedianaStop_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_NotEnoughCurvesForMedianaStop_Caption" ); } }
                        }

                    }

                }

                public class MbsGridPreviewerStrings
                {
                    public MbsGprvPanelStrings Panel { get; private set; } = new MbsGprvPanelStrings();
                    public MbsGprvChartStrings Chart { get; private set; } = new MbsGprvChartStrings();

                    public class MbsGprvPanelStrings
                    {
                        public MbsIndexGreaterThanAllowedProblemStrings ProblemIndexGreaterThanAllowed { get; private set; } = new MbsIndexGreaterThanAllowedProblemStrings();
                        public MbsIndexLowerThanAllowedProblemStrings ProblemIndexLowerThanAllowed { get; private set; } = new MbsIndexLowerThanAllowedProblemStrings();
                        public MbsImproperUserValueProblemStrings ProblemImproperUserValue { get; private set; } = new MbsImproperUserValueProblemStrings();
                        public MbsPerformOperationErrorStrings ErrorPerformOperation { get; private set; } = new MbsPerformOperationErrorStrings();
                        public MbsInvalidCurvePointsErrorStrings ErrorInvalidCurvePoints { get; private set; } = new MbsInvalidCurvePointsErrorStrings();

                        public class MbsIndexGreaterThanAllowedProblemStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_IndexGreaterThanAllowedProblem_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_IndexGreaterThanAllowedProblem_Caption" ); } }
                        }

                        public class MbsIndexLowerThanAllowedProblemStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_IndexLowerThanAllowedProblem_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_IndexLowerThanAllowedProblem_Caption" ); } }
                        }

                        public class MbsImproperUserValueProblemStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_ImproperUserValueProblem_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_ImproperUserValueProblem_Caption" ); } }
                        }

                        public class MbsPerformOperationErrorStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_PerformOperationError_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_PerformOperationError_Caption" ); } }
                        }

                        public class MbsInvalidCurvePointsErrorStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_InvalidCurvePointsError_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_InvalidCurvePointsError_Caption" ); } }
                        }

                    }

                    public class MbsGprvChartStrings
                    {
                        public MbsChartRefreshingErrorStrings ErrorChartRefreshing { get; private set; } = new MbsChartRefreshingErrorStrings();

                        public class MbsChartRefreshingErrorStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Chart_ChartRefreshingError_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Chart_ChartRefreshingError_Caption" ); } }
                        }

                    }

                }

                public class MbsPatternCurveDefinerStrings
                {
                    public MbsPcdHyperbolicTabStrings Hyperbolic { get; private set; } = new MbsPcdHyperbolicTabStrings();

                    public class MbsPcdHyperbolicTabStrings
                    {
                        public MbsPcdHyperbolicTabStrings ProblemDivisionByZero { get; private set; } = new MbsPcdHyperbolicTabStrings();

                        public class MbsPcdDivisionByZeroProblemStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_PatternCurveDefiner_Hyperbolic_DivisionByZeroProblem_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_PatternCurveDefiner_Hyperbolic_DivisionByZeroProblem_Caption" ); } }
                        }
                    }
                }

                public class MbsStatisticalAnalysisStrings
                {
                    public MbsSaPreviewStrings Preview { get; private set; } = new MbsSaPreviewStrings();

                    public class MbsSaPreviewStrings
                    {
                        public MbsSaValueOutOfRangeProblemStrings ProblemValueOutOfRange { get; private set; } = new MbsSaValueOutOfRangeProblemStrings();
                        public MbsSaUnrecognizedErrorStrings ErrorUnrecognized { get; private set; } = new MbsSaUnrecognizedErrorStrings();
                        public MbsSaPointsNotValidToChartProblemStrings ProblemPointsNotValidToChart { get; private set; } = new MbsSaPointsNotValidToChartProblemStrings();
                        public MbsSaNoSavedPresetsErrorStrings ErrorNoSavedPresets { get; private set; } = new MbsSaNoSavedPresetsErrorStrings();

                        public class MbsSaValueOutOfRangeProblemStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_ValueOutOfRangeProblem_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_ValueOutOfRangeProblem_Caption" ); } }
                        }

                        public class MbsSaUnrecognizedErrorStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_UnrecognizedError_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_UnrecognizedError_Caption" ); } }
                        }

                        public class MbsSaPointsNotValidToChartProblemStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_PointsNotValidToChartProblem_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_PointsNotValidToChartProblem_Caption" ); } }
                        }

                        public class MbsSaNoSavedPresetsErrorStrings
                        {
                            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_NoSavedPresetsError_Text" ); } }
                            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_NoSavedPresetsError_Caption" ); } }
                        }
                    }
                }

                public class MbsGeneralStrings
                {
                    public MbsGeneralOutOfMemoryExceptionStrings StopOutOfMemoryException { get; private set; } = new MbsGeneralOutOfMemoryExceptionStrings();

                    public class MbsGeneralOutOfMemoryExceptionStrings
                    {
                        public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_General_OutOfMemoryExceptionStop_Text" ); } }
                        public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "MessageBoxShower_General_OutOfMemoryExceptionStop_Caption" ); } }
                    }

                }

            }

            public class GridPreviewerStrings
            {
                public GprvUiStrings Ui { get; private set; } = new GprvUiStrings();

                public class GprvUiStrings
                {
                    public GprvUiFormStrings Form { get; private set; } = new GprvUiFormStrings();
                    public GprvUiPanelStrings Panel { get; private set; } = new GprvUiPanelStrings();
                    public GprvUiPreviewStrings Preview { get; private set; } = new GprvUiPreviewStrings();
                    public GprvUiDatasetStrings Dataset { get; private set; } = new GprvUiDatasetStrings();

                    public class GprvUiFormStrings
                    {
                        public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Form_Text" ); } }
                    }

                    public class GprvUiPanelStrings
                    {
                        public LocalizedString DtGrid { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_DatasetGrid" ); } }
                        public LocalizedString AutoSize { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_AutoSizeColumnsMode" ); } }
                        public LocalizedString Edit { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_FastEdit" ); } }
                        public LocalizedString OperT { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_OperationType" ); } }
                        public LocalizedString StartIdx { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_StartIndex" ); } }
                        public LocalizedString EndIdx { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_EndIndex" ); } }
                        public LocalizedString Value { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Value" ); } }
                        public LocalizedString Addend { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Addend" ); } }
                        public LocalizedString Subtrahend { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Subtrahend" ); } }
                        public LocalizedString Multiplier { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Multiplier" ); } }
                        public LocalizedString Divisor { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Divisor" ); } }
                        public LocalizedString Exponent { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Exponent" ); } }
                        public LocalizedString Basis { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Basis" ); } }
                        public LocalizedString NotApplicable { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_NotApplicable" ); } }
                        public LocalizedString Reset { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Reset" ); } }
                        public LocalizedString Perform { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Perform" ); } }
                        public LocalizedString Refresh { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Refresh" ); } }
                        public LocalizedString Save { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Save" ); } }
                        public LocalizedString Ok { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_OK" ); } }
                        public LocalizedString InfoGprvLoaded { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_GridPreviewerLoaded" ); } }
                        public LocalizedString InfoOperationRevoked { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_OperationRevoked" ); } }
                        public LocalizedString InfoChangesSaved { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_ChangesSaved" ); } }
                        public LocalizedString InfoInvalidUserValue { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_InvalidUserValue" ); } }
                        public LocalizedString InfoOperationRejected { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_OperationRejected" ); } }
                        public LocalizedString InfoPerformedAndRefreshed { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_PerformedAndRefreshed" ); } }
                        public LocalizedString InfoValuesRestored { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_ValuesRestored" ); } }
                    }

                    public class GprvUiPreviewStrings
                    {
                        public LocalizedString InfoChartNotRepainted { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Info_ChartNotRepainted" ); } }
                        public LocalizedString InfoChartRefreshError { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Info_ChartRefreshError" ); } }
                        public LocalizedString InfoChartRefreshed { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Info_ChartRefreshed" ); } }
                        public LocalizedString Prv { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Preview" ); } }
                    }

                    public class GprvUiDatasetStrings
                    {
                        public LocalizedString DtSet { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Dataset_Dataset" ); } }
                    }

                }

            }

            public class StatAnalysisStrings
            {
                public SaUiStrings Ui { get; private set; } = new SaUiStrings();

                public class SaUiStrings
                {
                    public SaUiFormStrings Form { get; private set; } = new SaUiFormStrings();
                    public SaUiStandardDeviationStrings StdDeviation { get; private set; } = new SaUiStandardDeviationStrings();
                    public SaUiPreviewStrings Preview { get; private set; } = new SaUiPreviewStrings();

                    public class SaUiFormStrings
                    {
                        public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Form_Text" ); } }
                    }

                    public class SaUiStandardDeviationStrings
                    {
                        public LocalizedString StdDev { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_StandardDeviation" ); } }
                        public LocalizedString Peek { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Peek" ); } }
                        public LocalizedString Deform { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Deform" ); } }
                        public LocalizedString Noise01 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Noise01" ); } }
                        public LocalizedString Noise05 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Noise05" ); } }
                        public LocalizedString Noise1 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Noise1" ); } }
                        public LocalizedString Noise2 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Noise2" ); } }
                    }

                    public class SaUiPreviewStrings
                    {
                        public LocalizedString Prv { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Preview" ); } }
                        public LocalizedString Chart { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Chart" ); } }
                        public LocalizedString Formula { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Formula" ); } }
                        public LocalizedString DtSet { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Dataset" ); } }
                        public LocalizedString CrvsNo1 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_NumberOfCurves1" ); } }
                        public LocalizedString Dens1 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Density1" ); } }
                        public LocalizedString NotApplicable { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_NotApplicable" ); } }
                        public LocalizedString CrvIdx { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_CurveIndex" ); } }
                        public LocalizedString CrvT { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_CurveType" ); } }
                        public LocalizedString Phen { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Phenomenon" ); } }
                        public LocalizedString Noise { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Noise" ); } }
                        public LocalizedString MeanT { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_MeanType" ); } }
                        public LocalizedString DtSetSel { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_DatasetSelection" ); } }
                    }

                }
            }

            public class PatternCurveDefinerStrings
            {
                public PcdFormStrings Form { get; private set; } = new PcdFormStrings();
                public PcdUiStrings Ui { get; private set; } = new PcdUiStrings();
                public PcdTabsStrings Tabs { get; private set; } = new PcdTabsStrings();

                public class PcdFormStrings
                {
                    public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Form_Text" ); } }
                }

                public class PcdUiStrings
                {
                    public LocalizedString Cancel { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Ui_Cancel" ); } }
                    public LocalizedString Ok { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Ui_OK" ); } }
                }

                public class PcdTabsStrings
                {
                    public PcdTabPolynomialStrings Polynomial { get; private set; } = new PcdTabPolynomialStrings();
                    public PcdTabHyperbolicStrings Hyperbolic { get; private set; } = new PcdTabHyperbolicStrings();
                    public PcdTabWaveformStrings Waveform { get; private set; } = new PcdTabWaveformStrings();

                    public class PcdTabPolynomialStrings
                    {
                        public LocalizedString Pol { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Polynomial_Polynomial" ); } }
                        public LocalizedString Params { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Polynomial_Parameters" ); } }
                    }

                    public class PcdTabHyperbolicStrings
                    {
                        public LocalizedString Hyp { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Hyperbolic" ); } }
                        public LocalizedString Params { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Parameters" ); } }
                        public LocalizedString BoundAc { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Bound_ac" ); } }
                        public LocalizedString BoundBd { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Bound_bd" ); } }
                    }

                    public class PcdTabWaveformStrings
                    {
                        public LocalizedString Wave { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Waveform" ); } }
                        public LocalizedString Params { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Parameters" ); } }
                        public LocalizedString T { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_WaveType" ); } }
                        public LocalizedString Sine { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Sine" ); } }
                        public LocalizedString Sq { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Square" ); } }
                        public LocalizedString Trg { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Triangle" ); } }
                        public LocalizedString Saw { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Sawtooth" ); } }
                    }

                }

            }

            public class ChartSettingsStrings
            {
                public CsFormStrings Form { get; private set; } = new CsFormStrings();
                public CsUiStrings Ui { get; private set; } = new CsUiStrings();

                public class CsFormStrings
                {
                    public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Form_Text" ); } }
                }

                public class CsUiStrings
                {
                    public CsUiGeneralStrings General { get; private set; } = new CsUiGeneralStrings();
                    public CsUiTabsStrings Tabs { get; private set; } = new CsUiTabsStrings();

                    public class CsUiTabsStrings
                    {
                        public CsUiTabsChartStrings Chart { get; private set; } = new CsUiTabsChartStrings();
                        public CsUiTabsChartAreaStrings ChartArea { get; private set; } = new CsUiTabsChartAreaStrings();
                        public CsUiTabsSeriesStrings Series { get; private set; } = new CsUiTabsSeriesStrings();

                        public class CsUiTabsChartStrings
                        {
                            public LocalizedString Chart { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_Chart_Title" ); } }
                        }

                        public class CsUiTabsChartAreaStrings
                        {
                            public LocalizedString Area { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_ChartArea_Title" ); } }
                            public LocalizedString ChA { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_ChartArea_ChartArea" ); } }
                            public LocalizedString Axes { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_ChartArea_Axes" ); } }
                        }

                        public class CsUiTabsSeriesStrings
                        {
                            public LocalizedString Srs { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_Series_Title" ); } }
                        }

                    }

                    public class CsUiGeneralStrings
                    {
                        public LocalizedString ApplyTo { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_General_ApplyToCurve" ); } }
                        public LocalizedString Ok { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_General_OK" ); } }
                    }

                }

            }

        }

    }
}
