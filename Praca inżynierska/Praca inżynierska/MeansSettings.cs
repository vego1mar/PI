using PI.src.general;
using PI.src.helpers;
using PI.src.parameters;
using System;
using System.Windows.Forms;

namespace PI
{
    public partial class MeansSettings : Form
    {
        public MeansParameters MeansParams { get; private set; }

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
        }

        private void Ui_Ok_Click( object sender, EventArgs e )
        {
            SaveAllSettings();
        }

        private void SaveAllSettings()
        {
            MeansParams.Generalized.Rank = UiControls.TryGetValue<int>( uiGrid_PowRank_Num );
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
            LocalizeCustomTolerance();
        }

        private void LocalizePowerMean()
        {
            uiGrid_Power_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.Power.Power.GetString();
            uiGrid_PowRank_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.Power.PowRank.GetString();
        }

        private void LocalizeCustomTolerance()
        {
            ui_Ok_Btn.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.Ok.GetString();
        }
    }
}
