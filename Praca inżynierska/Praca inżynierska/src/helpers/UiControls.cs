using System;
using System.Collections.Generic;
using System.Windows.Forms;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo( "PI_Tests" )]

namespace PI.src.helpers
{
    internal static class UiControls
    {

        public static void TrySelectTab( TabControl tabControl, int index )
        {
            try {
                tabControl.SelectTab( index );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        public static bool TryShowDialog( Form window, IWin32Window owner )
        {
            try {
                window.ShowDialog( owner );
            }
            catch ( ArgumentException x ) {
                Logger.WriteException( x );
                return false;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteException( x );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return false;
            }

            return true;
        }

        public static int TryGetSelectedIndex( Control control )
        {
            int selectedIndex = -1;
            var typeSwitch = new Dictionary<Type, Action> {
                { typeof(ComboBox), () => selectedIndex = (control as ComboBox).SelectedIndex },
                { typeof(ListBox), () => selectedIndex = (control as ListBox).SelectedIndex }
            };

            try {
                typeSwitch[control.GetType()]();
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( ArgumentException x ) {
                Logger.WriteException( x );
            }

            return selectedIndex;
        }

        internal static void SetSelectedIndexSafe( ComboBox comboBox, int index )
        {
            try {
                comboBox.SelectedIndex = index;
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        internal static void SetSelectedIndexSafe( ListBox listBox, int index )
        {
            try {
                listBox.SelectedIndex = index;
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( ArgumentException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        internal static T GetValue<T>( NumericUpDown numeric )
        {
            T value = default( T );

            try {
                value = (T) (Convert.ChangeType( numeric.Value, typeof( T ) ));
            }
            catch ( InvalidCastException x ) {
                Logger.WriteException( x );
            }
            catch ( FormatException x ) {
                Logger.WriteException( x );
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }

            return value;
        }

        internal static void SetValue<T>( NumericUpDown numeric, T value )
        {
            try {
                numeric.Value = Convert.ToDecimal( value );
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        internal static void SetValue( TrackBar trackBar, int value )
        {
            try {
                trackBar.Value = value;
            }
            catch ( ArgumentException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        internal static int GetValue( TrackBar trackBar )
        {
            int value = 0;

            try {
                value = trackBar.Value;
            }
            catch ( ArgumentException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }

            return value;
        }

        internal static Enums.Exceptions ShowMessageBoxSafe( string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            try {
                MessageBox.Show( text, caption, buttons, icon );
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteException( x );
                return Enums.Exceptions.InvalidEnumArgumentException;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteException( x );
                return Enums.Exceptions.InvalidOperationException;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return Enums.Exceptions.Exception;
            }

            return Enums.Exceptions.Exception;
        }

        internal static bool GetValue<T>( TextBox textBox, out T value )
        {
            value = default( T );

            try {
                value = (T) (Convert.ChangeType( textBox.Text, typeof( T ) ));
            }
            catch ( InvalidCastException x ) {
                Logger.WriteException( x );
                return false;
            }
            catch ( FormatException x ) {
                Logger.WriteException( x );
                return false;
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x );
                return false;
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteException( x );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return false;
            }

            return true;
        }

        internal static RowStyle GetRowStyleSafe( SizeType sizeType, float height )
        {
            RowStyle rowStyle = null;

            try {
                rowStyle = new RowStyle( sizeType, height );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }

            return rowStyle;
        }

        internal static int GetSelectedTab( TabControl tabControl )
        {
            int selectedIdx = -1;

            try {
                selectedIdx = tabControl.SelectedIndex;
            }
            catch ( ArgumentOutOfRangeException ex ) {
                Logger.WriteException( ex );
            }
            catch ( Exception ex ) {
                Logger.WriteException( ex );
            }

            return selectedIdx;
        }

    }
}
