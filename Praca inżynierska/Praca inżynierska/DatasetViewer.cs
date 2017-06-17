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
            UpdateUiByRangesForPointIndexComponents();

            if ( curveDataset != null ) {
                BuildAndPopulateDatasetGrid();
            }
        }

        private void UiPanel_Perform_Click( object sender, EventArgs e )
        {
            int selectedOperationType = WinFormsHelper.GetSelectedIndexSafe( uiPnl_OperT_ComBx );
            int selectedPointIndex = WinFormsHelper.GetValue( uiPnl_PointIdx_TrBr );
            bool isValueValid = WinFormsHelper.GetValue( uiPnl_Value2_TxtBx, out double userValue );
            bool isOperationValid = false;

            if ( !isValueValid ) {
                MsgBxShower.Dsv.CastOrConversionProblem();
                return;
            }

            switch ( selectedOperationType ) {
            case Constants.Dsv.Panel.OPERATION_TYPE_OVERRIDING:
                isOperationValid = PerformOperationOverriding( selectedPointIndex, userValue );
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_ADDITION:
            case Constants.Dsv.Panel.OPERATION_TYPE_SUBSTRACTION:
            case Constants.Dsv.Panel.OPERATION_TYPE_MULTIPLICATION:
            case Constants.Dsv.Panel.OPERATION_TYPE_DIVISION:
            case Constants.Dsv.Panel.OPERATION_TYPE_EXPONENTIATION:
            case Constants.Dsv.Panel.OPERATION_TYPE_LOGARITHMIC:
            case Constants.Dsv.Panel.OPERATION_TYPE_ROOTING:
            case Constants.Dsv.Panel.OPERATION_TYPE_CONSTANT:
                isOperationValid = PerformOperationOnSeriesPoints( userValue, selectedOperationType );
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_POSITIVE:
                PerformOperationPositive();
                isOperationValid = true;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_NEGATIVE:
                PerformOperationNegative();
                isOperationValid = true;
                break;
            default:
                MsgBxShower.Dsv.OperationTypeNotSelectedInfo();
                return;
            }

            if ( isOperationValid ) {
                BuildAndPopulateDatasetGrid();
            }
        }

        private void UpdateUiByRangesForPointIndexComponents()
        {
            uiPnl_PointIdx_Num.Minimum = 1;
            uiPnl_PointIdx_Num.Maximum = PreSets.Ui.NumberOfPoints;
            uiPnl_PointIdx_TrBr.Minimum = 1;
            uiPnl_PointIdx_TrBr.Maximum = PreSets.Ui.NumberOfPoints;
        }

        private void UiPanel_PointIndex_NumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            int currentValue = WinFormsHelper.GetValue<int>( uiPnl_PointIdx_Num );
            WinFormsHelper.SetValue( uiPnl_PointIdx_TrBr, currentValue );
        }

        private void UiPanel_PointIndex_TrackBar_Scroll( object sender, EventArgs e )
        {
            int currentValue = WinFormsHelper.GetValue( uiPnl_PointIdx_TrBr );
            WinFormsHelper.SetValue( uiPnl_PointIdx_Num, currentValue );
        }

        private void BuildAndPopulateDatasetGrid()
        {
            ClearDatasetGridAndBuildStartState();
            AddTitleHeaderComponentsToDatasetGrid();
            PopulateDatasetGridByCurvePointsData();
        }

        private void ClearDatasetGridAndBuildStartState()
        {
            uiGrid_TblLay.Controls.Clear();
            int lastRow = uiGrid_TblLay.RowCount - 1;
            RowStyle previousRowStyle = uiGrid_TblLay.RowStyles[lastRow];
            uiGrid_TblLay.RowStyles.Clear();
            uiGrid_TblLay.RowCount = 0;
            uiGrid_TblLay.RowStyles.Add( WinFormsHelper.GetRowStyleSafe( previousRowStyle.SizeType, previousRowStyle.Height ) );
            uiGrid_TblLay.RowStyles.Add( WinFormsHelper.GetRowStyleSafe( previousRowStyle.SizeType, previousRowStyle.Height ) );
            uiGrid_TblLay.RowCount = 2;
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

            int lastRow = uiGrid_TblLay.RowCount - 1;
            uiGrid_TblLay.Controls.Add( indexTextBox, 0, lastRow );
            uiGrid_TblLay.Controls.Add( axisXTextBox, 1, lastRow );
            uiGrid_TblLay.Controls.Add( axisYTextBox, 2, lastRow );
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
            int lastRow = uiGrid_TblLay.RowCount - 1;
            RowStyle previousRowStyle = uiGrid_TblLay.RowStyles[lastRow];
            uiGrid_TblLay.RowStyles.Add( new RowStyle( previousRowStyle.SizeType, previousRowStyle.Height ) );
            uiGrid_TblLay.RowCount++;
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

            int lastRow = uiGrid_TblLay.RowCount - 1;
            uiGrid_TblLay.Controls.Add( indexTextBox, 0, lastRow );
            uiGrid_TblLay.Controls.Add( axisXTextBox, 1, lastRow );
            uiGrid_TblLay.Controls.Add( axisYTextBox, 2, lastRow );
        }

        private void UiPanel_OperationType_SelectedIndexChanged( object sender, EventArgs e )
        {
            int selectedOperationType = WinFormsHelper.GetSelectedIndexSafe( uiPnl_OperT_ComBx );
            uiPnl_PointIdx_Num.Enabled = false;
            uiPnl_PointIdx_TrBr.Enabled = false;
            uiPnl_Value2_TxtBx.Enabled = true;

            switch ( selectedOperationType ) {
            case Constants.Dsv.Panel.OPERATION_TYPE_CONSTANT:
                uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.EDIT_CONTROL_VALUE_TEXT;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_OVERRIDING:
                uiPnl_PointIdx_Num.Enabled = true;
                uiPnl_PointIdx_TrBr.Enabled = true;
                uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.EDIT_CONTROL_VALUE_TEXT;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_ADDITION:
                uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.EDIT_CONTROL_ADDEND_TEXT;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_SUBSTRACTION:
                uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.EDIT_CONTROL_SUBTRAHEND_TEXT;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_MULTIPLICATION:
                uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.EDIT_CONTROL_MULTIPLIER_TEXT;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_DIVISION:
                uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.EDIT_CONTROL_DIVISOR_TEXT;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_EXPONENTIATION:
                uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.EDIT_CONTROL_EXPONENT_TEXT;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_LOGARITHMIC:
                uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.EDIT_CONTROL_BASE_TEXT;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_ROOTING:
                uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.EDIT_CONTROL_LEVEL_TEXT;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_POSITIVE:
            case Constants.Dsv.Panel.OPERATION_TYPE_NEGATIVE:
                uiPnl_Value2_TxtBx.Text = "0";
                uiPnl_Value2_TxtBx.Enabled = false;
                break;
            }
        }

        private bool PerformOperationOverriding( int indexOfPoint, double userValue )
        {
            if ( IsValidDecimalChartNumber( userValue ) ) {
                CurveDataSet.Points[indexOfPoint - 1].YValues[0] = userValue;
                return true;
            }

            UpdateUiByInfoAboutOverflow( userValue );
            MsgBxShower.Dsv.DecimalDataTypeOverflowStop();
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
                    UpdateUiByInfoAboutOverflow( value );
                    MsgBxShower.Dsv.DecimalDataTypeOverflowStop();
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
            case Constants.Dsv.Panel.OPERATION_TYPE_ADDITION:
                result = series.Points[pointIndex].YValues[0] + userValue;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_SUBSTRACTION:
                result = series.Points[pointIndex].YValues[0] - userValue;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_MULTIPLICATION:
                result = series.Points[pointIndex].YValues[0] * userValue;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_DIVISION:
                result = series.Points[pointIndex].YValues[0] / userValue;
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_EXPONENTIATION:
                result = Math.Pow( series.Points[pointIndex].YValues[0], userValue );
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_LOGARITHMIC:
                result = Math.Log( series.Points[pointIndex].YValues[0], userValue );
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_ROOTING:
                result = Math.Pow( series.Points[pointIndex].YValues[0], 1.0 / userValue );
                break;
            case Constants.Dsv.Panel.OPERATION_TYPE_CONSTANT:
                result = userValue;
                break;
            }

            series.Points[pointIndex].YValues[0] = result;
            return result;
        }

        private bool IsValidDecimalChartNumber( double number )
        {
            decimal convertite;

            try {
                convertite = Convert.ToDecimal( number );
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x, LoggerSection.DataSetViewer );
                return false;
            }
            catch ( Exception x ) {
                Logger.WriteException( x, LoggerSection.DataSetViewer );
                return false;
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

        private void UpdateUiByInfoAboutOverflow( double power )
        {
            uiPnl_Value1_TxtBx.Text = Constants.Dsv.Panel.OPERATION_ERR_OVERFLOW_TEXT;
            uiPnl_Value2_TxtBx.Text = StringFormatter.FormatAsNumeric( 4, power );
        }

    }
}
