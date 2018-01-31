using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.enums
{
    public class StandardMeanVariantStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                    Straight,
                    Offset
                };
            }
        }

        private LocalizedString Straight { get { return new LocalizedString( CurrentLanguage, "Enums_StandardMeanVariant_Straight" ); } }
        private LocalizedString Offset { get { return new LocalizedString( CurrentLanguage, "Enums_StandardMeanVariant_Offset" ); } }

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
