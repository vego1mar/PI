using PI.src.enumerators;
using PI.src.helpers;
using PI.src.localization.enums;
using PI.src.localization.windows;
using PI.src.settings;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    public partial class ChartSettings : Form
    {
        internal MainChartSettings Settings { get; private set; }
        private PreviousValue Previous { get; set; }
        private bool IsFormInitialized { get; set; }

        private enum ChartSettingsTabs
        {
            Chart = 0,
            ChartArea = 1,
            Series = 2
        }

        private class PreviousValue
        {
            public ChartAreaAxis AxisSelected { get; set; } = ChartAreaAxis.X;
            public ChartAreaGrid GridSelected { get; set; } = ChartAreaGrid.Major;
            public CurveApply ApplyMode { get; set; } = CurveApply.Average;
        }

        internal ChartSettings( MainChartSettings settings )
        {
            InitializeComponent();
            Settings = settings;
            Previous = new PreviousValue();
            IsFormInitialized = false;
            LocalizeWindow();
            UpdateUiBySettings();
            IsFormInitialized = true;
            UiControls.TrySetSelectedIndex( uiTop_ApplyTo_ComBx, (int) CurveApply.Average );
        }

        private void UpdateUiBySettings()
        {
            // Common
            UiControls.TrySetSelectedIndex( uiTop_ApplyTo_ComBx, (int) CurveApply.Ideal );

            // Tab: Chart
            UiControls.TrySetSelectedIndex( uiCtrChart_Aa_ComBx, (int) Settings.Common.AntiAliasing );
            UiControls.TrySetSelectedIndex( uiCtrChart_SupEx_ComBx, Convert.ToInt32( Settings.Common.SuppressExceptions ) );
            UiControls.TrySetSelectedIndex( uiCtrChart_BkCol_ComBx, uiCtrChart_BkCol_ComBx.Items.IndexOf( Settings.Common.BackColor.Name ) );

            // Tab: Chart area
            UiControls.TrySetSelectedIndex( uiCtrArea_3d_ComBx, Convert.ToInt32( Settings.Areas.Common.Area3dStyle ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_BkCol_ComBx, uiCtrArea_BkCol_ComBx.Items.IndexOf( Settings.Areas.Common.BackColor.Name ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_Axis_ComBx, (int) ChartAreaAxis.X );
            UiControls.TrySetSelectedIndex( uiCtrArea_Grid_ComBx, (int) ChartAreaGrid.Major );
            UiControls.TrySetSelectedIndex( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.X.MajorGrid.Enabled ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.X.MajorGrid.LineColor.Name ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnStyle_ComBx, (int) Settings.Areas.X.MajorGrid.LineDashStyle );
            UiControls.TrySetValue( uiCtrArea_LnWth_Num, Settings.Areas.X.MajorGrid.LineWidth );

            // Tab: Series
            UpdateUiByCurveSwitch( CurveApply.Average );
        }

        private void UpdateUiByCurveSwitch( CurveApply curve )
        {
            switch ( curve ) {
            case CurveApply.Ideal:
                UiControls.TrySetSelectedIndex( uiCtrSrs_Color_ComBx, uiCtrSrs_Color_ComBx.Items.IndexOf( Settings.Series.Ideal.Color.Name ) );
                UiControls.TrySetValue( uiCtrSrs_BorWth_Num, Settings.Series.Ideal.BorderWidth );
                UiControls.TrySetSelectedIndex( uiCtrSrs_BorStyle_ComBx, (int) Settings.Series.Ideal.BorderDashStyle );
                UiControls.TrySetSelectedIndex( uiCtrSrs_ChT_ComBx, (int) Settings.Series.Ideal.ChartType );
                break;
            case CurveApply.Modified:
                UiControls.TrySetSelectedIndex( uiCtrSrs_Color_ComBx, uiCtrSrs_Color_ComBx.Items.IndexOf( Settings.Series.Modified.Color.Name ) );
                UiControls.TrySetValue( uiCtrSrs_BorWth_Num, Settings.Series.Modified.BorderWidth );
                UiControls.TrySetSelectedIndex( uiCtrSrs_BorStyle_ComBx, (int) Settings.Series.Modified.BorderDashStyle );
                UiControls.TrySetSelectedIndex( uiCtrSrs_ChT_ComBx, (int) Settings.Series.Modified.ChartType );
                break;
            case CurveApply.Average:
                UiControls.TrySetSelectedIndex( uiCtrSrs_Color_ComBx, uiCtrSrs_Color_ComBx.Items.IndexOf( Settings.Series.Average.Color.Name ) );
                UiControls.TrySetValue( uiCtrSrs_BorWth_Num, Settings.Series.Average.BorderWidth );
                UiControls.TrySetSelectedIndex( uiCtrSrs_BorStyle_ComBx, (int) Settings.Series.Average.BorderDashStyle );
                UiControls.TrySetSelectedIndex( uiCtrSrs_ChT_ComBx, (int) Settings.Series.Average.ChartType );
                break;
            }
        }

        private void UpdateUiByEnablingControls( ChartSettingsTabs tab, bool isEnabled )
        {
            switch ( tab ) {
            case ChartSettingsTabs.Chart:
                uiCtrChart_Aa_TxtBx.Enabled = isEnabled;
                uiCtrChart_Aa_ComBx.Enabled = isEnabled;
                uiCtrChart_SupEx_TxtBx.Enabled = isEnabled;
                uiCtrChart_SupEx_ComBx.Enabled = isEnabled;
                uiCtrChart_BkCol_TxtBx.Enabled = isEnabled;
                uiCtrChart_BkCol_ComBx.Enabled = isEnabled;
                break;
            case ChartSettingsTabs.ChartArea:
                uiCtrArea_ChA_TxtBx.Enabled = isEnabled;
                uiCtrArea_3d_TxtBx.Enabled = isEnabled;
                uiCtrArea_3d_ComBx.Enabled = isEnabled;
                uiCtrArea_BkCol_TxtBx.Enabled = isEnabled;
                uiCtrArea_BkCol_ComBx.Enabled = isEnabled;
                uiCtrArea_Axes_TxtBx.Enabled = isEnabled;
                uiCtrArea_Axis_TxtBx.Enabled = isEnabled;
                uiCtrArea_Axis_ComBx.Enabled = isEnabled;
                uiCtrArea_Grid_TxtBx.Enabled = isEnabled;
                uiCtrArea_Grid_ComBx.Enabled = isEnabled;
                uiCtrArea_En_TxtBx.Enabled = isEnabled;
                uiCtrArea_En_ComBx.Enabled = isEnabled;
                uiCtrArea_LnCol_TxtBx.Enabled = isEnabled;
                uiCtrArea_LnCol_ComBx.Enabled = isEnabled;
                uiCtrArea_LnStyle_TxtBx.Enabled = isEnabled;
                uiCtrArea_LnStyle_ComBx.Enabled = isEnabled;
                uiCtrArea_LnWth_TxtBx.Enabled = isEnabled;
                uiCtrArea_LnWth_Num.Enabled = isEnabled;
                break;
            case ChartSettingsTabs.Series:
                uiCtrSrs_Color_TxtBx.Enabled = isEnabled;
                uiCtrSrs_Color_ComBx.Enabled = isEnabled;
                uiCtrSrs_BorWth_TxtBx.Enabled = isEnabled;
                uiCtrSrs_BorWth_Num.Enabled = isEnabled;
                uiCtrSrs_BorStyle_TxtBx.Enabled = isEnabled;
                uiCtrSrs_BorStyle_ComBx.Enabled = isEnabled;
                uiCtrSrs_ChT_TxtBx.Enabled = isEnabled;
                uiCtrSrs_ChT_ComBx.Enabled = isEnabled;
                break;
            }
        }

        private void SaveAllSettings()
        {
            // Common
            Settings.ApplyMode = (CurveApply) UiControls.TryGetSelectedIndex( uiTop_ApplyTo_ComBx );

            // Tab: Chart
            Settings.Common.AntiAliasing = (AntiAliasingStyles) UiControls.TryGetSelectedIndex( uiCtrChart_Aa_ComBx );
            Settings.Common.SuppressExceptions = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrChart_SupEx_ComBx ) );
            Settings.Common.BackColor = Color.FromName( uiCtrChart_BkCol_ComBx.Items[uiCtrChart_BkCol_ComBx.SelectedIndex].ToString() );

            // Tab: Chart area
            Settings.Areas.Common.Area3dStyle = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_3d_ComBx ) );
            Settings.Areas.Common.BackColor = Color.FromName( uiCtrArea_BkCol_ComBx.Items[uiCtrArea_BkCol_ComBx.SelectedIndex].ToString() );
            ChartAreaAxis axis = (ChartAreaAxis) UiControls.TryGetSelectedIndex( uiCtrArea_Axis_ComBx );
            ChartAreaGrid grid = (ChartAreaGrid) UiControls.TryGetSelectedIndex( uiCtrArea_Grid_ComBx );
            SaveAxesSettings( axis, grid );

            // Tab: Series
            SaveCurveSettings( Settings.ApplyMode );
        }

        private void SaveCurveSettings( CurveApply curve )
        {
            switch ( curve ) {
            case CurveApply.Ideal:
                Settings.Series.Ideal.Color = Color.FromName( uiCtrSrs_Color_ComBx.Items[uiCtrSrs_Color_ComBx.SelectedIndex].ToString() );
                Settings.Series.Ideal.BorderWidth = UiControls.TryGetValue<int>( uiCtrSrs_BorWth_Num );
                Settings.Series.Ideal.BorderDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrSrs_BorStyle_ComBx );
                Settings.Series.Ideal.ChartType = (SeriesChartType) UiControls.TryGetSelectedIndex( uiCtrSrs_ChT_ComBx );
                break;
            case CurveApply.Modified:
                Settings.Series.Modified.Color = Color.FromName( uiCtrSrs_Color_ComBx.Items[uiCtrSrs_Color_ComBx.SelectedIndex].ToString() );
                Settings.Series.Modified.BorderWidth = UiControls.TryGetValue<int>( uiCtrSrs_BorWth_Num );
                Settings.Series.Modified.BorderDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrSrs_BorStyle_ComBx );
                Settings.Series.Modified.ChartType = (SeriesChartType) UiControls.TryGetSelectedIndex( uiCtrSrs_ChT_ComBx );
                break;
            case CurveApply.Average:
                Settings.Series.Average.Color = Color.FromName( uiCtrSrs_Color_ComBx.Items[uiCtrSrs_Color_ComBx.SelectedIndex].ToString() );
                Settings.Series.Average.BorderWidth = UiControls.TryGetValue<int>( uiCtrSrs_BorWth_Num );
                Settings.Series.Average.BorderDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrSrs_BorStyle_ComBx );
                Settings.Series.Average.ChartType = (SeriesChartType) UiControls.TryGetSelectedIndex( uiCtrSrs_ChT_ComBx );
                break;
            }
        }

        private void PerformAxisAndGridSwitch()
        {
            SaveAxesSettings( Previous.AxisSelected, Previous.GridSelected );
            Previous.AxisSelected = (ChartAreaAxis) UiControls.TryGetSelectedIndex( uiCtrArea_Axis_ComBx );
            Previous.GridSelected = (ChartAreaGrid) UiControls.TryGetSelectedIndex( uiCtrArea_Grid_ComBx );
            UpdateUiByAxesSettings();
        }

        private void SaveAxesSettings( ChartAreaAxis axis, ChartAreaGrid grid )
        {
            switch ( axis ) {
            case ChartAreaAxis.X:
                SaveAxisXSettings( grid );
                break;
            case ChartAreaAxis.Y:
                SaveAxisYSettings( grid );
                break;
            }
        }

        private void SaveAxisXSettings( ChartAreaGrid grid )
        {
            switch ( grid ) {
            case ChartAreaGrid.Major:
                SaveAxisXMajorGridSettings();
                break;
            case ChartAreaGrid.Minor:
                SaveAxisXMinorGridSettings();
                break;
            }
        }

        private void SaveAxisYSettings( ChartAreaGrid grid )
        {
            switch ( grid ) {
            case ChartAreaGrid.Major:
                SaveAxisYMajorGridSettings();
                break;
            case ChartAreaGrid.Minor:
                SaveAxisYMinorGridSettings();
                break;
            }
        }

        private void SaveAxisYMajorGridSettings()
        {
            Settings.Areas.Y.MajorGrid.Enabled = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_En_ComBx ) );
            Settings.Areas.Y.MajorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.Y.MajorGrid.LineDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.Y.MajorGrid.LineWidth = UiControls.TryGetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void SaveAxisYMinorGridSettings()
        {
            Settings.Areas.Y.MinorGrid.Enabled = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_En_ComBx ) );
            Settings.Areas.Y.MinorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.Y.MinorGrid.LineDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.Y.MinorGrid.LineWidth = UiControls.TryGetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void SaveAxisXMajorGridSettings()
        {
            Settings.Areas.X.MajorGrid.Enabled = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_En_ComBx ) );
            Settings.Areas.X.MajorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.X.MajorGrid.LineDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.X.MajorGrid.LineWidth = UiControls.TryGetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void SaveAxisXMinorGridSettings()
        {
            Settings.Areas.X.MinorGrid.Enabled = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_En_ComBx ) );
            Settings.Areas.X.MinorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.X.MinorGrid.LineDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.X.MinorGrid.LineWidth = UiControls.TryGetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void UpdateUiByAxesSettings()
        {
            ChartAreaAxis axis = (ChartAreaAxis) UiControls.TryGetSelectedIndex( uiCtrArea_Axis_ComBx );
            ChartAreaGrid grid = (ChartAreaGrid) UiControls.TryGetSelectedIndex( uiCtrArea_Grid_ComBx );
            SetAxesSettings( axis, grid );
        }

        private void SetAxesSettings( ChartAreaAxis axis, ChartAreaGrid grid )
        {
            switch ( axis ) {
            case ChartAreaAxis.X:
                SetAxisXSettings( grid );
                break;
            case ChartAreaAxis.Y:
                SetAxisYSettings( grid );
                break;
            }
        }

        private void SetAxisXSettings( ChartAreaGrid grid )
        {
            switch ( grid ) {
            case ChartAreaGrid.Major:
                SetAxisXMajorGridSettings();
                break;
            case ChartAreaGrid.Minor:
                SetAxisXMinorGridSettings();
                break;
            }
        }

        private void SetAxisYSettings( ChartAreaGrid grid )
        {
            switch ( grid ) {
            case ChartAreaGrid.Major:
                SetAxisYMajorGridSettings();
                break;
            case ChartAreaGrid.Minor:
                SetAxisYMinorGridSettings();
                break;
            }
        }

        private void SetAxisXMajorGridSettings()
        {
            UiControls.TrySetSelectedIndex( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.X.MajorGrid.Enabled ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.X.MajorGrid.LineColor.Name ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.X.MajorGrid.LineDashStyle.ToString() ) );
            UiControls.TrySetValue( uiCtrArea_LnWth_Num, Settings.Areas.X.MajorGrid.LineWidth );
        }

        private void SetAxisXMinorGridSettings()
        {
            UiControls.TrySetSelectedIndex( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.X.MinorGrid.Enabled ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.X.MinorGrid.LineColor.Name ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.X.MinorGrid.LineDashStyle.ToString() ) );
            UiControls.TrySetValue( uiCtrArea_LnWth_Num, Settings.Areas.X.MinorGrid.LineWidth );
        }

        private void SetAxisYMajorGridSettings()
        {
            UiControls.TrySetSelectedIndex( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.Y.MajorGrid.Enabled ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.Y.MajorGrid.LineColor.Name ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.Y.MajorGrid.LineDashStyle.ToString() ) );
            UiControls.TrySetValue( uiCtrArea_LnWth_Num, Settings.Areas.Y.MajorGrid.LineWidth );
        }

        private void SetAxisYMinorGridSettings()
        {
            UiControls.TrySetSelectedIndex( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.Y.MinorGrid.Enabled ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.Y.MinorGrid.LineColor.Name ) );
            UiControls.TrySetSelectedIndex( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.Y.MinorGrid.LineDashStyle.ToString() ) );
            UiControls.TrySetValue( uiCtrArea_LnWth_Num, Settings.Areas.Y.MinorGrid.LineWidth );
        }

        private void LocalizeWindow()
        {
            ChartSettingsStrings names = new ChartSettingsStrings();
            Text = names.Form.Text.GetString();

            // Common
            uiTop_ApplyTo_TxtBx.Text = names.Ui.GeneralApplyTo.GetString();
            uiBtm_Ok_Btn.Text = names.Ui.GeneralOk.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.CurveApply, uiTop_ApplyTo_ComBx );

            // Tab: Chart
            uiCtr_Chart_TbPg.Text = names.Ui.ChartTitle.GetString();
            EnumsLocalizer.Populate( CSharpEnumerable.AntiAliasingStyles, uiCtrChart_Aa_ComBx );
            EnumsLocalizer.Localize( LocalizableEnumerator.Boolean, uiCtrChart_SupEx_ComBx );
            EnumsLocalizer.Populate( CSharpEnumerable.Color, uiCtrChart_BkCol_ComBx );

            // Tab: Chart area
            uiCtr_Area_TbPg.Text = names.Ui.ChartAreaTitle.GetString();
            uiCtrArea_ChA_TxtBx.Text = names.Ui.ChartAreaText.GetString();
            uiCtrArea_Axes_TxtBx.Text = names.Ui.ChartAreaAxes.GetString();
            EnumsLocalizer.Localize( LocalizableEnumerator.Boolean, uiCtrArea_3d_ComBx );
            EnumsLocalizer.Populate( CSharpEnumerable.Color, uiCtrArea_BkCol_ComBx );
            EnumsLocalizer.Localize( LocalizableEnumerator.Boolean, uiCtrArea_En_ComBx );
            EnumsLocalizer.Populate( CSharpEnumerable.Color, uiCtrArea_LnCol_ComBx );
            EnumsLocalizer.Populate( CSharpEnumerable.ChartDashStyle, uiCtrArea_LnStyle_ComBx );
            EnumsLocalizer.Populate( CSharpEnumerable.ChartAreaAxis, uiCtrArea_Axis_ComBx );
            EnumsLocalizer.Populate( CSharpEnumerable.ChartAreaGrid, uiCtrArea_Grid_ComBx );

            // Tab: Series
            uiCtr_Srs_TbPg.Text = names.Ui.SeriesTitle.GetString();
            EnumsLocalizer.Populate( CSharpEnumerable.Color, uiCtrSrs_Color_ComBx );
            EnumsLocalizer.Populate( CSharpEnumerable.ChartDashStyle, uiCtrSrs_BorStyle_ComBx );
            EnumsLocalizer.Populate( CSharpEnumerable.SeriesChartType, uiCtrSrs_ChT_ComBx );
        }

        #region Event handlers

        private void OnGridSelection( object sender, EventArgs e )
        {
            if ( IsFormInitialized ) {
                PerformAxisAndGridSwitch();
            }
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            Settings = null;
            Previous = null;
            IsFormInitialized = false;
            Dispose();
        }

        private void OnAxisSelection( object sender, EventArgs e )
        {
            if ( IsFormInitialized ) {
                PerformAxisAndGridSwitch();
            }
        }

        private void OnLoad( object sender, EventArgs e )
        {
            uiBtm_Ok_Btn.Select();
        }

        private void OnCurveSelection( object sender, EventArgs e )
        {
            if ( !IsFormInitialized ) {
                return;
            }

            CurveApply curve = (CurveApply) UiControls.TryGetSelectedIndex( uiTop_ApplyTo_ComBx );

            if ( curve == CurveApply.All ) {
                UiControls.TrySelectTab( uiCtr_TbCtrl, (int) ChartSettingsTabs.Chart );
                UpdateUiByEnablingControls( ChartSettingsTabs.Chart, true );
                UpdateUiByEnablingControls( ChartSettingsTabs.ChartArea, true );
                UpdateUiByEnablingControls( ChartSettingsTabs.Series, false );
                return;
            }

            UiControls.TrySelectTab( uiCtr_TbCtrl, (int) ChartSettingsTabs.Series );
            UpdateUiByEnablingControls( ChartSettingsTabs.Chart, false );
            UpdateUiByEnablingControls( ChartSettingsTabs.ChartArea, false );
            UpdateUiByEnablingControls( ChartSettingsTabs.Series, true );
            SaveCurveSettings( Previous.ApplyMode );
            UpdateUiByCurveSwitch( curve );

            Previous.ApplyMode = (CurveApply) uiTop_ApplyTo_ComBx.SelectedIndex;
        }

        private void OnOkClick( object sender, EventArgs e )
        {
            SaveAllSettings();
        }

        #endregion
    }
}
