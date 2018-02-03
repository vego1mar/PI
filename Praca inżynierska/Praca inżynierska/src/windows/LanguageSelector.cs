using PI.src.enumerators;
using PI.src.helpers;
using PI.src.localization.general;
using PI.src.localization.enums;
using System;
using System.Windows.Forms;
using PI.src.localization.windows;
using log4net;
using System.Reflection;

namespace PI.src.windows
{
    public partial class LanguageSelector : Form
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public LanguageSelector()
        {
            InitializeComponent();
            LocalizeWindow();
            UpdateUiBySettings();
        }

        public Languages GetSelectedLanguage()
        {
            return (Languages) UiControls.TryGetSelectedIndex( uiUp_LstBx );
        }

        private void UpdateUiBySettings()
        {
            UiControls.TrySetSelectedIndex( uiUp_LstBx, (int) LanguageAssist.GetCurrentUiLanguage() );
        }

        private void LocalizeWindow()
        {
            LanguageSelectorStrings names = new LanguageSelectorStrings();
            Text = names.Form.Text.GetString();

            // Ui
            EnumsLocalizer.Localize( LocalizableEnumerator.Languages, uiUp_LstBx );
        }

        #region Event handlers

        private void OnLoad( object sender, EventArgs e )
        {
            uiUp_LstBx.Select();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ')' );
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            Dispose();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ',' + e.CloseReason + ')' );
        }

        #endregion
    }
}
