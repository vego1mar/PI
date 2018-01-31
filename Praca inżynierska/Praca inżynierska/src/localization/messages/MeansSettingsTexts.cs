using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.messages
{
    public class MeansSettingsTexts
    {
        public WarningNotSupportedFinisherFunction NotSupportedFinisherFunction { get; } = new WarningNotSupportedFinisherFunction();

        public class WarningNotSupportedFinisherFunction
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_MeansSettings_NotSupportedFinisherFunction_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_MeansSettings_NotSupportedFinisherFunction_Caption" ); } }
        }
    }
}
