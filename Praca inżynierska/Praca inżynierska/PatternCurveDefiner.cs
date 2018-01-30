using PI.src.messages;
using PI.src.helpers;
using PI.src.settings;
using System;
using System.Windows.Forms;
using PI.src.enumerators;
using PI.src.localization.windows;

namespace PI
{
    public partial class PatternCurveDefiner : Form
    {

        internal IdealCurveDefinerGeneratorSettings Settings { get; private set; }

        internal PatternCurveDefiner( IdealCurveDefinerGeneratorSettings presets )
        {
            InitializeComponent();
            LocalizeWindow();
            DefinePropertiesInitialValues( presets );
            SelectChosenCurveTab();
            UpdateUiByParametersValues();
        }

        private void DefinePropertiesInitialValues( IdealCurveDefinerGeneratorSettings presets )
        {
            Settings = presets;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
        }

        private void SelectChosenCurveTab()
        {
            switch ( Settings.Scaffold ) {
            case IdealCurveScaffold.Polynomial:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) IdealCurveScaffold.Polynomial );
                uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case IdealCurveScaffold.Hyperbolic:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) IdealCurveScaffold.Hyperbolic );
                uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case IdealCurveScaffold.WaveformSine:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) IdealCurveScaffold.WaveformSine );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Sine_RdBtn.Checked = true;
                break;
            case IdealCurveScaffold.WaveformSquare:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) IdealCurveScaffold.WaveformSquare );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Sq_RdBtn.Checked = true;
                break;
            case IdealCurveScaffold.WaveformTriangle:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) IdealCurveScaffold.WaveformTriangle );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Trg_RdBtn.Checked = true;
                break;
            case IdealCurveScaffold.WaveformSawtooth:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) IdealCurveScaffold.WaveformSawtooth );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Saw_RdBtn.Checked = true;
                break;
            default:
                break;
            }
        }

        private void UpdateUiByParametersValues()
        {
            UiControls.TrySetValue( uiCntPol_a_Num, Settings.Parameters.Polynomial.A );
            UiControls.TrySetValue( uiCntPol_b_Num, Settings.Parameters.Polynomial.B );
            UiControls.TrySetValue( uiCntPol_c_Num, Settings.Parameters.Polynomial.C );
            UiControls.TrySetValue( uiCntPol_d_Num, Settings.Parameters.Polynomial.D );
            UiControls.TrySetValue( uiCntPol_e_Num, Settings.Parameters.Polynomial.E );
            UiControls.TrySetValue( uiCntPol_f_Num, Settings.Parameters.Polynomial.F );
            UiControls.TrySetValue( uiCntPol_i_Num, Settings.Parameters.Polynomial.I );
            UiControls.TrySetValue( uiCntHyp_a_Num, Settings.Parameters.Hyperbolic.A );
            UiControls.TrySetValue( uiCntHyp_b_Num, Settings.Parameters.Hyperbolic.B );
            UiControls.TrySetValue( uiCntHyp_c_Num, Settings.Parameters.Hyperbolic.C );
            UiControls.TrySetValue( uiCntHyp_d_Num, Settings.Parameters.Hyperbolic.D );
            UiControls.TrySetValue( uiCntHyp_f_Num, Settings.Parameters.Hyperbolic.F );
            UiControls.TrySetValue( uiCntHyp_i_Num, Settings.Parameters.Hyperbolic.I );
            UiControls.TrySetValue( uiCntWave_m_Num, Settings.Parameters.Waveform.M );
            UiControls.TrySetValue( uiCntWave_n_Num, Settings.Parameters.Waveform.N );
            UiControls.TrySetValue( uiCntWave_o_Num, Settings.Parameters.Waveform.O );
            UiControls.TrySetValue( uiCntWave_k_Num, Settings.Parameters.Waveform.K );
        }

        private void UiTabs_Polynomial_Click( object sender, EventArgs e )
        {
            Settings.Scaffold = IdealCurveScaffold.Polynomial;
            UiControls.TrySelectTab( uiCnt_TbCtrl, (int) IdealCurveScaffold.Polynomial );
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Hyperbolic_Click( object sender, EventArgs e )
        {
            Settings.Scaffold = IdealCurveScaffold.Hyperbolic;
            UiControls.TrySelectTab( uiCnt_TbCtrl, (int) IdealCurveScaffold.Hyperbolic );
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        // TODO: correct tab selection
        private void UiTabs_Waveform_Click( object sender, EventArgs e )
        {
            Settings.Scaffold = IdealCurveScaffold.WaveformSine;
            UiControls.TrySelectTab( uiCnt_TbCtrl, (int) IdealCurveScaffold.WaveformSine );
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiConfirmationPanel_Cancel_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UiConfirmationPanel_Ok_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.OK;
            SaveParametersWhileClosingDialog();
        }

        private void SaveParametersWhileClosingDialog()
        {
            switch ( Settings.Scaffold ) {
            case IdealCurveScaffold.Polynomial:
                SaveParametersForPolynomialPatternCurve();
                break;
            case IdealCurveScaffold.Hyperbolic:
                SaveParametersForHyperbolicPatternCurve();
                break;
            case IdealCurveScaffold.WaveformSine:
            case IdealCurveScaffold.WaveformSquare:
            case IdealCurveScaffold.WaveformTriangle:
            case IdealCurveScaffold.WaveformSawtooth:
                SaveParametersForWaveformPatternCurve( Settings.Scaffold );
                break;
            }
        }

        private void SaveParametersForPolynomialPatternCurve()
        {
            Settings.Parameters.Polynomial.A = UiControls.TryGetValue<double>( uiCntPol_a_Num );
            Settings.Parameters.Polynomial.B = UiControls.TryGetValue<double>( uiCntPol_b_Num );
            Settings.Parameters.Polynomial.C = UiControls.TryGetValue<double>( uiCntPol_c_Num );
            Settings.Parameters.Polynomial.D = UiControls.TryGetValue<double>( uiCntPol_d_Num );
            Settings.Parameters.Polynomial.E = UiControls.TryGetValue<double>( uiCntPol_e_Num );
            Settings.Parameters.Polynomial.F = UiControls.TryGetValue<double>( uiCntPol_f_Num );
            Settings.Parameters.Polynomial.I = UiControls.TryGetValue<double>( uiCntPol_i_Num );
        }

        private void SaveParametersForHyperbolicPatternCurve()
        {
            double userValue = UiControls.TryGetValue<double>( uiCntHyp_f_Num );

            // Checking 'decimal' value, not 'double'.
            // Four zeros after decimal separator are revelant here, not floating-point precision.
            if ( userValue == 0.0000 ) {
                Messages.PatternCurveDefiner.ExclamationOfDivisionByZero();
                userValue = 0.0001;
            }

            Settings.Parameters.Hyperbolic.F = userValue;
            Settings.Parameters.Hyperbolic.A = UiControls.TryGetValue<double>( uiCntHyp_a_Num );
            Settings.Parameters.Hyperbolic.B = UiControls.TryGetValue<double>( uiCntHyp_b_Num );
            Settings.Parameters.Hyperbolic.C = UiControls.TryGetValue<double>( uiCntHyp_c_Num );
            Settings.Parameters.Hyperbolic.D = UiControls.TryGetValue<double>( uiCntHyp_d_Num );
            Settings.Parameters.Hyperbolic.I = UiControls.TryGetValue<double>( uiCntHyp_i_Num );
        }

        private void SaveParametersForWaveformPatternCurve( IdealCurveScaffold type )
        {
            switch ( type ) {
            case IdealCurveScaffold.WaveformSine:
                SaveParametersOfSineWaveform();
                break;
            case IdealCurveScaffold.WaveformSquare:
            case IdealCurveScaffold.WaveformTriangle:
            case IdealCurveScaffold.WaveformSawtooth:
                SaveParametersOfOtherWaveform();
                break;
            }
        }

        private void SaveParametersOfSineWaveform()
        {
            Settings.Parameters.Waveform.M = UiControls.TryGetValue<double>( uiCntWave_m_Num );
            Settings.Parameters.Waveform.N = UiControls.TryGetValue<double>( uiCntWave_n_Num );
            Settings.Parameters.Waveform.O = UiControls.TryGetValue<double>( uiCntWave_o_Num );
            Settings.Parameters.Waveform.K = UiControls.TryGetValue<double>( uiCntWave_k_Num );
        }

        private void SaveParametersOfOtherWaveform()
        {
            Settings.Parameters.Waveform.M = UiControls.TryGetValue<double>( uiCntWave_m_Num );
            Settings.Parameters.Waveform.N = UiControls.TryGetValue<double>( uiCntWave_n_Num );
            Settings.Parameters.Waveform.K = UiControls.TryGetValue<double>( uiCntWave_k_Num );
        }

        private void UiContentWaveformType_Sine_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Sine_RdBtn.Checked ) {
                Settings.Scaffold = IdealCurveScaffold.WaveformSine;
                uiCntWave_o_Num.Enabled = true;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformSine;
            }
        }

        private void UiContentWaveformType_Square_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Sq_RdBtn.Checked ) {
                Settings.Scaffold = IdealCurveScaffold.WaveformSquare;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformSquare;
            }
        }

        private void UiContentWaveformType_Triangle_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Trg_RdBtn.Checked ) {
                Settings.Scaffold = IdealCurveScaffold.WaveformTriangle;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformTriangle;
            }
        }

        private void UiContentWaveformType_Sawtooth_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Saw_RdBtn.Checked ) {
                Settings.Scaffold = IdealCurveScaffold.WaveformSawtooth;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformSawtooth;
            }
        }

        private void UiContentHyperbolic_ParameterA_ValueChanged( object sender, EventArgs e )
        {
            if ( uiCntHyp_ac_ChBx.Checked ) {
                UiControls.TrySetValue( uiCntHyp_c_Num, UiControls.TryGetValue<decimal>( uiCntHyp_a_Num ) );
            }
        }

        private void UiContentHyperbolic_ParameterC_ValueChanged( object sender, EventArgs e )
        {
            if ( uiCntHyp_ac_ChBx.Checked ) {
                UiControls.TrySetValue( uiCntHyp_a_Num, UiControls.TryGetValue<decimal>( uiCntHyp_c_Num ) );
            }
        }

        private void UiContentHyperbolic_ParameterB_ValueChanged( object sender, EventArgs e )
        {
            if ( uiCntHyp_bd_ChBx.Checked ) {
                UiControls.TrySetValue( uiCntHyp_d_Num, UiControls.TryGetValue<decimal>( uiCntHyp_b_Num ) );
            }
        }

        private void UiContentHyperbolic_ParameterD_ValueChanged( object sender, EventArgs e )
        {
            if ( uiCntHyp_bd_ChBx.Checked ) {
                UiControls.TrySetValue( uiCntHyp_b_Num, UiControls.TryGetValue<decimal>( uiCntHyp_d_Num ) );
            }
        }

        private void PatternCurveDefiner_FormClosing( object sender, FormClosingEventArgs e )
        {
            Dispose();
        }

        private void LocalizeWindow()
        {
            PatternCurveDefinerStrings names = new PatternCurveDefinerStrings();
            Text = names.Form.Text.GetString();

            // Ui
            uiCfrm_Cancel_Btn.Text = names.Ui.CommonCancel.GetString();
            uiCfrm_Ok_Btn.Text = names.Ui.CommonOk.GetString();

            // Tab: Polynomial
            uiTabs_Pol_Btn.Text = names.Ui.PolynomialTitle.GetString();
            uiCntPol_Params_GrBx.Text = names.Ui.PolynomialParameters.GetString();

            // Tab: Hyperbolic
            uiTabs_Hyp_Btn.Text = names.Ui.HyperbolicTitle.GetString();
            uiCntHyp_Params_GrBx.Text = names.Ui.HyperbolicParameters.GetString();
            uiCntHyp_ac_ChBx.Text = names.Ui.HyperbolicBoundAc.GetString();
            uiCntHyp_bd_ChBx.Text = names.Ui.HyperbolicBoundBd.GetString();

            // Tab: Waveform
            uiTabs_Wave_Btn.Text = names.Ui.WaveformTitle.GetString();
            uiCntWave_Params_GrBx.Text = names.Ui.WaveformParameters.GetString();
            uiCntWave_T_GrBx.Text = names.Ui.WaveformWaveType.GetString();
            uiCntWaveT_Sine_RdBtn.Text = names.Ui.WaveformSine.GetString();
            uiCntWaveT_Sq_RdBtn.Text = names.Ui.WaveformSquare.GetString();
            uiCntWaveT_Trg_RdBtn.Text = names.Ui.WaveformTriangle.GetString();
            uiCntWaveT_Saw_RdBtn.Text = names.Ui.WaveformSawtooth.GetString();
        }

    }
}
