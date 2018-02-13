using PI.src.helpers;
using PI.src.localization.messages;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class MainWindowMessages
    {
        public void ExclamationOfSeriesSelection()
        {
            UiControls.TryShowMessageBox( 
                Messages.GetInstance().MainWindow.SeriesSelection.Text.GetString(), 
                Messages.GetInstance().MainWindow.SeriesSelection.Caption.GetString(), 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Exclamation 
                );
        }

        public void ErrorOfChartRefreshing()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().MainWindow.ChartRefreshing.Text.GetString(),
                Messages.GetInstance().MainWindow.ChartRefreshing.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
        }

        public void AsteriskOfCurveTypeNotSelected()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().MainWindow.CurveTypeNotSelected.Text.GetString(),
                Messages.GetInstance().MainWindow.CurveTypeNotSelected.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk
                );
        }

        public void StopOfPatternCurveNotChosen()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().MainWindow.PatternCurveNotChosen.Text.GetString(),
                Messages.GetInstance().MainWindow.PatternCurveNotChosen.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop
                );
        }

        public void ExclamationOfPointsNotValidToChart()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().MainWindow.PointsNotValidToChart.Text.GetString(),
                Messages.GetInstance().MainWindow.PointsNotValidToChart.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation
                );
        }

        public void ExclamationOfSpecifiedCurveDoesNotExist()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().MainWindow.SpecifiedCurveDoesNotExist.Text.GetString(),
                Messages.GetInstance().MainWindow.SpecifiedCurveDoesNotExist.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation
                );
        }

        public void StopOfOperationMalformRejected()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().MainWindow.OperationMalformRejected.Text.GetString(),
                Messages.GetInstance().MainWindow.OperationMalformRejected.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop
                );
        }

        public void StopOfUnsupportedImportingError()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().MainWindow.UnsupportedImportingError.Text.GetString(),
                Messages.GetInstance().MainWindow.UnsupportedImportingError.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop
                );
        }

        public void AsteriskOfCurveDataSetImported()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().MainWindow.CurveDataSetImported.Text.GetString(),
                Messages.GetInstance().MainWindow.CurveDataSetImported.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk
                );
        }
    }
}
