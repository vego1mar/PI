using System;
using System.Windows.Forms;

namespace PI
{
    static class WindowsFormsHelper
    {

        #region SelectTabSafe(...) : void
        internal static void SelectTabSafe( TabControl tabControl, int index )
        {
            try {
                tabControl.SelectTab( index );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
            }
        }
        #endregion

        #region ShowDialogSafe(...) : void
        internal static bool ShowDialogSafe( Form window, IWin32Window owner )
        {
            try {
                window.ShowDialog( owner );
            }
            catch ( ArgumentException x ) {
                Logger.WriteExceptionInfo( x );
                return false;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
                return false;
            }

            return true;
        }
        #endregion

        #region GetSelectedIndexSafe(...) : int
        internal static int GetSelectedIndexSafe( ComboBox comboBox )
        {
            int selectedIndex = -1;

            try {
                selectedIndex = comboBox.SelectedIndex;
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
            }

            return selectedIndex;
        }
        #endregion

        #region GetValueFromNumericUpDown(...) : double
        internal static T GetValueFromNumericUpDown<T>( NumericUpDown numeric )
        {
            T value = default( T );

            try {
                value = (T) (Convert.ChangeType( numeric.Value, typeof( T ) ));
            }
            catch ( InvalidCastException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( FormatException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( OverflowException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
            }

            return value;
        }
        #endregion

        #region SetValueForNumericUpDown(...) : void
        internal static void SetValueForNumericUpDown<T>( NumericUpDown numeric, T value )
        {
            try {
                numeric.Value = Convert.ToDecimal( value );
            }
            catch ( OverflowException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
            }
        }
        #endregion

        #region SetValueForTrackBar(...) : void
        internal static void SetValueForTrackBar( TrackBar trackBar, int value )
        {
            try {
                trackBar.Value = value;
            }
            catch ( ArgumentException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
            }
        }
        #endregion

        #region GetValueFromTrackBar(...) : int
        internal static int GetValueFromTrackBar( TrackBar trackBar )
        {
            int value = 0;

            try {
                value = trackBar.Value;
            }
            catch ( ArgumentException x ) {
                Logger.WriteExceptionInfo( x );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
            }

            return value;
        }
        #endregion

        #region ShowMessageBoxSafe(...) : int
        internal static int ShowMessageBoxSafe( string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            try {
                MessageBox.Show( text, caption, buttons, icon );
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteExceptionInfo( x );
                return SharedConstants.INVALID_ENUM_ARGUMENT_EXCEPTION;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x );
                return SharedConstants.INVALID_OPERATION_EXCEPTION;
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
                return SharedConstants.EXCEPTION;
            }

            return 0;
        }
        #endregion

    }
}
