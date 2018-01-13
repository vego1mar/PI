using PI.src.helpers;
using System.Windows.Forms;

namespace PI.src.application
{
    internal class GeneralMessages
    {
        public void StopOfOutOfMemoryException()
        {
            string text = Translator.GetInstance().Strings.MsgBxShower.General.StopOutOfMemoryException.Text.GetString();
            string caption = Translator.GetInstance().Strings.MsgBxShower.General.StopOutOfMemoryException.Caption.GetString();
            UiControls.TryShowMessageBox( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
        }
    }
}
