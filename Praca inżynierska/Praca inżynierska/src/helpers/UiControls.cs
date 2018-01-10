using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
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
                Logger.WriteException( ex );
            }
            catch ( NullReferenceException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
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
                Logger.WriteException( ex );
                return false;
            }
            catch ( ArgumentException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
                return false;
            }
            catch ( InvalidOperationException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
                return false;
            }
            catch ( OutOfMemoryException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
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
                Logger.WriteException( ex );
            }
            catch ( KeyNotFoundException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
            }
            catch ( ArgumentException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
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
                Logger.WriteException( ex );
            }
            catch ( KeyNotFoundException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
            }
            catch ( ArgumentOutOfRangeException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
            }
            catch ( ArgumentException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
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
                Logger.WriteException( ex );
                return false;
            }
            catch ( InvalidOperationException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
                return false;
            }
            catch ( NullReferenceException ex ) {
                log.Error( ex.Message, ex );
                Logger.WriteException( ex );
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
                Logger.WriteException( ex );
            }
            catch ( Exception ex ) {
                log.Fatal( ex.Message, ex );
            }

            return rowStyle;
        }

        // Pending to refactor :

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
    }
}
