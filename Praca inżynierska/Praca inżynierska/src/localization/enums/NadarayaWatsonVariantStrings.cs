using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.enums
{
    public class NadarayaWatsonVariantStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                    Subsitution,
                    NoiseCoursing
                };
            }
        }

        private LocalizedString Subsitution { get { return new LocalizedString( CurrentLanguage, "Enums_NadarayaWatsonVariant_Subsitution" ); } }
        private LocalizedString NoiseCoursing { get { return new LocalizedString( CurrentLanguage, "Enums_NadarayaWatsonVariant_NoiseCoursing" ); } }

        public IEnumerator<LocalizedString> GetEnumerator()
        {
            return Strings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
