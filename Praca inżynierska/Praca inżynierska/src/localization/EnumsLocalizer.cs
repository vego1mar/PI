using System.Windows.Forms;

namespace PI.src.localization
{
    internal static class EnumsLocalizer
    {
        public static void Localize( LocalizableEnumerator enumerator, Control control )
        {
            switch ( enumerator ) {
            case LocalizableEnumerator.Languages:
                LocalizeLanguages( control as ListBox );
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
    }
}
