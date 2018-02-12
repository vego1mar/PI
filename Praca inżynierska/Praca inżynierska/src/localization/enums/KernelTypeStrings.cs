using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.enums
{
    public class KernelTypeStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                    Boxcar,
                    Epanechnikov,
                    Gaussian
                };
            }
        }

        private LocalizedString Boxcar { get { return new LocalizedString( CurrentLanguage, "Enums_KernelType_Boxcar" ); } }
        private LocalizedString Epanechnikov { get { return new LocalizedString( CurrentLanguage, "Enums_KernelType_Epanechnikov" ); } }
        private LocalizedString Gaussian { get { return new LocalizedString( CurrentLanguage, "Enums_KernelType_Gaussian" ); } }

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
