using System.Windows.Forms;

namespace PI
{

    internal static class MsgBxShower
    {
        internal static UiMsgBoxes Ui { get; } = new UiMsgBoxes();
        internal static MenuMsgBoxes Menu { get; } = new MenuMsgBoxes();
        internal static GridPreviewerMsgBoxes Gprv { get; } = new GridPreviewerMsgBoxes();
        internal static PcdMsgBoxes Pcd { get; } = new PcdMsgBoxes();
        internal static StatMsgBoxes Stat { get; } = new StatMsgBoxes();

        internal class UiMsgBoxes
        {

            internal void SeriesSelectionProblem()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.ProblemSeriesSelection.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.ProblemSeriesSelection.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void ChartRefreshingError()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.ErrorChartRefreshing.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.ErrorChartRefreshing.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            internal void CurveTypeNotSelectedInfo()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.InfoCurveTypeNotSelected.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.InfoCurveTypeNotSelected.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
            }

            internal void PatternCurveNotChosenPrerequisite()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.PrerequisitePatternCurveNotChosen.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.PrerequisitePatternCurveNotChosen.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

            internal void PointsNotValidToChartProblem()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.ProblemPointsNotValidToChart.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.ProblemPointsNotValidToChart.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void SpecifiedCurveDoesntExistProblem()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.ProblemSpecifiedCurveDoesntExist.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.ProblemSpecifiedCurveDoesntExist.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void OperationMalformRejectedStop()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.StopOperationMalformRejected.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.StopOperationMalformRejected.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

            internal void NotEnoughCurvesForMedianaStop()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.StopNotEnoughCurvesForMediana.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Ui.StopNotEnoughCurvesForMediana.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

        }

        internal class MenuMsgBoxes
        {

            internal void CannotDownloadUpdateInfoProblem()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Menu.Update.ProblemCannotDownloadUpdateInfo.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Menu.Update.ProblemCannotDownloadUpdateInfo.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void RunningLatestReleaseAppInfo()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Menu.Update.InfoRunningLatestReleaseApp.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Menu.Update.InfoRunningLatestReleaseApp.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }

            internal void RunningObsoleteAppInfo()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Menu.Update.InfoRunningObsoleteApp.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Menu.Update.InfoRunningObsoleteApp.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }

            internal void CannotMatchVersionsError()
            {
                string text = Translator.GetInstance().Strings.MsgBxs.MainWnd.Menu.Update.ErrorCannotMatchVersions.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxs.MainWnd.Menu.Update.ErrorCannotMatchVersions.Caption.GetString();
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
                    string text = Translator.GetInstance().Strings.MsgBxs.Gprv.Pnl.ProblemIndexGreaterThanAllowed.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxs.Gprv.Pnl.ProblemIndexGreaterThanAllowed.Caption.GetString();
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

        internal class StatMsgBoxes
        {
            internal PreviewMsgBoxes Preview { get; } = new PreviewMsgBoxes();

            internal class PreviewMsgBoxes
            {
                internal ChartMsgBoxes Chart { get; } = new ChartMsgBoxes();

                internal class ChartMsgBoxes
                {
                    internal void ValueOutOfRangeProblem()
                    {
                        string text = Consts.Stat.Preview.Chart.OutOfRangeTxt;
                        string caption = Consts.Stat.Preview.Chart.OutOfRangeCpt;
                        WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                    }

                    internal void UnrecognizedError()
                    {
                        string text = Consts.Stat.Preview.Chart.UnrecognizedErrTxt;
                        string caption = Consts.Stat.Preview.Chart.UnrecognizedErrCpt;
                        WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }

                    internal void PointsNotValidToChartProblem()
                    {
                        string text = Consts.Stat.Preview.Chart.PointsNotValidTxt;
                        string caption = Consts.Stat.Preview.Chart.PointsNotValidCpt;
                        WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                    }
                }
            }
        }

    }

}
