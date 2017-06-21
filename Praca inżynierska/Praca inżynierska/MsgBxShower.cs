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
                string text = Consts.Ui.Panel.Datasheet.SELECTED_CURVE_SERIES_TEXT;
                string caption = Consts.Ui.Panel.Datasheet.SELECTED_CURVE_SERIES_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal static void ChartRefreshingError()
            {
                string text = Consts.Ui.Charts.REFRESHING_ERR_TEXT;
                string caption = Consts.Ui.Charts.REFRESHING_ERR_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            internal static void CurveTypeNotSelectedInfo()
            {
                string text = Consts.Ui.Panel.Datasheet.CURVE_TYPE_NOT_SELECTED_TEXT;
                string caption = Consts.Ui.Panel.Datasheet.CURVE_TYPE_NOT_SELECTED_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
            }

            internal static void PatternCurveNotChosenPrerequisite()
            {
                string text = Consts.Ui.Panel.Generate.GENERATE_SET_BTN_PREREQUISITE_WARNING_TEXT;
                string caption = Consts.Ui.Panel.Generate.GENERATE_SET_BTN_PREREQUISITE_WARNING_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

            internal static void PointsNotValidToChartProblem()
            {
                string text = Consts.Ui.Charts.GENERATING_WARN_TEXT;
                string caption = Consts.Ui.Charts.GENERATING_WARN_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

        }

        internal static class Menu
        {

            internal static void CannotDownloadUpdateInfoProblem()
            {
                string text = Consts.Ui.Menu.Update.DOWNLOADING_UPDATE_INFO_ERR_TEXT;
                string caption = Consts.Ui.Menu.Update.DOWNLOADING_UPDATE_INFO_ERR_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal static void RunningLatestReleaseAppInfo()
            {
                string text = Consts.Ui.Menu.Update.RUNNING_LATEST_APP_TEXT;
                string caption = Consts.Ui.Menu.Update.RUNNING_LATEST_APP_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }

            internal static void RunningObsoleteAppInfo()
            {
                string text = Consts.Ui.Menu.Update.RUNNING_OBSOLETE_APP_TEXT;
                string caption = Consts.Ui.Menu.Update.RUNNING_OBSOLETE_APP_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }

            internal static void CannotMatchVersionsError()
            {
                string text = Consts.Ui.Menu.Update.MATCHING_VERSIONS_ERR_TEXT;
                string caption = Consts.Ui.Menu.Update.MATCHING_VERSIONS_ERR_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

        }

        internal static class Dsv
        {

            internal static void CastOrConversionProblem()
            {
                string text = Consts.Dsv.Panel.USER_VALUE_NOT_VALID_TEXT;
                string caption = Consts.Dsv.Panel.USER_VALUE_NOT_VALID_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal static void OperationTypeNotSelectedInfo()
            {
                string text = Consts.Dsv.Panel.OPERATION_TYPE_NOT_SELECTED_TEXT;
                string caption = Consts.Dsv.Panel.OPERATION_TYPE_NOT_SELECTED_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
            }

            internal static void DecimalDataTypeOverflowStop()
            {
                string text = Consts.Dsv.Panel.NOT_VALID_DECIMAL_CHART_NUMBER_TEXT;
                string caption = Consts.Dsv.Panel.NOT_VALID_DECIMAL_CHART_NUMBER_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

        }

        internal static class Pcd
        {

            internal static void DivisionByZeroProblem()
            {
                string text = Consts.Pcd.Hyperbolic.PARAMS_ZERO_DIVISION_TEXT;
                string caption = Consts.Pcd.Hyperbolic.PARAMS_ZERO_DIVISION_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

        }

    }

}
