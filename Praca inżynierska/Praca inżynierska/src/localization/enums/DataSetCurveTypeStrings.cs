using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.enums
{
    public class DataSetCurveTypeStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                    Ideal,
                    Modified,
                    Average
                };
            }
        }

        private LocalizedString Ideal { get { return new LocalizedString( CurrentLanguage, "Enums_DataSetCurveType_Ideal" ); } }
        private LocalizedString Modified { get { return new LocalizedString( CurrentLanguage, "Enums_DataSetCurveType_Modified" ); } }
        private LocalizedString Average { get { return new LocalizedString( CurrentLanguage, "Enums_DataSetCurveType_Average" ); } }

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
