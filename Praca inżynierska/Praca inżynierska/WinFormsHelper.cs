using System;
using System.Windows.Forms;

namespace PI
{
    static class WinFormsHelper
    {

        internal static void SelectTabSafe( TabControl tabControl, int index )
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

        internal static bool ShowDialogSafe( Form window, IWin32Window owner )
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

        internal static int GetSelectedIndexSafe( ComboBox comboBox )
        {
            int selectedIndex = -1;

            try {
                selectedIndex = comboBox.SelectedIndex;
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
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

    }
}
