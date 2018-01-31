using PI.src.helpers;
using PI.src.localization.messages;
using System.Windows.Forms;

namespace PI.src.messages
{
    internal class GeneralMessages
    {
        public void StopOfOutOfMemoryException()
        {
            UiControls.TryShowMessageBox(
                Messages.GetInstance().General.OutOfMemoryException.Text.GetString(),
                Messages.GetInstance().General.OutOfMemoryException.Caption.GetString(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop
                );
        }
    }
}
