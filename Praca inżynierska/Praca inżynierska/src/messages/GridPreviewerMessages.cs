using PI.src.helpers;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class GridPreviewerMessages
    {
        public void ExclamationOfIndexGreaterThanAllowed()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemIndexGreaterThanAllowed.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemIndexGreaterThanAllowed.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        }

        public void ExclamationOfIndexLowerThanAllowed()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemIndexLowerThanAllowed.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemIndexLowerThanAllowed.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        }

        public void ExclamationOfImproperUserValue()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemImproperUserValue.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ProblemImproperUserValue.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        }

        public void ErrorOfPerformOperation()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ErrorPerformOperation.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ErrorPerformOperation.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        public void ErrorOfInvalidCurvePoints()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ErrorInvalidCurvePoints.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Panel.ErrorInvalidCurvePoints.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        public void ErrorOfChartRefreshing()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Chart.ErrorChartRefreshing.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.GridPreviewer.Chart.ErrorChartRefreshing.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }
    }
}
