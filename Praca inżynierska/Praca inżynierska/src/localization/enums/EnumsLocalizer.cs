using log4net;
using PI.src.localization.general;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace PI.src.localization.enums
{
    internal static class EnumsLocalizer
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public static void Localize( LocalizableEnumerator enumerator, Control control )
        {
            switch ( enumerator ) {
            case LocalizableEnumerator.Languages:
                Traverse( new LanguagesStrings(), control );
                break;
            case LocalizableEnumerator.GeometricMeanVariant:
                Traverse( new GeometricMeanVariantStrings(), control );
                break;
            case LocalizableEnumerator.StandardMeanVariant:
                Traverse( new StandardMeanVariantStrings(), control );
                break;
            case LocalizableEnumerator.MeanType:
                Traverse( new MeanTypesStrings(), control );
                break;
            case LocalizableEnumerator.DataSetCurveType:
                Traverse( new DataSetCurveTypeStrings(), control );
                break;
            case LocalizableEnumerator.Operation:
                Traverse( new OperationStrings(), control );
                break;
            case LocalizableEnumerator.Phenomenon:
                Traverse( new PhenomenonStrings(), control );
                break;
            case LocalizableEnumerator.CurveApply:
                Traverse( new CurveApplyStrings(), control );
                break;
            }
        }

        private static void Traverse<T>( IEnumerable<LocalizedString> strings, T control ) where T : Control
        {
            var typeSwitch = new Dictionary<Type, Action>() {
                { typeof(ComboBox), () => LocalizeComboBox(strings, control as ComboBox) },
                { typeof(ListBox), () => LocalizeListBox(strings, control as ListBox) }
            };

            try {
                typeSwitch[control.GetType()]();
            }
            catch ( NullReferenceException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( KeyNotFoundException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( InvalidCastException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }
        }

        private static void LocalizeComboBox( IEnumerable<LocalizedString> strings, ComboBox comboBox )
        {
            comboBox.Items.Clear();

            foreach ( LocalizedString item in strings ) {
                comboBox.Items.Add( item.GetString() );
            }
        }

        private static void LocalizeListBox( IEnumerable<LocalizedString> strings, ListBox listBox )
        {
            listBox.Items.Clear();

            foreach ( LocalizedString item in strings ) {
                listBox.Items.Add( item.GetString() );
            }
        }
    }
}
