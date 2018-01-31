using PI.src.helpers;
using PI.src.localization.messages;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class MeansSettingsMessages
    {
        public void WarningOfNotSupportedFinisherFunction()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().MeansSettings.NotSupportedFinisherFunction.Text.GetString(),
                Messages.GetInstance().MeansSettings.NotSupportedFinisherFunction.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
        }
    }
}
