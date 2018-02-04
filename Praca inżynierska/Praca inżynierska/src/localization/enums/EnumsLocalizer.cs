using log4net;
using PI.src.enumerators;
using PI.src.localization.general;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
            case LocalizableEnumerator.AutoSizeColumnsMode:
                Traverse( new AutoSizeColumnsModeStrings(), control );
                break;
            case LocalizableEnumerator.Boolean:
                Traverse( new BooleanStrings(), control );
                break;
            }
        }

        public static void Populate( CSharpEnumerable enumerable, Control control )
        {
            switch ( enumerable ) {
            case CSharpEnumerable.SeriesChartType:
                Traverse( ObtainEnumList( typeof( SeriesChartType ) ), control );
                break;
            case CSharpEnumerable.ChartDashStyle:
                Traverse( ObtainEnumList( typeof( ChartDashStyle ) ), control );
                break;
            case CSharpEnumerable.Color:
                Traverse( ObtainStructPropertiesList( typeof( Color ) ), control );
                break;
            case CSharpEnumerable.AntiAliasingStyles:
                Traverse( ObtainEnumList( typeof( AntiAliasingStyles ) ), control );
                break;
            case CSharpEnumerable.ChartAreaAxis:
                Traverse( ObtainEnumList( typeof( ChartAreaAxis ) ), control );
                break;
            case CSharpEnumerable.ChartAreaGrid:
                Traverse( ObtainEnumList( typeof( ChartAreaGrid ) ), control );
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

        private static void Traverse<T>( IList<string> strings, T control ) where T : Control
        {
            var typeSwitch = new Dictionary<Type, Action>() {
                { typeof(ComboBox), ()=> PopulateComboBox(strings, control as ComboBox) },
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

        public static void PopulateComboBox( IList<string> strings, ComboBox comboBox )
        {
            comboBox.Items.Clear();

            foreach ( string @string in strings ) {
                comboBox.Items.Add( @string );
            }
        }

        private static void LocalizeListBox( IEnumerable<LocalizedString> strings, ListBox listBox )
        {
            listBox.Items.Clear();

            foreach ( LocalizedString item in strings ) {
                listBox.Items.Add( item.GetString() );
            }
        }

        private static IList<string> ObtainEnumList( Type enumType )
        {
            IList<string> list = new List<string>();

            foreach ( string name in Enum.GetNames( enumType ) ) {
                list.Add( name );
            }

            return list;
        }

        private static IList<string> ObtainStructPropertiesList( Type structType )
        {
            IList<string> list = new List<string>();

            foreach ( var property in structType.GetProperties( BindingFlags.Static | BindingFlags.Public ) ) {
                if ( property.PropertyType == structType ) {
                    list.Add( property.Name );
                }
            }

            return list;
        }
    }
}
