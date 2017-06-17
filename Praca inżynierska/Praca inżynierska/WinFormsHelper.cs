﻿using System;
using System.Reflection;
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
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
        }

        internal static bool ShowDialogSafe( Form window, IWin32Window owner )
        {
            try {
                window.ShowDialog( owner );
            }
            catch ( ArgumentException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
                return false;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
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
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }

            return selectedIndex;
        }

        internal static T GetValue<T>( NumericUpDown numeric )
        {
            T value = default( T );

            try {
                value = (T) (Convert.ChangeType( numeric.Value, typeof( T ) ));
            }
            catch ( InvalidCastException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( FormatException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }

            return value;
        }

        internal static void SetValue<T>( NumericUpDown numeric, T value )
        {
            try {
                numeric.Value = Convert.ToDecimal( value );
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( ArgumentOutOfRangeException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
        }

        internal static void SetValue( TrackBar trackBar, int value )
        {
            try {
                trackBar.Value = value;
            }
            catch ( ArgumentException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
        }

        internal static int GetValue( TrackBar trackBar )
        {
            int value = 0;

            try {
                value = trackBar.Value;
            }
            catch ( ArgumentException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }

            return value;
        }

        internal static int ShowMessageBoxSafe( string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            try {
                MessageBox.Show( text, caption, buttons, icon );
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
                return Constants.Exceptions.INVALID_ENUM_ARGUMENT;
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
                return Constants.Exceptions.INVALID_OPERATION;
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
                return Constants.Exceptions.EXCEPTION;
            }

            return 0;
        }

        internal static bool GetValue<T>( TextBox textBox, out T value )
        {
            value = default( T );

            try {
                value = (T) (Convert.ChangeType( textBox.Text, typeof( T ) ));
            }
            catch ( InvalidCastException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
                return false;
            }
            catch ( FormatException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
                return false;
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
                return false;
            }
            catch ( ArgumentNullException x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
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
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.WinFormsHelper );
            }

            return rowStyle;
        }

    }
}
