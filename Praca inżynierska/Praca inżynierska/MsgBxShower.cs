using System.Windows.Forms;
using System.ComponentModel;

namespace PI
{

    [Description( "Do not explicitly instantiate any class in this scope." )]
    internal static class MsgBxShower
    {

        internal static class Ui
        {

            internal static void SeriesSelectionProblem()
            {
                string text = Constants.Ui.Panel.Datasheet.SELECTED_CURVE_SERIES_TEXT;
                string caption = Constants.Ui.Panel.Datasheet.SELECTED_CURVE_SERIES_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal static void ChartRefreshingError()
            {
                string text = Constants.Ui.Charts.REFRESHING_ERR_TEXT;
                string caption = Constants.Ui.Charts.REFRESHING_ERR_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            internal static void CurveTypeNotSelectedInfo()
            {
                string text = Constants.Ui.Panel.Datasheet.CURVE_TYPE_NOT_SELECTED_TEXT;
                string caption = Constants.Ui.Panel.Datasheet.CURVE_TYPE_NOT_SELECTED_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
            }

            internal static void PatternCurveNotChosenPrerequisite()
            {
                string text = Constants.Ui.Panel.Generate.GENERATE_SET_BTN_PREREQUISITE_WARNING_TEXT;
                string caption = Constants.Ui.Panel.Generate.GENERATE_SET_BTN_PREREQUISITE_WARNING_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

        }

        internal static class Dsv
        {

            internal static void CastOrConversionProblem()
            {
                string text = Constants.Dsv.Panel.USER_VALUE_NOT_VALID_TEXT;
                string caption = Constants.Dsv.Panel.USER_VALUE_NOT_VALID_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal static void OperationTypeNotSelectedInfo()
            {
                string text = Constants.Dsv.Panel.OPERATION_TYPE_NOT_SELECTED_TEXT;
                string caption = Constants.Dsv.Panel.OPERATION_TYPE_NOT_SELECTED_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
            }

            internal static void DecimalDataTypeOverflowStop()
            {
                string text = Constants.Dsv.Panel.NOT_VALID_DECIMAL_CHART_NUMBER_TEXT;
                string caption = Constants.Dsv.Panel.NOT_VALID_DECIMAL_CHART_NUMBER_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

        }

        internal static class Pcd
        {

            internal static void DivisionByZeroProblem()
            {
                string text = Constants.Pcd.Hyperbolic.PARAMS_ZERO_DIVISION_TEXT;
                string caption = Constants.Pcd.Hyperbolic.PARAMS_ZERO_DIVISION_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

        }

    }

}
