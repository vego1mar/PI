using log4net;
using PI.src.enumerators;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace PI.src.localization.general
{
    public static class LanguageAssist
    {
        public static Languages CurrentLanguage { get; set; } = Languages.English;
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public static Languages GetCurrentUiLanguage()
        {
            switch ( Thread.CurrentThread.CurrentCulture.Name ) {
            case "en-US":
                return Languages.English;
            case "pl-PL":
                return Languages.Polish;
            }

            return Languages.English;
        }

        public static string TryGetLocalizedString( string name, Languages lang )
        {
            string resourceString = string.Empty;
            string signature = string.Empty;

            try {
                signature = MethodBase.GetCurrentMethod().Name + '(' + name + ',' + lang + ')' ;

                switch ( lang ) {
                case Languages.English:
                    resourceString = Locales.en_US.ResourceManager.GetString( name );
                    break;
                case Languages.Polish:
                    resourceString = Locales.pl_PL.ResourceManager.GetString( name );
                    break;
                }
            }
            catch ( ArgumentNullException ex ) {
                log.Error( signature, ex );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( signature, ex );
            }
            catch ( System.Resources.MissingManifestResourceException ex ) {
                log.Error( signature, ex );
            }
            catch ( System.Resources.MissingSatelliteAssemblyException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }

            return resourceString;
        }

        public static void TryLocalizeCulture( Languages language )
        {
            string signature = string.Empty;

            try {
                signature = MethodBase.GetCurrentMethod().Name + '(' + language + ')';

                switch ( language ) {
                case Languages.English:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo( "en-US" );
                    break;
                case Languages.Polish:
                    Thread.CurrentThread.CurrentCulture = new CultureInfo( "pl-PL" );
                    break;
                }
            }
            catch ( CultureNotFoundException ex ) {
                log.Error( signature, ex );
            }
            catch ( ArgumentNullException ex ) {
                log.Error( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }
        }
    }
}
