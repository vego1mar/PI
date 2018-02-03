using PI.src.helpers;
using PI.src.settings;
using System;
using System.Windows.Forms;
using PI.src.enumerators;
using PI.src.localization.windows;
using System.Drawing;
using PI.src.general;
using log4net;
using System.Reflection;

namespace PI.src.windows
{
    public partial class PatternCurveDefiner : Form
    {
        internal IdealCurveDefinerGeneratorSettings Settings { get; private set; }
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        private enum Tabs
        {
            Polynomial = 0,
            Hyperbolic = 1,
            Waveform = 2
        }

        internal PatternCurveDefiner( IdealCurveDefinerGeneratorSettings presets )
        {
            InitializeComponent();
            LocalizeWindow();
            Settings = presets;
            UpdateUiByTabSelection();
            UpdateUiBySettings();
        }

        private void UpdateUiByTabSelection()
        {
            switch ( Settings.Scaffold ) {
            case IdealCurveScaffold.Polynomial:
                uiTabs_Pol_Btn.BackColor = Color.GhostWhite;
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Tabs.Polynomial );
                break;
            case IdealCurveScaffold.Hyperbolic:
                uiTabs_Hyp_Btn.BackColor = Color.GhostWhite;
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Tabs.Hyperbolic );
                break;
            case IdealCurveScaffold.WaveformSine:
            case IdealCurveScaffold.WaveformSquare:
            case IdealCurveScaffold.WaveformTriangle:
            case IdealCurveScaffold.WaveformSawtooth:
                uiTabs_Wave_Btn.BackColor = Color.GhostWhite;
                UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Tabs.Waveform );
                break;
            }

            switch ( Settings.Scaffold ) {
            case IdealCurveScaffold.WaveformSine:
                uiCntWaveT_Sine_RdBtn.Checked = true;
                break;
            case IdealCurveScaffold.WaveformSquare:
                uiCntWaveT_Sq_RdBtn.Checked = true;
                break;
            case IdealCurveScaffold.WaveformTriangle:
                uiCntWaveT_Trg_RdBtn.Checked = true;
                break;
            case IdealCurveScaffold.WaveformSawtooth:
                uiCntWaveT_Saw_RdBtn.Checked = true;
                break;
            }
        }

        private void UpdateUiBySettings()
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

        private void SaveParameters()
        {
            switch ( Settings.Scaffold ) {
            case IdealCurveScaffold.Polynomial:
                Settings.Parameters.Polynomial.A = UiControls.TryGetValue<double>( uiCntPol_a_Num );
                Settings.Parameters.Polynomial.B = UiControls.TryGetValue<double>( uiCntPol_b_Num );
                Settings.Parameters.Polynomial.C = UiControls.TryGetValue<double>( uiCntPol_c_Num );
                Settings.Parameters.Polynomial.D = UiControls.TryGetValue<double>( uiCntPol_d_Num );
                Settings.Parameters.Polynomial.E = UiControls.TryGetValue<double>( uiCntPol_e_Num );
                Settings.Parameters.Polynomial.F = UiControls.TryGetValue<double>( uiCntPol_f_Num );
                Settings.Parameters.Polynomial.I = UiControls.TryGetValue<double>( uiCntPol_i_Num );
                break;
            case IdealCurveScaffold.Hyperbolic:
                double userValue = UiControls.TryGetValue<double>( uiCntHyp_f_Num );
                Settings.Parameters.Hyperbolic.F = Mathematics.IsZero( userValue ) ? 0.0001 : userValue;
                Settings.Parameters.Hyperbolic.A = UiControls.TryGetValue<double>( uiCntHyp_a_Num );
                Settings.Parameters.Hyperbolic.B = UiControls.TryGetValue<double>( uiCntHyp_b_Num );
                Settings.Parameters.Hyperbolic.C = UiControls.TryGetValue<double>( uiCntHyp_c_Num );
                Settings.Parameters.Hyperbolic.D = UiControls.TryGetValue<double>( uiCntHyp_d_Num );
                Settings.Parameters.Hyperbolic.I = UiControls.TryGetValue<double>( uiCntHyp_i_Num );
                break;
            case IdealCurveScaffold.WaveformSine:
            case IdealCurveScaffold.WaveformSquare:
            case IdealCurveScaffold.WaveformTriangle:
            case IdealCurveScaffold.WaveformSawtooth:
                Settings.Parameters.Waveform.M = UiControls.TryGetValue<double>( uiCntWave_m_Num );
                Settings.Parameters.Waveform.N = UiControls.TryGetValue<double>( uiCntWave_n_Num );
                Settings.Parameters.Waveform.O = UiControls.TryGetValue<double>( uiCntWave_o_Num );
                Settings.Parameters.Waveform.K = UiControls.TryGetValue<double>( uiCntWave_k_Num );
                break;
            }
        }

        private void LocalizeWindow()
        {
            PatternCurveDefinerStrings names = new PatternCurveDefinerStrings();
            Text = names.Form.Text.GetString();

            // Common
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

        #region Event handlers

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            // Do not nullify Settings property
            Dispose();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ',' + e.CloseReason + ')' );
        }

        private void OnSineOptionChecked( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Sine_RdBtn.Checked ) {
                Settings.Scaffold = IdealCurveScaffold.WaveformSine;
                uiCntWave_o_Num.Enabled = true;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformSine;
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + Settings.Scaffold + ')' );
        }

        private void OnSquareOptionChecked( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Sq_RdBtn.Checked ) {
                Settings.Scaffold = IdealCurveScaffold.WaveformSquare;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformSquare;
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + Settings.Scaffold + ')' );
        }

        private void OnTriangleOptionChecked( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Trg_RdBtn.Checked ) {
                Settings.Scaffold = IdealCurveScaffold.WaveformTriangle;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformTriangle;
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + Settings.Scaffold + ')' );
        }

        private void OnSawtoothOptionChecked( object sender, EventArgs e )
        {
            if ( uiCntWaveT_Saw_RdBtn.Checked ) {
                Settings.Scaffold = IdealCurveScaffold.WaveformSawtooth;
                uiCntWave_o_Num.Enabled = false;
                uiCntWave_PicBx.Image = Properties.Resources.PatternScaffold_WaveformSawtooth;
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + Settings.Scaffold + ')' );
        }

        private void OnParameterAAlteration( object sender, EventArgs e )
        {
            if ( uiCntHyp_ac_ChBx.Checked ) {
                UiControls.TrySetValue( uiCntHyp_c_Num, UiControls.TryGetValue<decimal>( uiCntHyp_a_Num ) );
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + uiCntHyp_a_Num.Value + ')' );
        }

        private void OnParameterCAlteration( object sender, EventArgs e )
        {
            if ( uiCntHyp_ac_ChBx.Checked ) {
                UiControls.TrySetValue( uiCntHyp_a_Num, UiControls.TryGetValue<decimal>( uiCntHyp_c_Num ) );
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + uiCntHyp_c_Num.Value + ')' );
        }

        private void OnParameterBAlteration( object sender, EventArgs e )
        {
            if ( uiCntHyp_bd_ChBx.Checked ) {
                UiControls.TrySetValue( uiCntHyp_d_Num, UiControls.TryGetValue<decimal>( uiCntHyp_b_Num ) );
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + uiCntHyp_b_Num.Value + ')' );
        }

        private void OnParameterDAlteration( object sender, EventArgs e )
        {
            if ( uiCntHyp_bd_ChBx.Checked ) {
                UiControls.TrySetValue( uiCntHyp_b_Num, UiControls.TryGetValue<decimal>( uiCntHyp_d_Num ) );
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + uiCntHyp_d_Num.Value + ')' );
        }

        private void OnPolynomialTabSelection( object sender, EventArgs e )
        {
            Settings.Scaffold = IdealCurveScaffold.Polynomial;
            UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Tabs.Polynomial );
            uiTabs_Pol_Btn.BackColor = Color.GhostWhite;
            uiTabs_Hyp_Btn.BackColor = Color.White;
            uiTabs_Wave_Btn.BackColor = Color.White;
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + Tabs.Polynomial + ')' );
        }

        private void OnHyperbolicTabSelection( object sender, EventArgs e )
        {
            Settings.Scaffold = IdealCurveScaffold.Hyperbolic;
            UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Tabs.Hyperbolic );
            uiTabs_Hyp_Btn.BackColor = Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = Color.White;
            uiTabs_Wave_Btn.BackColor = Color.White;
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + Tabs.Hyperbolic + ')' );
        }

        private void OnWaveformTabSelection( object sender, EventArgs e )
        {
            Settings.Scaffold = IdealCurveScaffold.WaveformSine;
            UiControls.TrySelectTab( uiCnt_TbCtrl, (int) Tabs.Waveform );
            uiTabs_Wave_Btn.BackColor = Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = Color.White;
            uiTabs_Hyp_Btn.BackColor = Color.White;
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + Tabs.Waveform + ')' );
        }

        private void OnCancelClick( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        private void OnOkClick( object sender, EventArgs e )
        {
            SaveParameters();
            DialogResult = DialogResult.OK;
            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        #endregion
    }
}
