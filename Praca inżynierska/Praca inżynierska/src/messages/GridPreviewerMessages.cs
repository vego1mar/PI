using PI.src.helpers;
using PI.src.localization.messages;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class GridPreviewerMessages
    {
        public void ExclamationOfIndexGreaterThanAllowed()
        {
            UiControls.TryShowMessageBox( 
                Messages.GetInstance().GridPreviewer.IndexGreaterThanAllowed.Text.GetString(),
                Messages.GetInstance().GridPreviewer.IndexGreaterThanAllowed.Caption.GetString(), 
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation 
                );
        }

        public void ExclamationOfIndexLowerThanAllowed()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().GridPreviewer.IndexLowerThanAllowed.Text.GetString(),
                Messages.GetInstance().GridPreviewer.IndexLowerThanAllowed.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation
                );
        }

        public void ExclamationOfImproperUserValue()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().GridPreviewer.ImproperUserValue.Text.GetString(),
                Messages.GetInstance().GridPreviewer.ImproperUserValue.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation
                );
        }

        public void ErrorOfPerformOperation()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().GridPreviewer.PerformOperation.Text.GetString(),
                Messages.GetInstance().GridPreviewer.PerformOperation.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
        }

        public void ErrorOfInvalidCurvePoints()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().GridPreviewer.InvalidCurvePoints.Text.GetString(),
                Messages.GetInstance().GridPreviewer.InvalidCurvePoints.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
        }

        public void ErrorOfChartRefreshing()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().GridPreviewer.ChartRefreshing.Text.GetString(),
                Messages.GetInstance().GridPreviewer.ChartRefreshing.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
        }
    }
}
