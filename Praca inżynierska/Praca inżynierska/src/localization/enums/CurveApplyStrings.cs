using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.Translator;

namespace PI.src.localization.enums
{
    public class CurveApplyStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                                Ideal,
                                Modified,
                                Average,
                                All
                            };
            }
        }

        public LocalizedString Ideal { get { return new LocalizedString( CurrentLanguage, "Enums_CurveApply_Ideal" ); } }
        public LocalizedString Modified { get { return new LocalizedString( CurrentLanguage, "Enums_CurveApply_Modified" ); } }
        public LocalizedString Average { get { return new LocalizedString( CurrentLanguage, "Enums_CurveApply_Average" ); } }
        public LocalizedString All { get { return new LocalizedString( CurrentLanguage, "Enums_CurveApply_All" ); } }

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
