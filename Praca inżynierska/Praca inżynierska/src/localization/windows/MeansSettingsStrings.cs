using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.windows
{
    public class MeansSettingsStrings
    {
        public MeansSettingsFormStrings Form { get; private set; } = new MeansSettingsFormStrings();
        public MeansSettingsUiStrings Ui { get; private set; } = new MeansSettingsUiStrings();

        public class MeansSettingsFormStrings
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Form_Text" ); } }
        }

        public class MeansSettingsUiStrings
        {
            public LocalizedString Geometric { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_Geometric" ); } }
            public LocalizedString Variant { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_Variant" ); } }
            public LocalizedString AGM { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_AGM" ); } }
            public LocalizedString Heronian { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_Heronian" ); } }
            public LocalizedString Harmonic { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_Harmonic" ); } }
            public LocalizedString Generalized { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_Generalized" ); } }
            public LocalizedString Rank { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_Rank" ); } }
            public LocalizedString Tolerance1 { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_ToleranceHeader" ); } }
            public LocalizedString Tolerance2 { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_ToleranceText" ); } }
            public LocalizedString FinisherFunction { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_FinisherFunction" ); } }
            public LocalizedString Central { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_Central" ); } }
            public LocalizedString MassPercent { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_MassPercent" ); } }
            public LocalizedString NN { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_NN" ); } }
            public LocalizedString Amount { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_Amount" ); } }
            public LocalizedString OK { get { return new LocalizedString( CurrentLanguage, "MeansSettings_Ui_OK" ); } }
        }
    }
}
