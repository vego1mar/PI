using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    public partial class DatasetViewer : Form
    {

        #region Members
        public Series DatasetOfCurve { private set; get; }
        #endregion

        public DatasetViewer( Series curveDataset )
        {
            InitializeComponent();
            DatasetOfCurve = curveDataset;
            SetRangesToComponentsRelatedWithPointIndex();
            BuildAndPopulateDatasetGrid();
        }

        private void WfEditControlPerformButton_Click( object sender, EventArgs e )
        {
            string invoker = "PI.DatasetViewer.WfEditControlPerformButton_Click(sender, e)";
            int selectedOperationType = WindowsFormsHelper.GetSelectedIndexSafe( wfEditControlOperationTypeComboBox, invoker );
            int selectedPointIndex = WindowsFormsHelper.GetValueFromTrackBar( wfEditControlPointIndexTrackBar, invoker );
            bool isValueValid = WindowsFormsHelper.GetValueFromTextBox( wfEditControlValue2TextBox, out double userValue, invoker );

            if ( !isValueValid ) {
                string text = SharedConstants.DSV_USER_VALUE_NOT_VALID_TEXT;
                string caption = SharedConstants.DSV_USER_VALUE_NOT_VALID_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, invoker );
                return;
            }

            switch ( selectedOperationType ) {
            case SharedConstants.DSV_OPERATION_TYPE_OVERRIDING:
                PerformOperationOverriding( selectedPointIndex, userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_ADDITION:
                PerformOperationAddition( userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_SUBSTRACTION:
                PerformOperationSubstraction( userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_MULTIPLICATION:
                PerformOperationMultiplication( userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_DIVISION:
                PerformOperationDivision( userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_EXPONENTIATION:
                PerformOperationExponentiation( userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_LOGARITHMIC:
                PerformOperationLogarithmic( userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_ROOTING:
                PerformOperationRooting( userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_CONSTANT:
                PerformOperationConstant( userValue );
                break;
            default:
                string text = SharedConstants.DSV_OPERATION_TYPE_NOT_SELECTED_TEXT;
                string caption = SharedConstants.DSV_OPERATION_TYPE_NOT_SELECTED_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, invoker );
                return;
            }

            BuildAndPopulateDatasetGrid();
        }

        private void SetRangesToComponentsRelatedWithPointIndex()
        {
            wfEditControlPointIndexNumericUpDown.Minimum = 1;
            wfEditControlPointIndexNumericUpDown.Maximum = PreSets.NumberOfPoints;
            wfEditControlPointIndexTrackBar.Minimum = 1;
            wfEditControlPointIndexTrackBar.Maximum = PreSets.NumberOfPoints;
        }

        private void WfEditControlPointIndexNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            string invoker = "PI.DatasetViewer.WfEditControlPointIndexNumericUpDown_ValueChanged(sender, e)";
            int currentValue = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfEditControlPointIndexNumericUpDown, invoker );
            WindowsFormsHelper.SetValueForTrackBar( wfEditControlPointIndexTrackBar, currentValue, invoker );
        }

        private void WfEditControlPointIndexTrackBar_Scroll( object sender, EventArgs e )
        {
            string invoker = "PI.DatasetViewer.WfEditControlPointIndexTrackBar_Scroll(sender, e)";
            int currentValue = WindowsFormsHelper.GetValueFromTrackBar( wfEditControlPointIndexTrackBar, invoker );
            WindowsFormsHelper.SetValueForNumericUpDown( wfEditControlPointIndexNumericUpDown, currentValue, invoker );
        }

        private void BuildAndPopulateDatasetGrid()
        {
            ClearDatasetGridAndBuildStartState();
            AddTitleHeaderComponentsToDatasetGrid();
            PopulateDatasetGridByCurvePointsData();
        }

        private void ClearDatasetGridAndBuildStartState()
        {
            wfGridTableLayoutPanel.Controls.Clear();
            int lastRow = wfGridTableLayoutPanel.RowCount - 1;
            RowStyle previousRowStyle = wfGridTableLayoutPanel.RowStyles[lastRow];
            wfGridTableLayoutPanel.RowStyles.Clear();
            wfGridTableLayoutPanel.RowCount = 0;
            string invoker = "PI.DatasetViewer.ClearDatasetGridAndBuildStartState()";
            wfGridTableLayoutPanel.RowStyles.Add( WindowsFormsHelper.GetRowStyleSafe( previousRowStyle.SizeType, previousRowStyle.Height, invoker ) );
            wfGridTableLayoutPanel.RowStyles.Add( WindowsFormsHelper.GetRowStyleSafe( previousRowStyle.SizeType, previousRowStyle.Height, invoker ) );
            wfGridTableLayoutPanel.RowCount = 2;
        }

        private void AddTitleHeaderComponentsToDatasetGrid()
        {
            TextBox indexTextBox = new TextBox() {
                Text = "Index",
                TextAlign = HorizontalAlignment.Center,
                Enabled = true,
                ReadOnly = true,
                Multiline = false,
                Dock = DockStyle.Fill,
            };

            TextBox axisXTextBox = new TextBox() {
                Text = "Axis X",
                TextAlign = HorizontalAlignment.Center,
                Enabled = true,
                ReadOnly = true,
                Multiline = false,
                Dock = DockStyle.Fill
            };

            TextBox axisYTextBox = new TextBox() {
                Text = "Axis Y",
                TextAlign = HorizontalAlignment.Center,
                Enabled = true,
                ReadOnly = true,
                Multiline = false,
                Dock = DockStyle.Fill
            };

            int lastRow = wfGridTableLayoutPanel.RowCount - 1;
            wfGridTableLayoutPanel.Controls.Add( indexTextBox, 0, lastRow );
            wfGridTableLayoutPanel.Controls.Add( axisXTextBox, 1, lastRow );
            wfGridTableLayoutPanel.Controls.Add( axisYTextBox, 2, lastRow );
        }

        private void PopulateDatasetGridByCurvePointsData()
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                AddRowToDatasetGridAtEnd();
                double x = DatasetOfCurve.Points[i].XValue;
                double y = DatasetOfCurve.Points[i].YValues[0];
                AddRowComponentsToDatasetGridAtEnd( i + 1, x, y );
            }

            AddRowToDatasetGridAtEnd();
        }

        private void AddRowToDatasetGridAtEnd()
        {
            int lastRow = wfGridTableLayoutPanel.RowCount - 1;
            RowStyle previousRowStyle = wfGridTableLayoutPanel.RowStyles[lastRow];
            wfGridTableLayoutPanel.RowStyles.Add( new RowStyle( previousRowStyle.SizeType, previousRowStyle.Height ) );
            wfGridTableLayoutPanel.RowCount++;
        }

        private void AddRowComponentsToDatasetGridAtEnd( int i, double x, double y )
        {
            TextBox indexTextBox = new TextBox() {
                Text = i.ToString(),
                TextAlign = HorizontalAlignment.Right,
                Enabled = true,
                ReadOnly = true,
                Multiline = false,
                Dock = DockStyle.Fill,
            };

            string invoker = "PI.DatasetViewer.AddRowComponentsToDatasetGridAtEnd(i, x, y)";

            TextBox axisXTextBox = new TextBox() {
                Text = StringFormatter.FormatAsNumeric( 4, x, invoker ),
                TextAlign = HorizontalAlignment.Right,
                Enabled = true,
                ReadOnly = true,
                Multiline = false,
                Dock = DockStyle.Fill,
            };

            TextBox axisYTextBox = new TextBox() {
                Text = StringFormatter.FormatAsNumeric( 4, y, invoker ),
                TextAlign = HorizontalAlignment.Right,
                Enabled = true,
                ReadOnly = true,
                Multiline = false,
                Dock = DockStyle.Fill,
            };

            int lastRow = wfGridTableLayoutPanel.RowCount - 1;
            wfGridTableLayoutPanel.Controls.Add( indexTextBox, 0, lastRow );
            wfGridTableLayoutPanel.Controls.Add( axisXTextBox, 1, lastRow );
            wfGridTableLayoutPanel.Controls.Add( axisYTextBox, 2, lastRow );
        }

        private void WfEditControlOperationTypeComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            string invoker = "PI.DatasetViewer.WfEditControlOperationTypeComboBox_SelectedIndexChanged(sender, e)";
            int selectedOperationType = WindowsFormsHelper.GetSelectedIndexSafe( wfEditControlOperationTypeComboBox, invoker );
            wfEditControlPointIndexNumericUpDown.Enabled = false;
            wfEditControlPointIndexTrackBar.Enabled = false;

            switch ( selectedOperationType ) {
            case SharedConstants.DSV_OPERATION_TYPE_CONSTANT:
                wfEditControlValue1TextBox.Text = SharedConstants.DSV_EDIT_CONTROL_VALUE_TEXT;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_OVERRIDING:
                wfEditControlPointIndexNumericUpDown.Enabled = true;
                wfEditControlPointIndexTrackBar.Enabled = true;
                wfEditControlValue1TextBox.Text = SharedConstants.DSV_EDIT_CONTROL_VALUE_TEXT;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_ADDITION:
                wfEditControlValue1TextBox.Text = SharedConstants.DSV_EDIT_CONTROL_ADDEND_TEXT;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_SUBSTRACTION:
                wfEditControlValue1TextBox.Text = SharedConstants.DSV_EDIT_CONTROL_SUBTRAHEND_TEXT;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_MULTIPLICATION:
                wfEditControlValue1TextBox.Text = SharedConstants.DSV_EDIT_CONTROL_MULTIPLIER_TEXT;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_DIVISION:
                wfEditControlValue1TextBox.Text = SharedConstants.DSV_EDIT_CONTROL_DIVISOR_TEXT;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_EXPONENTIATION:
                wfEditControlValue1TextBox.Text = SharedConstants.DSV_EDIT_CONTROL_EXPONENT_TEXT;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_LOGARITHMIC:
                wfEditControlValue1TextBox.Text = SharedConstants.DSV_EDIT_CONTROL_BASE_TEXT;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_ROOTING:
                wfEditControlValue1TextBox.Text = SharedConstants.DSV_EDIT_CONTROL_LEVEL_TEXT;
                break;
            }
        }

        private void PerformOperationOverriding( int indexOfPoint, double userValue )
        {
            DatasetOfCurve.Points[indexOfPoint - 1].YValues[0] = userValue;
        }

        private void PerformOperationConstant( double userValue )
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                DatasetOfCurve.Points[i].YValues[0] = userValue;
            }
        }

        private void PerformOperationAddition( double userValue )
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                DatasetOfCurve.Points[i].YValues[0] += userValue;
            }
        }

        private void PerformOperationSubstraction( double userValue )
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                DatasetOfCurve.Points[i].YValues[0] -= userValue;
            }
        }

        private void PerformOperationMultiplication( double userValue )
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                DatasetOfCurve.Points[i].YValues[0] *= userValue;
            }
        }

        private void PerformOperationDivision( double userValue )
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                DatasetOfCurve.Points[i].YValues[0] /= userValue;
            }
        }

        private void PerformOperationExponentiation( double userValue )
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                DatasetOfCurve.Points[i].YValues[0] = Math.Pow( DatasetOfCurve.Points[i].YValues[0], userValue );
            }
        }

        private void PerformOperationLogarithmic( double userValue )
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                DatasetOfCurve.Points[i].YValues[0] = Math.Log( DatasetOfCurve.Points[i].YValues[0], userValue );
            }
        }

        private void PerformOperationRooting( double userValue )
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                DatasetOfCurve.Points[i].YValues[0] = Math.Pow( DatasetOfCurve.Points[i].YValues[0], 1.0 / userValue );
            }
        }

    }
}
