using PI.src.localization;
using System;
using System.Windows.Forms;

namespace PI.src.windows
{
    public partial class LangSelector : Form
    {
        public LangSelector()
        {
            InitializeComponent();
            SetWindowDefaults();
        }

        public Languages GetSelectedLanguage()
        {
            return (Languages) WinFormsHelper.GetSelectedIndexSafe( uiUp_LstBx );
        }

        private void SetWindowDefaults()
        {
            LocalizeWindow();
            WinFormsHelper.SetSelectedIndexSafe( uiUp_LstBx, (int) LanguageHelper.GetCurrentUiLanguage() );
        }

        private void LocalizeWindow()
        {
            Text = Translator.GetInstance().Strings.LangSelector.Ui.Form.Text.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.Languages, uiUp_LstBx );
        }

        #region Event handlers

        private void OnLoad( object sender, EventArgs e )
        {
            uiUp_LstBx.Select();
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            Dispose();
        }

        #endregion
    }
}
