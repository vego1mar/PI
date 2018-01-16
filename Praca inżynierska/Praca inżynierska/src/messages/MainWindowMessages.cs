using PI.src.helpers;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class MainWindowMessages
    {
        public void ExclamationOfSeriesSelection()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemSeriesSelection.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemSeriesSelection.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        }

        public void ErrorOfChartRefreshing()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ErrorChartRefreshing.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ErrorChartRefreshing.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        public void AsteriskOfCurveTypeNotSelected()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.InfoCurveTypeNotSelected.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.InfoCurveTypeNotSelected.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
        }

        public void StopOfPatternCurveNotChosen()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.PrerequisitePatternCurveNotChosen.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.PrerequisitePatternCurveNotChosen.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
        }

        public void ExclamationOfPointsNotValidToChart()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemPointsNotValidToChart.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemPointsNotValidToChart.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        }

        public void ExclamationOfSpecifiedCurveDoesNotExist()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemSpecifiedCurveDoesntExist.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.ProblemSpecifiedCurveDoesntExist.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        }

        public void StopOfOperationMalformRejected()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.StopOperationMalformRejected.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.StopOperationMalformRejected.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
        }

        public void ErrorOfNotEnoughCurvesForMediana()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.StopNotEnoughCurvesForMediana.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.MainWindow.Ui.StopNotEnoughCurvesForMediana.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }
    }
}
