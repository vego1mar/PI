using System.Windows.Forms;
using static PI.Translator;

namespace PI.src.localization.enums
{
    internal static class EnumsLocalizer
    {
        public static void Localize( LocalizableEnumerator enumerator, Control control )
        {
            switch ( enumerator ) {
            case LocalizableEnumerator.Languages:
                LocalizeLanguages( control as ListBox );
                break;
            case LocalizableEnumerator.GeometricMeanVariant:
                LocalizeGeometricMeanVariant( control as ComboBox );
                break;
            }
        }

        private static void LocalizeLanguages<T>( T control ) where T : ListBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.Languages ) {
                control.Items.Add( item.GetString() );
            }
        }

        private static void LocalizeGeometricMeanVariant<T>( T control ) where T : ComboBox
        {
            GeometricMeanVariantStrings strings = new GeometricMeanVariantStrings();
            control.Items.Clear();

            foreach ( LocalizedString item in strings ) {
                control.Items.Add( item.GetString() );
            }
        }
    }
}
