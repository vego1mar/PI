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
using System.Drawing;

namespace PI.src.windows
{
    public partial class GridPreviewer : Form
    {
        public Series Curve { get; private set; }
        private IList<double> Originals { get; set; }
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
        private const int VALUES_DECIMAL_PLACES = 8;

        private enum PanelStateInformation
        {
            GridPreviewerLoaded,
            ChartNotRepainted,
            ChartRefreshingError,
            ChartRefreshed,
            InvalidUserValue,
            OperationRevoked,
            OperationRejected,
            PerformedAndRefreshed,
            ValuesRestored,
            ChangesSaved
        }

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
            GridAssist.SetDefaultSettings( uiGrid_db_grid );
            UpdateUiByPopulatingGrid();
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( PanelStateInformation.GridPreviewerLoaded );
        }

        private void UpdateUiByPopulatingGrid()
        {
            GridAssist.AlterColumnHeader( Index, new GridPreviewerStrings().Ui.DatasetIndex.GetString() );
            GridAssist.AlterColumnHeader( x, "x" );
            GridAssist.AlterColumnHeader( y, "y", false );
            GridAssist.AddRows( uiGrid_db_grid, Curve.Points.Count );
            GridAssist.PopulateColumn( uiGrid_db_grid, Index.Name, Lists.GetOrdinalValues( 0, Convert.ToUInt64( Curve.Points.Count ) ) );
            GridAssist.PopulateColumn( uiGrid_db_grid, x.Name, SeriesAssist.GetArguments( Curve ), 4 );
            GridAssist.PopulateColumn( uiGrid_db_grid, y.Name, SeriesAssist.GetValues( Curve ), VALUES_DECIMAL_PLACES );
        }

        private void UpdateUiBySwitchingOperationType( Operation operation )
        {
            GridPreviewerStrings names = new GridPreviewerStrings();
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

        private void UpdateUiByPanelStateInfo( PanelStateInformation information )
        {
            GridPreviewerStrings names = new GridPreviewerStrings();
            string commonString = '(' + Strings.GetTimeHeader() + ") ";

            switch ( information ) {
            case PanelStateInformation.ChangesSaved:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PanelInfoChangesSaved.GetString();
                break;
            case PanelStateInformation.ChartNotRepainted:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PreviewInfoChartNotRepainted.GetString();
                break;
            case PanelStateInformation.ChartRefreshed:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PreviewInfoChartRefreshed.GetString();
                break;
            case PanelStateInformation.ChartRefreshingError:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PreviewInfoChartRefreshError.GetString();
                break;
            case PanelStateInformation.GridPreviewerLoaded:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PanelInfoGridPreviewerLoaded.GetString();
                break;
            case PanelStateInformation.InvalidUserValue:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PanelInfoInvalidUserValue.GetString();
                break;
            case PanelStateInformation.OperationRejected:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PanelInfoOperationRejected.GetString();
                break;
            case PanelStateInformation.OperationRevoked:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PanelInfoOperationRevoked.GetString();
                break;
            case PanelStateInformation.PerformedAndRefreshed:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PanelInfoPerformedAndRefreshed.GetString();
                break;
            case PanelStateInformation.ValuesRestored:
                uiPnl_Info_TxtBx.Text = commonString + names.Ui.PanelInfoValuesRestored.GetString();
                break;
            }
        }

        private void UpdateUiByRefreshingChart()
        {
            try {
                ChartAssist.Refresh( Curve, Color.Black, uiChart_Prv );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( MethodBase.GetCurrentMethod().Name + '(' + PanelStateInformation.ChartNotRepainted + ')', ex );
                UpdateUiByPanelStateInfo( PanelStateInformation.ChartNotRepainted );
                AppMessages.GridPreviewer.ErrorOfChartRefreshing();
            }
            catch ( Exception ex ) {
                log.Error( MethodBase.GetCurrentMethod().Name + '(' + PanelStateInformation.ChartRefreshingError + ')', ex );
                UpdateUiByPanelStateInfo( PanelStateInformation.ChartRefreshingError );
            }

            UpdateUiByPanelStateInfo( PanelStateInformation.ChartRefreshed );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + PanelStateInformation.ChartRefreshed + ')' );
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
            UpdateUiBySwitchingOperationType( (Operation) UiControls.TryGetSelectedIndex( uiPnl_OperT_ComBx ) );
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
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ')' );
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            // Do not nullify Curve property
            Originals = null;
            Dispose();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ',' + e.CloseReason + ')' );
        }

        private void OnAutoSizeColumnsModeSelection( object sender, EventArgs e )
        {
            AutoSizeColumnsMode mode = (AutoSizeColumnsMode) UiControls.TryGetSelectedIndex( uiPnl_AutoSize_ComBx );
            GridAssist.SetAutoSizeColumnsMode( uiGrid_db_grid, mode );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + mode + ')' );
        }

        private void OnOperationTypeSelection( object sender, EventArgs e )
        {
            Operation operation = (Operation) UiControls.TryGetSelectedIndex( uiPnl_OperT_ComBx );
            UpdateUiBySwitchingOperationType( operation );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + operation + ')' );
        }

        private void OnStartIndexAlteration( object sender, EventArgs e )
        {
            if ( uiPnl_StartIdx_Num.Value > uiPnl_EndIdx_Num.Value ) {
                uiPnl_StartIdx_Num.Value -= 1;
                AppMessages.GridPreviewer.ExclamationOfIndexGreaterThanAllowed();
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as NumericUpDown).Value + ')' );
        }

        private void OnEndIndexAlteration( object sender, EventArgs e )
        {
            if ( uiPnl_EndIdx_Num.Value < uiPnl_StartIdx_Num.Value ) {
                uiPnl_EndIdx_Num.Value += 1;
                AppMessages.GridPreviewer.ExclamationOfIndexLowerThanAllowed();
            }

            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as NumericUpDown).Value + ')' );
        }

        private void OnPerformClick( object sender, EventArgs e )
        {
            Operation @operator = (Operation) UiControls.TryGetSelectedIndex( uiPnl_OperT_ComBx );
            double? userValue = (@operator == Operation.Positive || @operator == Operation.Negative) ? default( double ) : Strings.TryGetValue( uiPnl_Val2_TxtBx.Text );

            if ( userValue == null || Curve == null ) {
                log.Info( MethodBase.GetCurrentMethod().Name + '(' + userValue + ',' + Curve + ')' );
                UpdateUiByPanelStateInfo( PanelStateInformation.InvalidUserValue );
                AppMessages.GridPreviewer.ExclamationOfImproperUserValue();
                return;
            }

            int startIndex = UiControls.TryGetValue<int>( uiPnl_StartIdx_Num );
            int endIndex = UiControls.TryGetValue<int>( uiPnl_EndIdx_Num );
            Series seriesCopy = SeriesAssist.GetCopy( Curve );
            Operation operation = (Operation) uiPnl_OperT_ComBx.SelectedIndex;
            string signature = string.Empty;

            try {
                signature = MethodBase.GetCurrentMethod().Name + '(' + startIndex + ',' + endIndex + ',' + operation + ',' + userValue.Value + ')';
                SeriesAssist.Alter( operation, userValue.Value, seriesCopy, startIndex, endIndex );
            }
            catch ( NotFiniteNumberException ex ) {
                log.Error( signature, ex );
                UpdateUiByPanelStateInfo( PanelStateInformation.OperationRevoked );
                AppMessages.GridPreviewer.ErrorOfInvalidCurvePoints();
                return;
            }
            catch ( Exception ex ) {
                log.Fatal( signature, ex );
                UpdateUiByPanelStateInfo( PanelStateInformation.OperationRejected );
                AppMessages.GridPreviewer.ErrorOfPerformOperation();
                return;
            }

            Curve.Points.Clear();
            SeriesAssist.CopyPoints( seriesCopy, Curve );
            GridAssist.PopulateColumn( uiGrid_db_grid, y.Name, SeriesAssist.GetValues( Curve ), VALUES_DECIMAL_PLACES );
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( PanelStateInformation.PerformedAndRefreshed );
            log.Info( signature );
        }

        private void OnResetClick( object sender, EventArgs e )
        {
            Series copy = SeriesAssist.GetCopy( Curve );
            Curve.Points.Clear();
            SeriesAssist.CopyPoints( Curve, copy, Originals );
            GridAssist.PopulateColumn( uiGrid_db_grid, y.Name, SeriesAssist.GetValues( Curve ), VALUES_DECIMAL_PLACES );
            UpdateUiByRefreshingChart();
            UpdateUiByPanelStateInfo( PanelStateInformation.ValuesRestored );
            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        private void OnRefreshClick( object sender, EventArgs e )
        {
            UpdateUiByRefreshingChart();
            log.Info( MethodBase.GetCurrentMethod().Name + "()" );
        }

        private void OnSaveClick( object sender, EventArgs e )
        {
            IList<double> values = Lists.Cast<object, double>( GridAssist.GetColumnValues( uiGrid_db_grid, y.HeaderText ) );

            if ( !SeriesAssist.IsChartAcceptable( values ) ) {
                log.Info( MethodBase.GetCurrentMethod().Name + '(' + PanelStateInformation.OperationRevoked + ')' );
                UpdateUiByPanelStateInfo( PanelStateInformation.OperationRevoked );
                AppMessages.GridPreviewer.ErrorOfInvalidCurvePoints();
                return;
            }

            SeriesAssist.OverrideValues( Curve, values );
            UpdateUiByPanelStateInfo( PanelStateInformation.ChangesSaved );
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + PanelStateInformation.ChangesSaved + ')' );
        }

        #endregion
    }
}
