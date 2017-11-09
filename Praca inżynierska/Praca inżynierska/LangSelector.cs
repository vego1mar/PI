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
            WinFormsHelper.SetSelectedIndexSafe( uiUp_LstBx, (int) GetCurrentUiLanguage() );
        }

        private void LocalizeWindow()
        {
            Text = Translator.GetInstance().Strings.LangSelector.Ui.Form.Text.GetString();
            AddLocalizedLanguages( uiUp_LstBx );
        }

        private void LangSelector_Load( object sender, EventArgs e )
        {
            uiUp_LstBx.Select();
        }

        private void LangSelector_FormClosing( object sender, FormClosingEventArgs e )
        {
            Dispose();
        }

        private void UiDown_Ok_Button_Click( object sender, EventArgs e )
        {
            SelectedLanguage = (Languages) WinFormsHelper.GetSelectedIndexSafe( uiUp_LstBx );
        }

        private Languages GetCurrentUiLanguage()
        {
            switch ( System.Threading.Thread.CurrentThread.CurrentCulture.Name ) {
            case "en-US":
                return Languages.English;
            case "pl-PL":
                return Languages.Polish;
            }

            return Languages.English;
        }

        private void AddLocalizedLanguages<T>( T control ) where T : ListBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.Languages ) {
                control.Items.Add( item.GetString() );
            }
        }

    }
}
