using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{

    public partial class GridPreviewer : Form
    {

        public Series ChartDataSet { get; private set; }

        public GridPreviewer( Series series )
        {
            InitializeComponent();
            SetWindowDefaults( series );
        }

        private void SetWindowDefaults( Series series )
        {
            CurvesDataset.SetDefaultProperties( uiChart_Prv );
            ChartDataSet = series;
            UpdateUiByPopulatingGrid();
            WinFormsHelper.SetSelectedIndexSafe( uiPnl_AutoSize_ComBx, (int) Enums.AutoSizeColumnsMode.Fill );
            WinFormsHelper.SetSelectedIndexSafe( uiPnl_OperT_ComBx, (int) Enums.OperationType.Positive );
            uiPnl_StartIdx_Num.Minimum = 0;
            uiPnl_StartIdx_Num.Maximum = ChartDataSet.Points.Count - 1;
            uiPnl_StartIdx_Num.Value = 0;
            uiPnl_EndIdx_Num.Minimum = 0;
            uiPnl_EndIdx_Num.Maximum = ChartDataSet.Points.Count - 1;
            uiPnl_EndIdx_Num.Value = ChartDataSet.Points.Count - 1;
        }

        private void UpdateUiByPopulatingGrid()
        {
            for ( int i = 0; i < ChartDataSet.Points.Count; i++ ) {
                uiGrid_db_grid.Rows.Add();
                uiGrid_db_grid.Rows[i].Cells["Index"].ValueType = typeof( ulong );
                uiGrid_db_grid.Rows[i].Cells["x"].ValueType = typeof( double );
                uiGrid_db_grid.Rows[i].Cells["y"].ValueType = typeof( double );
                uiGrid_db_grid.Rows[i].Cells["Index"].Value = i;
                uiGrid_db_grid.Rows[i].Cells["x"].Value = ChartDataSet.Points[i].XValue;
                uiGrid_db_grid.Rows[i].Cells["y"].Value = ChartDataSet.Points[i].YValues[0];
            }
        }

        private void UiPanel_Save_Click( object sender, EventArgs e )
        {
            for ( int i = 0; i < ChartDataSet.Points.Count; i++ ) {
                ChartDataSet.Points[i].YValues[0] = (double) uiGrid_db_grid.Rows[i].Cells["y"].Value;
            }
        }

        private void UiPanel_AutoSizeColumnsMode_SelectedIndexChanged( object sender, EventArgs e )
        {
            Enums.AutoSizeColumnsMode mode = (Enums.AutoSizeColumnsMode) uiPnl_AutoSize_ComBx.SelectedIndex;

            switch ( mode ) {
            case Enums.AutoSizeColumnsMode.None:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                break;
            case Enums.AutoSizeColumnsMode.AllCells:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                break;
            case Enums.AutoSizeColumnsMode.DisplayedCells:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                break;
            case Enums.AutoSizeColumnsMode.Fill:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                break;
            }
        }

        private void UiPanel_OperationType_SelectedIndexChanged( object sender, EventArgs e )
        {
            Enums.OperationType operation = (Enums.OperationType) uiPnl_OperT_ComBx.SelectedIndex;
            uiPnl_Val2_TxtBx.Enabled = true;

            switch ( operation ) {
            case Enums.OperationType.Addition:
                uiPnl_Val1_TxtBx.Text = Consts.Gprv.Panel.ADDEND_TXT;
                break;
            case Enums.OperationType.Substraction:
                uiPnl_Val1_TxtBx.Text = Consts.Gprv.Panel.SUBTRAHEND_TXT;
                break;
            case Enums.OperationType.Multiplication:
                uiPnl_Val1_TxtBx.Text = Consts.Gprv.Panel.MULTIPLIER_TXT;
                break;
            case Enums.OperationType.Division:
                uiPnl_Val1_TxtBx.Text = Consts.Gprv.Panel.DIVISOR_TXT;
                break;
            case Enums.OperationType.Exponentiation:
                uiPnl_Val1_TxtBx.Text = Consts.Gprv.Panel.EXPONENT_TXT;
                break;
            case Enums.OperationType.Logarithmic:
                uiPnl_Val1_TxtBx.Text = Consts.Gprv.Panel.BASIS_TXT;
                break;
            case Enums.OperationType.Rooting:
                uiPnl_Val1_TxtBx.Text = Consts.Gprv.Panel.BASIS_TXT;
                break;
            case Enums.OperationType.Constant:
                uiPnl_Val1_TxtBx.Text = Consts.Gprv.Panel.VALUE_TXT;
                break;
            case Enums.OperationType.Positive:
            case Enums.OperationType.Negative:
                uiPnl_Val1_TxtBx.Text = Consts.Gprv.Panel.NOT_APPLICABLE_ABBR_TXT;
                uiPnl_Val2_TxtBx.Enabled = false;
                break;
            }
        }

        private void UiPanel_StartIndex_ValueChanged( object sender, EventArgs e )
        {
            if ( uiPnl_StartIdx_Num.Value > uiPnl_EndIdx_Num.Value ) {
                uiPnl_StartIdx_Num.Value -= 1;
                MsgBxShower.Gprv.Panel.IndexGreaterThanAllowedProblem();
            }
        }

        private void UiPanel_EndIndex_ValueChanged( object sender, EventArgs e )
        {
            if ( uiPnl_EndIdx_Num.Value < uiPnl_StartIdx_Num.Value ) {
                uiPnl_EndIdx_Num.Value += 1;
                MsgBxShower.Gprv.Panel.IndexLowerThanAllowedProblem();
            }
        }

        private void UiPanel_Perform_Click( object sender, EventArgs e )
        {
            double? userValue = GetUserValue();

            if ( userValue == null ) {
                MsgBxShower.Gprv.Panel.ImproperUserValueProblem();
                return;
            }

            int startIndex = WinFormsHelper.GetValue<int>( uiPnl_StartIdx_Num );
            int endIndex = WinFormsHelper.GetValue<int>( uiPnl_EndIdx_Num );
            Series seriesCopy = GetCopyOfSeriesPoints();
            Enums.OperationType operation = (Enums.OperationType) uiPnl_OperT_ComBx.SelectedIndex;
            bool result = PerformOperation( operation, startIndex, endIndex, userValue.Value, ref seriesCopy );

            if ( !result ) {
                MsgBxShower.Gprv.Panel.PerformOperationError();
                return;
            }

            // TODO: watch this
            // ValidatePointsAsDecimal  -->  ApplyPointsAlteration() if OK
            ApplyPointsAlteration( seriesCopy );
            // Refresh Grid and Chart
            uiGrid_db_grid.Rows.Clear();
            UpdateUiByPopulatingGrid();
        }

        private double? GetUserValue()
        {
            switch ( (Enums.OperationType) uiPnl_OperT_ComBx.SelectedIndex ) {
            case Enums.OperationType.Positive:
            case Enums.OperationType.Negative:
                return 0.0;
            }

            double? userValue = null;

            try {
                userValue = Convert.ToDouble( uiPnl_Val2_TxtBx.Text );
            }
            catch ( OverflowException x ) {
                Logger.WriteException( x );
                return null;
            }
            catch ( FormatException x ) {
                Logger.WriteException( x );
                return null;
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return null;
            }

            return userValue;
        }

        private Series GetCopyOfSeriesPoints()
        {
            Series series = new Series();
            series.Points.Clear();

            for ( int i = 0; i < ChartDataSet.Points.Count; i++ ) {
                series.Points.AddXY( ChartDataSet.Points[i].XValue, ChartDataSet.Points[i].YValues[0] );
            }

            return series;
        }

        private void ApplyPointsAlteration( Series source )
        {
            for ( int i = 0; i < ChartDataSet.Points.Count; i++ ) {
                ChartDataSet.Points[i].YValues[0] = source.Points[i].YValues[0];
            }
        }

        private bool PerformOperation( Enums.OperationType operation, int startIndex, int endIndex, double value, ref Series series )
        {
            try {
                switch ( operation ) {
                case Enums.OperationType.Addition:
                    PerformAddition( startIndex, endIndex, value, ref series );
                    break;
                case Enums.OperationType.Substraction:
                    PerformSubstraction( startIndex, endIndex, value, ref series );
                    break;
                case Enums.OperationType.Multiplication:
                    PerformMultiplication( startIndex, endIndex, value, ref series );
                    break;
                case Enums.OperationType.Division:
                    PerformDivision( startIndex, endIndex, value, ref series );
                    break;
                case Enums.OperationType.Exponentiation:
                    PerformExponentiation( startIndex, endIndex, value, ref series );
                    break;
                case Enums.OperationType.Logarithmic:
                    PerformLogarithmic( startIndex, endIndex, value, ref series );
                    break;
                case Enums.OperationType.Rooting:
                    PerformRooting( startIndex, endIndex, value, ref series );
                    break;
                case Enums.OperationType.Constant:
                    PerformConstant( startIndex, endIndex, value, ref series );
                    break;
                case Enums.OperationType.Positive:
                    PerformPositive( startIndex, endIndex, ref series );
                    break;
                case Enums.OperationType.Negative:
                    PerformNegative( startIndex, endIndex, ref series );
                    break;
                }
            }
            catch ( Exception x ) {
                Logger.WriteException( x );
                return false;
            }

            return true;
        }

        private void PerformAddition( int startIndex, int endIndex, double addend, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] += addend;
            }
        }

        private void PerformSubstraction( int startIndex, int endIndex, double subtrahend, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] -= subtrahend;
            }
        }

        private void PerformMultiplication( int startIndex, int endIndex, double multiplier, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] *= multiplier;
            }
        }

        private void PerformDivision( int startIndex, int endIndex, double divisor, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] /= divisor;
            }
        }

        private void PerformExponentiation( int startIndex, int endIndex, double exponent, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] = Math.Pow( series.Points[i].YValues[0], exponent );
            }
        }

        private void PerformLogarithmic( int startIndex, int endIndex, double basis, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] = Math.Log( series.Points[i].YValues[0], basis );
            }
        }

        private void PerformRooting( int startIndex, int endIndex, double basis, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] = Math.Pow( series.Points[i].YValues[0], 1.0 / basis );
            }
        }

        private void PerformConstant( int startIndex, int endIndex, double value, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] = value;
            }
        }

        private void PerformPositive( int startIndex, int endIndex, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] = Math.Abs( series.Points[i].YValues[0] );
            }
        }

        private void PerformNegative( int startIndex, int endIndex, ref Series series )
        {
            for ( int i = startIndex; i <= endIndex; i++ ) {
                series.Points[i].YValues[0] = -Math.Abs( series.Points[i].YValues[0] );
            }
        }

    }
}
