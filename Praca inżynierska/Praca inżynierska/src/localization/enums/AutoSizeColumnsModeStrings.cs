using PI.src.localization.general;
using System.Collections;
using System.Collections.Generic;
using static PI.Translator;

namespace PI.src.localization.enums
{
    public class AutoSizeColumnsModeStrings : IEnumerable<LocalizedString>
    {
        private IList<LocalizedString> Strings {
            get {
                return new List<LocalizedString>() {
                    AllCells,
                    AllCellsExceptHeader,
                    ColumnHeader,
                    DisplayedCells,
                    DisplayedCellsExceptHeader,
                    Fill,
                    None
                };
            }
        }

        private LocalizedString AllCells { get { return new LocalizedString( CurrentLanguage, "Enums_AutoSizeColumnsMode_AllCells" ); } }
        private LocalizedString AllCellsExceptHeader { get { return new LocalizedString( CurrentLanguage, "Enums_AutoSizeColumnsMode_AllCellsExceptHeader" ); } }
        private LocalizedString ColumnHeader { get { return new LocalizedString( CurrentLanguage, "Enums_AutoSizeColumnsMode_ColumnHeader" ); } }
        private LocalizedString DisplayedCells { get { return new LocalizedString( CurrentLanguage, "Enums_AutoSizeColumnsMode_DisplayedCells" ); } }
        private LocalizedString DisplayedCellsExceptHeader { get { return new LocalizedString( CurrentLanguage, "Enums_AutoSizeColumnsMode_DisplayedCellsExceptHeader" ); } }
        private LocalizedString Fill { get { return new LocalizedString( CurrentLanguage, "Enums_AutoSizeColumnsMode_Fill" ); } }
        private LocalizedString None { get { return new LocalizedString( CurrentLanguage, "Enums_AutoSizeColumnsMode_None" ); } }

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
