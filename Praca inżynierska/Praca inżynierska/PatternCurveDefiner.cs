using System;
using System.Windows.Forms;

namespace PI
{
    public partial class PatternCurveDefiner : Form
    {

        #region Properties
        public int ChosenCurve { set; get; }
        public double ParameterA { set; get; }
        public double ParameterB { set; get; }
        public double ParameterC { set; get; }
        public double ParameterD { set; get; }
        public double ParameterE { set; get; }
        public double ParameterF { set; get; }
        #endregion

        #region PatternCurveDefiner()
        public PatternCurveDefiner()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            DefinePropertiesInitialValues();
            SelectChosenCurveTab();
            UpdateComponentsRelatedWithParameters();
        }
        #endregion

        #region DefinePropertiesInitialValues() : void
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
        #endregion

        #region SelectChosenCurveTab() : void
        private void SelectChosenCurveTab()
        {
            WindowsFormsHelper.SelectTabSafe( wfContentTabControl, ChosenCurve );

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
        #endregion

        #region UpdateComponentsRelatedWithParameters() : void
        private void UpdateComponentsRelatedWithParameters()
        {
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialANumericUpDown, PreSets.ParameterA );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialBNumericUpDown, PreSets.ParameterB );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialCNumericUpDown, PreSets.ParameterC );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialDNumericUpDown, PreSets.ParameterD );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialENumericUpDown, PreSets.ParameterE );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentPolynomialFNumericUpDown, PreSets.ParameterF );
            WindowsFormsHelper.SetValueForNumericUpDown( wfContentHyperbolicCNumericUpDown, PreSets.ParameterC );
        }
        #endregion

        #region wfTabsPanelPolynomialButton_Click(...) : void
        private void wfTabsPanelPolynomialButton_Click( object sender, EventArgs e )
        {
            ChosenCurve = SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL;
            WindowsFormsHelper.SelectTabSafe( wfContentTabControl, SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL );
            wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.GhostWhite;
            wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.White;
        }
        #endregion

        #region wfTabsPanelHyperbolicButton_Click(...) : void
        private void wfTabsPanelHyperbolicButton_Click( object sender, EventArgs e )
        {
            ChosenCurve = SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC;
            WindowsFormsHelper.SelectTabSafe( wfContentTabControl, SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC );
            wfTabsPanelHyperbolicButton.BackColor = System.Drawing.Color.GhostWhite;
            wfTabsPanelPolynomialButton.BackColor = System.Drawing.Color.White;
        }
        #endregion

        #region wfConfirmationCancelButton_Click(...) : void
        private void wfConfirmationCancelButton_Click( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region wfConfirmationOKButton_Click(...) : void
        private void wfConfirmationOKButton_Click( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.OK;
            SaveParametersWhileClosingDialog();
        }
        #endregion

        #region SaveParametersWhileClosingDialog() : void
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
        #endregion

        #region SaveParametersForPolynomialPatternCurve() : void
        private void SaveParametersForPolynomialPatternCurve()
        {
            ParameterA = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialANumericUpDown );
            ParameterB = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialBNumericUpDown );
            ParameterC = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialCNumericUpDown );
            ParameterD = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialDNumericUpDown );
            ParameterE = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialENumericUpDown );
            ParameterF = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentPolynomialFNumericUpDown );
        }
        #endregion

        #region SaveParametersForHyperbolicPatternCurve() : void
        private void SaveParametersForHyperbolicPatternCurve()
        {
            ParameterC = WindowsFormsHelper.GetValueFromNumericUpDown<double>( wfContentHyperbolicCNumericUpDown );
        }
        #endregion

    }
}
