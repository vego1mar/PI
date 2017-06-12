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
        public double ParameterG { get; private set; }

        public PatternCurveDefiner()
        {
            InitializeComponent();
            MaximizeBox = false;
            DefinePropertiesInitialValues();
            SelectChosenCurveTab();
            UpdateComponentsRelatedWithParameters();
        }

        private void DefinePropertiesInitialValues()
        {
            ChosenCurve = PreSets.Pcd.ChosenScaffold;
            ParameterA = PreSets.Pcd.ParameterA;
            ParameterB = PreSets.Pcd.ParameterB;
            ParameterC = PreSets.Pcd.ParameterC;
            ParameterD = PreSets.Pcd.ParameterD;
            ParameterE = PreSets.Pcd.ParameterE;
            ParameterF = PreSets.Pcd.ParameterF;
            ParameterG = PreSets.Pcd.ParameterG;
        }

        private void SelectChosenCurveTab()
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            WinFormsHelper.SelectTabSafe( wfContentTabControl, ChosenCurve );

            switch ( ChosenCurve ) {
            case Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL:
                wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC:
                wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.GhostWhite;
                break;
            default:
                break;
            }
        }

        private void UpdateComponentsRelatedWithParameters()
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            WinFormsHelper.SetValue( wfContentPolynomialANumericUpDown, PreSets.Pcd.ParameterA );
            WinFormsHelper.SetValue( wfContentPolynomialBNumericUpDown, PreSets.Pcd.ParameterB );
            WinFormsHelper.SetValue( wfContentPolynomialCNumericUpDown, PreSets.Pcd.ParameterC );
            WinFormsHelper.SetValue( wfContentPolynomialDNumericUpDown, PreSets.Pcd.ParameterD );
            WinFormsHelper.SetValue( wfContentPolynomialENumericUpDown, PreSets.Pcd.ParameterE );
            WinFormsHelper.SetValue( wfContentPolynomialFNumericUpDown, PreSets.Pcd.ParameterF );
            WinFormsHelper.SetValue( wfContentHyperbolicGNumericUpDown, PreSets.Pcd.ParameterG );
        }

        private void WfTabsPanelPolynomialButton_Click( object sender, EventArgs e )
        {
            ChosenCurve = Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL;
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            WinFormsHelper.SelectTabSafe( wfContentTabControl, Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL );
            wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.GhostWhite;
            wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.White;
        }

        private void WfTabsPanelHyperbolicButton_Click( object sender, EventArgs e )
        {
            ChosenCurve = Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC;
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            WinFormsHelper.SelectTabSafe( wfContentTabControl, Constants.Ui.Panel.Generate.SCAFFOLD_HYPERBOLIC );
            wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.GhostWhite;
            wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.White;
        }

        private void WfConfirmationCancelButton_Click( object sender, EventArgs e )
        {
            Logger.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            try {
                DialogResult = DialogResult.Cancel;
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteException( x );
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
            }
            finally {
                Logger.Context = string.Empty;
            }
        }

        private void WfConfirmationOKButton_Click( object sender, EventArgs e )
        {
            Logger.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

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
            finally {
                Logger.Context = string.Empty;
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
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ParameterA = WinFormsHelper.GetValue<double>( wfContentPolynomialANumericUpDown );
            ParameterB = WinFormsHelper.GetValue<double>( wfContentPolynomialBNumericUpDown );
            ParameterC = WinFormsHelper.GetValue<double>( wfContentPolynomialCNumericUpDown );
            ParameterD = WinFormsHelper.GetValue<double>( wfContentPolynomialDNumericUpDown );
            ParameterE = WinFormsHelper.GetValue<double>( wfContentPolynomialENumericUpDown );
            ParameterF = WinFormsHelper.GetValue<double>( wfContentPolynomialFNumericUpDown );
        }

        private void SaveParametersForHyperbolicPatternCurve()
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            double userValue = WinFormsHelper.GetValue<double>( wfContentHyperbolicGNumericUpDown );

            // Checking 'decimal' value, not 'double'.
            // Four zeros after decimal separator are revelant here, not floating-point precision.
            if ( userValue == 0.0000 ) {
                string text = Constants.Pcd.Hyperbolic.PARAMS_ZERO_DIVISION_TEXT;
                string caption = Constants.Pcd.Hyperbolic.PARAMS_ZERO_DIVISION_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                userValue = 0.0001;
            }

            ParameterG = userValue;
        }

    }
}
