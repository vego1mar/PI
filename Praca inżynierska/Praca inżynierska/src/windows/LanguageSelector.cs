﻿using PI.src.enumerators;
using PI.src.helpers;
using PI.src.localization.general;
using PI.src.localization.enums;
using System;
using System.Windows.Forms;
using PI.src.localization.windows;

namespace PI.src.windows
{
    public partial class LanguageSelector : Form
    {
        public LanguageSelector()
        {
            InitializeComponent();
            SetWindowDefaults();
        }

        public Languages GetSelectedLanguage()
        {
            return (Languages) UiControls.TryGetSelectedIndex( uiUp_LstBx );
        }

        private void SetWindowDefaults()
        {
            LocalizeWindow();
            UiControls.TrySetSelectedIndex( uiUp_LstBx, (int) LanguageAssist.GetCurrentUiLanguage() );
        }

        private void LocalizeWindow()
        {
            LanguageSelectorStrings names = new LanguageSelectorStrings();
            Text = names.Form.Text.GetString();

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
