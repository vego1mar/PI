using PI.src.application;
using PI.src.helpers;
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
            LocalizeWindow();
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
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Polynomial );
                uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case Enums.PatternCurveScaffold.Hyperbolic:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Hyperbolic );
                uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case Enums.PatternCurveScaffold.WaveformSine:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Sine_RdBtn.Checked = true;
                break;
            case Enums.PatternCurveScaffold.WaveformSquare:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Sq_RdBtn.Checked = true;
                break;
            case Enums.PatternCurveScaffold.WaveformTriangle:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Trg_RdBtn.Checked = true;
                break;
            case Enums.PatternCurveScaffold.WaveformSawtooth:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
                uiCntWaveT_Saw_RdBtn.Checked = true;
                break;
            case Enums.PatternCurveScaffold.Waveform:
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
                uiTabs_Wave_Btn.BackColor = System.Drawing.Color.GhostWhite;
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
            Settings.Scaffold = Enums.PatternCurveScaffold.Polynomial;
            UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Polynomial );
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Hyperbolic_Click( object sender, EventArgs e )
        {
            Settings.Scaffold = Enums.PatternCurveScaffold.Hyperbolic;
            UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Hyperbolic );
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Waveform_Click( object sender, EventArgs e )
        {
            Settings.Scaffold = Enums.PatternCurveScaffold.WaveformSine;
            UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Waveform );
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
                Messages.Pcd.DivisionByZeroProblem();
                userValue = 0.0001;
            }

            Settings.Parameters.Hyperbolic.F = userValue;
            Settings.Parameters.Hyperbolic.A = UiControls.TryGetValue<double>( uiCntHyp_a_Num );
            Settings.Parameters.Hyperbolic.B = UiControls.TryGetValue<double>( uiCntHyp_b_Num );
            Settings.Parameters.Hyperbolic.C = UiControls.TryGetValue<double>( uiCntHyp_c_Num );
            Settings.Parameters.Hyperbolic.D = UiControls.TryGetValue<double>( uiCntHyp_d_Num );
            Settings.Parameters.Hyperbolic.I = UiControls.TryGetValue<double>( uiCntHyp_i_Num );
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
            LocalizeForm();
            LocalizeUi();
            LocalizeTabs();
        }

        private void LocalizeForm()
        {
            Text = Translator.GetInstance().Strings.PatternCurveDefiner.Form.Text.GetString();
        }

        private void LocalizeUi()
        {
            uiCfrm_Cancel_Btn.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Ui.Cancel.GetString();
            uiCfrm_Ok_Btn.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Ui.Ok.GetString();
        }

        private void LocalizeTabs()
        {
            LocalizeTabPolynomial();
            LocalizeTabHyperbolic();
            LocalizeTabWaveform();
        }

        private void LocalizeTabPolynomial()
        {
            uiTabs_Pol_Btn.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Polynomial.Pol.GetString();
            uiCntPol_Params_GrBx.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Polynomial.Params.GetString();
        }

        private void LocalizeTabHyperbolic()
        {
            uiTabs_Hyp_Btn.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Hyperbolic.Hyp.GetString();
            uiCntHyp_Params_GrBx.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Hyperbolic.Params.GetString();
            uiCntHyp_ac_ChBx.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Hyperbolic.BoundAc.GetString();
            uiCntHyp_bd_ChBx.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Hyperbolic.BoundBd.GetString();
        }

        private void LocalizeTabWaveform()
        {
            uiTabs_Wave_Btn.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Waveform.Wave.GetString();
            uiCntWave_Params_GrBx.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Waveform.Params.GetString();
            uiCntWave_T_GrBx.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Waveform.T.GetString();
            uiCntWaveT_Sine_RdBtn.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Waveform.Sine.GetString();
            uiCntWaveT_Sq_RdBtn.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Waveform.Sq.GetString();
            uiCntWaveT_Trg_RdBtn.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Waveform.Trg.GetString();
            uiCntWaveT_Saw_RdBtn.Text = Translator.GetInstance().Strings.PatternCurveDefiner.Tabs.Waveform.Saw.GetString();
        }

    }
}
