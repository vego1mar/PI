using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.enums
{
    public class GeometricMeanVariantStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                    Sign,
                    Parity,
                    Absolute,
                    Offset
                };
            }
        }

        private LocalizedString Sign { get { return new LocalizedString( CurrentLanguage, "Enums_GeometricMeanVariant_Sign" ); } }
        private LocalizedString Parity { get { return new LocalizedString( CurrentLanguage, "Enums_GeometricMeanVariant_Parity" ); } }
        private LocalizedString Absolute { get { return new LocalizedString( CurrentLanguage, "Enums_GeometricMeanVariant_Absolute" ); } }
        private LocalizedString Offset { get { return new LocalizedString( CurrentLanguage, "Enums_GeometricMeanVariant_Offset" ); } }

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
