using PI.src.general;
using PI.src.helpers;
using PI.src.settings;
using System;
using System.Windows.Forms;

namespace PI
{
    public partial class MeansSettings : Form
    {
        public MeansParameters MeansParams { get; private set; }

        // TODO: remove this
        public enum CustomDifferentialMeanMode
        {
            Mediana,
            Sum
        }

        // TODO: remove this
        public enum CustomToleranceComparerType
        {
            Mediana,
            ArithmeticMean
        }

        // TODO: replace this by MeanType
        public enum CustomToleranceFinisherFunction
        {
            Mediana,
            ArithmeticMean,
            GeometricMean,
            Maximum,
            Minimum
        }

        public MeansSettings()
        {
            InitializeComponent();
            InitializeProperties();
            LocalizeWindow();
            UpdateUiByDefaultSettings();
        }

        private void InitializeProperties()
        {
            MeansParams = new MeansParameters();
        }

        private void LocalizeWindow()
        {
            LocalizeForm();
            LocalizeUi();
        }

        private void UpdateUiByDefaultSettings()
        {
            UiControls.TrySetValue( uiGrid_PowRank_Num, MeansParams.Generalized.Rank );
            UiControls.TrySetValue( uiGrid_Toler_Num, MeansParams.Tolerance.Tolerance );
        }

        private void Ui_Ok_Click( object sender, EventArgs e )
        {
            SaveAllSettings();
        }

        private void SaveAllSettings()
        {
            MeansParams.Generalized.Rank = UiControls.TryGetValue<int>( uiGrid_PowRank_Num );
            MeansParams.Tolerance.Tolerance = UiControls.TryGetValue<double>( uiGrid_Toler_Num );
        }

        public void SetPowerMeanRank( int value )
        {
            if ( SeriesAssist.IsChartAcceptable( value ) ) {
                MeansParams.Generalized.Rank = value;
            }

            UiControls.TrySetValue( uiGrid_PowRank_Num, MeansParams.Generalized.Rank );
        }

        public void SetCustomToleranceMeanTolerance( double value )
        {
            MeansParams.Tolerance.Tolerance = value;
            UiControls.TrySetValue( uiGrid_Toler_Num, value );
        }

        private void MeansSettings_FormClosing( object sender, FormClosingEventArgs e )
        {
            Dispose();
        }

        private void LocalizeForm()
        {
            Text = Translator.GetInstance().Strings.MeansSettings.Form.Text.GetString();
        }

        private void LocalizeUi()
        {
            LocalizePowerMean();
            LocalizeCustomDifferential();
            LocalizeCustomTolerance();
        }

        private void LocalizePowerMean()
        {
            uiGrid_Power_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.Power.Power.GetString();
            uiGrid_PowRank_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.Power.PowRank.GetString();
        }

        private void LocalizeCustomDifferential()
        {
            uiGrid_CstDiff_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomDifferential.CstDiff.GetString();
            uiGrid_DiffMode_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomDifferential.DiffMode.GetString();
            AddLocalizedCustomDifferentialMeanModes( uiGrid_DiffMode_ComBx );
        }

        private void LocalizeCustomTolerance()
        {
            uiGrid_CstTol_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.CstTol.GetString();
            uiGrid_Comp_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.Comp.GetString();
            AddLocalizedCustomToleranceComparerTypes( uiGrid_Comp_ComBx );
            uiGrid_Toler_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.Toler.GetString();
            uiGrid_Finish_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.Finish.GetString();
            AddLocalizedCustomToleranceFinisherFunctions( uiGrid_Finish_ComBx );
            ui_Ok_Btn.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.Ok.GetString();
        }

        // TODO: remove this
        private void AddLocalizedCustomDifferentialMeanModes<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.CustomDifferentialMeanModes ) {
                control.Items.Add( item.GetString() );
            }
        }

        // TODO: remove this
        private void AddLocalizedCustomToleranceComparerTypes<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.CustomToleranceComparerTypes ) {
                control.Items.Add( item.GetString() );
            }
        }

        // TODO: remove this
        private void AddLocalizedCustomToleranceFinisherFunctions<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.CustomToleranceFinisherFunctions ) {
                control.Items.Add( item.GetString() );
            }
        }

    }
}
