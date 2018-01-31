using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.windows
{
    public class LanguageSelectorStrings
    {
        public LanguageSelectorFormStrings Form { get; private set; } = new LanguageSelectorFormStrings();
        public LanguageSelectorUiStrings Ui { get; private set; } = new LanguageSelectorUiStrings();

        public class LanguageSelectorFormStrings
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "LangSelector_Form_Text" ); } }
        }

        public class LanguageSelectorUiStrings
        {
            public LocalizedString DownOkButton { get { return new LocalizedString( CurrentLanguage, "LangSelector_Ui_Down_OkButton" ); } }
        }
    }
}
