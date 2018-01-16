using PI.src.helpers;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class StatisticalAnalysisMessages
    {
        public void ExclamationOfValueOutOfRange()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ProblemValueOutOfRange.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ProblemValueOutOfRange.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        }

        public void ErrorOfUnrecognized()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ErrorUnrecognized.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ErrorUnrecognized.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        public void ExclamationOfPointsNotValidToChart()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ProblemPointsNotValidToChart.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ProblemPointsNotValidToChart.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        }

        public void ErrorOfNoSavedPresets()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ErrorNoSavedPresets.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.StatAnalysis.Preview.ErrorNoSavedPresets.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }
    }
}
