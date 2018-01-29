using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.Translator;

namespace PI.src.localization.enums
{
    public class PhenomenonStrings : IEnumerable<LocalizedString>
    {
        private List<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                                Peek,
                                Saturation
                            };
            }
        }

        public LocalizedString Peek { get { return new LocalizedString( CurrentLanguage, "Enums_Phenomenon_Peek" ); } }
        public LocalizedString Saturation { get { return new LocalizedString( CurrentLanguage, "Enums_Phenomenon_Saturation" ); } }

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
