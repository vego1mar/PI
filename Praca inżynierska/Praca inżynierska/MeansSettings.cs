using PI.src.enumerators;
using PI.src.helpers;
using PI.src.localization.enums;
using PI.src.parameters;
using System;
using System.Windows.Forms;

namespace PI
{
    public partial class MeansSettings : Form
    {
        public MeansParameters MeansParams { get; private set; } = new MeansParameters();

        public MeansSettings()
        {
            InitializeComponent();
            LocalizeUi();
            UpdateUiBySettings();
        }

        // TODO: provide full settings
        public void UpdateUiBySettings()
        {
            UiControls.TrySetSelectedIndex( uiGrid_GeoVar_ComBx, (int) MeansParams.Geometric.Variant );
        }

        // TODO: provide full saving
        private void SaveSettings()
        {
            MeansParams.Geometric.Variant = (GeometricMeanVariant) UiControls.TryGetSelectedIndex( uiGrid_GeoVar_ComBx );
        }

        // TODO: provide proper localization
        private void LocalizeUi()
        {
            // Form
            Text = Translator.GetInstance().Strings.MeansSettings.Form.Text.GetString();

            // Ui
            uiGrid_Geo_TxtBx.Text = "Geometric";
            uiGrid_GeoVar_TxtBx.Text = "Variant:";
            EnumsLocalizer.Localize( LocalizableEnumerator.GeometricMeanVariant, uiGrid_GeoVar_ComBx );

            ui_Ok_Btn.Text = "OK";
        }

        #region Event handlers

        private void OnOkClick( object sender, EventArgs e )
        {
            SaveSettings();
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            Dispose();
        }

        #endregion
    }
}
