using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using log4net;

namespace PI.src.helpers
{
    internal static class UiControls
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public static void TrySelectTab( TabControl tabControl, int index )
        {
            try {
                tabControl.SelectTab( index );
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( NullReferenceException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }
        }

        public static bool TryShowDialog( Form window, IWin32Window owner )
        {
            try {
                window.ShowDialog( owner );
            }
            catch ( NullReferenceException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( ArgumentException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( InvalidOperationException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( OutOfMemoryException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
                return false;
            }

            return true;
        }

        public static int TryGetSelectedIndex( Control control )
        {
            int selectedIndex = -1;
            var typeSwitch = new Dictionary<Type, Action> {
                { typeof(ComboBox), () => selectedIndex = (control as ComboBox).SelectedIndex },
                { typeof(ListBox), () => selectedIndex = (control as ListBox).SelectedIndex },
                { typeof(TabControl), () => selectedIndex = (control as TabControl).SelectedIndex }
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
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( ArgumentException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }

            return selectedIndex;
        }

        public static void TrySetSelectedIndex( Control control, int index )
        {
            var typeSwitch = new Dictionary<Type, Action> {
                { typeof(ComboBox), () => (control as ComboBox).SelectedIndex = index },
                { typeof(ListBox), () => (control as ListBox).SelectedIndex = index }
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
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( ArgumentException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }
        }

        public static bool TryShowMessageBox( string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            try {
                MessageBox.Show( text, caption, buttons, icon );
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( InvalidOperationException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( NullReferenceException ex ) {
                log.Error( ex.Message, ex );
                return false;
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }

            return true;
        }

        public static RowStyle TryGetRowStyle( SizeType sizeType, float height )
        {
            RowStyle rowStyle = null;

            try {
                rowStyle = new RowStyle( sizeType, height );
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }

            return rowStyle;
        }

        public static void TrySetValue<T>( Control control, T value )
        {
            var typeSwitch = new Dictionary<Type, Action>() {
                { typeof(NumericUpDown), () => (control as NumericUpDown).Value = Convert.ToDecimal(value) },
                { typeof(TrackBar), () => (control as TrackBar).Value = Convert.ToInt32(value) }
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
            catch ( FormatException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( InvalidCastException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( OverflowException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }
        }

        public static T TryGetValue<T>( Control control )
        {
            T value = default( T );
            var typeSwitch = new Dictionary<Type, Action>() {
                { typeof(NumericUpDown), () => value = (T)(Convert.ChangeType((control as NumericUpDown).Value, typeof(T), CultureInfo.InvariantCulture)) },
                { typeof(TrackBar), () => value = (T)(Convert.ChangeType((control as TrackBar).Value, typeof(T), CultureInfo.InvariantCulture)) },
                { typeof(TextBox), () => value = (T)(Convert.ChangeType((control as TextBox).Text, typeof(T), CultureInfo.InvariantCulture)) }
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
            catch ( FormatException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( OverflowException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( ArgumentNullException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }

            return value;
        }

        public static void TryRefreshOfProperty( Control control )
        {
            var typeSwitch = new Dictionary<Type, Action>() {
                { typeof(NumericUpDown), () => TryRefreshNumericUpDown(control as NumericUpDown) }
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
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }
        }

        private static void TryRefreshNumericUpDown( NumericUpDown numeric )
        {
            string signature = string.Empty;
            decimal original;

            try {
                MethodBase @base = MethodBase.GetCurrentMethod();
                signature = @base.DeclaringType.Name + "." + @base.Name + "(" + numeric.Name + ")";

                if ( numeric.Value == numeric.Minimum && numeric.Minimum == numeric.Maximum ) {
                    original = numeric.Value;
                    numeric.Maximum += numeric.Increment;
                    numeric.Value = numeric.Maximum;
                    numeric.Value = original;
                    numeric.Maximum -= numeric.Increment;
                    return;
                }

                if ( numeric.Minimum + numeric.Increment == numeric.Maximum ) {
                    if ( numeric.Value == numeric.Minimum ) {
                        numeric.Value = numeric.Maximum;
                        numeric.Value = numeric.Minimum;
                        return;
                    }

                    numeric.Value = numeric.Minimum;
                    numeric.Value = numeric.Maximum;
                    return;
                }

                original = numeric.Value;
                numeric.Value = numeric.Minimum + numeric.Increment;
                numeric.Value = original;
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Fatal( signature, ex );
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
            }
        }
    }
}
