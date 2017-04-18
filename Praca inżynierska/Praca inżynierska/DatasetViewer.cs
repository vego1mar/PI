using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    public partial class DatasetViewer : Form
    {

        #region Properties
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
            bool isOperationValid = false;

            if ( !isValueValid ) {
                string text = SharedConstants.DSV_USER_VALUE_NOT_VALID_TEXT;
                string caption = SharedConstants.DSV_USER_VALUE_NOT_VALID_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, invoker );
                return;
            }

            switch ( selectedOperationType ) {
            case SharedConstants.DSV_OPERATION_TYPE_OVERRIDING:
                isOperationValid = PerformOperationOverriding( selectedPointIndex, userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_ADDITION:
            case SharedConstants.DSV_OPERATION_TYPE_SUBSTRACTION:
            case SharedConstants.DSV_OPERATION_TYPE_MULTIPLICATION:
            case SharedConstants.DSV_OPERATION_TYPE_DIVISION:
            case SharedConstants.DSV_OPERATION_TYPE_EXPONENTIATION:
            case SharedConstants.DSV_OPERATION_TYPE_LOGARITHMIC:
            case SharedConstants.DSV_OPERATION_TYPE_ROOTING:
            case SharedConstants.DSV_OPERATION_TYPE_CONSTANT:
                isOperationValid = PerformOperationOnSeriesPoints( userValue, selectedOperationType );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_POSITIVE:
                PerformOperationPositive();
                isOperationValid = true;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_NEGATIVE:
                PerformOperationNegative();
                isOperationValid = true;
                break;
            default:
                string text = SharedConstants.DSV_OPERATION_TYPE_NOT_SELECTED_TEXT;
                string caption = SharedConstants.DSV_OPERATION_TYPE_NOT_SELECTED_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, invoker );
                return;
            }

            if ( isOperationValid ) {
                BuildAndPopulateDatasetGrid();
            }
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
            wfEditControlValue2TextBox.Enabled = true;

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
            case SharedConstants.DSV_OPERATION_TYPE_POSITIVE:
            case SharedConstants.DSV_OPERATION_TYPE_NEGATIVE:
                wfEditControlValue2TextBox.Text = "0";
                wfEditControlValue2TextBox.Enabled = false;
                break;
            }
        }

        private bool PerformOperationOverriding( int indexOfPoint, double userValue )
        {
            if ( IsValidDecimalChartNumber( userValue ) ) {
                DatasetOfCurve.Points[indexOfPoint - 1].YValues[0] = userValue;
                return true;
            }

            string invoker = "PI.DatasetViewer.PerformOperationOverriding(indexOfPoint, userValue)";
            string text = SharedConstants.DSV_NOT_VALID_DECIMAL_CHART_NUMBER_TEXT;
            string caption = SharedConstants.DSV_NOT_VALID_DECIMAL_CHART_NUMBER_CAPTION;
            SetSpecifiedComponentsToInformAboutOverflow( userValue );
            WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop, invoker );
            return false;
        }

        private void PerformOperationPositive()
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                if ( DatasetOfCurve.Points[i].YValues[0] < 0 ) {
                    DatasetOfCurve.Points[i].YValues[0] = Math.Abs( DatasetOfCurve.Points[i].YValues[0] );
                }
            }
        }

        private void PerformOperationNegative()
        {
            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                if ( DatasetOfCurve.Points[i].YValues[0] > 0 ) {
                    DatasetOfCurve.Points[i].YValues[0] = -(DatasetOfCurve.Points[i].YValues[0]);
                }
            }
        }

        private bool PerformOperationOnSeriesPoints( double userValue, int operationType )
        {
            Series series = GetCopyOfCurrentCurveSeriesPoints();

            for ( int i = 0; i < series.Points.Count; i++ ) {
                double value = SelectAndPerformOperation( operationType, userValue, series, i );

                if ( !IsValidDecimalChartNumber( value ) ) {
                    string invoker = "PI.DatasetViewer.PerformOperationOnSeriesPoints(userValue, operationType)";
                    string text = SharedConstants.DSV_NOT_VALID_DECIMAL_CHART_NUMBER_TEXT;
                    string caption = SharedConstants.DSV_NOT_VALID_DECIMAL_CHART_NUMBER_CAPTION;
                    SetSpecifiedComponentsToInformAboutOverflow( value );
                    WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop, invoker );
                    return false;
                }
            }

            CopySeriesPointsBack( series, DatasetOfCurve );
            return true;
        }

        private double SelectAndPerformOperation( int operationType, double userValue, Series series, int pointIndex )
        {
            double result = userValue;

            switch ( operationType ) {
            case SharedConstants.DSV_OPERATION_TYPE_ADDITION:
                result = series.Points[pointIndex].YValues[0] + userValue;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_SUBSTRACTION:
                result = series.Points[pointIndex].YValues[0] - userValue;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_MULTIPLICATION:
                result = series.Points[pointIndex].YValues[0] * userValue;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_DIVISION:
                result = series.Points[pointIndex].YValues[0] / userValue;
                break;
            case SharedConstants.DSV_OPERATION_TYPE_EXPONENTIATION:
                result = Math.Pow( series.Points[pointIndex].YValues[0], userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_LOGARITHMIC:
                result = Math.Log( series.Points[pointIndex].YValues[0], userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_ROOTING:
                result = Math.Pow( series.Points[pointIndex].YValues[0], 1.0 / userValue );
                break;
            case SharedConstants.DSV_OPERATION_TYPE_CONSTANT:
                result = userValue;
                break;
            }

            series.Points[pointIndex].YValues[0] = result;
            return result;
        }

        private bool IsValidDecimalChartNumber( double number )
        {
            string invoker = "PI.DatasetViewer.IsValidDecimalChartNumber(number)";
            decimal convertite;

            try {
                convertite = Convert.ToDecimal( number );
            }
            catch ( OverflowException x ) {
                Logger.WriteExceptionInfo( x, invoker );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, invoker );
                return false;
            }

            return true;
        }

        private Series GetCopyOfCurrentCurveSeriesPoints()
        {
            Series series = new Series();
            CurvesDataset.SetDefaultPropertiesForChartingSeries( series, "CopyOfSeries" );

            for ( int i = 0; i < DatasetOfCurve.Points.Count; i++ ) {
                series.Points.AddXY( DatasetOfCurve.Points[i].XValue, DatasetOfCurve.Points[i].YValues[0] );
            }

            return series;
        }

        private void CopySeriesPointsBack( Series source, Series target )
        {
            for ( int i = 0; i < source.Points.Count; i++ ) {
                target.Points[i].XValue = source.Points[i].XValue;
                target.Points[i].YValues[0] = source.Points[i].YValues[0];
            }
        }

        private void SetSpecifiedComponentsToInformAboutOverflow( double power )
        {
            string invoker = "PI.DatasetViewer.SetSpecifiedComponentsToInformAboutOverflow(power)";
            wfEditControlValue1TextBox.Text = SharedConstants.DSV_OPERATION_ERROR_OVERFLOW_TEXT;
            wfEditControlValue2TextBox.Text = StringFormatter.FormatAsNumeric( 4, power, invoker );
        }

    }
}
