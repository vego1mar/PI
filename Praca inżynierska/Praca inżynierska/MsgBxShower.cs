using System.Windows.Forms;

namespace PI
{

    internal static class MsgBxShower
    {
        internal static UiMsgBoxes Ui { get; } = new UiMsgBoxes();
        internal static MenuMsgBoxes Menu { get; } = new MenuMsgBoxes();
        internal static GridPreviewerMsgBoxes Gprv { get; } = new GridPreviewerMsgBoxes();
        internal static PcdMsgBoxes Pcd { get; } = new PcdMsgBoxes();

        internal class UiMsgBoxes
        {

            internal void SeriesSelectionProblem()
            {
                string text = Consts.Ui.Panel.Datasheet.CrvSeriesSelectionTxt;
                string caption = Consts.Ui.Panel.Datasheet.CrvSeriesSelectionCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void ChartRefreshingError()
            {
                string text = Consts.Ui.Charts.RefreshingErrTxt;
                string caption = Consts.Ui.Charts.RefreshingErrCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            internal void CurveTypeNotSelectedInfo()
            {
                string text = Consts.Ui.Panel.Datasheet.CrvTypeNotSelectedTxt;
                string caption = Consts.Ui.Panel.Datasheet.CrvTypeNotSelectedCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
            }

            internal void PatternCurveNotChosenPrerequisite()
            {
                string text = Consts.Ui.Panel.Generate.GenerateSetBtnPrerequisiteWarnTxt;
                string caption = Consts.Ui.Panel.Generate.GenerateSetBtnPrerequisiteWarnCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

            internal void PointsNotValidToChartProblem()
            {
                string text = Consts.Ui.Charts.GeneratingWarnTxt;
                string caption = Consts.Ui.Charts.GeneratingWarnCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void SpecifiedCurveDoesntExistProblem()
            {
                string text = Consts.Ui.Panel.Datasheet.SpecifiedCrvDoesntExistTxt;
                string caption = Consts.Ui.Panel.Datasheet.SpecifiedCrvDoesntExistCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void OperationMalformRejectedStop()
            {
                string text = Consts.Ui.Panel.Datasheet.OperationMalformRejectedTxt;
                string caption = Consts.Ui.Panel.Datasheet.OperationMalformRejectedCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

            internal void NotEnoughCurvesForMedianaStop()
            {
                string text = Consts.Ui.Panel.Generate.NotEnoughCurvesForMedianaTxt;
                string caption = Consts.Ui.Panel.Generate.NotEnoughCurvesForMedianaCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

        }

        internal class MenuMsgBoxes
        {

            internal void CannotDownloadUpdateInfoProblem()
            {
                string text = Consts.Ui.Menu.Update.DownloadingUpdateInfoErrTxt;
                string caption = Consts.Ui.Menu.Update.DownloadingUpdateInfoErrCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void RunningLatestReleaseAppInfo()
            {
                string text = Consts.Ui.Menu.Update.RunningLatestAppTxt;
                string caption = Consts.Ui.Menu.Update.RunningLatestAppCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }

            internal void RunningObsoleteAppInfo()
            {
                string text = Consts.Ui.Menu.Update.RunningObsoleteAppTxt;
                string caption = Consts.Ui.Menu.Update.RunningObsoleteAppCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }

            internal void CannotMatchVersionsError()
            {
                string text = Consts.Ui.Menu.Update.MatchingVersionsErrTxt;
                string caption = Consts.Ui.Menu.Update.MatchingVersionsErrCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

        }

        internal class GridPreviewerMsgBoxes
        {
            internal PanelMsgBoxes Panel { get; } = new PanelMsgBoxes();
            internal ChartMsgBoxes Chart { get; } = new ChartMsgBoxes();

            internal class PanelMsgBoxes
            {
                internal void IndexGreaterThanAllowedProblem()
                {
                    string text = Consts.Gprv.Panel.IdxGreaterThanAllowedTxt;
                    string caption = Consts.Gprv.Panel.IdxGreaterThanAllowedCpt;
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }

                internal void IndexLowerThanAllowedProblem()
                {
                    string text = Consts.Gprv.Panel.IdxLowerThanAllowedTxt;
                    string caption = Consts.Gprv.Panel.IdxLowerThanAllowedCpt;
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }

                internal void ImproperUserValueProblem()
                {
                    string text = Consts.Gprv.Panel.UserValueImproperTxt;
                    string caption = Consts.Gprv.Panel.UserValueImproperCpt;
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }

                internal void PerformOperationError()
                {
                    string text = Consts.Gprv.Panel.OperationErrTxt;
                    string caption = Consts.Gprv.Panel.OperationErrCpt;
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }

                internal void InvalidCurvePointsError()
                {
                    string text = Consts.Gprv.Panel.InvalCurvePointsTxt;
                    string caption = Consts.Gprv.Panel.InvalCurvePointsCpt;
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }

            internal class ChartMsgBoxes
            {
                internal void ChartRefreshingError()
                {
                    string text = Consts.Gprv.Chart.RefreshErrTxt;
                    string caption = Consts.Gprv.Chart.RefreshErrCpt;
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        internal class PcdMsgBoxes
        {
            internal void DivisionByZeroProblem()
            {
                string text = Consts.Pcd.Hyperbolic.ParamsZeroDivTxt;
                string caption = Consts.Pcd.Hyperbolic.ParamsZeroDivCpt;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }
        }

    }

}
