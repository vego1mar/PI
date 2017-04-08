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
        internal static double GetValueFromNumericUpDown( NumericUpDown numeric )
            {
            double value = 0.0;

            try {
                value = Convert.ToDouble( numeric.Value );
                }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteExceptionInfo( x );
                }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x );
                }

            return value;
            }
        #endregion

        #region SetValueForNumericUpDown(...) : void
        internal static void SetValueForNumericUpDown( NumericUpDown numeric, double value )
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

        }
    }
