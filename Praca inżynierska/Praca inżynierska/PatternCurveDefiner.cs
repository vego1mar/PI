using System;
using System.Windows.Forms;

namespace PI
{
    public partial class PatternCurveDefiner : Form
    {

        public int ChosenCurve { get; private set; }
        public double ParameterA { get; private set; }
        public double ParameterB { get; private set; }
        public double ParameterC { get; private set; }
        public double ParameterD { get; private set; }
        public double ParameterE { get; private set; }
        public double ParameterF { get; private set; }
        public double ParameterI { get; private set; } 
        public double ParameterG { get; private set; }
        public double ParameterJ { get; private set; }

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
            ParameterA = PreSets.Pcd.Parameters.A;
            ParameterB = PreSets.Pcd.Parameters.B;
            ParameterC = PreSets.Pcd.Parameters.C;
            ParameterD = PreSets.Pcd.Parameters.D;
            ParameterE = PreSets.Pcd.Parameters.E;
            ParameterF = PreSets.Pcd.Parameters.F;
            ParameterI = PreSets.Pcd.Parameters.I;
            ParameterG = PreSets.Pcd.Parameters.G;
            ParameterJ = PreSets.Pcd.Parameters.J;
        }

        private void SelectChosenCurveTab()
        {
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, ChosenCurve );

            switch ( ChosenCurve ) {
            case Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL:
                uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
                break;
            default:
                break;
            }
        }

        private void UpdateUiByParametersValues()
        {
            WinFormsHelper.SetValue( uiCntPol_a_Num, PreSets.Pcd.Parameters.A );
            WinFormsHelper.SetValue( uiCntPol_b_Num, PreSets.Pcd.Parameters.B );
            WinFormsHelper.SetValue( uiCntPol_c_Num, PreSets.Pcd.Parameters.C );
            WinFormsHelper.SetValue( uiCntPol_d_Num, PreSets.Pcd.Parameters.D );
            WinFormsHelper.SetValue( uiCntPol_e_Num, PreSets.Pcd.Parameters.E );
            WinFormsHelper.SetValue( uiCntPol_f_Num, PreSets.Pcd.Parameters.F );
            WinFormsHelper.SetValue( uiCntPol_i_Num, PreSets.Pcd.Parameters.I );
            WinFormsHelper.SetValue( uiCntHyp_g_Num, PreSets.Pcd.Parameters.G );
            WinFormsHelper.SetValue( uiCntHyp_j_Num, PreSets.Pcd.Parameters.J );
        }

        private void UiTabs_Polynomial_Click( object sender, EventArgs e )
        {
            ChosenCurve = Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL );
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.White;
        }

        private void UiTabs_Hyperbolic_Click( object sender, EventArgs e )
        {
            ChosenCurve = Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC;
            WinFormsHelper.SelectTabSafe( uiCnt_TbCtrl, Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC );
            uiTabs_Hyp_Btn.BackColor = System.Drawing.Color.GhostWhite;
            uiTabs_Pol_Btn.BackColor = System.Drawing.Color.White;
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
            case Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL:
                SaveParametersForPolynomialPatternCurve();
                break;
            case Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                SaveParametersForHyperbolicPatternCurve();
                break;
            }
        }

        private void SaveParametersForPolynomialPatternCurve()
        {
            ParameterA = WinFormsHelper.GetValue<double>( uiCntPol_a_Num );
            ParameterB = WinFormsHelper.GetValue<double>( uiCntPol_b_Num );
            ParameterC = WinFormsHelper.GetValue<double>( uiCntPol_c_Num );
            ParameterD = WinFormsHelper.GetValue<double>( uiCntPol_d_Num );
            ParameterE = WinFormsHelper.GetValue<double>( uiCntPol_e_Num );
            ParameterF = WinFormsHelper.GetValue<double>( uiCntPol_f_Num );
            ParameterI = WinFormsHelper.GetValue<double>( uiCntPol_i_Num );
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

            ParameterG = userValue;
            ParameterJ = WinFormsHelper.GetValue<double>( uiCntHyp_j_Num );
        }

    }
}
