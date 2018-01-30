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
            public MessageBoxShowerStrings MsgBxShower { get; private set; } = new MessageBoxShowerStrings();

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

        }

    }
}
