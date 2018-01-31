using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.messages
{
    public class GeneralTexts
    {
        public StopOutOfMemoryException OutOfMemoryException { get; private set; } = new StopOutOfMemoryException();

        public class StopOutOfMemoryException
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_General_OutOfMemoryException_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_General_OutOfMemoryException_Caption" ); } }
        }
    }
}
