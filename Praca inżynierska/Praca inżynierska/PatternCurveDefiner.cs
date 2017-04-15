using System;
using System.Windows.Forms;

namespace PI
{
    public partial class PatternCurveDefiner : Form
    {

        #region Properties
        public int ChosenCurve { private set; get; }
        public double ParameterA { private set; get; }
        public double ParameterB { private set; get; }
        public double ParameterC { private set; get; }
        public double ParameterD { private set; get; }
        public double ParameterE { private set; get; }
        public double ParameterF { private set; get; }
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
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentHyperbolicCNumericUpDown, PreSets.ParameterC, invoker );
        }

        private void wfTabsPanelPolynomialButton_Click( object sender, EventArgs e )
        {
            ChosenCurve = SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL;
            string invoker = "PI.PatternCurveDefiner.wfTabsPanelPolynomialButton_Click(sender, e)";
            WindowsFormsHelper.SelectTabSafe( wfContentTabControl, SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL, invoker );
            wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.GhostWhite;
            wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.White;
        }

        private void wfTabsPanelHyperbolicButton_Click( object sender, EventArgs e )
        {
            ChosenCurve = SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC;
            string invoker = "PI.PatternCurveDefiner.wfTabsPanelHyperbolicButton_Click(sender, e)";
            WindowsFormsHelper.SelectTabSafe( wfContentTabControl, SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC, invoker );
            wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.GhostWhite;
            wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.White;
        }

        private void wfConfirmationCancelButton_Click( object sender, EventArgs e )
        {
            string invoker = "PI.PatternCurveDefiner.wfConfirmationCancelButton_Click(sender, e)";

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

        private void wfConfirmationOKButton_Click( object sender, EventArgs e )
        {
            string invoker = "PI.PatternCurveDefiner.wfConfirmationOKButton_Click(sender, e)";

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
            ParameterC = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentHyperbolicCNumericUpDown, invoker );
        }

    }
}
