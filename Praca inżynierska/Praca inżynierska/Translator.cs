using System;
using System.Collections;
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

            public class LangSelectorStrings
            {
                public LanguagesStrings Languages { get; private set; } = new LanguagesStrings();
                public UiStrings Ui { get; private set; } = new UiStrings();

                public class LanguagesStrings : IEnumerable<TaggedString>
                {
                    private List<TaggedString> Languages { get { return new List<TaggedString>() { Default, English, Polish }; } }
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
            }
        }

    }
}
