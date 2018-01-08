namespace PI.src.localization
{
    public static class LanguageHelper
    {
        public static Languages GetCurrentUiLanguage()
        {
            switch ( System.Threading.Thread.CurrentThread.CurrentCulture.Name ) {
            case "en-US":
                return Languages.English;
            case "pl-PL":
                return Languages.Polish;
            }

            return Languages.English;
        }
    }
}
