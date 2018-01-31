using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.windows
{
    public class ChartSettingsStrings
    {
        public ChartSettingsFormStrings Form { get; private set; } = new ChartSettingsFormStrings();
        public ChartSettingsUiStrings Ui { get; private set; } = new ChartSettingsUiStrings();

        public class ChartSettingsFormStrings
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Form_Text" ); } }
        }

        public class ChartSettingsUiStrings
        {
            public LocalizedString GeneralApplyTo { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_General_ApplyToCurve" ); } }
            public LocalizedString GeneralOk { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_General_OK" ); } }
            public LocalizedString ChartTitle { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_Chart_Title" ); } }
            public LocalizedString ChartAreaTitle { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_ChartArea_Title" ); } }
            public LocalizedString ChartAreaText { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_ChartArea_ChartArea" ); } }
            public LocalizedString ChartAreaAxes { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_ChartArea_Axes" ); } }
            public LocalizedString SeriesTitle { get { return new LocalizedString( CurrentLanguage, "ChartSettings_Ui_Tabs_Series_Title" ); } }
        }
    }
}
