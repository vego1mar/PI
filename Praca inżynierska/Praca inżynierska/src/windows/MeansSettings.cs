using PI.src.enumerators;
using PI.src.helpers;
using PI.src.localization.enums;
using PI.src.localization.windows;
using PI.src.messages;
using PI.src.parameters;
using System;
using System.Windows.Forms;

namespace PI.src.windows
{
    public partial class MeansSettings : Form
    {
        public MeansParameters MeansParams { get; set; }

        public MeansSettings()
        {
            InitializeComponent();
            MeansParams = new MeansParameters();
            LocalizeWindow();
            UpdateUiBySettings();
        }

        public void UpdateUiBySettings()
        {
            UiControls.TrySetSelectedIndex( uiGrid_GeoVar_ComBx, (int) MeansParams.Geometric.Variant );
            UiControls.TrySetSelectedIndex( uiGrid_AgmVar_ComBx, (int) MeansParams.AGM.Variant );
            UiControls.TrySetSelectedIndex( uiGrid_HerVar_ComBx, (int) MeansParams.Heronian.Variant );
            UiControls.TrySetSelectedIndex( uiGrid_HarVar_ComBx, (int) MeansParams.Harmonic.Variant );
            UiControls.TrySetSelectedIndex( uiGrid_GenVar_ComBx, (int) MeansParams.Generalized.Variant );
            UiControls.TrySetValue( uiGrid_GenRank_Num, MeansParams.Generalized.Rank );
            UiControls.TrySetValue( uiGrid_TolerTol_Num, MeansParams.Tolerance.Tolerance );
            UiControls.TrySetSelectedIndex( uiGrid_TolerFin_ComBx, (int) MeansParams.Tolerance.Finisher );
            UiControls.TrySetValue( uiGrid_CentralDivs_Num, MeansParams.Central.IntervalDivisions );
            UiControls.TrySetValue( uiGrid_CentralMass_Num, MeansParams.Central.MassPercent );
            UiControls.TrySetValue( uiGrid_NNk_Num, MeansParams.NN.Amount );
        }

        private void SaveSettings()
        {
            MeansParams.Geometric.Variant = (GeometricMeanVariant) UiControls.TryGetSelectedIndex( uiGrid_GeoVar_ComBx );
            MeansParams.AGM.Variant = (GeometricMeanVariant) UiControls.TryGetSelectedIndex( uiGrid_AgmVar_ComBx );
            MeansParams.Heronian.Variant = (GeometricMeanVariant) UiControls.TryGetSelectedIndex( uiGrid_HerVar_ComBx );
            MeansParams.Harmonic.Variant = (StandardMeanVariants) UiControls.TryGetSelectedIndex( uiGrid_HarVar_ComBx );
            MeansParams.Generalized.Variant = (StandardMeanVariants) UiControls.TryGetSelectedIndex( uiGrid_GenVar_ComBx );
            MeansParams.Generalized.Rank = UiControls.TryGetValue<int>( uiGrid_GenRank_Num );
            MeansParams.Tolerance.Tolerance = UiControls.TryGetValue<double>( uiGrid_TolerTol_Num );
            MeansParams.Tolerance.Finisher = (MeanType) UiControls.TryGetSelectedIndex( uiGrid_TolerFin_ComBx );
            MeansParams.Central.IntervalDivisions = UiControls.TryGetValue<int>( uiGrid_CentralDivs_Num );
            MeansParams.Central.MassPercent = UiControls.TryGetValue<short>( uiGrid_CentralMass_Num );
            MeansParams.NN.Amount = UiControls.TryGetValue<int>( uiGrid_NNk_Num );
        }

        private void LocalizeWindow()
        {
            MeansSettingsStrings names = new MeansSettingsStrings();
            Text = names.Form.Text.GetString();

            uiGrid_Geo_TxtBx.Text = names.Ui.Geometric.GetString();
            uiGrid_GeoVar_TxtBx.Text = names.Ui.Variant.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.GeometricMeanVariant, uiGrid_GeoVar_ComBx );
            uiGrid_Agm_TxtBx.Text = names.Ui.AGM.GetString();
            uiGrid_AgmVar_TxtBx.Text = names.Ui.Variant.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.GeometricMeanVariant, uiGrid_AgmVar_ComBx );
            uiGrid_Her_TxtBx.Text = names.Ui.Heronian.GetString();
            uiGrid_HerVar_TxtBx.Text = names.Ui.Variant.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.GeometricMeanVariant, uiGrid_HerVar_ComBx );
            uiGrid_Har_TxtBx.Text = names.Ui.Harmonic.GetString();
            uiGrid_HarVar_TxtBx.Text = names.Ui.Variant.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.StandardMeanVariant, uiGrid_HarVar_ComBx );
            uiGrid_Gen_TxtBx.Text = names.Ui.Generalized.GetString();
            uiGrid_GenVar_TxtBx.Text = names.Ui.Variant.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.StandardMeanVariant, uiGrid_GenVar_ComBx );
            uiGrid_GenRank_TxtBx.Text = names.Ui.Rank.GetString();
            UiControls.TryRefreshOfProperty( uiGrid_GenRank_Num );
            uiGrid_Toler_TxtBx.Text = names.Ui.Tolerance1.GetString();
            uiGrid_TolerTol_TxtBx.Text = names.Ui.Tolerance2.GetString();
            UiControls.TryRefreshOfProperty( uiGrid_TolerTol_Num );
            uiGrid_TolerFin_TxtBx.Text = names.Ui.FinisherFunction.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.MeanType, uiGrid_TolerFin_ComBx );
            uiGrid_Central_TxtBx.Text = names.Ui.Central.GetString();
            uiGrid_CentralDivs_TxtBx.Text = names.Ui.IntervalDivisions.GetString();
            UiControls.TryRefreshOfProperty( uiGrid_CentralDivs_Num );
            uiGrid_CentralMass_TxtBx.Text = names.Ui.MassPercent.GetString();
            UiControls.TryRefreshOfProperty( uiGrid_CentralMass_Num );
            uiGrid_NN_TxtBx.Text = names.Ui.NN.GetString();
            uiGrid_NNk_TxtBx.Text = names.Ui.Amount.GetString();
            UiControls.TryRefreshOfProperty( uiGrid_NNk_Num );
            ui_Ok_Btn.Text = names.Ui.OK.GetString();
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

        private void OnToleranceFinisherFunctionSelection( object sender, EventArgs e )
        {
            switch ( (MeanType) UiControls.TryGetSelectedIndex( uiGrid_TolerFin_ComBx ) ) {
            case MeanType.Tolerance:
            case MeanType.NN:
                UiControls.TrySetSelectedIndex( uiGrid_TolerFin_ComBx, (int) MeansParams.Tolerance.Finisher );
                AppMessages.MeansSettings.WarningOfNotSupportedFinisherFunction();
                break;
            default:
                MeansParams.Tolerance.Finisher = (MeanType) UiControls.TryGetSelectedIndex( uiGrid_TolerFin_ComBx );
                break;
            }
        }

        #endregion
    }
}
