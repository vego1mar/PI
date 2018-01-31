using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.enums
{
    public class OperationStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                                Addition,
                                Substraction,
                                Multiplication,
                                Division,
                                Exponentiation,
                                Logarithmic,
                                Rooting,
                                Constant,
                                Positive,
                                Negative
                            };
            }
        }

        public LocalizedString Addition { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Addition" ); } }
        public LocalizedString Substraction { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Substraction" ); } }
        public LocalizedString Multiplication { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Multiplication" ); } }
        public LocalizedString Division { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Division" ); } }
        public LocalizedString Exponentiation { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Exponentiation" ); } }
        public LocalizedString Logarithmic { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Logarithmic" ); } }
        public LocalizedString Rooting { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Rooting" ); } }
        public LocalizedString Constant { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Constant" ); } }
        public LocalizedString Positive { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Positive" ); } }
        public LocalizedString Negative { get { return new LocalizedString( CurrentLanguage, "Enums_Operations_Negative" ); } }

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
