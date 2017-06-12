using System;
using System.Windows.Forms;

namespace PI
{
    static class WinFormsHelper
    {

        public static string Context { get; set; } 

        internal static void SelectTabSafe( TabControl tabControl, int index )
        {
            Logger.Context = Context;

            try {
                tabControl.SelectTab( index );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
            finally {
                Logger.Context = string.Empty;
            }
        }

        internal static bool ShowDialogSafe( Form window, IWin32Window owner )
        {
            Logger.Context = Context;

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
            finally {
                Logger.Context = string.Empty;
            }

            return true;
        }

        internal static int GetSelectedIndexSafe( ComboBox comboBox )
        {
            int selectedIndex = -1;
            Logger.Context = Context;

            try {
                selectedIndex = comboBox.SelectedIndex;
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
            finally {
                Logger.Context = string.Empty;
            }

            return selectedIndex;
        }

        internal static T GetValue<T>( NumericUpDown numeric )
        {
            T value = default( T );
            Logger.Context = Context;

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
            finally {
                Logger.Context = string.Empty;
            }

            return value;
        }

        internal static void SetValue<T>( NumericUpDown numeric, T value )
        {
            Logger.Context = Context;

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
            finally {
                Logger.Context = string.Empty;
            }
        }

        internal static void SetValue( TrackBar trackBar, int value )
        {
            Logger.Context = Context;

            try {
                trackBar.Value = value;
            }
            catch ( ArgumentException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
            finally {
                Logger.Context = string.Empty;
            }
        }

        internal static int GetValue( TrackBar trackBar )
        {
            Logger.Context = Context;
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
            finally {
                Logger.Context = string.Empty;
            }

            return value;
        }

        internal static int ShowMessageBoxSafe( string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            Logger.Context = Context;

            try {
                MessageBox.Show( text, caption, buttons, icon );
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteException( x );
                return Constants.Exceptions.INVALID_ENUM_ARGUMENT;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteException( x );
                return Constants.Exceptions.INVALID_OPERATION;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return Constants.Exceptions.EXCEPTION;
            }
            finally {
                Logger.Context = string.Empty;
            }

            return 0;
        }

        internal static bool GetValue<T>( TextBox textBox, out T value )
        {
            Logger.Context = Context;
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
            finally {
                Logger.Context = string.Empty;
            }

            return true;
        }

        internal static RowStyle GetRowStyleSafe( SizeType sizeType, float height )
        {
            Logger.Context = Context;
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
            finally {
                Logger.Context = string.Empty;
            }

            return rowStyle;
        }

    }
}
