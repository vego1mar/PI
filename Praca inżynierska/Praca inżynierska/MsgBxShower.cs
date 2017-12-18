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
        internal static GeneralMsgBoxes General { get; } = new GeneralMsgBoxes();

        internal class UiMsgBoxes
        {

            internal void SeriesSelectionProblem()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemSeriesSelection.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemSeriesSelection.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void ChartRefreshingError()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ErrorChartRefreshing.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ErrorChartRefreshing.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            internal void CurveTypeNotSelectedInfo()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.InfoCurveTypeNotSelected.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.InfoCurveTypeNotSelected.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
            }

            internal void PatternCurveNotChosenPrerequisite()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.PrerequisitePatternCurveNotChosen.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.PrerequisitePatternCurveNotChosen.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

            internal void PointsNotValidToChartProblem()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemPointsNotValidToChart.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemPointsNotValidToChart.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void SpecifiedCurveDoesntExistProblem()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemSpecifiedCurveDoesntExist.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemSpecifiedCurveDoesntExist.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void OperationMalformRejectedStop()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.StopOperationMalformRejected.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.StopOperationMalformRejected.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }

            internal void NotEnoughCurvesForMedianaStop()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.StopNotEnoughCurvesForMediana.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.StopNotEnoughCurvesForMediana.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

        }

        internal class MenuMsgBoxes
        {

            internal void CannotDownloadUpdateInfoProblem()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Menu.Update.ProblemCannotDownloadUpdateInfo.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Menu.Update.ProblemCannotDownloadUpdateInfo.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

            internal void RunningLatestReleaseAppInfo()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Menu.Update.InfoRunningLatestReleaseApp.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Menu.Update.InfoRunningLatestReleaseApp.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }

            internal void RunningObsoleteAppInfo()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Menu.Update.InfoRunningObsoleteApp.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Menu.Update.InfoRunningObsoleteApp.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }

            internal void CannotMatchVersionsError()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Menu.Update.ErrorCannotMatchVersions.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Menu.Update.ErrorCannotMatchVersions.Caption.GetString();
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
                    string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemIndexGreaterThanAllowed.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemIndexGreaterThanAllowed.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }

                internal void IndexLowerThanAllowedProblem()
                {
                    string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemIndexLowerThanAllowed.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemIndexLowerThanAllowed.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }

                internal void ImproperUserValueProblem()
                {
                    string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemImproperUserValue.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemImproperUserValue.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }

                internal void PerformOperationError()
                {
                    string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ErrorPerformOperation.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ErrorPerformOperation.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }

                internal void InvalidCurvePointsError()
                {
                    string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ErrorInvalidCurvePoints.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ErrorInvalidCurvePoints.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }

            internal class ChartMsgBoxes
            {
                internal void ChartRefreshingError()
                {
                    string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Chart.ErrorChartRefreshing.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Chart.ErrorChartRefreshing.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        internal class PcdMsgBoxes
        {
            internal void DivisionByZeroProblem()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Chart.ErrorChartRefreshing.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Chart.ErrorChartRefreshing.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }
        }

        internal class StatMsgBoxes
        {
            internal PreviewMsgBoxes Preview { get; } = new PreviewMsgBoxes();

            internal class PreviewMsgBoxes
            {
                internal void ValueOutOfRangeProblem()
                {
                    string text = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ProblemValueOutOfRange.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ProblemValueOutOfRange.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }

                internal void UnrecognizedError()
                {
                    string text = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ErrorUnrecognized.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ErrorUnrecognized.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }

                internal void PointsNotValidToChartProblem()
                {
                    string text = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ProblemPointsNotValidToChart.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ProblemPointsNotValidToChart.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }

                internal void NoSavedPresetsError()
                {
                    string text = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ErrorNoSavedPresets.Text.GetString();
                    string caption = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ErrorNoSavedPresets.Caption.GetString();
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }

            }
        }

        internal class GeneralMsgBoxes
        {
            internal void OutOfMemoryExceptionStop()
            {
                string text = Translator.GetInstance().Strings.MsgBxShower.General.StopOutOfMemoryException.Text.GetString();
                string caption = Translator.GetInstance().Strings.MsgBxShower.General.StopOutOfMemoryException.Caption.GetString();
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }
        }

    }

}
