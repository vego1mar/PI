using PI.src.localization.general;
using static PI.Translator;

namespace PI.src.localization.windows
{
    public class LanguageSelectorStrings
    {
        public LanguageSelectorFormStrings Form { get; private set; } = new LanguageSelectorFormStrings();
        public LanguageSelectorUiStrings Ui { get; private set; } = new LanguageSelectorUiStrings();

        public class LanguageSelectorFormStrings
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "LangSelector_Ui_Form_Text" ); } }
        }

        public class LanguageSelectorUiStrings
        {
            public LocalizedString OkButton { get { return new LocalizedString( CurrentLanguage, "LangSelector_Ui_Down_OkButton" ); } }
        }
    }
}
