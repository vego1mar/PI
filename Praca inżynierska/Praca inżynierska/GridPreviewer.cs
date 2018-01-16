using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using PI.src.helpers;
using PI.src.messages;
using PI.src.general;
using log4net;
using System.Reflection;

namespace PI
{

    public partial class GridPreviewer : Form
    {

        public Series ChartDataSet { get; private set; }
        private List<double> OriginalValues { get; set; }

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
            OriginalValues = GetPointsValues( series );
            UiControls.TrySetSelectedIndex( uiPnl_AutoSize_ComBx, (int) Enums.AutoSizeColumnsMode.Fill );
            UiControls.TrySetSelectedIndex( uiPnl_OperT_ComBx, (int) Enums.Operation.Positive );
            uiPnl_StartIdx_Num.Minimum = 0;
            uiPnl_StartIdx_Num.Maximum = ChartDataSet.Points.Count - 1;
            uiPnl_StartIdx_Num.Value = 0;
            uiPnl_EndIdx_Num.Minimum = 0;
            uiPnl_EndIdx_Num.Maximum = ChartDataSet.Points.Count - 1;
            uiPnl_EndIdx_Num.Value = ChartDataSet.Points.Count - 1;
            UpdateUiByPopulatingGrid();
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.InfoGprvLoaded.GetString() );
        }

        private List<double> GetPointsValues( Series series )
        {
            List<double> values = new List<double>();

            for ( int i = 0; i < series.Points.Count; i++ ) {
                values.Add( series.Points[i].YValues[0] );
            }

            return values;
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

            for ( int i = 0; i < uiGrid_db_grid.Rows.Count; i++ ) {
                series.Points.AddY( (double) uiGrid_db_grid.Rows[i].Cells["y"].Value );
            }

            if ( !SeriesAssist.IsChartAcceptable( series ) ) {
                UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.InfoOperationRevoked.GetString() );
                Messages.GridPreviewer.ErrorOfInvalidCurvePoints();
                return;
            }

            for ( int i = 0; i < ChartDataSet.Points.Count; i++ ) {
                ChartDataSet.Points[i].YValues[0] = (double) uiGrid_db_grid.Rows[i].Cells["y"].Value;
            }

            UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.InfoChangesSaved.GetString() );
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
            UpdateUiBySwitchingOperationType();
        }

        private void UpdateUiBySwitchingOperationType()
        {
            Enums.Operation operation = (Enums.Operation) uiPnl_OperT_ComBx.SelectedIndex;
            uiPnl_Val2_TxtBx.Enabled = true;

            switch ( operation ) {
            case Enums.Operation.Addition:
                uiPnl_Val1_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Addend.GetString();
                break;
            case Enums.Operation.Substraction:
                uiPnl_Val1_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Subtrahend.GetString();
                break;
            case Enums.Operation.Multiplication:
                uiPnl_Val1_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Multiplier.GetString();
                break;
            case Enums.Operation.Division:
                uiPnl_Val1_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Divisor.GetString();
                break;
            case Enums.Operation.Exponentiation:
                uiPnl_Val1_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Exponent.GetString();
                break;
            case Enums.Operation.Logarithmic:
                uiPnl_Val1_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Basis.GetString();
                break;
            case Enums.Operation.Rooting:
                uiPnl_Val1_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Basis.GetString();
                break;
            case Enums.Operation.Constant:
                uiPnl_Val1_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Value.GetString();
                break;
            case Enums.Operation.Positive:
            case Enums.Operation.Negative:
                uiPnl_Val1_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.NotApplicable.GetString();
                uiPnl_Val2_TxtBx.Enabled = false;
                break;
            }
        }

        private void UiPanel_StartIndex_ValueChanged( object sender, EventArgs e )
        {
            if ( uiPnl_StartIdx_Num.Value > uiPnl_EndIdx_Num.Value ) {
                uiPnl_StartIdx_Num.Value -= 1;
                Messages.GridPreviewer.ExclamationOfIndexGreaterThanAllowed();
            }
        }

        private void UiPanel_EndIndex_ValueChanged( object sender, EventArgs e )
        {
            if ( uiPnl_EndIdx_Num.Value < uiPnl_StartIdx_Num.Value ) {
                uiPnl_EndIdx_Num.Value += 1;
                Messages.GridPreviewer.ExclamationOfIndexLowerThanAllowed();
            }
        }

        private void UiPanel_Perform_Click( object sender, EventArgs e )
        {
            double? userValue = GetUserValue();

            if ( userValue == null ) {
                UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.InfoInvalidUserValue.GetString() );
                Messages.GridPreviewer.ExclamationOfImproperUserValue();
                return;
            }

            int startIndex = UiControls.TryGetValue<int>( uiPnl_StartIdx_Num );
            int endIndex = UiControls.TryGetValue<int>( uiPnl_EndIdx_Num );
            Series seriesCopy = GetCopyOfSeriesPoints();
            Enums.Operation operation = (Enums.Operation) uiPnl_OperT_ComBx.SelectedIndex;
            bool result = PerformOperation( operation, startIndex, endIndex, userValue.Value, ref seriesCopy );

            if ( !result ) {
                UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.InfoOperationRejected.GetString() );
                Messages.GridPreviewer.ErrorOfPerformOperation();
                return;
            }

            if ( !SeriesAssist.IsChartAcceptable( seriesCopy ) ) {
                UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.InfoOperationRevoked.GetString() );
                Messages.GridPreviewer.ErrorOfInvalidCurvePoints();
                return;
            }

            ApplyPointsAlteration( seriesCopy );
            UpdateUiByAlteringGrid();
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.InfoPerformedAndRefreshed.GetString() );
        }

        private double? GetUserValue()
        {
            switch ( (Enums.Operation) uiPnl_OperT_ComBx.SelectedIndex ) {
            case Enums.Operation.Positive:
            case Enums.Operation.Negative:
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

        private bool PerformOperation( Enums.Operation operation, int startIndex, int endIndex, double value, ref Series series )
        {
            string signature = string.Empty;

            try {
                MethodBase @base = MethodBase.GetCurrentMethod();
                signature = @base.DeclaringType.Name + "." + @base.Name + "()";

                switch ( operation ) {
                case Enums.Operation.Addition:
                    PerformAddition( startIndex, endIndex, value, ref series );
                    break;
                case Enums.Operation.Substraction:
                    PerformSubstraction( startIndex, endIndex, value, ref series );
                    break;
                case Enums.Operation.Multiplication:
                    PerformMultiplication( startIndex, endIndex, value, ref series );
                    break;
                case Enums.Operation.Division:
                    PerformDivision( startIndex, endIndex, value, ref series );
                    break;
                case Enums.Operation.Exponentiation:
                    PerformExponentiation( startIndex, endIndex, value, ref series );
                    break;
                case Enums.Operation.Logarithmic:
                    PerformLogarithmic( startIndex, endIndex, value, ref series );
                    break;
                case Enums.Operation.Rooting:
                    PerformRooting( startIndex, endIndex, value, ref series );
                    break;
                case Enums.Operation.Constant:
                    PerformConstant( startIndex, endIndex, value, ref series );
                    break;
                case Enums.Operation.Positive:
                    PerformPositive( startIndex, endIndex, ref series );
                    break;
                case Enums.Operation.Negative:
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
            UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.InfoValuesRestored.GetString() );
        }

        private void UiPanel_Refresh_Click( object sender, EventArgs e )
        {
            UpdateUiByRefreshingChart();
        }

        private void UpdateUiByRefreshingChart()
        {
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
                UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Preview.InfoChartNotRepainted.GetString() );
                Messages.GridPreviewer.ErrorOfChartRefreshing();
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
                UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Preview.InfoChartRefreshError.GetString() );
            }

            UpdateUiByPanelStateInfo( Translator.GetInstance().Strings.GridPreviewer.Ui.Preview.InfoChartRefreshed.GetString() );
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
            LocalizeForm();
            LocalizePanel();
            LocalizeGrid();
            LocalizePreview();
        }

        private void LocalizeForm()
        {
            Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Form.Text.GetString();
        }

        private void LocalizePanel()
        {
            uiPnl_DtGrid_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.DtGrid.GetString();
            uiPnl_AutoSize_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.AutoSize.GetString();
            uiPnl_Edit_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Edit.GetString();
            uiPnl_OperT_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.OperT.GetString();
            AddLocalizedOperations( uiPnl_OperT_ComBx );
            uiPnl_StartIdx_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.StartIdx.GetString();
            uiPnl_EndIdx_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.EndIdx.GetString();
            UpdateUiBySwitchingOperationType();
            uiPnl_Reset_Btn.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Reset.GetString();
            uiPnl_Perform_Btn.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Perform.GetString();
            uiPnl_Refresh_Btn.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Refresh.GetString();
            uiPnl_Save_Btn.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Save.GetString();
            uiPnl_Ok_Btn.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Panel.Ok.GetString();
        }

        private void LocalizeGrid()
        {
            uiGrid_DtSet_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Dataset.DtSet.GetString();
        }

        private void LocalizePreview()
        {
            uiChart_Prv_TxtBx.Text = Translator.GetInstance().Strings.GridPreviewer.Ui.Preview.Prv.GetString();
        }

        private void AddLocalizedOperations<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.Operations ) {
                control.Items.Add( item.GetString() );
            }
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
