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
            if ( language == LangSelector.Languages.Default ) {
                CurrentLanguage = LangSelector.Languages.English;
                return;
            }

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
                    case LangSelector.Languages.Default:
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
            public LangSelectorStrings LngSel { get; private set; } = new LangSelectorStrings();
            public MainWindowStrings MainWnd { get; private set; } = new MainWindowStrings();
            public EnumsStrings Enums { get; private set; } = new EnumsStrings();

            public class LangSelectorStrings
            {
                public UiStrings Ui { get; private set; } = new UiStrings();

                public class UiStrings
                {
                    public DownStrings Up { get; private set; } = new DownStrings();
                    public FormStrings Form { get; private set; } = new FormStrings();

                    public class DownStrings
                    {
                        public TaggedString OkBtn { get { return new TaggedString( CurrentLanguage, "LangSelector_Ui_Down_OkBtn" ); } }
                    }

                    public class FormStrings
                    {
                        public TaggedString Text { get { return new TaggedString( CurrentLanguage, "LangSelector_Ui_Form_Text" ); } }
                    }
                }
            }

            public class MainWindowStrings
            {
                public MenuStrings Menu { get; private set; } = new MenuStrings();
                public UiStrings Ui { get; private set; } = new UiStrings();
                public PanelStrings Pnl { get; private set; } = new PanelStrings();

                public class MenuStrings
                {
                    public ProgramStrings Prg { get; private set; } = new ProgramStrings();
                    public PanelStrings Pnl { get; private set; } = new PanelStrings();
                    public MeansStrings Means { get; private set; } = new MeansStrings();
                    public ChartStrings Chart { get; private set; } = new ChartStrings();

                    public class ProgramStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program" ); } }
                        public TaggedString StatAnal { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program_StatisticalAnalysis" ); } }
                        public TaggedString Lang { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program_SelectLanguage" ); } }
                        public TaggedString Update { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program_CheckUpdate" ); } }
                        public TaggedString Exit { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Program_Exit" ); } }
                    }

                    public class PanelStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Panel" ); } }
                        public TaggedString KeepProp { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Panel_KeepProportions" ); } }
                        public TaggedString Hide { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Panel_Hide" ); } }
                        public TaggedString Lock { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Panel_Lock" ); } }
                    }

                    public class MeansStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Means" ); } }
                        public TaggedString AvgInfo { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Means_AveragingInfo" ); } }
                        public TaggedString Settings { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Means_Settings" ); } }
                    }

                    public class ChartStrings
                    {
                        public TaggedString Title { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Chart" ); } }
                        public TaggedString Settings { get { return new TaggedString( CurrentLanguage, "MainWindow_Menu_Chart_Settings" ); } }
                    }
                }

                public class UiStrings
                {
                    public FormStrings Form { get; private set; } = new FormStrings();

                    public class FormStrings
                    {
                        public TaggedString Text { get { return new TaggedString( CurrentLanguage, "MainWindow_Ui_Form_Text" ); } }
                    }
                }

                public class PanelStrings
                {
                    public GenerateTabStrings Gen { get; private set; } = new GenerateTabStrings();
                    public DatasheetTabStrings DtSh { get; private set; } = new DatasheetTabStrings();
                    public ProgramTabStrings Prg { get; private set; } = new ProgramTabStrings();

                    public class GenerateTabStrings
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

                    public class DatasheetTabStrings
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

                    public class ProgramTabStrings
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
                                Default,
                                English,
                                Polish
                            };
                        }
                    }

                    public TaggedString Default { get { return new TaggedString( CurrentLanguage, "LangSelector_Languages_Default" ); } }
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

    }
}
