using PI.src.helpers;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class PatternCurveDefinerMessages
    {
        public void ExclamationOfDivisionByZero()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Chart.ErrorChartRefreshing.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Chart.ErrorChartRefreshing.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        }
    }
}
