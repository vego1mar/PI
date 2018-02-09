using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.windows
{
    public class AboutStrings
    {
        public AboutFormStrings Form { get; private set; } = new AboutFormStrings();
        public AboutUiStrings Ui { get; private set; } = new AboutUiStrings();

        public class AboutFormStrings
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "About_Form_Text" ); } }
        }

        public class AboutUiStrings
        {
            public LocalizedString TechnicalTitle { get { return new LocalizedString( CurrentLanguage, "About_Ui_TechnicalInformations_Title" ); } }
            public LocalizedString TechnicalDotNet { get { return new LocalizedString( CurrentLanguage, "About_Ui_TechnicalInformations_NETFramework" ); } }
            public LocalizedString TechnicalLogPath { get { return new LocalizedString( CurrentLanguage, "About_Ui_TechnicalInformations_LogPath" ); } }
            public LocalizedString MetadataTitle { get { return new LocalizedString( CurrentLanguage, "About_Ui_AssemblyMetadata_Title" ); } }
            public LocalizedString MetadataVersion { get { return new LocalizedString( CurrentLanguage, "About_Ui_AssemblyMetadata_Version" ); } }
            public LocalizedString MetadataCompany { get { return new LocalizedString( CurrentLanguage, "About_Ui_AssemblyMetadata_Company" ); } }
            public LocalizedString OtherTitle { get { return new LocalizedString( CurrentLanguage, "About_Ui_Other_Title" ); } }
            public LocalizedString OtherCounter { get { return new LocalizedString( CurrentLanguage, "About_Ui_Other_Counter" ); } }
            public LocalizedString OtherRepository { get { return new LocalizedString( CurrentLanguage, "About_Ui_Other_Repository" ); } }
            public LocalizedString OtherPromoter1 { get { return new LocalizedString( CurrentLanguage, "About_Ui_Other_Promoter1" ); } }
            public LocalizedString OtherPromoter2 { get { return new LocalizedString( CurrentLanguage, "About_Ui_Other_Promoter2" ); } }
        }
    }
}
