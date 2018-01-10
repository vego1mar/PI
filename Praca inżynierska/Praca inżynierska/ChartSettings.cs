using PI.src.helpers;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    public partial class ChartSettings : Form
    {
        internal ChartSettingsPool Settings { get; private set; }
        private PreviousValue Previous { get; set; }
        private bool IsFormInitialized { get; set; }

        internal enum ApplyToCurve
        {
            Generated = 0,
            Pattern = 1,
            Average = 2,
            All = 3
        }

        private enum ChartSettingsTabs
        {
            Chart = 0,
            ChartArea = 1,
            Series = 2
        }

        private enum ChartAreaAxis
        {
            X = 0,
            Y = 1
        }

        private enum ChartAreaGrid
        {
            MajorGrid = 0,
            MinorGrid = 1
        }

        private class PreviousValue
        {
            public ChartAreaAxis AxisSelected { get; set; }
            public ChartAreaGrid GridSelected { get; set; }
            public ApplyToCurve ApplyMode { get; set; }
        }

        internal ChartSettings( ChartSettingsPool settings )
        {
            InitializeComponent();
            InitializeProperties( settings );
            LocalizeWindow();
            UpdateUiByComposingControls();
            UpdateUiByDefaultSettings();
            UpdateUiByFormInitializeSettings();
        }

        private void InitializeProperties( ChartSettingsPool settings )
        {
            Settings = settings;

            Previous = new PreviousValue() {
                AxisSelected = ChartAreaAxis.X,
                GridSelected = ChartAreaGrid.MajorGrid,
                ApplyMode = ApplyToCurve.Average
            };

            IsFormInitialized = false;
        }

        private void UpdateUiByComposingControls()
        {
            DefineChartTabPageComboBoxes();
            DefineChartAreaTabPageComboBoxes();
            DefineSeriesTabPageComboBoxes();
        }

        private void UpdateUiByDefaultSettings()
        {
            UiControls.SetSelectedIndexSafe( uiTop_ApplyTo_ComBx, (int) ApplyToCurve.Pattern );
            SetChartTabPageDefaults();
            SetChartAreaTabPageDefaults();
            SetSeriesTabPageDefaults();
        }

        private void SetChartTabPageDefaults()
        {
            UiControls.SetSelectedIndexSafe( uiCtrChart_Aa_ComBx, (int) Settings.Common.AntiAliasing );
            UiControls.SetSelectedIndexSafe( uiCtrChart_SupEx_ComBx, Convert.ToInt32( Settings.Common.SuppressExceptions ) );
            UiControls.SetSelectedIndexSafe( uiCtrChart_BkCol_ComBx, uiCtrChart_BkCol_ComBx.Items.IndexOf( Settings.Common.BackColor.Name ) );
        }

        private void SetChartAreaTabPageDefaults()
        {
            UiControls.SetSelectedIndexSafe( uiCtrArea_3d_ComBx, Convert.ToInt32( Settings.Areas.Common.Area3dStyle ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_BkCol_ComBx, uiCtrArea_BkCol_ComBx.Items.IndexOf( Settings.Areas.Common.BackColor.Name ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_Axis_ComBx, (int) ChartAreaAxis.X );
            UiControls.SetSelectedIndexSafe( uiCtrArea_Grid_ComBx, (int) ChartAreaGrid.MajorGrid );
            UiControls.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.X.MajorGrid.Enabled ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.X.MajorGrid.LineColor.Name ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, (int) Settings.Areas.X.MajorGrid.LineDashStyle );
            UiControls.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.X.MajorGrid.LineWidth );
        }

        private void SetSeriesTabPageDefaults()
        {
            UiControls.SetSelectedIndexSafe( uiCtrSrs_Color_ComBx, uiCtrSrs_Color_ComBx.Items.IndexOf( Settings.Series.Average.Color.Name ) );
            UiControls.SetValue( uiCtrSrs_BorWth_Num, Settings.Series.Average.BorderWidth );
            UiControls.SetSelectedIndexSafe( uiCtrSrs_BorStyle_ComBx, (int) Settings.Series.Average.BorderDashStyle );
            UiControls.SetSelectedIndexSafe( uiCtrSrs_ChT_ComBx, (int) Settings.Series.Average.ChartType );
        }

        private void UpdateUiByFormInitializeSettings()
        {
            IsFormInitialized = true;
            UiControls.SetSelectedIndexSafe( uiTop_ApplyTo_ComBx, (int) ApplyToCurve.Average );
        }

        private void ChartSettings_Load( object sender, EventArgs e )
        {
            uiBtm_Ok_Btn.Select();
        }

        private void DefineChartAntiAliasingComboBox()
        {
            uiCtrChart_Aa_ComBx.Items.Clear();
            AddAntiAliasingStyles( uiCtrChart_Aa_ComBx );
        }

        private void AddAntiAliasingStyles( ComboBox comboBox )
        {
            comboBox.Items.Add( AntiAliasingStyles.None.ToString() );
            comboBox.Items.Add( AntiAliasingStyles.Text.ToString() );
            comboBox.Items.Add( AntiAliasingStyles.Graphics.ToString() );
            comboBox.Items.Add( AntiAliasingStyles.All.ToString() );
        }

        private void DefineChartSuppressWarningsComboBox()
        {
            uiCtrChart_SupEx_ComBx.Items.Clear();
            AddBooleanValues( uiCtrChart_SupEx_ComBx );
        }

        private void AddBooleanValues( ComboBox comboBox )
        {
            comboBox.Items.Add( Boolean.FalseString );
            comboBox.Items.Add( Boolean.TrueString );
        }

        private void DefineChartBackColorComboBox()
        {
            uiCtrChart_BkCol_ComBx.Items.Clear();
            AddSystemDrawingColors( uiCtrChart_BkCol_ComBx );
        }

        private void DefineChartTabPageComboBoxes()
        {
            AddApplyToCurve( uiTop_ApplyTo_ComBx );
            DefineChartAntiAliasingComboBox();
            DefineChartSuppressWarningsComboBox();
            DefineChartBackColorComboBox();
        }

        private void AddSystemDrawingColors<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( var property in typeof( Color ).GetProperties( System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public ) ) {
                if ( property.PropertyType == typeof( Color ) ) {
                    control.Items.Add( property.Name );
                }
            }
        }

        private void UiTop_ApplyToCurve_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( !IsFormInitialized ) {
                return;
            }

            switch ( (ApplyToCurve) uiTop_ApplyTo_ComBx.SelectedIndex ) {
            case ApplyToCurve.All:
                PerformApplyToCurveAllSwitch();
                break;
            case ApplyToCurve.Generated:
                PerformApplyToCurveNotAllSwitch();
                SaveSeriesSettings( Previous.ApplyMode );
                UpdateUiByGeneratedCurveSettings();
                break;
            case ApplyToCurve.Pattern:
                PerformApplyToCurveNotAllSwitch();
                SaveSeriesSettings( Previous.ApplyMode );
                UpdateUiByPatternCurveSettings();
                break;
            case ApplyToCurve.Average:
                PerformApplyToCurveNotAllSwitch();
                SaveSeriesSettings( Previous.ApplyMode );
                SetSeriesTabPageDefaults();
                break;
            }

            Previous.ApplyMode = (ApplyToCurve) uiTop_ApplyTo_ComBx.SelectedIndex;
        }

        private void PerformApplyToCurveAllSwitch()
        {
            UiControls.TrySelectTab( uiCtr_TbCtrl, (int) ChartSettingsTabs.Chart );
            EnableChartTabPageControls( true );
            EnableChartAreaTabPageControls( true );
            EnableSeriesTabPageControls( false );
        }

        private void PerformApplyToCurveNotAllSwitch()
        {
            UiControls.TrySelectTab( uiCtr_TbCtrl, (int) ChartSettingsTabs.Series );
            EnableChartTabPageControls( false );
            EnableChartAreaTabPageControls( false );
            EnableSeriesTabPageControls( true );
        }

        private void UpdateUiByGeneratedCurveSettings()
        {
            UiControls.SetSelectedIndexSafe( uiCtrSrs_Color_ComBx, uiCtrSrs_Color_ComBx.Items.IndexOf( Settings.Series.Generated.Color.Name ) );
            UiControls.SetValue( uiCtrSrs_BorWth_Num, Settings.Series.Generated.BorderWidth );
            UiControls.SetSelectedIndexSafe( uiCtrSrs_BorStyle_ComBx, (int) Settings.Series.Generated.BorderDashStyle );
            UiControls.SetSelectedIndexSafe( uiCtrSrs_ChT_ComBx, (int) Settings.Series.Generated.ChartType );
        }

        private void UpdateUiByPatternCurveSettings()
        {
            UiControls.SetSelectedIndexSafe( uiCtrSrs_Color_ComBx, uiCtrSrs_Color_ComBx.Items.IndexOf( Settings.Series.Pattern.Color.Name ) );
            UiControls.SetValue( uiCtrSrs_BorWth_Num, Settings.Series.Pattern.BorderWidth );
            UiControls.SetSelectedIndexSafe( uiCtrSrs_BorStyle_ComBx, (int) Settings.Series.Pattern.BorderDashStyle );
            UiControls.SetSelectedIndexSafe( uiCtrSrs_ChT_ComBx, (int) Settings.Series.Pattern.ChartType );
        }

        private void EnableChartTabPageControls( bool value )
        {
            uiCtrChart_Aa_TxtBx.Enabled = value;
            uiCtrChart_Aa_ComBx.Enabled = value;
            uiCtrChart_SupEx_TxtBx.Enabled = value;
            uiCtrChart_SupEx_ComBx.Enabled = value;
            uiCtrChart_BkCol_TxtBx.Enabled = value;
            uiCtrChart_BkCol_ComBx.Enabled = value;
        }

        private void EnableChartAreaTabPageControls( bool value )
        {
            uiCtrArea_ChA_TxtBx.Enabled = value;
            uiCtrArea_3d_TxtBx.Enabled = value;
            uiCtrArea_3d_ComBx.Enabled = value;
            uiCtrArea_BkCol_TxtBx.Enabled = value;
            uiCtrArea_BkCol_ComBx.Enabled = value;
            uiCtrArea_Axes_TxtBx.Enabled = value;
            uiCtrArea_Axis_TxtBx.Enabled = value;
            uiCtrArea_Axis_ComBx.Enabled = value;
            uiCtrArea_Grid_TxtBx.Enabled = value;
            uiCtrArea_Grid_ComBx.Enabled = value;
            uiCtrArea_En_TxtBx.Enabled = value;
            uiCtrArea_En_ComBx.Enabled = value;
            uiCtrArea_LnCol_TxtBx.Enabled = value;
            uiCtrArea_LnCol_ComBx.Enabled = value;
            uiCtrArea_LnStyle_TxtBx.Enabled = value;
            uiCtrArea_LnStyle_ComBx.Enabled = value;
            uiCtrArea_LnWth_TxtBx.Enabled = value;
            uiCtrArea_LnWth_Num.Enabled = value;
        }

        private void EnableSeriesTabPageControls( bool value )
        {
            uiCtrSrs_Color_TxtBx.Enabled = value;
            uiCtrSrs_Color_ComBx.Enabled = value;
            uiCtrSrs_BorWth_TxtBx.Enabled = value;
            uiCtrSrs_BorWth_Num.Enabled = value;
            uiCtrSrs_BorStyle_TxtBx.Enabled = value;
            uiCtrSrs_BorStyle_ComBx.Enabled = value;
            uiCtrSrs_ChT_TxtBx.Enabled = value;
            uiCtrSrs_ChT_ComBx.Enabled = value;
        }

        private void UiBottom_Ok_Click( object sender, EventArgs e )
        {
            SaveAllSettings();
        }

        private void SaveAllSettings()
        {
            Settings.ApplyMode = (ApplyToCurve) UiControls.TryGetSelectedIndex( uiTop_ApplyTo_ComBx );
            SaveChartSettings();
            SaveChartAreaSettings();
            SaveSeriesSettings( Settings.ApplyMode );
        }

        private void SaveChartSettings()
        {
            Settings.Common.AntiAliasing = (AntiAliasingStyles) UiControls.TryGetSelectedIndex( uiCtrChart_Aa_ComBx );
            Settings.Common.SuppressExceptions = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrChart_SupEx_ComBx ) );
            Settings.Common.BackColor = Color.FromName( uiCtrChart_BkCol_ComBx.Items[uiCtrChart_BkCol_ComBx.SelectedIndex].ToString() );
        }

        private void SaveChartAreaSettings()
        {
            Settings.Areas.Common.Area3dStyle = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_3d_ComBx ) );
            Settings.Areas.Common.BackColor = Color.FromName( uiCtrArea_BkCol_ComBx.Items[uiCtrArea_BkCol_ComBx.SelectedIndex].ToString() );
            ChartAreaAxis axis = (ChartAreaAxis) UiControls.TryGetSelectedIndex( uiCtrArea_Axis_ComBx );
            ChartAreaGrid grid = (ChartAreaGrid) UiControls.TryGetSelectedIndex( uiCtrArea_Grid_ComBx );
            SaveAxesSettings( axis, grid );
        }

        private void SaveSeriesSettings( ApplyToCurve applyMode )
        {
            switch ( applyMode ) {
            case ApplyToCurve.Pattern:
                SaveSeriesPatternSettings();
                break;
            case ApplyToCurve.Generated:
                SaveSeriesGeneratedSettings();
                break;
            case ApplyToCurve.Average:
                SaveSeriesAverageSettings();
                break;
            }
        }

        private void SaveSeriesPatternSettings()
        {
            Settings.Series.Pattern.Color = Color.FromName( uiCtrSrs_Color_ComBx.Items[uiCtrSrs_Color_ComBx.SelectedIndex].ToString() );
            Settings.Series.Pattern.BorderWidth = UiControls.GetValue<int>( uiCtrSrs_BorWth_Num );
            Settings.Series.Pattern.BorderDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrSrs_BorStyle_ComBx );
            Settings.Series.Pattern.ChartType = (SeriesChartType) UiControls.TryGetSelectedIndex( uiCtrSrs_ChT_ComBx );
        }

        private void SaveSeriesGeneratedSettings()
        {
            Settings.Series.Generated.Color = Color.FromName( uiCtrSrs_Color_ComBx.Items[uiCtrSrs_Color_ComBx.SelectedIndex].ToString() );
            Settings.Series.Generated.BorderWidth = UiControls.GetValue<int>( uiCtrSrs_BorWth_Num );
            Settings.Series.Generated.BorderDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrSrs_BorStyle_ComBx );
            Settings.Series.Generated.ChartType = (SeriesChartType) UiControls.TryGetSelectedIndex( uiCtrSrs_ChT_ComBx );
        }

        private void SaveSeriesAverageSettings()
        {
            Settings.Series.Average.Color = Color.FromName( uiCtrSrs_Color_ComBx.Items[uiCtrSrs_Color_ComBx.SelectedIndex].ToString() );
            Settings.Series.Average.BorderWidth = UiControls.GetValue<int>( uiCtrSrs_BorWth_Num );
            Settings.Series.Average.BorderDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrSrs_BorStyle_ComBx );
            Settings.Series.Average.ChartType = (SeriesChartType) UiControls.TryGetSelectedIndex( uiCtrSrs_ChT_ComBx );
        }

        private void DefineChartAreaTabPageComboBoxes()
        {
            DefineChartArea3dStyleComboBox();
            DefineChartAreaBackColorComboBox();
            DefineChartAreaEnableComboBox();
            DefineChartAreaLineColorComboBox();
            DefineChartAreaLineDashStyleComboBox();
            DefineChartAreaAxisComboBox();
            DefineChartAreaGridComboBox();
        }

        private void DefineChartArea3dStyleComboBox()
        {
            uiCtrArea_3d_ComBx.Items.Clear();
            AddBooleanValues( uiCtrArea_3d_ComBx );
        }

        private void DefineChartAreaBackColorComboBox()
        {
            uiCtrArea_BkCol_ComBx.Items.Clear();
            AddSystemDrawingColors( uiCtrArea_BkCol_ComBx );
        }

        private void DefineChartAreaEnableComboBox()
        {
            uiCtrArea_En_ComBx.Items.Clear();
            AddBooleanValues( uiCtrArea_En_ComBx );
        }

        private void DefineChartAreaLineColorComboBox()
        {
            uiCtrArea_LnCol_ComBx.Items.Clear();
            AddSystemDrawingColors( uiCtrArea_LnCol_ComBx );
        }

        private void DefineChartAreaLineDashStyleComboBox()
        {
            uiCtrArea_LnStyle_ComBx.Items.Clear();
            AddChartDashStyles( uiCtrArea_LnStyle_ComBx );
        }

        private void AddChartDashStyles<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( string name in Enum.GetNames( typeof( ChartDashStyle ) ) ) {
                control.Items.Add( name );
            }
        }

        private void DefineChartAreaAxisComboBox()
        {
            uiCtrArea_Axis_ComBx.Items.Clear();
            uiCtrArea_Axis_ComBx.Items.Add( ChartAreaAxis.X.ToString() );
            uiCtrArea_Axis_ComBx.Items.Add( ChartAreaAxis.Y.ToString() );
        }

        private void DefineChartAreaGridComboBox()
        {
            uiCtrArea_Grid_ComBx.Items.Clear();
            uiCtrArea_Grid_ComBx.Items.Add( ChartAreaGrid.MajorGrid.ToString() );
            uiCtrArea_Grid_ComBx.Items.Add( ChartAreaGrid.MinorGrid.ToString() );
        }

        private void PerformAxisGridSwitch()
        {
            SaveAxesSettings( Previous.AxisSelected, Previous.GridSelected );
            Previous.AxisSelected = (ChartAreaAxis) UiControls.TryGetSelectedIndex( uiCtrArea_Axis_ComBx );
            Previous.GridSelected = (ChartAreaGrid) UiControls.TryGetSelectedIndex( uiCtrArea_Grid_ComBx );
            UpdateUiByAxesSettings();
        }

        private void UiCenterChartArea_Axis_SelectedIndexChanged( object sender, EventArgs e )
        {
            CheckFormInitialization();
        }

        private void CheckFormInitialization()
        {
            if ( !IsFormInitialized ) {
                return;
            }

            PerformAxisGridSwitch();
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
            case ChartAreaGrid.MajorGrid:
                SaveAxisXMajorGridSettings();
                break;
            case ChartAreaGrid.MinorGrid:
                SaveAxisXMinorGridSettings();
                break;
            }
        }

        private void SaveAxisYSettings( ChartAreaGrid grid )
        {
            switch ( grid ) {
            case ChartAreaGrid.MajorGrid:
                SaveAxisYMajorGridSettings();
                break;
            case ChartAreaGrid.MinorGrid:
                SaveAxisYMinorGridSettings();
                break;
            }
        }

        private void SaveAxisYMajorGridSettings()
        {
            Settings.Areas.Y.MajorGrid.Enabled = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_En_ComBx ) );
            Settings.Areas.Y.MajorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.Y.MajorGrid.LineDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.Y.MajorGrid.LineWidth = UiControls.GetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void SaveAxisYMinorGridSettings()
        {
            Settings.Areas.Y.MinorGrid.Enabled = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_En_ComBx ) );
            Settings.Areas.Y.MinorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.Y.MinorGrid.LineDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.Y.MinorGrid.LineWidth = UiControls.GetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void SaveAxisXMajorGridSettings()
        {
            Settings.Areas.X.MajorGrid.Enabled = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_En_ComBx ) );
            Settings.Areas.X.MajorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.X.MajorGrid.LineDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.X.MajorGrid.LineWidth = UiControls.GetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void SaveAxisXMinorGridSettings()
        {
            Settings.Areas.X.MinorGrid.Enabled = Convert.ToBoolean( UiControls.TryGetSelectedIndex( uiCtrArea_En_ComBx ) );
            Settings.Areas.X.MinorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.X.MinorGrid.LineDashStyle = (ChartDashStyle) UiControls.TryGetSelectedIndex( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.X.MinorGrid.LineWidth = UiControls.GetValue<int>( uiCtrArea_LnWth_Num );
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
            case ChartAreaGrid.MajorGrid:
                SetAxisXMajorGridSettings();
                break;
            case ChartAreaGrid.MinorGrid:
                SetAxisXMinorGridSettings();
                break;
            }
        }

        private void SetAxisYSettings( ChartAreaGrid grid )
        {
            switch ( grid ) {
            case ChartAreaGrid.MajorGrid:
                SetAxisYMajorGridSettings();
                break;
            case ChartAreaGrid.MinorGrid:
                SetAxisYMinorGridSettings();
                break;
            }
        }

        private void SetAxisXMajorGridSettings()
        {
            UiControls.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.X.MajorGrid.Enabled ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.X.MajorGrid.LineColor.Name ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.X.MajorGrid.LineDashStyle.ToString() ) );
            UiControls.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.X.MajorGrid.LineWidth );
        }

        private void SetAxisXMinorGridSettings()
        {
            UiControls.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.X.MinorGrid.Enabled ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.X.MinorGrid.LineColor.Name ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.X.MinorGrid.LineDashStyle.ToString() ) );
            UiControls.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.X.MinorGrid.LineWidth );
        }

        private void SetAxisYMajorGridSettings()
        {
            UiControls.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.Y.MajorGrid.Enabled ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.Y.MajorGrid.LineColor.Name ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.Y.MajorGrid.LineDashStyle.ToString() ) );
            UiControls.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.Y.MajorGrid.LineWidth );
        }

        private void SetAxisYMinorGridSettings()
        {
            UiControls.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.Y.MinorGrid.Enabled ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.Y.MinorGrid.LineColor.Name ) );
            UiControls.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.Y.MinorGrid.LineDashStyle.ToString() ) );
            UiControls.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.Y.MinorGrid.LineWidth );
        }

        private void UiCenterArea_Grid_SelectedIndexChanged( object sender, EventArgs e )
        {
            CheckFormInitialization();
        }

        private void DefineSeriesTabPageComboBoxes()
        {
            DefineSeriesColorComboBox();
            DefineSeriesBorderDashStyleComboBox();
            DefineSeriesChartTypeComboBox();
        }

        private void DefineSeriesColorComboBox()
        {
            uiCtrSrs_Color_ComBx.Items.Clear();
            AddSystemDrawingColors( uiCtrSrs_Color_ComBx );
        }

        private void DefineSeriesBorderDashStyleComboBox()
        {
            uiCtrSrs_BorStyle_ComBx.Items.Clear();
            AddChartDashStyles( uiCtrSrs_BorStyle_ComBx );
        }

        private void DefineSeriesChartTypeComboBox()
        {
            uiCtrSrs_ChT_ComBx.Items.Clear();
            AddChartTypes( uiCtrSrs_ChT_ComBx );

        }

        private void AddChartTypes<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( string name in Enum.GetNames( typeof( SeriesChartType ) ) ) {
                control.Items.Add( name );
            }
        }

        private void ChartSettings_FormClosing( object sender, FormClosingEventArgs e )
        {
            Previous = null;
            Dispose();
        }

        private void LocalizeWindow()
        {
            LocalizeForm();
            LocalizeUi();
        }

        private void LocalizeForm()
        {
            Text = Translator.GetInstance().Strings.ChartSettings.Form.Text.GetString();
        }

        private void LocalizeUi()
        {
            LocalizeGenerals();
            LocalizeTabChart();
            LocalizeTabChartArea();
            LocalizeTabSeries();
        }

        private void LocalizeGenerals()
        {
            uiTop_ApplyTo_TxtBx.Text = Translator.GetInstance().Strings.ChartSettings.Ui.General.ApplyTo.GetString();
            uiBtm_Ok_Btn.Text = Translator.GetInstance().Strings.ChartSettings.Ui.General.Ok.GetString();
        }

        private void LocalizeTabChart()
        {
            uiCtr_Chart_TbPg.Text = Translator.GetInstance().Strings.ChartSettings.Ui.Tabs.Chart.Chart.GetString();
        }

        private void LocalizeTabChartArea()
        {
            uiCtr_Area_TbPg.Text = Translator.GetInstance().Strings.ChartSettings.Ui.Tabs.ChartArea.Area.GetString();
            uiCtrArea_ChA_TxtBx.Text = Translator.GetInstance().Strings.ChartSettings.Ui.Tabs.ChartArea.ChA.GetString();
            uiCtrArea_Axes_TxtBx.Text = Translator.GetInstance().Strings.ChartSettings.Ui.Tabs.ChartArea.Axes.GetString();
        }

        private void LocalizeTabSeries()
        {
            uiCtr_Srs_TbPg.Text = Translator.GetInstance().Strings.ChartSettings.Ui.Tabs.Series.Srs.GetString();
        }

        public void AddApplyToCurve<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.ApplyToCurve ) {
                control.Items.Add( item.GetString() );
            }
        }

    }
}
