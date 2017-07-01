using System;
using System.Windows.Forms;

namespace PI
{
    public partial class MeansSettings : Form
    {
        public double PowerMeanRank { get; private set; } = 0.5;

        public MeansSettings()
        {
            InitializeComponent();
            UpdateUiByDefaultSettings();
        }

        private void UpdateUiByDefaultSettings()
        {
            WinFormsHelper.SetValue( uiPower_Num, PowerMeanRank );
        }

        private void Ui_Ok_Click( object sender, EventArgs e )
        {
            PowerMeanRank = WinFormsHelper.GetValue<double>( uiPower_Num );
        }

        public void SetPowerMeanRank( double value )
        {
            CurvesDataManagerConsts.ChartValuesConsts consts = new CurvesDataManagerConsts.ChartValuesConsts();

            if ( value > consts.AcceptableMin && value < consts.AcceptableMax ) {
                PowerMeanRank = value;
            }

            WinFormsHelper.SetValue( uiPower_Num, PowerMeanRank );
        }

    }
}
