using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    public partial class DatasetViewer : Form
    {

        public Series CurveDataSet { private set; get; }

        public DatasetViewer( Series curveDataset )
        {
            InitializeComponent();
            CurveDataSet = curveDataset;
            SetRangesToComponentsRelatedWithPointIndex();
            BuildAndPopulateDatasetGrid();
        }

        private void WfEditControlPerformButton_Click( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int selectedOperationType = WinFormsHelper.GetSelectedIndexSafe( wfEditControlOperationTypeComboBox );
            int selectedPointIndex = WinFormsHelper.GetValue( wfEditControlPointIndexTrackBar );
            bool isValueValid = WinFormsHelper.GetValue( wfEditControlValue2TextBox, out double userValue );
            bool isOperationValid = false;

            if ( !isValueValid ) {
                string text = Constants.Dsv.EditControl.USER_VALUE_NOT_VALID_TEXT;
                string caption = Constants.Dsv.EditControl.USER_VALUE_NOT_VALID_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                return;
            }

            switch ( selectedOperationType ) {
            case Constants.Dsv.EditControl.OPERATION_TYPE_OVERRIDING:
                isOperationValid = PerformOperationOverriding( selectedPointIndex, userValue );
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_ADDITION:
            case Constants.Dsv.EditControl.OPERATION_TYPE_SUBSTRACTION:
            case Constants.Dsv.EditControl.OPERATION_TYPE_MULTIPLICATION:
            case Constants.Dsv.EditControl.OPERATION_TYPE_DIVISION:
            case Constants.Dsv.EditControl.OPERATION_TYPE_EXPONENTIATION:
            case Constants.Dsv.EditControl.OPERATION_TYPE_LOGARITHMIC:
            case Constants.Dsv.EditControl.OPERATION_TYPE_ROOTING:
            case Constants.Dsv.EditControl.OPERATION_TYPE_CONSTANT:
                isOperationValid = PerformOperationOnSeriesPoints( userValue, selectedOperationType );
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_POSITIVE:
                PerformOperationPositive();
                isOperationValid = true;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_NEGATIVE:
                PerformOperationNegative();
                isOperationValid = true;
                break;
            default:
                string text = Constants.Dsv.EditControl.OPERATION_TYPE_NOT_SELECTED_TEXT;
                string caption = Constants.Dsv.EditControl.OPERATION_TYPE_NOT_SELECTED_CAPTION;
                WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
                return;
            }

            if ( isOperationValid ) {
                BuildAndPopulateDatasetGrid();
            }
        }

        private void SetRangesToComponentsRelatedWithPointIndex()
        {
            wfEditControlPointIndexNumericUpDown.Minimum = 1;
            wfEditControlPointIndexNumericUpDown.Maximum = PreSets.Ui.NumberOfPoints;
            wfEditControlPointIndexTrackBar.Minimum = 1;
            wfEditControlPointIndexTrackBar.Maximum = PreSets.Ui.NumberOfPoints;
        }

        private void WfEditControlPointIndexNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int currentValue = WinFormsHelper.GetValue<int>( wfEditControlPointIndexNumericUpDown );
            WinFormsHelper.SetValue( wfEditControlPointIndexTrackBar, currentValue );
        }

        private void WfEditControlPointIndexTrackBar_Scroll( object sender, EventArgs e )
        {
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int currentValue = WinFormsHelper.GetValue( wfEditControlPointIndexTrackBar );
            WinFormsHelper.SetValue( wfEditControlPointIndexNumericUpDown, currentValue );
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
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            wfGridTableLayoutPanel.RowStyles.Add( WinFormsHelper.GetRowStyleSafe( previousRowStyle.SizeType, previousRowStyle.Height ) );
            wfGridTableLayoutPanel.RowStyles.Add( WinFormsHelper.GetRowStyleSafe( previousRowStyle.SizeType, previousRowStyle.Height ) );
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
            for ( int i = 0; i < CurveDataSet.Points.Count; i++ ) {
                AddRowToDatasetGridAtEnd();
                double x = CurveDataSet.Points[i].XValue;
                double y = CurveDataSet.Points[i].YValues[0];
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

            StringFormatter.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;

            TextBox axisXTextBox = new TextBox() {
                Text = StringFormatter.FormatAsNumeric( 4, x ),
                TextAlign = HorizontalAlignment.Right,
                Enabled = true,
                ReadOnly = true,
                Multiline = false,
                Dock = DockStyle.Fill,
            };

            TextBox axisYTextBox = new TextBox() {
                Text = StringFormatter.FormatAsNumeric( 4, y ),
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
            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int selectedOperationType = WinFormsHelper.GetSelectedIndexSafe( wfEditControlOperationTypeComboBox );
            wfEditControlPointIndexNumericUpDown.Enabled = false;
            wfEditControlPointIndexTrackBar.Enabled = false;
            wfEditControlValue2TextBox.Enabled = true;

            switch ( selectedOperationType ) {
            case Constants.Dsv.EditControl.OPERATION_TYPE_CONSTANT:
                wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.EDIT_CONTROL_VALUE_TEXT;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_OVERRIDING:
                wfEditControlPointIndexNumericUpDown.Enabled = true;
                wfEditControlPointIndexTrackBar.Enabled = true;
                wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.EDIT_CONTROL_VALUE_TEXT;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_ADDITION:
                wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.EDIT_CONTROL_ADDEND_TEXT;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_SUBSTRACTION:
                wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.EDIT_CONTROL_SUBTRAHEND_TEXT;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_MULTIPLICATION:
                wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.EDIT_CONTROL_MULTIPLIER_TEXT;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_DIVISION:
                wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.EDIT_CONTROL_DIVISOR_TEXT;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_EXPONENTIATION:
                wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.EDIT_CONTROL_EXPONENT_TEXT;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_LOGARITHMIC:
                wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.EDIT_CONTROL_BASE_TEXT;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_ROOTING:
                wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.EDIT_CONTROL_LEVEL_TEXT;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_POSITIVE:
            case Constants.Dsv.EditControl.OPERATION_TYPE_NEGATIVE:
                wfEditControlValue2TextBox.Text = "0";
                wfEditControlValue2TextBox.Enabled = false;
                break;
            }
        }

        private bool PerformOperationOverriding( int indexOfPoint, double userValue )
        {
            if ( IsValidDecimalChartNumber( userValue ) ) {
                CurveDataSet.Points[indexOfPoint - 1].YValues[0] = userValue;
                return true;
            }

            WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string text = Constants.Dsv.EditControl.NOT_VALID_DECIMAL_CHART_NUMBER_TEXT;
            string caption = Constants.Dsv.EditControl.NOT_VALID_DECIMAL_CHART_NUMBER_CAPTION;
            SetSpecifiedComponentsToInformAboutOverflow( userValue );
            WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            return false;
        }

        private void PerformOperationPositive()
        {
            for ( int i = 0; i < CurveDataSet.Points.Count; i++ ) {
                if ( CurveDataSet.Points[i].YValues[0] < 0 ) {
                    CurveDataSet.Points[i].YValues[0] = Math.Abs( CurveDataSet.Points[i].YValues[0] );
                }
            }
        }

        private void PerformOperationNegative()
        {
            for ( int i = 0; i < CurveDataSet.Points.Count; i++ ) {
                if ( CurveDataSet.Points[i].YValues[0] > 0 ) {
                    CurveDataSet.Points[i].YValues[0] = -(CurveDataSet.Points[i].YValues[0]);
                }
            }
        }

        private bool PerformOperationOnSeriesPoints( double userValue, int operationType )
        {
            Series series = GetCopyOfCurrentCurveSeriesPoints();

            for ( int i = 0; i < series.Points.Count; i++ ) {
                double value = SelectAndPerformOperation( operationType, userValue, series, i );

                if ( !IsValidDecimalChartNumber( value ) ) {
                    WinFormsHelper.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    string text = Constants.Dsv.EditControl.NOT_VALID_DECIMAL_CHART_NUMBER_TEXT;
                    string caption = Constants.Dsv.EditControl.NOT_VALID_DECIMAL_CHART_NUMBER_CAPTION;
                    SetSpecifiedComponentsToInformAboutOverflow( value );
                    WinFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop );
                    return false;
                }
            }

            CopySeriesPointsBack( series, CurveDataSet );
            return true;
        }

        private double SelectAndPerformOperation( int operationType, double userValue, Series series, int pointIndex )
        {
            double result = userValue;

            switch ( operationType ) {
            case Constants.Dsv.EditControl.OPERATION_TYPE_ADDITION:
                result = series.Points[pointIndex].YValues[0] + userValue;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_SUBSTRACTION:
                result = series.Points[pointIndex].YValues[0] - userValue;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_MULTIPLICATION:
                result = series.Points[pointIndex].YValues[0] * userValue;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_DIVISION:
                result = series.Points[pointIndex].YValues[0] / userValue;
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_EXPONENTIATION:
                result = Math.Pow( series.Points[pointIndex].YValues[0], userValue );
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_LOGARITHMIC:
                result = Math.Log( series.Points[pointIndex].YValues[0], userValue );
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_ROOTING:
                result = Math.Pow( series.Points[pointIndex].YValues[0], 1.0 / userValue );
                break;
            case Constants.Dsv.EditControl.OPERATION_TYPE_CONSTANT:
                result = userValue;
                break;
            }

            series.Points[pointIndex].YValues[0] = result;
            return result;
        }

        private bool IsValidDecimalChartNumber( double number )
        {
            Logger.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            decimal convertite;

            try {
                convertite = Convert.ToDecimal( number );
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return false;
            }
            finally {
                Logger.Context = string.Empty;
            }

            return true;
        }

        private Series GetCopyOfCurrentCurveSeriesPoints()
        {
            Series series = new Series();
            CurvesDataset.SetDefaultPropertiesForChartingSeries( series, "CopyOfSeries" );

            for ( int i = 0; i < CurveDataSet.Points.Count; i++ ) {
                series.Points.AddXY( CurveDataSet.Points[i].XValue, CurveDataSet.Points[i].YValues[0] );
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
            StringFormatter.Context = System.Reflection.MethodBase.GetCurrentMethod().Name;
            wfEditControlValue1TextBox.Text = Constants.Dsv.EditControl.OPERATION_ERR_OVERFLOW_TEXT;
            wfEditControlValue2TextBox.Text = StringFormatter.FormatAsNumeric( 4, power );
        }

    }
}
