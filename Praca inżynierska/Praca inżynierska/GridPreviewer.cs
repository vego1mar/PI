﻿using System;
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
using System.Drawing;

namespace PI
{
    public partial class GridPreviewer : Form
    {
        public Series Curve { get; private set; }
        private IList<double> Originals { get; set; }
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        public GridPreviewer( Series series )
        {
            InitializeComponent();
            Curve = series;
            Originals = SeriesAssist.GetValues( series );
            LocalizeWindow();
            UpdateUiBySettings();
        }

        private void UpdateUiBySettings()
        {
            ChartAssist.SetDefaultSettings( uiChart_Prv );
            UiControls.TrySetSelectedIndex( uiPnl_AutoSize_ComBx, (int) AutoSizeColumnsMode.Fill );
            UiControls.TrySetSelectedIndex( uiPnl_OperT_ComBx, (int) Operation.Positive );
            uiPnl_StartIdx_Num.Minimum = 0;
            uiPnl_StartIdx_Num.Maximum = Curve.Points.Count - 1;
            uiPnl_StartIdx_Num.Value = 0;
            uiPnl_EndIdx_Num.Minimum = 0;
            uiPnl_EndIdx_Num.Maximum = Curve.Points.Count - 1;
            uiPnl_EndIdx_Num.Value = Curve.Points.Count - 1;
            UpdateUiByPopulatingGrid();
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( new GridPreviewerStrings().Ui.PanelInfoGridPreviewerLoaded.GetString() );
        }

        private void UpdateUiByPopulatingGrid()
        {
            for ( int i = 0; i < Curve.Points.Count; i++ ) {
                uiGrid_db_grid.Rows.Add();
                uiGrid_db_grid.Rows[i].Cells["Index"].ValueType = typeof( ulong );
                uiGrid_db_grid.Rows[i].Cells["x"].ValueType = typeof( double );
                uiGrid_db_grid.Rows[i].Cells["y"].ValueType = typeof( double );
                uiGrid_db_grid.Rows[i].Cells["Index"].Value = i;
                uiGrid_db_grid.Rows[i].Cells["x"].Value = Curve.Points[i].XValue;
                uiGrid_db_grid.Rows[i].Cells["y"].Value = Curve.Points[i].YValues[0];
            }
        }

        private void UpdateUiByAlteringGrid()
        {
            for ( int i = 0; i < Curve.Points.Count; i++ ) {
                uiGrid_db_grid.Rows[i].Cells["y"].Value = Curve.Points[i].YValues[0];
            }
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

        private void UpdateUiByRefreshingChart()
        {
            GridPreviewerStrings names = new GridPreviewerStrings();
            string signature = string.Empty;

            try {
                signature = MethodBase.GetCurrentMethod().Name + "()";
                ChartAssist.Refresh( Curve, Color.Black, uiChart_Prv );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( signature, ex );
                UpdateUiByPanelStateInfo( names.Ui.PreviewInfoChartNotRepainted.GetString() );
                AppMessages.GridPreviewer.ErrorOfChartRefreshing();
            }
            catch ( Exception ex ) {
                log.Error( signature, ex );
                UpdateUiByPanelStateInfo( names.Ui.PreviewInfoChartRefreshError.GetString() );
            }

            UpdateUiByPanelStateInfo( names.Ui.PreviewInfoChartRefreshed.GetString() );
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

        #region Event handlers

        private void OnLoad( object sender, EventArgs e )
        {
            uiPnl_Ok_Btn.Select();
            uiPnl_Ok_Btn.Focus();
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            // Do not nullify Curve property
            Originals = null;
            Dispose();
        }

        // TODO: GridAssist
        private void OnAutoSizeColumnsModeSelection( object sender, EventArgs e )
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

        private void OnOperationTypeSelection( object sender, EventArgs e )
        {
            UpdateUiBySwitchingOperationType();
        }

        private void OnStartIndexAlteration( object sender, EventArgs e )
        {
            if ( uiPnl_StartIdx_Num.Value > uiPnl_EndIdx_Num.Value ) {
                uiPnl_StartIdx_Num.Value -= 1;
                AppMessages.GridPreviewer.ExclamationOfIndexGreaterThanAllowed();
            }
        }

        private void OnEndIndexAlteration( object sender, EventArgs e )
        {
            if ( uiPnl_EndIdx_Num.Value < uiPnl_StartIdx_Num.Value ) {
                uiPnl_EndIdx_Num.Value += 1;
                AppMessages.GridPreviewer.ExclamationOfIndexLowerThanAllowed();
            }
        }

        private void OnPerformClick( object sender, EventArgs e )
        {
            GridPreviewerStrings names = new GridPreviewerStrings();
            double? userValue = GetUserValue();

            if ( userValue == null || Curve == null ) {
                UpdateUiByPanelStateInfo( names.Ui.PanelInfoInvalidUserValue.GetString() );
                AppMessages.GridPreviewer.ExclamationOfImproperUserValue();
                return;
            }

            int startIndex = UiControls.TryGetValue<int>( uiPnl_StartIdx_Num );
            int endIndex = UiControls.TryGetValue<int>( uiPnl_EndIdx_Num );
            Series seriesCopy = SeriesAssist.GetCopy( Curve );
            Operation operation = (Operation) uiPnl_OperT_ComBx.SelectedIndex;
            string signature = string.Empty;

            try {
                signature = MethodBase.GetCurrentMethod().Name + "(" + ( sender as Button ).Name + ", " + e + ")";
                SeriesAssist.Alter( operation, userValue.Value, seriesCopy, startIndex, endIndex );
            }
            catch ( NotFiniteNumberException ex ) {
                log.Error( signature, ex );
                UpdateUiByPanelStateInfo( names.Ui.PanelInfoOperationRevoked.GetString() );
                AppMessages.GridPreviewer.ErrorOfInvalidCurvePoints();
                return;
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
                UpdateUiByPanelStateInfo( names.Ui.PanelInfoOperationRejected.GetString() );
                AppMessages.GridPreviewer.ErrorOfPerformOperation();
                return;
            }

            Curve.Points.Clear();
            SeriesAssist.CopyPoints( seriesCopy, Curve );
            UpdateUiByAlteringGrid();
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( names.Ui.PanelInfoPerformedAndRefreshed.GetString() );
        }

        private void OnResetClick( object sender, EventArgs e )
        {
            Series copy = SeriesAssist.GetCopy( Curve );
            Curve.Points.Clear();
            SeriesAssist.CopyPoints( Curve, copy, Originals );
            UpdateUiByAlteringGrid();
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( new GridPreviewerStrings().Ui.PanelInfoValuesRestored.GetString() );
        }

        private void OnRefreshClick( object sender, EventArgs e )
        {
            UpdateUiByRefreshingChart();
        }

        private void OnSaveClick( object sender, EventArgs e )
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

            for ( int i = 0; i < Curve.Points.Count; i++ ) {
                Curve.Points[i].YValues[0] = (double) uiGrid_db_grid.Rows[i].Cells["y"].Value;
            }

            UpdateUiByPanelStateInfo( names.Ui.PanelInfoChangesSaved.GetString() );
        }

        #endregion
    }
}
