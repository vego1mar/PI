using System;
using System.Windows.Forms;

namespace PI
{
    static class WindowsFormsHelper
    {

        internal static void SelectTabSafe( TabControl tabControl, int index, string invokerName )
        {
            try {
                tabControl.SelectTab( index );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
        }

        internal static bool ShowDialogSafe( Form window, IWin32Window owner, string invokerName )
        {
            try {
                window.ShowDialog( owner );
            }
            catch ( ArgumentException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return false;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return false;
            }

            return true;
        }

        internal static int GetSelectedIndexSafe( ComboBox comboBox, string invokerName )
        {
            int selectedIndex = -1;

            try {
                selectedIndex = comboBox.SelectedIndex;
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }

            return selectedIndex;
        }

        internal static T GetValueFromNumericUpDown<T>( NumericUpDown numeric, string invokerName )
        {
            T value = default( T );

            try {
                value = (T) (Convert.ChangeType( numeric.Value, typeof( T ) ));
            }
            catch ( InvalidCastException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( FormatException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( OverflowException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }

            return value;
        }

        internal static void SetValueForNumericUpDown<T>( NumericUpDown numeric, T value, string invokerName )
        {
            try {
                numeric.Value = Convert.ToDecimal( value );
            }
            catch ( OverflowException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
        }

        internal static void SetValueForTrackBar( TrackBar trackBar, int value, string invokerName )
        {
            try {
                trackBar.Value = value;
            }
            catch ( ArgumentException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
        }

        internal static int GetValueFromTrackBar( TrackBar trackBar, string invokerName )
        {
            int value = 0;

            try {
                value = trackBar.Value;
            }
            catch ( ArgumentException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }

            return value;
        }

        internal static int ShowMessageBoxSafe( string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, string invokerName )
        {
            try {
                MessageBox.Show( text, caption, buttons, icon );
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.INVALID_ENUM_ARGUMENT_EXCEPTION;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.INVALID_OPERATION_EXCEPTION;
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return SharedConstants.EXCEPTION;
            }

            return 0;
        }

        internal static bool GetValueFromTextBox<T>( TextBox textBox, out T value, string invokerName )
        {
            value = default( T );

            try {
                value = (T) (Convert.ChangeType( textBox.Text, typeof( T ) ));
            }
            catch ( InvalidCastException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return false;
            }
            catch ( FormatException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return false;
            }
            catch ( OverflowException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return false;
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
                return false;
            }

            return true;
        }

        internal static RowStyle GetRowStyleSafe( SizeType sizeType, float height, string invokerName )
        {
            RowStyle rowStyle = null;

            try {
                rowStyle = new RowStyle( sizeType, height );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invokerName );
            }

            return rowStyle;
        }

    }
}
