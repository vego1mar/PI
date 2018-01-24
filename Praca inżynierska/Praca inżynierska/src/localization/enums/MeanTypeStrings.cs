using System.Collections;
using System.Collections.Generic;
using static PI.Translator;

namespace PI.src.localization.enums
{
    public class MeanTypesStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                                Median,
                                Maximum,
                                Minimum,
                                Arithmetic,
                                Geometric,
                                AGM,
                                Heronian,
                                Harmonic,
                                Generalized,
                                SMA,
                                Tolerance,
                                Central,
                                NN
                            };
            }
        }

        public LocalizedString Median { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Median" ); } }
        public LocalizedString Maximum { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Maximum" ); } }
        public LocalizedString Minimum { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Minimum" ); } }
        public LocalizedString Arithmetic { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Arithmetic" ); } }
        public LocalizedString Geometric { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Geometric" ); } }
        public LocalizedString AGM { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_AGM" ); } }
        public LocalizedString Heronian { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Heronian" ); } }
        public LocalizedString Harmonic { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Harmonic" ); } }
        public LocalizedString Generalized { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Generalized" ); } }
        public LocalizedString SMA { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_SMA" ); } }
        public LocalizedString Tolerance { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Tolerance" ); } }
        public LocalizedString Central { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_Central" ); } }
        public LocalizedString NN { get { return new LocalizedString( CurrentLanguage, "Localization_Enums_MeanType_NN" ); } }

        public IEnumerator<LocalizedString> GetEnumerator()
        {
            return Strings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IList<string> ToList()
        {
            IList<string> list = new List<string>();

            foreach ( LocalizedString item in Strings ) {
                list.Add( item.GetString() );
            }

            return list;
        }
    }
}
