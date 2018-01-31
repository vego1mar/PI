using PI.src.helpers;
using PI.src.localization.messages;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class StatisticalAnalysisMessages
    {
        public void ExclamationOfValueOutOfRange()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().StatisticalAnalysis.ValueOutOfRange.Text.GetString(),
                Messages.GetInstance().StatisticalAnalysis.ValueOutOfRange.Caption.GetString(),
                MessageBoxButtons.OK, 
                MessageBoxIcon.Exclamation 
                );
        }

        public void ErrorOfUnrecognized()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().StatisticalAnalysis.Unrecognized.Text.GetString(),
                Messages.GetInstance().StatisticalAnalysis.Unrecognized.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
        }

        public void ExclamationOfPointsNotValidToChart()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().StatisticalAnalysis.PointsNotValidToChart.Text.GetString(),
                Messages.GetInstance().StatisticalAnalysis.PointsNotValidToChart.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation
                );
        }
    }
}
