using System;
using System.Windows.Forms;

namespace PI
{
    public partial class LangSelector : Form
    {

        public Languages SelectedLanguage { get; private set; } = Languages.English;

        public enum Languages
        {
            English,
            Polish
        }

        public LangSelector()
        {
            InitializeComponent();
            SetWindowDefaults();
        }

        private void SetWindowDefaults()
        {
            LocalizeWindow();
            WinFormsHelper.SetSelectedIndexSafe( uiUp_LstBx, (int) Languages.English );
        }

        private void LocalizeWindow()
        {
            Text = Translator.GetInstance().Strings.LangSelector.Ui.Form.Text.GetString();
            Translator.AddLocalizedLanguages( uiUp_LstBx );
        }

        private void LangSelector_Load( object sender, EventArgs e )
        {
            uiDown_Ok_Btn.Select();
        }

        private void LangSelector_FormClosing( object sender, FormClosingEventArgs e )
        {
            Dispose();
        }

        private void UiDown_Ok_Button_Click( object sender, EventArgs e )
        {
            SelectedLanguage = (Languages) WinFormsHelper.GetSelectedIndexSafe( uiUp_LstBx );
        }

    }
}
