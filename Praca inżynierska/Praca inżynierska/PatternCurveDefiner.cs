using System;
using System.Windows.Forms;

namespace PI
{
    public partial class PatternCurveDefiner : Form
    {

        #region Properties
        public int ChosenCurve { get; private set; }
        public double ParameterA { get; private set; }
        public double ParameterB { get; private set; }
        public double ParameterC { get; private set; }
        public double ParameterD { get; private set; }
        public double ParameterE { get; private set; }
        public double ParameterF { get; private set; }
        public double ParameterG { get; private set; }
        #endregion

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
            ChosenCurve = PreSets.ChosenPatternCurveScaffold;
            ParameterA = PreSets.ParameterA;
            ParameterB = PreSets.ParameterB;
            ParameterC = PreSets.ParameterC;
            ParameterD = PreSets.ParameterD;
            ParameterE = PreSets.ParameterE;
            ParameterF = PreSets.ParameterF;
            ParameterG = PreSets.ParameterG;
        }

        private void SelectChosenCurveTab()
        {
            string invoker = "PI.PatternCurveDefiner.SelectChosenCurveTab()";
            WindowsFormsHelper.SelectTabSafe( wfContentTabControl, ChosenCurve, invoker );

            switch ( ChosenCurve ) {
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL:
                wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.GhostWhite;
                break;
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC:
                wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.GhostWhite;
                break;
            default:
                break;
            }
        }

        private void UpdateComponentsRelatedWithParameters()
        {
            string invoker = "PI.PatternCurveDefiner.UpdateComponentsRelatedWithParameters()";
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialANumericUpDown, PreSets.ParameterA, invoker );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialBNumericUpDown, PreSets.ParameterB, invoker );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialCNumericUpDown, PreSets.ParameterC, invoker );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialDNumericUpDown, PreSets.ParameterD, invoker );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialENumericUpDown, PreSets.ParameterE, invoker );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialFNumericUpDown, PreSets.ParameterF, invoker );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentHyperbolicGNumericUpDown, PreSets.ParameterG, invoker );
        }

        private void WfTabsPanelPolynomialButton_Click( object sender, EventArgs e )
        {
            ChosenCurve = SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL;
            string invoker = "PI.PatternCurveDefiner.WfTabsPanelPolynomialButton_Click(sender, e)";
            WindowsFormsHelper.SelectTabSafe( wfContentTabControl, SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL, invoker );
            wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.GhostWhite;
            wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.White;
        }

        private void WfTabsPanelHyperbolicButton_Click( object sender, EventArgs e )
        {
            ChosenCurve = SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC;
            string invoker = "PI.PatternCurveDefiner.WfTabsPanelHyperbolicButton_Click(sender, e)";
            WindowsFormsHelper.SelectTabSafe( wfContentTabControl, SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC, invoker );
            wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.GhostWhite;
            wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.White;
        }

        private void WfConfirmationCancelButton_Click( object sender, EventArgs e )
        {
            string invoker = "PI.PatternCurveDefiner.WfConfirmationCancelButton_Click(sender, e)";

            try {
                DialogResult = DialogResult.Cancel;
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
        }

        private void WfConfirmationOKButton_Click( object sender, EventArgs e )
        {
            string invoker = "PI.PatternCurveDefiner.WfConfirmationOKButton_Click(sender, e)";

            try {
                DialogResult = DialogResult.OK;
                SaveParametersWhileClosingDialog();
            }
            catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invoker );
            }
        }

        private void SaveParametersWhileClosingDialog()
        {
            switch ( ChosenCurve ) {
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL:
                SaveParametersForPolynomialPatternCurve();
                break;
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC:
                SaveParametersForHyperbolicPatternCurve();
                break;
            }
        }

        private void SaveParametersForPolynomialPatternCurve()
        {
            string invoker = "PI.PatternCurveDefiner.SaveParametersForPolynomialPatternCurve()";
            ParameterA = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialANumericUpDown, invoker );
            ParameterB = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialBNumericUpDown, invoker );
            ParameterC = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialCNumericUpDown, invoker );
            ParameterD = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialDNumericUpDown, invoker );
            ParameterE = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialENumericUpDown, invoker );
            ParameterF = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialFNumericUpDown, invoker );
        }

        private void SaveParametersForHyperbolicPatternCurve()
        {
            string invoker = "PI.PatternCurveDefiner.SaveParametersForHyperbolicPatternCurve()";
            double userValue = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentHyperbolicGNumericUpDown, invoker );

            // Checking 'decimal' value, not 'double'.
            // Four zeros after decimal separator are revelant here, not floating-point precision.
            if ( userValue == 0.0000 ) {
                string text = SharedConstants.PCD_PARAMETERS_HYPERBOLIC_ZERO_DIVISION_TEXT;
                string caption = SharedConstants.PCD_PARAMETERS_HYPERBOLIC_ZERO_DIVISION_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, invoker );
                userValue = 0.0001;
            }

            ParameterG = userValue;
        }

    }
}
