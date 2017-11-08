using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PI
{
    public class Translator
    {

        private static volatile Translator instance = null;
        public static LangSelector.Languages CurrentLanguage { get; private set; } = LangSelector.Languages.English;
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

        public static void SetLanguage( LangSelector.Languages language )
        {
            CurrentLanguage = language;
        }

        public class TaggedString
        {
            private string StringName { get; set; }
            private LangSelector.Languages Language { get; set; }

            public TaggedString( LangSelector.Languages language, string name )
            {
                Language = language;
                StringName = name;
            }

            public string GetString()
            {
                return GetLocalizedStringSafe( StringName, Language );
            }

            private string GetLocalizedStringSafe( string name, LangSelector.Languages lang )
            {
                string resourceString = string.Empty;

                try {
                    switch ( lang ) {
                    case LangSelector.Languages.English:
                        resourceString = Locales.en_US.ResourceManager.GetString( name );
                        break;
                    case LangSelector.Languages.Polish:
                        resourceString = Locales.pl_PL.ResourceManager.GetString( name );
                        break;
                    }
                }
                catch ( ArgumentNullException ex ) {
                    Logger.WriteException( ex );
                }
                catch ( InvalidOperationException ex ) {
                    Logger.WriteException( ex );
                }
                catch ( System.Resources.MissingManifestResourceException ex ) {
                    Logger.WriteException( ex );
                }
                catch ( System.Resources.MissingSatelliteAssemblyException ex ) {
                    Logger.WriteException( ex );
                }
                catch ( Exception ex ) {
                    Logger.WriteException( ex );
                }

                return resourceString;
            }
        }

        public class StringsHierarchy
        {
            public LangSelectorStrings LangSelector { get; private set; } = new LangSelectorStrings();
            public MainWindowStrings MainWindow { get; private set; } = new MainWindowStrings();
            public EnumsStrings Enums { get; private set; } = new EnumsStrings();
            public MessageBoxShowerStrings MsgBxShower { get; private set; } = new MessageBoxShowerStrings();
            public GridPreviewerStrings GridPreviewer { get; private set; } = new GridPreviewerStrings();
            public StatAnalysisStrings StatAnalysis { get; private set; } = new StatAnalysisStrings();
            public AvgInfoStrings AvgInfo { get; private set; } = new AvgInfoStrings();
            public PatternCurveDefinerStrings PatternCurveDefiner { get; private set; } = new PatternCurveDefinerStrings();

            public class LangSelectorStrings
            {
                public LsUiStrings Ui { get; private set; } = new LsUiStrings();

                public class LsUiStrings
                {
                    public LsUiDownStrings Up { get; private set; } = new LsUiDownStrings();
                    public LsUiFormStrings Form { get; private set; } = new LsUiFormStrings();

                    public class LsUiDownStrings
                    {
                        public TaggedString OkBtn { get { return new TaggedString( CurrentLanguage, "LangSelector_Ui_Down_OkBtn" ); } }
                    }

                    public class LsUiFormStrings
                    {
                        public TaggedString Text { get { return new TaggedString( CurrentLanguage, "LangSelector_Ui_Form_Text" ); } }
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
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program" ); } }
                        public TaggedString StatAnal { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program_StatisticalAnalysis" ); } }
                        public TaggedString Lang { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program_SelectLanguage" ); } }
                        public TaggedString Update { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program_CheckUpdate" ); } }
                        public TaggedString Exit { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program_Exit" ); } }
                    }

                    public class MwPanelMenuStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Panel" ); } }
                        public TaggedString KeepProp { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Panel_KeepProportions" ); } }
                        public TaggedString Hide { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Panel_Hide" ); } }
                        public TaggedString Lock { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Panel_Lock" ); } }
                    }

                    public class MwMeansMenuStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Means" ); } }
                        public TaggedString AvgInfo { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Means_AveragingInfo" ); } }
                        public TaggedString Settings { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Means_Settings" ); } }
                    }

                    public class MwChartMenuStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Chart" ); } }
                        public TaggedString Settings { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Chart_Settings" ); } }
                    }
                }

                public class MwUiStrings
                {
                    public MwFormStrings Form { get; private set; } = new MwFormStrings();

                    public class MwFormStrings
                    {
                        public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MainWindow_Ui_Form_Text" ); } }
                    }
                }

                public class MwPanelStrings
                {
                    public MwGenerateTabStrings Generate { get; private set; } = new MwGenerateTabStrings();
                    public MwDatasheetTabStrings Datasheet { get; private set; } = new MwDatasheetTabStrings();
                    public MwProgramTabStrings Program { get; private set; } = new MwProgramTabStrings();

                    public class MwGenerateTabStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate" ); } }
                        public TaggedString PattCrvScaff { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_PatternCurveScaffold" ); } }
                        public TaggedString CrvScaff1 { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold1" ); } }
                        public TaggedString ScaffPoly { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_Polynomial" ); } }
                        public TaggedString ScaffHyp { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_Hyperbolic" ); } }
                        public TaggedString ScaffWave { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_Waveform" ); } }
                        public TaggedString ScaffNone { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_CurveScaffold2_NotChosen" ); } }
                        public TaggedString Def { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_Define" ); } }
                        public TaggedString CrvsSet { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_CurvesSet" ); } }
                        public TaggedString Crvs1No { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_NumberOfCurves1" ); } }
                        public TaggedString StartX { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_StartingXPoint" ); } }
                        public TaggedString EndX { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_EndingXPoint" ); } }
                        public TaggedString Dens { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_Density" ); } }
                        public TaggedString GenSet { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_GenerateSet" ); } }
                        public TaggedString Avg { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_Averaging" ); } }
                        public TaggedString MeanT { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_MeanType" ); } }
                        public TaggedString Crvs2No { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_NumberOfCurves2" ); } }
                        public TaggedString Apply { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Generate_Apply" ); } }
                    }

                    public class MwDatasheetTabStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Datasheet" ); } }
                        public TaggedString DtSetCtrl { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Datasheet_DatasetControl" ); } }
                        public TaggedString CrvT { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Datasheet_CurveType" ); } }
                        public TaggedString CrvIdx { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Datasheet_CurveIndex" ); } }
                        public TaggedString ShowDtSet { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Datasheet_ShowDataSet" ); } }
                        public TaggedString GsNoise { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Datasheet_GaussianNoise" ); } }
                        public TaggedString CrvNo { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Datasheet_CurvesNumber" ); } }
                        public TaggedString Surr { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Datasheet_Surrounding" ); } }
                        public TaggedString Malform { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Datasheet_Malform" ); } }
                    }

                    public class MwProgramTabStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program" ); } }
                        public TaggedString Timer { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_Timer" ); } }
                        public TaggedString ActState1 { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState1" ); } }
                        public TaggedString StateFail { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Failure" ); } }
                        public TaggedString StateSucc { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Success" ); } }
                        public TaggedString Cnts1 { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Counts1" ); } }
                        public TaggedString Info { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Informations" ); } }
                        public TaggedString DotNetFr1 { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_DotNetFramework" ); } }
                        public TaggedString InfoObtErrTxt { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_InfoObtainingErrorText" ); } }
                        public TaggedString OsVer1 { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_OsVersion1" ); } }
                        public TaggedString Log { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_Logging" ); } }
                        public TaggedString LogPath1 { get { return new TaggedString( CurrentLanguage, "MainWindow_Panel_Program_ActualState2_LogPath1" ); } }
                    }

                }

            }

            public class EnumsStrings
            {
                public MeanTypesStrings MeanTypes { get; private set; } = new MeanTypesStrings();
                public LanguagesStrings Languages { get; private set; } = new LanguagesStrings();
                public DataSetCurveTypeStrings DataSetCurveTypes { get; private set; } = new DataSetCurveTypeStrings();
                public OperationsStrings Operations { get; private set; } = new OperationsStrings();
                public StatAnalysisPhenomenonIndicesStrings Phenomenons { get; private set; } = new StatAnalysisPhenomenonIndicesStrings();
                public AvgInfoTabs AiTabs { get; private set; } = new AvgInfoTabs();

                public class MeanTypesStrings : IEnumerable<TaggedString>
                {
                    private List<TaggedString> MeanTypes {
                        get {
                            return new List<TaggedString>() {
                                Mediana,
                                Maximum,
                                Minimum,
                                Arithmetic,
                                Geometric,
                                Agm,
                                Heronian,
                                Harmonic,
                                Rms,
                                Power,
                                Logarithmic,
                                Ema,
                                LnWages,
                                CstDiff,
                                CstTol,
                                CstGeo
                            };
                        }
                    }

                    public TaggedString Mediana { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_Mediana" ); } }
                    public TaggedString Maximum { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_Maximum" ); } }
                    public TaggedString Minimum { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_Minimum" ); } }
                    public TaggedString Arithmetic { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_Arithmetic" ); } }
                    public TaggedString Geometric { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_Geometric" ); } }
                    public TaggedString Agm { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_AGM" ); } }
                    public TaggedString Heronian { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_Heronian" ); } }
                    public TaggedString Harmonic { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_Harmonic" ); } }
                    public TaggedString Rms { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_RMS" ); } }
                    public TaggedString Power { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_Power" ); } }
                    public TaggedString Logarithmic { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_Logarithmic" ); } }
                    public TaggedString Ema { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_EMA" ); } }
                    public TaggedString LnWages { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_LnWages" ); } }
                    public TaggedString CstDiff { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_CustomDifferential" ); } }
                    public TaggedString CstTol { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_CustomTolerance" ); } }
                    public TaggedString CstGeo { get { return new TaggedString( CurrentLanguage, "Enums_MeanTypes_CustomGeometric" ); } }

                    public IEnumerator<TaggedString> GetEnumerator()
                    {
                        return MeanTypes.GetEnumerator();
                    }

                    IEnumerator IEnumerable.GetEnumerator()
                    {
                        return GetEnumerator();
                    }

                }

                public class LanguagesStrings : IEnumerable<TaggedString>
                {
                    private List<TaggedString> Languages {
                        get {
                            return new List<TaggedString>() {
                                English,
                                Polish
                            };
                        }
                    }

                    public TaggedString English { get { return new TaggedString( CurrentLanguage, "LangSelector_Languages_English" ); } }
                    public TaggedString Polish { get { return new TaggedString( CurrentLanguage, "LangSelector_Languages_Polish" ); } }

                    public IEnumerator<TaggedString> GetEnumerator()
                    {
                        return Languages.GetEnumerator();
                    }

                    IEnumerator IEnumerable.GetEnumerator()
                    {
                        return GetEnumerator();
                    }

                }

                public class DataSetCurveTypeStrings : IEnumerable<TaggedString>
                {
                    private List<TaggedString> CurveTypes {
                        get {
                            return new List<TaggedString>() {
                                Generated,
                                Pattern,
                                Average
                            };
                        }
                    }

                    public TaggedString Generated { get { return new TaggedString( CurrentLanguage, "Enums_DataSetCurvesTypes_Generated" ); } }
                    public TaggedString Pattern { get { return new TaggedString( CurrentLanguage, "Enums_DataSetCurvesTypes_Pattern" ); } }
                    public TaggedString Average { get { return new TaggedString( CurrentLanguage, "Enums_DataSetCurvesTypes_Average" ); } }

                    public IEnumerator<TaggedString> GetEnumerator()
                    {
                        return CurveTypes.GetEnumerator();
                    }

                    IEnumerator IEnumerable.GetEnumerator()
                    {
                        return GetEnumerator();
                    }

                }

                public class OperationsStrings : IEnumerable<TaggedString>
                {
                    private List<TaggedString> Operations {
                        get {
                            return new List<TaggedString>() {
                                Addition,
                                Substraction,
                                Multiplication,
                                Division,
                                Exponentiation,
                                Logarithmic,
                                Rooting,
                                Constant,
                                Positive,
                                Negative
                            };
                        }
                    }

                    public TaggedString Addition { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Addition" ); } }
                    public TaggedString Substraction { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Substraction" ); } }
                    public TaggedString Multiplication { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Multiplication" ); } }
                    public TaggedString Division { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Division" ); } }
                    public TaggedString Exponentiation { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Exponentiation" ); } }
                    public TaggedString Logarithmic { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Logarithmic" ); } }
                    public TaggedString Rooting { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Rooting" ); } }
                    public TaggedString Constant { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Constant" ); } }
                    public TaggedString Positive { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Positive" ); } }
                    public TaggedString Negative { get { return new TaggedString( CurrentLanguage, "Enums_Operations_Negative" ); } }

                    public IEnumerator<TaggedString> GetEnumerator()
                    {
                        return Operations.GetEnumerator();
                    }

                    IEnumerator IEnumerable.GetEnumerator()
                    {
                        return GetEnumerator();
                    }

                }

                public class StatAnalysisPhenomenonIndicesStrings : IEnumerable<TaggedString>
                {
                    private List<TaggedString> Phenomenons {
                        get {
                            return new List<TaggedString>() {
                                Peek,
                                Deformation
                            };
                        }
                    }

                    public TaggedString Peek { get { return new TaggedString( CurrentLanguage, "Enums_PhenomenonIndex_Peek" ); } }
                    public TaggedString Deformation { get { return new TaggedString( CurrentLanguage, "Enums_PhenomenonIndex_Deformation" ); } }

                    public IEnumerator<TaggedString> GetEnumerator()
                    {
                        return Phenomenons.GetEnumerator();
                    }

                    IEnumerator IEnumerable.GetEnumerator()
                    {
                        return GetEnumerator();
                    }

                }

                public class AvgInfoTabs : IEnumerable<TaggedString>
                {
                    private List<TaggedString> Tabs {
                        get {
                            return new List<TaggedString>() {
                                Geometric,
                                Agm,
                                Heronian,
                                Harmonic,
                                Power,
                                Rms,
                                Logarithmic,
                                Ema,
                                LnWages,
                                CustomDifferential,
                                CustomTolerance,
                                CustomGeometric
                            };
                        }
                    }

                    public TaggedString Geometric { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_Geometric" ); } }
                    public TaggedString Agm { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_AGM" ); } }
                    public TaggedString Heronian { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_Heronian" ); } }
                    public TaggedString Harmonic { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_Harmonic" ); } }
                    public TaggedString Power { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_Power" ); } }
                    public TaggedString Rms { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_RMS" ); } }
                    public TaggedString Logarithmic { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_Logarithmic" ); } }
                    public TaggedString Ema { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_EMA" ); } }
                    public TaggedString LnWages { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_LnWages" ); } }
                    public TaggedString CustomDifferential { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_CustomDifferential" ); } }
                    public TaggedString CustomTolerance { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_CustomTolerance" ); } }
                    public TaggedString CustomGeometric { get { return new TaggedString( CurrentLanguage, "Enums_AvgInfo_Tabs_CustomGeometric" ); } }

                    public IEnumerator<TaggedString> GetEnumerator()
                    {
                        return Tabs.GetEnumerator();
                    }

                    IEnumerator IEnumerable.GetEnumerator()
                    {
                        return GetEnumerator();
                    }

                }

            }

            public class MessageBoxShowerStrings
            {
                public MbsMainWindowStrings MainWindow { get; private set; } = new MbsMainWindowStrings();
                public MbsGridPreviewerStrings GridPreviewer { get; private set; } = new MbsGridPreviewerStrings();
                public MbsStatisticalAnalysisStrings StatAnalysis { get; private set; } = new MbsStatisticalAnalysisStrings();

                public class MbsMainWindowStrings
                {
                    public MbsUiStrings Ui { get; private set; } = new MbsUiStrings();
                    public MbsMenuStrings Menu { get; private set; } = new MbsMenuStrings();

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
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_SeriesSelectionProblem_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_SeriesSelectionProblem_Caption" ); } }
                        }

                        public class MbsChartRefreshingErrorStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_ChartRefreshingError_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_ChartRefreshingError_Caption" ); } }
                        }

                        public class MbsCurveTypeNotSelectedInfoStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_CurveTypeNotSelectedInfo_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_CurveTypeNotSelectedInfo_Caption" ); } }
                        }

                        public class MbsPatternCurveNotChosenPrerequisiteStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_PatternCurveNotChosenPrerequisite_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_PatternCurveNotChosenPrerequisite_Caption" ); } }
                        }

                        public class MbsPointsNotValidToChartProblemStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_PointsNotValidToChartProblem_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_PointsNotValidToChartProblem_Caption" ); } }
                        }

                        public class MbsSpecifiedCurveDoesntExistProblemStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_SpecifiedCurveDoesntExistProblem_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_SpecifiedCurveDoesntExistProblem_Caption" ); } }
                        }

                        public class MbsOperationMalformRejectedStopStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_OperationMalformRejectedStop_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_OperationMalformRejectedStop_Caption" ); } }
                        }

                        public class MbsNotEnoughCurvesForMedianaStopStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_NotEnoughCurvesForMedianaStop_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Ui_NotEnoughCurvesForMedianaStop_Caption" ); } }
                        }

                    }

                    public class MbsMenuStrings
                    {
                        public MbsMenuUpdateStrings Update { get; private set; } = new MbsMenuUpdateStrings();

                        public class MbsMenuUpdateStrings
                        {
                            public MbsCannotDownloadUpdateInfoProblemStrings ProblemCannotDownloadUpdateInfo { get; private set; } = new MbsCannotDownloadUpdateInfoProblemStrings();
                            public MbsRunningLatestReleaseAppInfoStrings InfoRunningLatestReleaseApp { get; private set; } = new MbsRunningLatestReleaseAppInfoStrings();
                            public MbsRunningObsoleteAppInfoStrings InfoRunningObsoleteApp { get; private set; } = new MbsRunningObsoleteAppInfoStrings();
                            public MbsCannotMatchVersionsErrorStrings ErrorCannotMatchVersions { get; private set; } = new MbsCannotMatchVersionsErrorStrings();

                            public class MbsCannotDownloadUpdateInfoProblemStrings
                            {
                                public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Menu_Update_CannotDownloadUpdateInfoProblem_Text" ); } }
                                public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Menu_Update_CannotDownloadUpdateInfoProblem_Caption" ); } }
                            }

                            public class MbsRunningLatestReleaseAppInfoStrings
                            {
                                public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Menu_Update_RunningLatestReleaseAppInfo_Text" ); } }
                                public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Menu_Update_RunningLatestReleaseAppInfo_Caption" ); } }
                            }

                            public class MbsRunningObsoleteAppInfoStrings
                            {
                                public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Menu_Update_RunningObsoleteAppInfo_Text" ); } }
                                public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Menu_Update_RunningObsoleteAppInfo_Caption" ); } }
                            }

                            public class MbsCannotMatchVersionsErrorStrings
                            {
                                public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Menu_Update_CannotMatchVersionsError_Text" ); } }
                                public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_MainWindow_Menu_Update_CannotMatchVersionsError_Caption" ); } }
                            }

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
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_IndexGreaterThanAllowedProblem_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_IndexGreaterThanAllowedProblem_Caption" ); } }
                        }

                        public class MbsIndexLowerThanAllowedProblemStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_IndexLowerThanAllowedProblem_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_IndexLowerThanAllowedProblem_Caption" ); } }
                        }

                        public class MbsImproperUserValueProblemStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_ImproperUserValueProblem_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_ImproperUserValueProblem_Caption" ); } }
                        }

                        public class MbsPerformOperationErrorStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_PerformOperationError_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_PerformOperationError_Caption" ); } }
                        }

                        public class MbsInvalidCurvePointsErrorStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_InvalidCurvePointsError_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Panel_InvalidCurvePointsError_Caption" ); } }
                        }

                    }

                    public class MbsGprvChartStrings
                    {
                        public MbsChartRefreshingErrorStrings ErrorChartRefreshing { get; private set; } = new MbsChartRefreshingErrorStrings();

                        public class MbsChartRefreshingErrorStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Chart_ChartRefreshingError_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_GridPreviewer_Chart_ChartRefreshingError_Caption" ); } }
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
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_PatternCurveDefiner_Hyperbolic_DivisionByZeroProblem_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_PatternCurveDefiner_Hyperbolic_DivisionByZeroProblem_Caption" ); } }
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

                        public class MbsSaValueOutOfRangeProblemStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_ValueOutOfRangeProblem_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_ValueOutOfRangeProblem_Caption" ); } }
                        }

                        public class MbsSaUnrecognizedErrorStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_UnrecognizedError_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_UnrecognizedError_Caption" ); } }
                        }

                        public class MbsSaPointsNotValidToChartProblemStrings
                        {
                            public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_PointsNotValidToChartProblem_Text" ); } }
                            public TaggedString Caption { get { return new TaggedString( CurrentLanguage, "MessageBoxShower_StatisticalAnalysis_Preview_PointsNotValidToChartProblem_Caption" ); } }
                        }

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
                        public TaggedString Text { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Form_Text" ); } }
                    }

                    public class GprvUiPanelStrings
                    {
                        public TaggedString DtGrid { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_DatasetGrid" ); } }
                        public TaggedString AutoSize { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_AutoSizeColumnsMode" ); } }
                        public TaggedString Edit { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_FastEdit" ); } }
                        public TaggedString OperT { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_OperationType" ); } }
                        public TaggedString StartIdx { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_StartIndex" ); } }
                        public TaggedString EndIdx { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_EndIndex" ); } }
                        public TaggedString Value { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Value" ); } }
                        public TaggedString Addend { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Addend" ); } }
                        public TaggedString Subtrahend { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Subtrahend" ); } }
                        public TaggedString Multiplier { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Multiplier" ); } }
                        public TaggedString Divisor { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Divisor" ); } }
                        public TaggedString Exponent { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Exponent" ); } }
                        public TaggedString Basis { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Basis" ); } }
                        public TaggedString NotApplicable { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_NotApplicable" ); } }
                        public TaggedString Reset { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Reset" ); } }
                        public TaggedString Perform { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Perform" ); } }
                        public TaggedString Refresh { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Refresh" ); } }
                        public TaggedString Save { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Save" ); } }
                        public TaggedString Ok { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_OK" ); } }
                        public TaggedString InfoGprvLoaded { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_GridPreviewerLoaded" ); } }
                        public TaggedString InfoOperationRevoked { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_OperationRevoked" ); } }
                        public TaggedString InfoChangesSaved { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_ChangesSaved" ); } }
                        public TaggedString InfoInvalidUserValue { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_InvalidUserValue" ); } }
                        public TaggedString InfoOperationRejected { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_OperationRejected" ); } }
                        public TaggedString InfoPerformedAndRefreshed { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_PerformedAndRefreshed" ); } }
                        public TaggedString InfoValuesRestored { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_ValuesRestored" ); } }
                    }

                    public class GprvUiPreviewStrings
                    {
                        public TaggedString InfoChartNotRepainted { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Info_ChartNotRepainted" ); } }
                        public TaggedString InfoChartRefreshError { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Info_ChartRefreshError" ); } }
                        public TaggedString InfoChartRefreshed { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Info_ChartRefreshed" ); } }
                        public TaggedString Prv { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Preview" ); } }
                    }

                    public class GprvUiDatasetStrings
                    {
                        public TaggedString DtSet { get { return new TaggedString( CurrentLanguage, "GridPreviewer_Ui_Dataset_Dataset" ); } }
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
                        public TaggedString Text { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_Form_Text" ); } }
                    }

                    public class SaUiStandardDeviationStrings
                    {
                        public TaggedString StdDev { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_StandardDeviation" ); } }
                        public TaggedString Peek { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Peek" ); } }
                        public TaggedString Deform { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Deform" ); } }
                    }

                    public class SaUiPreviewStrings
                    {
                        public TaggedString Prv { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Preview" ); } }
                        public TaggedString Chart { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Chart" ); } }
                        public TaggedString Formula { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Formula" ); } }
                        public TaggedString DtSet { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Dataset" ); } }
                        public TaggedString CrvsNo1 { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_NumberOfCurves1" ); } }
                        public TaggedString Dens1 { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Density1" ); } }
                        public TaggedString NotApplicable { get { return new TaggedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_NotApplicable" ); } }
                    }

                }
            }

            public class AvgInfoStrings
            {
                public AiTabsStrings Tabs { get; private set; } = new AiTabsStrings();
                public AiFormStrings Form { get; private set; } = new AiFormStrings();

                public class AiTabsStrings
                {
                    public AiGeometricStrings Geometric { get; private set; } = new AiGeometricStrings();
                    public AiAgmStrings Agm { get; private set; } = new AiAgmStrings();
                    public AiHeronianStrings Heronian { get; private set; } = new AiHeronianStrings();
                    public AiHarmonicStrings Harmonic { get; private set; } = new AiHarmonicStrings();
                    public AiPowerStrings Power { get; private set; } = new AiPowerStrings();
                    public AiRmsStrings Rms { get; private set; } = new AiRmsStrings();
                    public AiLogarithmicStrings Logarithmic { get; private set; } = new AiLogarithmicStrings();
                    public AiEmaStrings Ema { get; private set; } = new AiEmaStrings();
                    public AiLnWagesStrings LnWages { get; private set; } = new AiLnWagesStrings();
                    public AiCustomDifferentialStrings CustomDifferential { get; private set; } = new AiCustomDifferentialStrings();
                    public AiCustomToleranceStrings CustomTolerance { get; private set; } = new AiCustomToleranceStrings();
                    public AiCustomGeometricStrings CustomGeometric { get; private set; } = new AiCustomGeometricStrings();

                    public class AiGeometricStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Geometric_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Geometric_TextBox2Text" ); } }
                    }

                    public class AiAgmStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_AGM_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_AGM_TextBox2Text" ); } }
                    }

                    public class AiHeronianStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Heronian_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Heronian_TextBox2Text" ); } }
                    }

                    public class AiHarmonicStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Harmonic_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Harmonic_TextBox2Text" ); } }
                    }

                    public class AiPowerStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Power_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Power_TextBox2Text" ); } }
                    }

                    public class AiRmsStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_RMS_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_RMS_TextBox2Text" ); } }
                    }

                    public class AiLogarithmicStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Logarithmic_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Logarithmic_TextBox2Text" ); } }
                    }

                    public class AiEmaStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_EMA_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_EMA_TextBox2Text" ); } }
                    }

                    public class AiLnWagesStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_LnWages_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_LnWages_TextBox2Text" ); } }
                    }

                    public class AiCustomDifferentialStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_CustomDifferential_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_CustomDifferential_TextBox2Text" ); } }
                    }

                    public class AiCustomToleranceStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_CustomTolerance_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_CustomTolerance_TextBox2Text" ); } }
                    }

                    public class AiCustomGeometricStrings
                    {
                        public TaggedString TextBox1Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_CustomGeometric_TextBox1Text" ); } }
                        public TaggedString TextBox2Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_CustomGeometric_TextBox2Text" ); } }
                    }

                }

                public class AiFormStrings
                {
                    public TaggedString Text { get { return new TaggedString( CurrentLanguage, "AveragingInfo_Form_Text" ); } }
                }

            }

            public class PatternCurveDefinerStrings
            {
                public PcdFormStrings Form { get; private set; } = new PcdFormStrings();
                public PcdUiStrings Ui { get; private set; } = new PcdUiStrings();
                public PcdTabsStrings Tabs { get; private set; } = new PcdTabsStrings();

                public class PcdFormStrings
                {
                    public TaggedString Text { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Form_Text" ); } }
                }

                public class PcdUiStrings
                {
                    public TaggedString Cancel { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Ui_Cancel" ); } }
                    public TaggedString Ok { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Ui_OK" ); } }
                }

                public class PcdTabsStrings
                {
                    public PcdTabPolynomialStrings Polynomial { get; private set; } = new PcdTabPolynomialStrings();
                    public PcdTabHyperbolicStrings Hyperbolic { get; private set; } = new PcdTabHyperbolicStrings();
                    public PcdTabWaveformStrings Waveform { get; private set; } = new PcdTabWaveformStrings();

                    public class PcdTabPolynomialStrings
                    {
                        public TaggedString Pol { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Polynomial_Polynomial" ); } }
                        public TaggedString Params { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Polynomial_Parameters" ); } }
                    }

                    public class PcdTabHyperbolicStrings
                    {
                        public TaggedString Hyp { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Hyperbolic" ); } }
                        public TaggedString Params { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Parameters" ); } }
                        public TaggedString BoundAc { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Bound_ac" ); } }
                        public TaggedString BoundBd { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Bound_bd" ); } }
                    }

                    public class PcdTabWaveformStrings
                    {
                        public TaggedString Wave { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Waveform" ); } }
                        public TaggedString Params { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Parameters" ); } }
                        public TaggedString T { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_WaveType" ); } }
                        public TaggedString Sine { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Sine" ); } }
                        public TaggedString Sq { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Square" ); } }
                        public TaggedString Trg { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Triangle" ); } }
                        public TaggedString Saw { get { return new TaggedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Sawtooth" ); } }
                    }

                }

            }

        }

        public static void AddLocalizedMeanTypes<T>( T control, bool clearAtStart = true ) where T : ComboBox
        {
            if ( clearAtStart ) {
                control.Items.Clear();
            }

            foreach ( var item in GetInstance().Strings.Enums.MeanTypes ) {
                control.Items.Add( item.GetString() );
            }
        }

        public static void AddLocalizedLanguages<T>( T control, bool clearAtStart = true ) where T : ListBox
        {
            if ( clearAtStart ) {
                control.Items.Clear();
            }

            foreach ( var item in GetInstance().Strings.Enums.Languages ) {
                control.Items.Add( item.GetString() );
            }
        }

        public static void AddLocalizedDataSetCurveTypes<T>( T control, bool clearAtStart = true ) where T : ComboBox
        {
            if ( clearAtStart ) {
                control.Items.Clear();
            }

            foreach ( var item in GetInstance().Strings.Enums.DataSetCurveTypes ) {
                control.Items.Add( item.GetString() );
            }
        }

        public static void AddLocalizedOperations<T>( T control, bool clearAtStart = true ) where T : ComboBox
        {
            if ( clearAtStart ) {
                control.Items.Clear();
            }

            foreach ( var item in GetInstance().Strings.Enums.Operations ) {
                control.Items.Add( item.GetString() );
            }
        }

        public static void AddLocalizedPhenomenonsIndices<T>( T control, bool clearAtStart = true ) where T : ComboBox
        {
            if ( clearAtStart ) {
                control.Items.Clear();
            }

            foreach ( var item in GetInstance().Strings.Enums.Phenomenons ) {
                control.Items.Add( item.GetString() );
            }
        }

    }
}
