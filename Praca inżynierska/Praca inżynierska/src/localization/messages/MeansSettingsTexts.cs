using PI.src.localization.general;
using static PI.Translator;

namespace PI.src.localization.messages
{
    public class MeansSettingsTexts
    {
        public WarningNotSupportedFinisherFunction NotSupportedFinisher { get; } = new WarningNotSupportedFinisherFunction();

        public class WarningNotSupportedFinisherFunction
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_MeansSettings_WarningNotSupportedFinisherFunction_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_MeansSettings_WarningNotSupportedFinisherFunction_Caption" ); } }
        }
    }
}
