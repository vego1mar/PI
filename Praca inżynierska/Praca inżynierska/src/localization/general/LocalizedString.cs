using PI.src.enumerators;

namespace PI.src.localization.general
{
    public class LocalizedString
    {
        private string StringName { get; set; }
        private Languages Language { get; set; }

        public LocalizedString( Languages language, string name )
        {
            Language = language;
            StringName = name;
        }

        public string GetString()
        {
            return LanguageAssist.TryGetLocalizedString( StringName, Language );
        }
    }
}
