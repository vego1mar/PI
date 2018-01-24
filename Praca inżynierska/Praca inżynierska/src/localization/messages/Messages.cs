using System.Runtime.CompilerServices;

namespace PI.src.localization.messages
{
    public class Messages
    {
        private static volatile Messages singleton;
        public MeansSettingsTexts MeansSettings { get; private set; }

        private Messages()
        {
            MeansSettings = new MeansSettingsTexts();
        }

        [MethodImpl( MethodImplOptions.Synchronized )]
        public static Messages GetInstance()
        {
            if ( singleton == null ) {
                singleton = new Messages();
            }

            return singleton;
        }
    }
}
