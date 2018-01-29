using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.Translator;

namespace PI.src.localization.enums
{
    public class LanguagesStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                    English,
                    Polish
                };
            }
        }

        private LocalizedString English { get { return new LocalizedString( CurrentLanguage, "Enums_Languages_English" ); } }
        private LocalizedString Polish { get { return new LocalizedString( CurrentLanguage, "Enums_Languages_Polish" ); } }

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
