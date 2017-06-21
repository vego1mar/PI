using System;
using System.Windows.Forms;

namespace PI
{
    public partial class PatternCurveDefiner : Form
    {

        public int ChosenCurve { get; private set; }
        public Params Parameters { get; private set; }

        public PatternCurveDefiner()
        {
            InitializeComponent();
            MaximizeBox = false;
            DefinePropertiesInitialValues();
            SelectChosenCurveTab();
            UpdateUiByParametersValues();
        }

        private void DefinePropertiesInitialValues()
        {
            ChosenCurve = PreSets.Pcd.ChosenScaffold;
            Parameters = PreSets.Pcd.Parameters;
        }

        private void SelectChosenCurveTab()
        {
            switch ( ChosenCurve ) {
            case Consts.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL );
                uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case Consts.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC );
                uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SINE:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_WAVEFORM );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Sine_RdBtn.Checked = true;
                break;
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SQUARE:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_WAVEFORM );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Sq_RdBtn.Checked = true;
                break;
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_TRIANGLE:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_WAVEFORM );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Trg_RdBtn.Checked = true;
                break;
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SAWTOOTH:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_WAVEFORM );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Saw_RdBtn.Checked = true;
                break;
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVEFORM:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_WAVEFORM );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            default:
                break;
            }
        }

        private void UpdateUiByParametersValues()
        {
            WinFormsHelper.SetValue( uiCntPol_a_Num, PreSets.Pcd.Parameters.Polynomial.A );
            WinFormsHelper.SetValue( uiCntPol_b_Num, PreSets.Pcd.Parameters.Polynomial.B );
            WinFormsHelper.SetValue( uiCntPol_c_Num, PreSets.Pcd.Parameters.Polynomial.C );
            WinFormsHelper.SetValue( uiCntPol_d_Num, PreSets.Pcd.Parameters.Polynomial.D );
            WinFormsHelper.SetValue( uiCntPol_e_Num, PreSets.Pcd.Parameters.Polynomial.E );
            WinFormsHelper.SetValue( uiCntPol_f_Num, PreSets.Pcd.Parameters.Polynomial.F );
            WinFormsHelper.SetValue( uiCntPol_i_Num, PreSets.Pcd.Parameters.Polynomial.I );
            WinFormsHelper.SetValue( uiCntHyp_g_Num, PreSets.Pcd.Parameters.Hyperbolic.G );
            WinFormsHelper.SetValue( uiCntHyp_j_Num, PreSets.Pcd.Parameters.Hyperbolic.J );
            WinFormsHelper.SetValue( uiCntWave_m_Num, PreSets.Pcd.Parameters.Waveform.M );
            WinFormsHelper.SetValue( uiCntWave_n_Num, PreSets.Pcd.Parameters.Waveform.N );
            WinFormsHelper.SetValue( uiCntWave_o_Num, PreSets.Pcd.Parameters.Waveform.O );
            WinFormsHelper.SetValue( uiCntWave_k_Num, PreSets.Pcd.Parameters.Waveform.K );
        }

        private void UiTabs_Polynomial_Click( object sender, EventArgs e )
        {
            ChosenCurve = Consts.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL );
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Hyperbolic_Click( object sender, EventArgs e )
        {
            ChosenCurve = Consts.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC );
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Waveform_Click( object sender, EventArgs e )
        {
            ChosenCurve = Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SINE;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Consts.Ui.Panel.Generate.SCAFFOLD_WAVEFORM );
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiConfirmationPanel_Cancel_Click( object sender, EventArgs e )
        {
            try {
                DialogResult = DialogResult.Cancel;
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        private void UiConfirmationPanel_Ok_Click( object sender, EventArgs e )
        {
            try {
                DialogResult = DialogResult.OK;
                SaveParametersWhileClosingDialog();
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
        }

        private void SaveParametersWhileClosingDialog()
        {
            switch ( ChosenCurve ) {
            case Consts.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL:
                SaveParametersForPolynomialPatternCurve();
                break;
            case Consts.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                SaveParametersForHyperbolicPatternCurve();
                break;
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SINE:
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SQUARE:
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_TRIANGLE:
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SAWTOOTH:
                SaveParametersForWaveformPatternCurve( ChosenCurve );
                break;
            }
        }

        private void SaveParametersForPolynomialPatternCurve()
        {
            Parameters.Polynomial.A = WinFormsHelper.GetValue<double>( uiCntPol_a_Num );
            Parameters.Polynomial.B = WinFormsHelper.GetValue<double>( uiCntPol_b_Num );
            Parameters.Polynomial.C = WinFormsHelper.GetValue<double>( uiCntPol_c_Num );
            Parameters.Polynomial.D = WinFormsHelper.GetValue<double>( uiCntPol_d_Num );
            Parameters.Polynomial.E = WinFormsHelper.GetValue<double>( uiCntPol_e_Num );
            Parameters.Polynomial.F = WinFormsHelper.GetValue<double>( uiCntPol_f_Num );
            Parameters.Polynomial.I = WinFormsHelper.GetValue<double>( uiCntPol_i_Num );
        }

        private void SaveParametersForHyperbolicPatternCurve()
        {
            double userValue = WinFormsHelper.GetValue<double>( uiCntHyp_g_Num );

            // Checking 'decimal' value, not 'double'.
            // Four zeros after decimal separator are revelant here, not floating-point precision.
            if ( userValue == 0.0000 ) {
                MsgBxShower.Pcd.DivisionByZeroProblem();
                userValue = 0.0001;
            }

            Parameters.Hyperbolic.G = userValue;
            Parameters.Hyperbolic.J = WinFormsHelper.GetValue<double>( uiCntHyp_j_Num );
        }

        private void SaveParametersForWaveformPatternCurve( int type )
        {
            switch ( type ) {
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SINE:
                SaveParametersOfSineWaveform();
                break;
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SQUARE:
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_TRIANGLE:
            case Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SAWTOOTH:
                SaveParametersOfOtherWaveform();
                break;
            }
        }

        private void SaveParametersOfSineWaveform()
        {
            Parameters.Waveform.M = WinFormsHelper.GetValue<double>( uiCntWave_m_Num );
            Parameters.Waveform.N = WinFormsHelper.GetValue<double>( uiCntWave_n_Num );
            Parameters.Waveform.O = WinFormsHelper.GetValue<double>( uiCntWave_o_Num );
            Parameters.Waveform.K = WinFormsHelper.GetValue<double>( uiCntWave_k_Num );
        }

        private void SaveParametersOfOtherWaveform()
        {
            Parameters.Waveform.M = WinFormsHelper.GetValue<double>( uiCntWave_m_Num );
            Parameters.Waveform.N = WinFormsHelper.GetValue<double>( uiCntWave_n_Num );
            Parameters.Waveform.K = WinFormsHelper.GetValue<double>( uiCntWave_k_Num );
        }

        private void UiContentWaveformType_Sine_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Sine_RdBtn.Checked ) {
                ChosenCurve = Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SINE;
                uiCntWave_o_Num.Enabled = true;
                uiCntWave_PicBx.Image = Properties.Resources.Pattern_scaffold_waveform_sine;
            }
        }

        private void UiContentWaveformType_Square_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Sq_RdBtn.Checked ) {
                ChosenCurve = Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SQUARE;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.Pattern_scaffold_waveform_square;
            }
        }

        private void UiContentWaveformType_Triangle_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Trg_RdBtn.Checked ) {
                ChosenCurve = Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_TRIANGLE;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.Pattern_scaffold_waveform_triangle;
            }
        }

        private void UiContentWaveformType_Sawtooth_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Saw_RdBtn.Checked ) {
                ChosenCurve = Consts.Ui.Panel.Generate.SCAFFOLD_WAVE_SAWTOOTH;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.Pattern_scaffold_waveform_sawtooth;
            }
        }

    }
}
