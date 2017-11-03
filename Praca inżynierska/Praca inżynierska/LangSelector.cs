using System;
using System.Windows.Forms;

namespace PI
{
    public partial class LangSelector : Form
    {

        public Languages SelectedLanguage { get; private set; } = Languages.Default;

        public enum Languages
        {
            Default,
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
            WinFormsHelper.SetSelectedIndexSafe( uiUp_LstBx, (int) Languages.Default );
        }

        private void LocalizeWindow()
        {
            Text = Translator.GetInstance().Strings.LngSel.Ui.Form.Text.GetString();
            AddLocalizedLanguages( uiUp_LstBx );
        }

        private void AddLocalizedLanguages( ListBox listBox )
        {
            foreach ( var item in Translator.GetInstance().Strings.LngSel.Languages ) {
                listBox.Items.Add( item.GetString() );
            }
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
