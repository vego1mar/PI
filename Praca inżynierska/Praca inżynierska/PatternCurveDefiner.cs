using System;
using System.Windows.Forms;

namespace PI
{
    public partial class PatternCurveDefiner : Form
    {

        internal Enums.PatternCurveScaffold ChosenCurve { get; private set; }
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
            ChosenCurve = Presets.Pcd.ChosenScaffold;
            Parameters = Presets.Pcd.Parameters;
        }

        private void SelectChosenCurveTab()
        {
            switch ( ChosenCurve ) {
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
            WinFormsHelper.SetValue( uiCntPol_a_Num, Presets.Pcd.Parameters.Polynomial.A );
            WinFormsHelper.SetValue( uiCntPol_b_Num, Presets.Pcd.Parameters.Polynomial.B );
            WinFormsHelper.SetValue( uiCntPol_c_Num, Presets.Pcd.Parameters.Polynomial.C );
            WinFormsHelper.SetValue( uiCntPol_d_Num, Presets.Pcd.Parameters.Polynomial.D );
            WinFormsHelper.SetValue( uiCntPol_e_Num, Presets.Pcd.Parameters.Polynomial.E );
            WinFormsHelper.SetValue( uiCntPol_f_Num, Presets.Pcd.Parameters.Polynomial.F );
            WinFormsHelper.SetValue( uiCntPol_i_Num, Presets.Pcd.Parameters.Polynomial.I );
            WinFormsHelper.SetValue( uiCntHyp_g_Num, Presets.Pcd.Parameters.Hyperbolic.G );
            WinFormsHelper.SetValue( uiCntHyp_j_Num, Presets.Pcd.Parameters.Hyperbolic.J );
            WinFormsHelper.SetValue( uiCntWave_m_Num, Presets.Pcd.Parameters.Waveform.M );
            WinFormsHelper.SetValue( uiCntWave_n_Num, Presets.Pcd.Parameters.Waveform.N );
            WinFormsHelper.SetValue( uiCntWave_o_Num, Presets.Pcd.Parameters.Waveform.O );
            WinFormsHelper.SetValue( uiCntWave_k_Num, Presets.Pcd.Parameters.Waveform.K );
        }

        private void UiTabs_Polynomial_Click( object sender, EventArgs e )
        {
            ChosenCurve = Enums.PatternCurveScaffold.Polynomial;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Polynomial );
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Hyperbolic_Click( object sender, EventArgs e )
        {
            ChosenCurve = Enums.PatternCurveScaffold.Hyperbolic;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, (int) Enums.PatternCurveScaffold.Hyperbolic );
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
            uiTabs_Wave_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Waveform_Click( object sender, EventArgs e )
        {
            ChosenCurve = Enums.PatternCurveScaffold.WaveformSine;
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
            switch ( ChosenCurve ) {
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
                ChosenCurve = Enums.PatternCurveScaffold.WaveformSine;
                uiCntWave_o_Num.Enabled = true;
                uiCntWave_PicBx.Image = Properties.Resources.Pattern_scaffold_waveform_sine;
            }
        }

        private void UiContentWaveformType_Square_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Sq_RdBtn.Checked ) {
                ChosenCurve = Enums.PatternCurveScaffold.WaveformSquare;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.Pattern_scaffold_waveform_square;
            }
        }

        private void UiContentWaveformType_Triangle_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Trg_RdBtn.Checked ) {
                ChosenCurve = Enums.PatternCurveScaffold.WaveformTriangle;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.Pattern_scaffold_waveform_triangle;
            }
        }

        private void UiContentWaveformType_Sawtooth_CheckedChanged( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Saw_RdBtn.Checked ) {
                ChosenCurve = Enums.PatternCurveScaffold.WaveformSawtooth;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.Pattern_scaffold_waveform_sawtooth;
            }
        }

    }
}
