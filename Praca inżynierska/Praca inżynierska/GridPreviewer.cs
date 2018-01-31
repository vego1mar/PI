using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using PI.src.helpers;
using PI.src.messages;
using PI.src.general;
using log4net;
using System.Reflection;
using PI.src.enumerators;
using PI.src.localization.enums;
using PI.src.localization.windows;

namespace PI
{

    public partial class GridPreviewer : Form
    {

        public Series ChartDataSet { get; private set; }
        private IList<double> OriginalValues { get; set; }

        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public GridPreviewer( Series series )
        {
            InitializeComponent();
            LocalizeWindow();
            SetWindowDefaults( series );
        }

        private void SetWindowDefaults( Series series )
        {
            ChartAssist.SetDefaultSettings( uiChart_Prv );
            ChartDataSet = series;
            OriginalValues = SeriesAssist.GetValues( series );
            UiControls.TrySetSelectedIndex( uiPnl_AutoSize_ComBx, (int) AutoSizeColumnsMode.Fill );
            UiControls.TrySetSelectedIndex( uiPnl_OperT_ComBx, (int) Operation.Positive );
            uiPnl_StartIdx_Num.Minimum = 0;
            uiPnl_StartIdx_Num.Maximum = ChartDataSet.Points.Count - 1;
            uiPnl_StartIdx_Num.Value = 0;
            uiPnl_EndIdx_Num.Minimum = 0;
            uiPnl_EndIdx_Num.Maximum = ChartDataSet.Points.Count - 1;
            uiPnl_EndIdx_Num.Value = ChartDataSet.Points.Count - 1;
            UpdateUiByPopulatingGrid();
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( new GridPreviewerStrings().Ui.PanelInfoGridPreviewerLoaded.GetString() );
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

        private void UpdateUiByAlteringGrid()
        {
            for ( int i = 0; i < ChartDataSet.Points.Count; i++ ) {
                uiGrid_db_grid.Rows[i].Cells["y"].Value = ChartDataSet.Points[i].YValues[0];
            }
        }

        private void UiPanel_Save_Click( object sender, EventArgs e )
        {
            Series series = new Series();
            GridPreviewerStrings names = new GridPreviewerStrings();

            for ( int i = 0; i < uiGrid_db_grid.Rows.Count; i++ ) {
                series.Points.AddY( (double) uiGrid_db_grid.Rows[i].Cells["y"].Value );
            }

            if ( !SeriesAssist.IsChartAcceptable( series ) ) {
                UpdateUiByPanelStateInfo( names.Ui.PanelInfoOperationRevoked.GetString() );
                AppMessages.GridPreviewer.ErrorOfInvalidCurvePoints();
                return;
            }

            for ( int i = 0; i < ChartDataSet.Points.Count; i++ ) {
                ChartDataSet.Points[i].YValues[0] = (double) uiGrid_db_grid.Rows[i].Cells["y"].Value;
            }

            UpdateUiByPanelStateInfo( names.Ui.PanelInfoChangesSaved.GetString() );
        }

        private void UiPanel_AutoSizeColumnsMode_SelectedIndexChanged( object sender, EventArgs e )
        {
            AutoSizeColumnsMode mode = (AutoSizeColumnsMode) uiPnl_AutoSize_ComBx.SelectedIndex;

            switch ( mode ) {
            case AutoSizeColumnsMode.AllCells:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                break;
            case AutoSizeColumnsMode.AllCellsExceptHeader:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
                break;
            case AutoSizeColumnsMode.ColumnHeader:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                break;
            case AutoSizeColumnsMode.DisplayedCells:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                break;
            case AutoSizeColumnsMode.DisplayedCellsExceptHeader:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
                break;
            case AutoSizeColumnsMode.Fill:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                break;
            case AutoSizeColumnsMode.None:
                uiGrid_db_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                break;
            }
        }

        private void UiPanel_OperationType_SelectedIndexChanged( object sender, EventArgs e )
        {
            UpdateUiBySwitchingOperationType();
        }

        private void UpdateUiBySwitchingOperationType()
        {
            GridPreviewerStrings names = new GridPreviewerStrings();
            Operation operation = (Operation) uiPnl_OperT_ComBx.SelectedIndex;
            uiPnl_Val2_TxtBx.Enabled = true;

            switch ( operation ) {
            case Operation.Addition:
                uiPnl_Val1_TxtBx.Text = names.Ui.PanelAddend.GetString();
                break;
            case Operation.Subtraction:
                uiPnl_Val1_TxtBx.Text = names.Ui.PanelSubtrahend.GetString();
                break;
            case Operation.Multiplication:
                uiPnl_Val1_TxtBx.Text = names.Ui.PanelMultiplier.GetString();
                break;
            case Operation.Division:
                uiPnl_Val1_TxtBx.Text = names.Ui.PanelDivisor.GetString();
                break;
            case Operation.Exponentiation:
                uiPnl_Val1_TxtBx.Text = names.Ui.PanelExponent.GetString();
                break;
            case Operation.Logarithmic:
                uiPnl_Val1_TxtBx.Text = names.Ui.PanelBasis.GetString();
                break;
            case Operation.Rooting:
                uiPnl_Val1_TxtBx.Text = names.Ui.PanelBasis.GetString();
                break;
            case Operation.Constant:
                uiPnl_Val1_TxtBx.Text = names.Ui.PanelValue.GetString();
                break;
            case Operation.Positive:
            case Operation.Negative:
                uiPnl_Val1_TxtBx.Text = names.Ui.PanelNotApplicable.GetString();
                uiPnl_Val2_TxtBx.Enabled = false;
                break;
            }
        }

        private void UiPanel_StartIndex_ValueChanged( object sender, EventArgs e )
        {
            if ( uiPnl_StartIdx_Num.Value > uiPnl_EndIdx_Num.Value ) {
                uiPnl_StartIdx_Num.Value -= 1;
                AppMessages.GridPreviewer.ExclamationOfIndexGreaterThanAllowed();
            }
        }

        private void UiPanel_EndIndex_ValueChanged( object sender, EventArgs e )
        {
            if ( uiPnl_EndIdx_Num.Value < uiPnl_StartIdx_Num.Value ) {
                uiPnl_EndIdx_Num.Value += 1;
                AppMessages.GridPreviewer.ExclamationOfIndexLowerThanAllowed();
            }
        }

        private void UiPanel_Perform_Click( object sender, EventArgs e )
        {
            GridPreviewerStrings names = new GridPreviewerStrings();
            double? userValue = GetUserValue();

            if ( userValue == null ) {
                UpdateUiByPanelStateInfo( names.Ui.PanelInfoInvalidUserValue.GetString() );
                AppMessages.GridPreviewer.ExclamationOfImproperUserValue();
                return;
            }

            int startIndex = UiControls.TryGetValue<int>( uiPnl_StartIdx_Num );
            int endIndex = UiControls.TryGetValue<int>( uiPnl_EndIdx_Num );
            Series seriesCopy = GetCopyOfSeriesPoints();
            Operation operation = (Operation) uiPnl_OperT_ComBx.SelectedIndex;
            bool result = PerformOperation( operation, startIndex, endIndex, userValue.Value, ref seriesCopy );

            if ( !result ) {
                UpdateUiByPanelStateInfo( names.Ui.PanelInfoOperationRejected.GetString() );
                AppMessages.GridPreviewer.ErrorOfPerformOperation();
                return;
            }

            if ( !SeriesAssist.IsChartAcceptable( seriesCopy ) ) {
                UpdateUiByPanelStateInfo( names.Ui.PanelInfoOperationRevoked.GetString() );
                AppMessages.GridPreviewer.ErrorOfInvalidCurvePoints();
                return;
            }

            ApplyPointsAlteration( seriesCopy );
            UpdateUiByAlteringGrid();
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( names.Ui.PanelInfoPerformedAndRefreshed.GetString() );
        }

        private double? GetUserValue()
        {
            switch ( (Operation) uiPnl_OperT_ComBx.SelectedIndex ) {
            case Operation.Positive:
            case Operation.Negative:
                return 0.0;
            }

            double? userValue = null;
            string signature = string.Empty;

            try {
                MethodBase @base = MethodBase.GetCurrentMethod();
                signature = @base.DeclaringType.Name + "." + @base.Name + "()";
                userValue = Convert.ToDouble( uiPnl_Val2_TxtBx.Text );
            }
            catch ( OverflowException ex ) {
                log.Error( signature, ex );
                return null;
            }
            catch ( FormatException ex ) {
                log.Error( signature, ex );
                return null;
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
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

        private bool PerformOperation( Operation operation, int startIndex, int endIndex, double value, ref Series series )
        {
            string signature = string.Empty;

            try {
                MethodBase @base = MethodBase.GetCurrentMethod();
                signature = @base.DeclaringType.Name + "." + @base.Name + "()";

                switch ( operation ) {
                case Operation.Addition:
                    PerformAddition( startIndex, endIndex, value, ref series );
                    break;
                case Operation.Subtraction:
                    PerformSubstraction( startIndex, endIndex, value, ref series );
                    break;
                case Operation.Multiplication:
                    PerformMultiplication( startIndex, endIndex, value, ref series );
                    break;
                case Operation.Division:
                    PerformDivision( startIndex, endIndex, value, ref series );
                    break;
                case Operation.Exponentiation:
                    PerformExponentiation( startIndex, endIndex, value, ref series );
                    break;
                case Operation.Logarithmic:
                    PerformLogarithmic( startIndex, endIndex, value, ref series );
                    break;
                case Operation.Rooting:
                    PerformRooting( startIndex, endIndex, value, ref series );
                    break;
                case Operation.Constant:
                    PerformConstant( startIndex, endIndex, value, ref series );
                    break;
                case Operation.Positive:
                    PerformPositive( startIndex, endIndex, ref series );
                    break;
                case Operation.Negative:
                    PerformNegative( startIndex, endIndex, ref series );
                    break;
                }
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
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

        private string GetStateInfoTimeHeader()
        {
            string date = DateTime.Now.ToString( System.Globalization.CultureInfo.InvariantCulture );
            string time = date.Substring( date.IndexOf( ' ' ) + 1 );
            return "(" + time + ") ";
        }

        private void UpdateUiByPanelStateInfo( string info )
        {
            uiPnl_Info_TxtBx.Text = GetStateInfoTimeHeader() + info;
        }

        private void UiPanel_Reset_Click( object sender, EventArgs e )
        {
            for ( int i = 0; i < OriginalValues.Count; i++ ) {
                ChartDataSet.Points[i].YValues[0] = OriginalValues[i];
            }

            UpdateUiByAlteringGrid();
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( new GridPreviewerStrings().Ui.PanelInfoValuesRestored.GetString() );
        }

        private void UiPanel_Refresh_Click( object sender, EventArgs e )
        {
            UpdateUiByRefreshingChart();
        }

        private void UpdateUiByRefreshingChart()
        {
            GridPreviewerStrings names = new GridPreviewerStrings();
            string signature = string.Empty;

            try {
                MethodBase @base = MethodBase.GetCurrentMethod();
                signature = @base.DeclaringType.Name + "." + @base.Name + "()";
                uiChart_Prv.Series.Clear();
                Series series = GetCopyOfSeriesPoints();
                SeriesAssist.SetDefaultSettings( series );
                uiChart_Prv.Series.Add( series );
                uiChart_Prv.ChartAreas[0].RecalculateAxesScale();
                uiChart_Prv.Visible = true;
                uiChart_Prv.Invalidate();
            }
            catch ( InvalidOperationException ex ) {
                log.Error( signature, ex );
                UpdateUiByPanelStateInfo( names.Ui.PreviewInfoChartNotRepainted.GetString() );
                AppMessages.GridPreviewer.ErrorOfChartRefreshing();
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
                UpdateUiByPanelStateInfo( names.Ui.PreviewInfoChartRefreshError.GetString() );
            }

            UpdateUiByPanelStateInfo( names.Ui.PreviewInfoChartRefreshed.GetString() );
        }

        private void UiGridPreviewer_Load( object sender, EventArgs e )
        {
            uiPnl_Ok_Btn.Select();
            uiPnl_Ok_Btn.Focus();
        }

        private void GridPreviewer_FormClosing( object sender, FormClosingEventArgs e )
        {
            OriginalValues = null;
            Dispose();
        }

        private void LocalizeWindow()
        {
            GridPreviewerStrings names = new GridPreviewerStrings();
            Text = names.Form.Text.GetString();

            // Panel
            uiPnl_DtGrid_TxtBx.Text = names.Ui.PanelDatasetGrid.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.AutoSizeColumnsMode, uiPnl_AutoSize_ComBx );
            uiPnl_AutoSize_TxtBx.Text = names.Ui.PanelAutoSizeColumnsMode.GetString();
            uiPnl_Edit_TxtBx.Text = names.Ui.PanelFastEdit.GetString();
            uiPnl_OperT_TxtBx.Text = names.Ui.PanelOperationType.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.Operation, uiPnl_OperT_ComBx );
            uiPnl_StartIdx_TxtBx.Text = names.Ui.PanelStartIndex.GetString();
            uiPnl_EndIdx_TxtBx.Text = names.Ui.PanelEndIndex.GetString();
            UpdateUiBySwitchingOperationType();
            uiPnl_Reset_Btn.Text = names.Ui.PanelReset.GetString();
            uiPnl_Perform_Btn.Text = names.Ui.PanelPerform.GetString();
            uiPnl_Refresh_Btn.Text = names.Ui.PanelRefresh.GetString();
            uiPnl_Save_Btn.Text = names.Ui.PanelSave.GetString();
            uiPnl_Ok_Btn.Text = names.Ui.PanelOk.GetString();

            // Grid
            uiGrid_DtSet_TxtBx.Text = names.Ui.DatasetTitle.GetString();

            // Preview
            uiChart_Prv_TxtBx.Text = names.Ui.PreviewTitle.GetString();
        }

        public void SetFastEditControls( bool isAvailable )
        {
            uiPnl_OperT_ComBx.Enabled = isAvailable;
            uiPnl_StartIdx_Num.Enabled = isAvailable;
            uiPnl_EndIdx_Num.Enabled = isAvailable;
            uiPnl_Val2_TxtBx.Enabled = isAvailable;
            uiPnl_Perform_Btn.Enabled = isAvailable;
            uiPnl_Save_Btn.Enabled = isAvailable;
        }

    }
}
