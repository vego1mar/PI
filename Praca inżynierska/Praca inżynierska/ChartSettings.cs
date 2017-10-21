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
            WinFormsHelper.SetSelectedIndexSafe( uiTop_ApplyTo_ComBx, (int) ApplyToCurve.Pattern );
            SetChartTabPageDefaults();
            SetChartAreaTabPageDefaults();
            SetSeriesTabPageDefaults();
        }

        private void SetChartTabPageDefaults()
        {
            WinFormsHelper.SetSelectedIndexSafe( uiCtrChart_Aa_ComBx, (int) Settings.Common.AntiAliasing );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrChart_SupEx_ComBx, Convert.ToInt32( Settings.Common.SuppressExceptions ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrChart_BkCol_ComBx, uiCtrChart_BkCol_ComBx.Items.IndexOf( Settings.Common.BackColor.Name ) );
        }

        private void SetChartAreaTabPageDefaults()
        {
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_3d_ComBx, Convert.ToInt32( Settings.Areas.Common.Area3dStyle ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_BkCol_ComBx, uiCtrArea_BkCol_ComBx.Items.IndexOf( Settings.Areas.Common.BackColor.Name ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_Axis_ComBx, (int) ChartAreaAxis.X );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_Grid_ComBx, (int) ChartAreaGrid.MajorGrid );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.X.MajorGrid.Enabled ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.X.MajorGrid.LineColor.Name ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, (int) Settings.Areas.X.MajorGrid.LineDashStyle );
            WinFormsHelper.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.X.MajorGrid.LineWidth );
        }

        private void SetSeriesTabPageDefaults()
        {
            WinFormsHelper.SetSelectedIndexSafe( uiCtrSrs_Color_ComBx, uiCtrSrs_Color_ComBx.Items.IndexOf( Settings.Series.Average.Color.Name ) );
            WinFormsHelper.SetValue( uiCtrSrs_BorWth_Num, Settings.Series.Average.BorderWidth );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrSrs_BorStyle_ComBx, (int) Settings.Series.Average.BorderDashStyle );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrSrs_ChT_ComBx, (int) Settings.Series.Average.ChartType );
        }

        private void UpdateUiByFormInitializeSettings()
        {
            IsFormInitialized = true;
            WinFormsHelper.SetSelectedIndexSafe( uiTop_ApplyTo_ComBx, (int) ApplyToCurve.Average );
        }

        private void ChartSettings_Load( object sender, EventArgs e )
        {
            uiBtm_Ok_Btn.Select();
        }

        private void DefineChartApplyToCurveComboBox()
        {
            uiTop_ApplyTo_ComBx.Items.Clear();
            uiTop_ApplyTo_ComBx.Items.Add( ApplyToCurve.Generated.ToString() );
            uiTop_ApplyTo_ComBx.Items.Add( ApplyToCurve.Pattern.ToString() );
            uiTop_ApplyTo_ComBx.Items.Add( ApplyToCurve.Average.ToString() );
            uiTop_ApplyTo_ComBx.Items.Add( ApplyToCurve.All.ToString() );
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
            DefineChartApplyToCurveComboBox();
            DefineChartAntiAliasingComboBox();
            DefineChartSuppressWarningsComboBox();
            DefineChartBackColorComboBox();
        }

        private void AddSystemDrawingColors( ComboBox comboBox )
        {
            comboBox.Items.Add( Color.AliceBlue.Name );
            comboBox.Items.Add( Color.AntiqueWhite.Name );
            comboBox.Items.Add( Color.Aqua.Name );
            comboBox.Items.Add( Color.Aquamarine.Name );
            comboBox.Items.Add( Color.Azure.Name );
            comboBox.Items.Add( Color.Beige.Name );
            comboBox.Items.Add( Color.Bisque.Name );
            comboBox.Items.Add( Color.Black.Name );
            comboBox.Items.Add( Color.BlanchedAlmond.Name );
            comboBox.Items.Add( Color.Blue.Name );
            comboBox.Items.Add( Color.BlueViolet.Name );
            comboBox.Items.Add( Color.Brown.Name );
            comboBox.Items.Add( Color.BurlyWood.Name );
            comboBox.Items.Add( Color.CadetBlue.Name );
            comboBox.Items.Add( Color.Chartreuse.Name );
            comboBox.Items.Add( Color.Chocolate.Name );
            comboBox.Items.Add( Color.Coral.Name );
            comboBox.Items.Add( Color.CornflowerBlue.Name );
            comboBox.Items.Add( Color.Cornsilk.Name );
            comboBox.Items.Add( Color.Crimson.Name );
            comboBox.Items.Add( Color.Cyan.Name );
            comboBox.Items.Add( Color.DarkBlue.Name );
            comboBox.Items.Add( Color.DarkCyan.Name );
            comboBox.Items.Add( Color.DarkGoldenrod.Name );
            comboBox.Items.Add( Color.DarkGray.Name );
            comboBox.Items.Add( Color.DarkGreen.Name );
            comboBox.Items.Add( Color.DarkKhaki.Name );
            comboBox.Items.Add( Color.DarkMagenta.Name );
            comboBox.Items.Add( Color.DarkOliveGreen.Name );
            comboBox.Items.Add( Color.DarkOrange.Name );
            comboBox.Items.Add( Color.DarkOrchid.Name );
            comboBox.Items.Add( Color.DarkRed.Name );
            comboBox.Items.Add( Color.DarkSalmon.Name );
            comboBox.Items.Add( Color.DarkSeaGreen.Name );
            comboBox.Items.Add( Color.DarkSlateBlue.Name );
            comboBox.Items.Add( Color.DarkSlateGray.Name );
            comboBox.Items.Add( Color.DarkTurquoise.Name );
            comboBox.Items.Add( Color.DarkViolet.Name );
            comboBox.Items.Add( Color.DeepPink.Name );
            comboBox.Items.Add( Color.DeepSkyBlue.Name );
            comboBox.Items.Add( Color.DimGray.Name );
            comboBox.Items.Add( Color.DodgerBlue.Name );
            comboBox.Items.Add( Color.Firebrick.Name );
            comboBox.Items.Add( Color.FloralWhite.Name );
            comboBox.Items.Add( Color.ForestGreen.Name );
            comboBox.Items.Add( Color.Fuchsia.Name );
            comboBox.Items.Add( Color.Gainsboro.Name );
            comboBox.Items.Add( Color.GhostWhite.Name );
            comboBox.Items.Add( Color.Gold.Name );
            comboBox.Items.Add( Color.Goldenrod.Name );
            comboBox.Items.Add( Color.Gray.Name );
            comboBox.Items.Add( Color.Green.Name );
            comboBox.Items.Add( Color.GreenYellow.Name );
            comboBox.Items.Add( Color.Honeydew.Name );
            comboBox.Items.Add( Color.HotPink.Name );
            comboBox.Items.Add( Color.IndianRed.Name );
            comboBox.Items.Add( Color.Indigo.Name );
            comboBox.Items.Add( Color.Ivory.Name );
            comboBox.Items.Add( Color.Khaki.Name );
            comboBox.Items.Add( Color.Lavender.Name );
            comboBox.Items.Add( Color.LavenderBlush.Name );
            comboBox.Items.Add( Color.LawnGreen.Name );
            comboBox.Items.Add( Color.LemonChiffon.Name );
            comboBox.Items.Add( Color.LightBlue.Name );
            comboBox.Items.Add( Color.LightCoral.Name );
            comboBox.Items.Add( Color.LightCyan.Name );
            comboBox.Items.Add( Color.LightGoldenrodYellow.Name );
            comboBox.Items.Add( Color.LightGray.Name );
            comboBox.Items.Add( Color.LightGreen.Name );
            comboBox.Items.Add( Color.LightPink.Name );
            comboBox.Items.Add( Color.LightSalmon.Name );
            comboBox.Items.Add( Color.LightSeaGreen.Name );
            comboBox.Items.Add( Color.LightSkyBlue.Name );
            comboBox.Items.Add( Color.LightSlateGray.Name );
            comboBox.Items.Add( Color.LightSteelBlue.Name );
            comboBox.Items.Add( Color.LightYellow.Name );
            comboBox.Items.Add( Color.Lime.Name );
            comboBox.Items.Add( Color.LimeGreen.Name );
            comboBox.Items.Add( Color.Linen.Name );
            comboBox.Items.Add( Color.Magenta.Name );
            comboBox.Items.Add( Color.Maroon.Name );
            comboBox.Items.Add( Color.MediumAquamarine.Name );
            comboBox.Items.Add( Color.MediumBlue.Name );
            comboBox.Items.Add( Color.MediumOrchid.Name );
            comboBox.Items.Add( Color.MediumPurple.Name );
            comboBox.Items.Add( Color.MediumSeaGreen.Name );
            comboBox.Items.Add( Color.MediumSlateBlue.Name );
            comboBox.Items.Add( Color.MediumSpringGreen.Name );
            comboBox.Items.Add( Color.MediumTurquoise.Name );
            comboBox.Items.Add( Color.MediumVioletRed.Name );
            comboBox.Items.Add( Color.MidnightBlue.Name );
            comboBox.Items.Add( Color.MintCream.Name );
            comboBox.Items.Add( Color.MistyRose.Name );
            comboBox.Items.Add( Color.Moccasin.Name );
            comboBox.Items.Add( Color.NavajoWhite.Name );
            comboBox.Items.Add( Color.Navy.Name );
            comboBox.Items.Add( Color.OldLace.Name );
            comboBox.Items.Add( Color.Olive.Name );
            comboBox.Items.Add( Color.OliveDrab.Name );
            comboBox.Items.Add( Color.Orange.Name );
            comboBox.Items.Add( Color.OrangeRed.Name );
            comboBox.Items.Add( Color.Orchid.Name );
            comboBox.Items.Add( Color.PaleGoldenrod.Name );
            comboBox.Items.Add( Color.PaleGreen.Name );
            comboBox.Items.Add( Color.PaleTurquoise.Name );
            comboBox.Items.Add( Color.PaleVioletRed.Name );
            comboBox.Items.Add( Color.PapayaWhip.Name );
            comboBox.Items.Add( Color.PeachPuff.Name );
            comboBox.Items.Add( Color.Peru.Name );
            comboBox.Items.Add( Color.Pink.Name );
            comboBox.Items.Add( Color.Plum.Name );
            comboBox.Items.Add( Color.PowderBlue.Name );
            comboBox.Items.Add( Color.Purple.Name );
            comboBox.Items.Add( Color.Red.Name );
            comboBox.Items.Add( Color.RosyBrown.Name );
            comboBox.Items.Add( Color.RoyalBlue.Name );
            comboBox.Items.Add( Color.SaddleBrown.Name );
            comboBox.Items.Add( Color.Salmon.Name );
            comboBox.Items.Add( Color.SandyBrown.Name );
            comboBox.Items.Add( Color.SeaGreen.Name );
            comboBox.Items.Add( Color.SeaShell.Name );
            comboBox.Items.Add( Color.Sienna.Name );
            comboBox.Items.Add( Color.Silver.Name );
            comboBox.Items.Add( Color.SkyBlue.Name );
            comboBox.Items.Add( Color.SlateBlue.Name );
            comboBox.Items.Add( Color.SlateGray.Name );
            comboBox.Items.Add( Color.Snow.Name );
            comboBox.Items.Add( Color.SpringGreen.Name );
            comboBox.Items.Add( Color.SteelBlue.Name );
            comboBox.Items.Add( Color.Tan.Name );
            comboBox.Items.Add( Color.Teal.Name );
            comboBox.Items.Add( Color.Thistle.Name );
            comboBox.Items.Add( Color.Tomato.Name );
            comboBox.Items.Add( Color.Transparent.Name );
            comboBox.Items.Add( Color.Turquoise.Name );
            comboBox.Items.Add( Color.Violet.Name );
            comboBox.Items.Add( Color.Wheat.Name );
            comboBox.Items.Add( Color.White.Name );
            comboBox.Items.Add( Color.WhiteSmoke.Name );
            comboBox.Items.Add( Color.Yellow.Name );
            comboBox.Items.Add( Color.YellowGreen.Name );
            comboBox.Items.Add( Color.Empty.Name );
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
            WinFormsHelper.SelectTabSafe( uiCtr_TbCtrl, (int) ChartSettingsTabs.Chart );
            EnableChartTabPageControls( true );
            EnableChartAreaTabPageControls( true );
            EnableSeriesTabPageControls( false );
        }

        private void PerformApplyToCurveNotAllSwitch()
        {
            WinFormsHelper.SelectTabSafe( uiCtr_TbCtrl, (int) ChartSettingsTabs.Series );
            EnableChartTabPageControls( false );
            EnableChartAreaTabPageControls( false );
            EnableSeriesTabPageControls( true );
        }

        private void UpdateUiByGeneratedCurveSettings()
        {
            WinFormsHelper.SetSelectedIndexSafe( uiCtrSrs_Color_ComBx, uiCtrSrs_Color_ComBx.Items.IndexOf( Settings.Series.Generated.Color.Name ) );
            WinFormsHelper.SetValue( uiCtrSrs_BorWth_Num, Settings.Series.Generated.BorderWidth );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrSrs_BorStyle_ComBx, (int) Settings.Series.Generated.BorderDashStyle );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrSrs_ChT_ComBx, (int) Settings.Series.Generated.ChartType );
        }

        private void UpdateUiByPatternCurveSettings()
        {
            WinFormsHelper.SetSelectedIndexSafe( uiCtrSrs_Color_ComBx, uiCtrSrs_Color_ComBx.Items.IndexOf( Settings.Series.Pattern.Color.Name ) );
            WinFormsHelper.SetValue( uiCtrSrs_BorWth_Num, Settings.Series.Pattern.BorderWidth );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrSrs_BorStyle_ComBx, (int) Settings.Series.Pattern.BorderDashStyle );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrSrs_ChT_ComBx, (int) Settings.Series.Pattern.ChartType );
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
            Settings.ApplyMode = (ApplyToCurve) WinFormsHelper.GetSelectedIndexSafe( uiTop_ApplyTo_ComBx );
            SaveChartSettings();
            SaveChartAreaSettings();
            SaveSeriesSettings( Settings.ApplyMode );
        }

        private void SaveChartSettings()
        {
            Settings.Common.AntiAliasing = (AntiAliasingStyles) WinFormsHelper.GetSelectedIndexSafe( uiCtrChart_Aa_ComBx );
            Settings.Common.SuppressExceptions = Convert.ToBoolean( WinFormsHelper.GetSelectedIndexSafe( uiCtrChart_SupEx_ComBx ) );
            Settings.Common.BackColor = Color.FromName( uiCtrChart_BkCol_ComBx.Items[uiCtrChart_BkCol_ComBx.SelectedIndex].ToString() );
        }

        private void SaveChartAreaSettings()
        {
            Settings.Areas.Common.Area3dStyle = Convert.ToBoolean( WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_3d_ComBx ) );
            Settings.Areas.Common.BackColor = Color.FromName( uiCtrArea_BkCol_ComBx.Items[uiCtrArea_BkCol_ComBx.SelectedIndex].ToString() );
            ChartAreaAxis axis = (ChartAreaAxis) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_Axis_ComBx );
            ChartAreaGrid grid = (ChartAreaGrid) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_Grid_ComBx );
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
            Settings.Series.Pattern.BorderWidth = WinFormsHelper.GetValue<int>( uiCtrSrs_BorWth_Num );
            Settings.Series.Pattern.BorderDashStyle = (ChartDashStyle) WinFormsHelper.GetSelectedIndexSafe( uiCtrSrs_BorStyle_ComBx );
            Settings.Series.Pattern.ChartType = (SeriesChartType) WinFormsHelper.GetSelectedIndexSafe( uiCtrSrs_ChT_ComBx );
        }

        private void SaveSeriesGeneratedSettings()
        {
            Settings.Series.Generated.Color = Color.FromName( uiCtrSrs_Color_ComBx.Items[uiCtrSrs_Color_ComBx.SelectedIndex].ToString() );
            Settings.Series.Generated.BorderWidth = WinFormsHelper.GetValue<int>( uiCtrSrs_BorWth_Num );
            Settings.Series.Generated.BorderDashStyle = (ChartDashStyle) WinFormsHelper.GetSelectedIndexSafe( uiCtrSrs_BorStyle_ComBx );
            Settings.Series.Generated.ChartType = (SeriesChartType) WinFormsHelper.GetSelectedIndexSafe( uiCtrSrs_ChT_ComBx );
        }

        private void SaveSeriesAverageSettings()
        {
            Settings.Series.Average.Color = Color.FromName( uiCtrSrs_Color_ComBx.Items[uiCtrSrs_Color_ComBx.SelectedIndex].ToString() );
            Settings.Series.Average.BorderWidth = WinFormsHelper.GetValue<int>( uiCtrSrs_BorWth_Num );
            Settings.Series.Average.BorderDashStyle = (ChartDashStyle) WinFormsHelper.GetSelectedIndexSafe( uiCtrSrs_BorStyle_ComBx );
            Settings.Series.Average.ChartType = (SeriesChartType) WinFormsHelper.GetSelectedIndexSafe( uiCtrSrs_ChT_ComBx );
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

        private void AddChartDashStyles( ComboBox comboBox )
        {
            comboBox.Items.Add( ChartDashStyle.NotSet.ToString() );
            comboBox.Items.Add( ChartDashStyle.Dash.ToString() );
            comboBox.Items.Add( ChartDashStyle.DashDot.ToString() );
            comboBox.Items.Add( ChartDashStyle.DashDotDot.ToString() );
            comboBox.Items.Add( ChartDashStyle.Dot.ToString() );
            comboBox.Items.Add( ChartDashStyle.Solid.ToString() );
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
            Previous.AxisSelected = (ChartAreaAxis) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_Axis_ComBx );
            Previous.GridSelected = (ChartAreaGrid) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_Grid_ComBx );
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
            Settings.Areas.Y.MajorGrid.Enabled = Convert.ToBoolean( WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_En_ComBx ) );
            Settings.Areas.Y.MajorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.Y.MajorGrid.LineDashStyle = (ChartDashStyle) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.Y.MajorGrid.LineWidth = WinFormsHelper.GetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void SaveAxisYMinorGridSettings()
        {
            Settings.Areas.Y.MinorGrid.Enabled = Convert.ToBoolean( WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_En_ComBx ) );
            Settings.Areas.Y.MinorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.Y.MinorGrid.LineDashStyle = (ChartDashStyle) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.Y.MinorGrid.LineWidth = WinFormsHelper.GetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void SaveAxisXMajorGridSettings()
        {
            Settings.Areas.X.MajorGrid.Enabled = Convert.ToBoolean( WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_En_ComBx ) );
            Settings.Areas.X.MajorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.X.MajorGrid.LineDashStyle = (ChartDashStyle) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.X.MajorGrid.LineWidth = WinFormsHelper.GetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void SaveAxisXMinorGridSettings()
        {
            Settings.Areas.X.MinorGrid.Enabled = Convert.ToBoolean( WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_En_ComBx ) );
            Settings.Areas.X.MinorGrid.LineColor = Color.FromName( uiCtrArea_LnCol_ComBx.Items[uiCtrArea_LnCol_ComBx.SelectedIndex].ToString() );
            Settings.Areas.X.MinorGrid.LineDashStyle = (ChartDashStyle) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx );
            Settings.Areas.X.MinorGrid.LineWidth = WinFormsHelper.GetValue<int>( uiCtrArea_LnWth_Num );
        }

        private void UpdateUiByAxesSettings()
        {
            ChartAreaAxis axis = (ChartAreaAxis) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_Axis_ComBx );
            ChartAreaGrid grid = (ChartAreaGrid) WinFormsHelper.GetSelectedIndexSafe( uiCtrArea_Grid_ComBx );
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
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.X.MajorGrid.Enabled ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.X.MajorGrid.LineColor.Name ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.X.MajorGrid.LineDashStyle.ToString() ) );
            WinFormsHelper.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.X.MajorGrid.LineWidth );
        }

        private void SetAxisXMinorGridSettings()
        {
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.X.MinorGrid.Enabled ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.X.MinorGrid.LineColor.Name ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.X.MinorGrid.LineDashStyle.ToString() ) );
            WinFormsHelper.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.X.MinorGrid.LineWidth );
        }

        private void SetAxisYMajorGridSettings()
        {
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.Y.MajorGrid.Enabled ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.Y.MajorGrid.LineColor.Name ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.Y.MajorGrid.LineDashStyle.ToString() ) );
            WinFormsHelper.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.Y.MajorGrid.LineWidth );
        }

        private void SetAxisYMinorGridSettings()
        {
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_En_ComBx, Convert.ToInt32( Settings.Areas.Y.MinorGrid.Enabled ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnCol_ComBx, uiCtrArea_LnCol_ComBx.Items.IndexOf( Settings.Areas.Y.MinorGrid.LineColor.Name ) );
            WinFormsHelper.SetSelectedIndexSafe( uiCtrArea_LnStyle_ComBx, uiCtrArea_LnStyle_ComBx.Items.IndexOf( Settings.Areas.Y.MinorGrid.LineDashStyle.ToString() ) );
            WinFormsHelper.SetValue( uiCtrArea_LnWth_Num, Settings.Areas.Y.MinorGrid.LineWidth );
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

        private void AddChartTypes( ComboBox comboBox )
        {
            comboBox.Items.Add( SeriesChartType.Point.ToString() );
            comboBox.Items.Add( SeriesChartType.FastPoint.ToString() );
            comboBox.Items.Add( SeriesChartType.Bubble.ToString() );
            comboBox.Items.Add( SeriesChartType.Line.ToString() );
            comboBox.Items.Add( SeriesChartType.Spline.ToString() );
            comboBox.Items.Add( SeriesChartType.StepLine.ToString() );
            comboBox.Items.Add( SeriesChartType.FastLine.ToString() );
            comboBox.Items.Add( SeriesChartType.Bar.ToString() );
            comboBox.Items.Add( SeriesChartType.StackedBar.ToString() );
            comboBox.Items.Add( SeriesChartType.StackedBar100.ToString() );
            comboBox.Items.Add( SeriesChartType.Column.ToString() );
            comboBox.Items.Add( SeriesChartType.StackedColumn.ToString() );
            comboBox.Items.Add( SeriesChartType.StackedColumn100.ToString() );
            comboBox.Items.Add( SeriesChartType.Area.ToString() );
            comboBox.Items.Add( SeriesChartType.SplineArea.ToString() );
            comboBox.Items.Add( SeriesChartType.StackedArea.ToString() );
            comboBox.Items.Add( SeriesChartType.StackedArea100.ToString() );
            comboBox.Items.Add( SeriesChartType.Pie.ToString() );
            comboBox.Items.Add( SeriesChartType.Doughnut.ToString() );
            comboBox.Items.Add( SeriesChartType.Stock.ToString() );
            comboBox.Items.Add( SeriesChartType.Candlestick.ToString() );
            comboBox.Items.Add( SeriesChartType.Range.ToString() );
            comboBox.Items.Add( SeriesChartType.SplineRange.ToString() );
            comboBox.Items.Add( SeriesChartType.RangeBar.ToString() );
            comboBox.Items.Add( SeriesChartType.RangeColumn.ToString() );
            comboBox.Items.Add( SeriesChartType.Radar.ToString() );
            comboBox.Items.Add( SeriesChartType.Polar.ToString() );
            comboBox.Items.Add( SeriesChartType.ErrorBar.ToString() );
            comboBox.Items.Add( SeriesChartType.BoxPlot.ToString() );
            comboBox.Items.Add( SeriesChartType.Renko.ToString() );
            comboBox.Items.Add( SeriesChartType.ThreeLineBreak.ToString() );
            comboBox.Items.Add( SeriesChartType.Kagi.ToString() );
            comboBox.Items.Add( SeriesChartType.PointAndFigure.ToString() );
            comboBox.Items.Add( SeriesChartType.Funnel.ToString() );
            comboBox.Items.Add( SeriesChartType.Pyramid.ToString() );
        }

    }
}
