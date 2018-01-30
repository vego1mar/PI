using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.Translator;

namespace PI.src.localization.enums
{
    public class BooleanStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                    False,
                    True
                };
            }
        }

        private LocalizedString False { get { return new LocalizedString( CurrentLanguage, "Enums_Boolean_False" ); } }
        private LocalizedString True { get { return new LocalizedString( CurrentLanguage, "Enums_Boolean_True" ); } }

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
