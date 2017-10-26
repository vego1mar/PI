using System;
using System.Windows.Forms;

namespace PI
{
    public partial class PatternCurveDefiner : Form
    {

        internal GenSettings.PcdGenSettings Settings { get; private set; }

        internal PatternCurveDefiner( GenSettings.PcdGenSettings presets )
        {
            InitializeComponent();
            DefinePropertiesInitialValues( presets );
            SelectChosenCurveTab();
            UpdateUiByParametersValues();
        }

        private void DefinePropertiesInitialValues( GenSettings.PcdGenSettings presets )
        {
            Settings = presets;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
        }

        private void SelectChosenCurveTab()
        {
            switch ( Settings.Scaffold ) {
            case Enums.PatternCurveScaffold.Polynomial:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Polynomial );
                uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case Enums.PatternCurveScaffold.Hyperbolic:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Hyperbolic );
                uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case Enums.PatternCurveScaffold.WaveformSine:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Sine_RdBtn.Checked = true;
                break;
            case Enums.PatternCurveScaffold.WaveformSquare:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Sq_RdBtn.Checked = true;
                break;
            case Enums.PatternCurveScaffold.WaveformTriangle:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Trg_RdBtn.Checked = true;
                break;
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Saw_RdBtn.Checked = true;
                break;
            case Enums.PatternCurveScaffold.Waveform:
                WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            default:
                break;
            }
        }

        private void UpdateUiByParametersValues()
        {
            WinFormsHelper.SetValue( uiCntPol_a_Num, Settings.Parameters.Polynomial.A );
            WinFormsHelper.SetValue( uiCntPol_b_Num, Settings.Parameters.Polynomial.B );
            WinFormsHelper.SetValue( uiCntPol_c_Num, Settings.Parameters.Polynomial.C );
            WinFormsHelper.SetValue( uiCntPol_d_Num, Settings.Parameters.Polynomial.D );
            WinFormsHelper.SetValue( uiCntPol_e_Num, Settings.Parameters.Polynomial.E );
            WinFormsHelper.SetValue( uiCntPol_f_Num, Settings.Parameters.Polynomial.F );
            WinFormsHelper.SetValue( uiCntPol_i_Num, Settings.Parameters.Polynomial.I );
            WinFormsHelper.SetValue( uiCntHyp_a_Num, Settings.Parameters.Hyperbolic.A );
            WinFormsHelper.SetValue( uiCntHyp_b_Num, Settings.Parameters.Hyperbolic.B );
            WinFormsHelper.SetValue( uiCntHyp_c_Num, Settings.Parameters.Hyperbolic.C );
            WinFormsHelper.SetValue( uiCntHyp_d_Num, Settings.Parameters.Hyperbolic.D );
            WinFormsHelper.SetValue( uiCntHyp_f_Num, Settings.Parameters.Hyperbolic.F );
            WinFormsHelper.SetValue( uiCntHyp_i_Num, Settings.Parameters.Hyperbolic.I );
            WinFormsHelper.SetValue( uiCntWave_m_Num, Settings.Parameters.Waveform.M );
            WinFormsHelper.SetValue( uiCntWave_n_Num, Settings.Parameters.Waveform.N );
            WinFormsHelper.SetValue( uiCntWave_o_Num, Settings.Parameters.Waveform.O );
            WinFormsHelper.SetValue( uiCntWave_k_Num, Settings.Parameters.Waveform.K );
        }

        private void UiTabs_Polynomial_Click( object sender, EventArgs e )
        {
            Settings.Scaffold = Enums.PatternCurveScaffold.Polynomial;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Polynomial );
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Hyperbolic_Click( object sender, EventArgs e )
        {
            Settings.Scaffold = Enums.PatternCurveScaffold.Hyperbolic;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Hyperbolic );
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Waveform_Click( object sender, EventArgs e )
        {
            Settings.Scaffold = Enums.PatternCurveScaffold.WaveformSine;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
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
            switch ( Settings.Scaffold ) {
            case Enums.PatternCurveScaffold.Polynomial:
                SaveParametersForPolynomialPatternCurve();
                break;
            case Enums.PatternCurveScaffold.Hyperbolic:
                SaveParametersForHyperbolicPatternCurve();
                break;
            case Enums.PatternCurveScaffold.WaveformSine:
            case Enums.PatternCurveScaffold.WaveformSquare:
            case Enums.PatternCurveScaffold.WaveformTriangle:
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                SaveParametersForWaveformPatternCurve( Settings.Scaffold );
                break;
            }
        }

        private void SaveParametersForPolynomialPatternCurve()
        {
            Settings.Parameters.Polynomial.A = WinFormsHelper.GetValue<double>( uiCntPol_a_Num );
            Settings.Parameters.Polynomial.B = WinFormsHelper.GetValue<double>( uiCntPol_b_Num );
            Settings.Parameters.Polynomial.C = WinFormsHelper.GetValue<double>( uiCntPol_c_Num );
            Settings.Parameters.Polynomial.D = WinFormsHelper.GetValue<double>( uiCntPol_d_Num );
            Settings.Parameters.Polynomial.E = WinFormsHelper.GetValue<double>( uiCntPol_e_Num );
            Settings.Parameters.Polynomial.F = WinFormsHelper.GetValue<double>( uiCntPol_f_Num );
            Settings.Parameters.Polynomial.I = WinFormsHelper.GetValue<double>( uiCntPol_i_Num );
        }

        private void SaveParametersForHyperbolicPatternCurve()
        {
            double userValue = WinFormsHelper.GetValue<double>( uiCntHyp_f_Num );

            // Checking 'decimal' value, not 'double'.
            // Four zeros after decimal separator are revelant here, not floating-point precision.
            if ( userValue == 0.0000 ) {
                MsgBxShower.Pcd.DivisionByZeroProblem();
                userValue = 0.0001;
            }

            Settings.Parameters.Hyperbolic.F = userValue;
            Settings.Parameters.Hyperbolic.A = WinFormsHelper.GetValue<double>( uiCntHyp_a_Num );
            Settings.Parameters.Hyperbolic.B = WinFormsHelper.GetValue<double>( uiCntHyp_b_Num );
            Settings.Parameters.Hyperbolic.C = WinFormsHelper.GetValue<double>( uiCntHyp_c_Num );
            Settings.Parameters.Hyperbolic.D = WinFormsHelper.GetValue<double>( uiCntHyp_d_Num );
            Settings.Parameters.Hyperbolic.I = WinFormsHelper.GetValue<double>( uiCntHyp_i_Num );
        }

        private void SaveParametersForWaveformPatternCurve( Enums.PatternCurveScaffold type )
        {
            switch ( type ) {
            case Enums.PatternCurveScaffold.WaveformSine:
                SaveParametersOfSineWaveform();
                break;
            case Enums.PatternCurveScaffold.WaveformSquare:
            case Enums.PatternCurveScaffold.WaveformTriangle:
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                SaveParametersOfOtherWaveform();
                break;
            }
        }

        private void SaveParametersOfSineWaveform()
        {
            Settings.Parameters.Waveform.M = WinFormsHelper.GetValue<double>( uiCntWave_m_Num );
            Settings.Parameters.Waveform.N = WinFormsHelper.GetValue<double>( uiCntWave_n_Num );
            Settings.Parameters.Waveform.O = WinFormsHelper.GetValue<double>( uiCntWave_o_Num );
            Settings.Parameters.Waveform.K = WinFormsHelper.GetValue<double>( uiCntWave_k_Num );
        }

        private void SaveParametersOfOtherWaveform()
        {
            Settings.Parameters.Waveform.M = WinFormsHelper.GetValue<double>( uiCntWave_m_Num );
            Settings.Parameters.Waveform.N = WinFormsHelper.GetValue<double>( uiCntWave_n_Num );
            Settings.Parameters.Waveform.K = WinFormsHelper.GetValue<double>( uiCntWave_k_Num );
        }

        private void UiContentWaveformType_Sine_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Sine_RdBtn.Checked ) {
                Settings.Scaffold = Enums.PatternCurveScaffold.WaveformSine;
                uiCntWave_o_Num.Enabled = true;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformSine;
            }
        }

        private void UiContentWaveformType_Square_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Sq_RdBtn.Checked ) {
                Settings.Scaffold = Enums.PatternCurveScaffold.WaveformSquare;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformSquare;
            }
        }

        private void UiContentWaveformType_Triangle_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Trg_RdBtn.Checked ) {
                Settings.Scaffold = Enums.PatternCurveScaffold.WaveformTriangle;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformTriangle;
            }
        }

        private void UiContentWaveformType_Sawtooth_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Saw_RdBtn.Checked ) {
                Settings.Scaffold = Enums.PatternCurveScaffold.WaveformSawtooth;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformSawtooth;
            }
        }

        private void UiContentHyperbolic_ParameterA_ValueChanged( object sender, EventArgs e )
        {
            if ( uiCntHyp_ac_ChBx.Checked ) {
                WinFormsHelper.SetValue( uiCntHyp_c_Num, WinFormsHelper.GetValue<decimal>( uiCntHyp_a_Num ) );
            }
        }

        private void UiContentHyperbolic_ParameterC_ValueChanged( object sender, EventArgs e )
        {
            if ( uiCntHyp_ac_ChBx.Checked ) {
                WinFormsHelper.SetValue( uiCntHyp_a_Num, WinFormsHelper.GetValue<decimal>( uiCntHyp_c_Num ) );
            }
        }

        private void UiContentHyperbolic_ParameterB_ValueChanged( object sender, EventArgs e )
        {
            if ( uiCntHyp_bd_ChBx.Checked ) {
                WinFormsHelper.SetValue( uiCntHyp_d_Num, WinFormsHelper.GetValue<decimal>( uiCntHyp_b_Num ) );
            }
        }

        private void UiContentHyperbolic_ParameterD_ValueChanged( object sender, EventArgs e )
        {
            if ( uiCntHyp_bd_ChBx.Checked ) {
                WinFormsHelper.SetValue( uiCntHyp_b_Num, WinFormsHelper.GetValue<decimal>( uiCntHyp_d_Num ) );
            }
        }

        private void PatternCurveDefiner_FormClosing( object sender, FormClosingEventArgs e )
        {
            Dispose();
        }

    }
}
