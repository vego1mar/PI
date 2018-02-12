using log4net;
using PI.src.enumerators;
using PI.src.helpers;
using PI.src.localization.enums;
using PI.src.localization.windows;
using PI.src.messages;
using PI.src.parameters;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace PI.src.windows
{
    public partial class MeansSettings : Form
    {
        public MeansParameters MeansParams { get; set; }
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

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
            UiControls.TrySetValue( uiGrid_CentralMass_Num, MeansParams.Central.MassPercent );
            UiControls.TrySetValue( uiGrid_NNk_Num, MeansParams.NN.Amount );
            UiControls.TrySetSelectedIndex( uiGrid_NWVar_ComBx, (int) MeansParams.NadarayaWatson.Variant );
            UiControls.TrySetSelectedIndex( uiGrid_NWKerT_ComBx, (int) MeansParams.NadarayaWatson.KernelType );
            UiControls.TrySetValue( uiGrid_NWKerS_Num, MeansParams.NadarayaWatson.KernelSize );
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
            MeansParams.Central.MassPercent = UiControls.TryGetValue<short>( uiGrid_CentralMass_Num );
            MeansParams.NN.Amount = UiControls.TryGetValue<int>( uiGrid_NNk_Num );
            MeansParams.NadarayaWatson.Variant = (NadarayaWatsonVariant) UiControls.TryGetSelectedIndex( uiGrid_NWVar_ComBx );
            MeansParams.NadarayaWatson.KernelType = (KernelType) UiControls.TryGetSelectedIndex( uiGrid_NWKerT_ComBx );
            MeansParams.NadarayaWatson.KernelSize = UiControls.TryGetValue<double>( uiGrid_NWKerS_Num );
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
            uiGrid_CentralMass_TxtBx.Text = names.Ui.MassPercent.GetString();
            UiControls.TryRefreshOfProperty( uiGrid_CentralMass_Num );
            uiGrid_NN_TxtBx.Text = names.Ui.NN.GetString();
            uiGrid_NNk_TxtBx.Text = names.Ui.Amount.GetString();
            UiControls.TryRefreshOfProperty( uiGrid_NNk_Num );
            uiGrid_NW_TxtBx.Text = names.Ui.NadarayaWatson.GetString();
            uiGrid_NWVar_TxtBx.Text = names.Ui.Variant.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.NadarayaWatsonVariant, uiGrid_NWVar_ComBx );
            uiGrid_NWKerT_TxtBx.Text = names.Ui.KernelType.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.KernelType, uiGrid_NWKerT_ComBx );
            uiGrid_NWKerS_TxtBx.Text = names.Ui.KernelSize.GetString();
            ui_Ok_Btn.Text = names.Ui.OK.GetString();
        }

        #region Event handlers

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            // Do not nullify MeansParams property
            Dispose();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ',' + e.CloseReason + ')' );
        }

        private void OnOkClick( object sender, EventArgs e )
        {
            SaveSettings();
            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        private void OnToleranceFinisherFunctionSelection( object sender, EventArgs e )
        {
            MeanType before = (MeanType) UiControls.TryGetSelectedIndex( uiGrid_TolerFin_ComBx );

            switch ( before ) {
            case MeanType.Tolerance:
            case MeanType.NN:
            case MeanType.NadarayaWatson:
                UiControls.TrySetSelectedIndex( uiGrid_TolerFin_ComBx, (int) MeansParams.Tolerance.Finisher );
                AppMessages.MeansSettings.WarningOfNotSupportedFinisherFunction();
                break;
            default:
                MeansParams.Tolerance.Finisher = (MeanType) UiControls.TryGetSelectedIndex( uiGrid_TolerFin_ComBx );
                break;
            }

            MeanType after = (MeanType) UiControls.TryGetSelectedIndex( uiGrid_TolerFin_ComBx );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + before + ',' + after + ')' );
        }

        #endregion
    }
}
