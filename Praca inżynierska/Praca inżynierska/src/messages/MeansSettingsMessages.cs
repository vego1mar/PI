using PI.src.helpers;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class MeansSettingsMessages
    {
        public void WarningNotSupportedFinisherFunction()
        {
            UiControls.TryShowMessageBox(
                localization.messages.Messages.GetInstance().MeansSettings.NotSupportedFinisher.Text.GetString(),
                localization.messages.Messages.GetInstance().MeansSettings.NotSupportedFinisher.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
        }
    }
}
