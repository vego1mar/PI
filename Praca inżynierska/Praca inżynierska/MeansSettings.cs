using System;
using System.Windows.Forms;

namespace PI
{
    public partial class MeansSettings : Form
    {
        internal Params MeansParams { get; private set; }

        internal class Params
        {
            public Power PowerMean { get; set; } = new Power();
            public CustomDifferential CustomDifferentialMean { get; set; } = new CustomDifferential();
            public CustomTolerance CustomToleranceMean { get; set; } = new CustomTolerance();

            internal class Power
            {
                public double Rank { get; set; } = 0.5;
            }

            internal class CustomDifferential
            {
                public CustomDifferentialMeanMode Mode { get; set; } = CustomDifferentialMeanMode.Sum;
            }

            internal class CustomTolerance
            {
                public CustomToleranceComparerType Comparer { get; set; } = CustomToleranceComparerType.Mediana;
                public double Tolerance { get; set; } = 1.05;
                public CustomToleranceFinisherFunction Finisher { get; set; } = CustomToleranceFinisherFunction.ArithmeticMean;
            }
        }

        public enum CustomDifferentialMeanMode
        {
            Mediana,
            Sum
        }

        public enum CustomToleranceComparerType
        {
            Mediana,
            ArithmeticMean
        }

        public enum CustomToleranceFinisherFunction
        {
            Mediana,
            ArithmeticMean,
            GeometricMean,
            Maximum,
            Minimum
        }

        public MeansSettings()
        {
            InitializeComponent();
            InitializeProperties();
            LocalizeWindow();
            UpdateUiByDefaultSettings();
        }

        private void InitializeProperties()
        {
            MeansParams = new Params();
        }

        private void LocalizeWindow()
        {
            LocalizeForm();
            LocalizeUi();
        }

        private void UpdateUiByDefaultSettings()
        {
            WinFormsHelper.SetValue( uiGrid_PowRank_Num, MeansParams.PowerMean.Rank );
            WinFormsHelper.SetSelectedIndexSafe( uiGrid_DiffMode_ComBx, (int) MeansParams.CustomDifferentialMean.Mode );
            WinFormsHelper.SetSelectedIndexSafe( uiGrid_Comp_ComBx, (int) MeansParams.CustomToleranceMean.Comparer );
            WinFormsHelper.SetValue( uiGrid_Toler_Num, MeansParams.CustomToleranceMean.Tolerance );
            WinFormsHelper.SetSelectedIndexSafe( uiGrid_Finish_ComBx, (int) MeansParams.CustomToleranceMean.Finisher );
        }

        private void Ui_Ok_Click( object sender, EventArgs e )
        {
            SaveAllSettings();
        }

        private void SaveAllSettings()
        {
            MeansParams.PowerMean.Rank = WinFormsHelper.GetValue<double>( uiGrid_PowRank_Num );
            MeansParams.CustomDifferentialMean.Mode = (CustomDifferentialMeanMode) WinFormsHelper.GetSelectedIndexSafe( uiGrid_DiffMode_ComBx );
            MeansParams.CustomToleranceMean.Comparer = (CustomToleranceComparerType) WinFormsHelper.GetSelectedIndexSafe( uiGrid_Comp_ComBx );
            MeansParams.CustomToleranceMean.Tolerance = WinFormsHelper.GetValue<double>( uiGrid_Toler_Num );
            MeansParams.CustomToleranceMean.Finisher = (CustomToleranceFinisherFunction) WinFormsHelper.GetSelectedIndexSafe( uiGrid_Finish_ComBx );
        }

        public void SetPowerMeanRank( double value )
        {
            CurvesDataManager.CurvesDataManagerConsts.ChartValuesConsts consts = new CurvesDataManager.CurvesDataManagerConsts.ChartValuesConsts();

            if ( value > consts.AcceptableMin && value < consts.AcceptableMax ) {
                MeansParams.PowerMean.Rank = value;
            }

            WinFormsHelper.SetValue( uiGrid_PowRank_Num, MeansParams.PowerMean.Rank );
        }

        public void SetCustomDifferentialMeanMode( CustomDifferentialMeanMode mode )
        {
            MeansParams.CustomDifferentialMean.Mode = mode;
            WinFormsHelper.SetSelectedIndexSafe( uiGrid_DiffMode_ComBx, (int) MeansParams.CustomDifferentialMean.Mode );
        }

        public void SetCustomToleranceMeanComparer( CustomToleranceComparerType type )
        {
            MeansParams.CustomToleranceMean.Comparer = type;
            WinFormsHelper.SetSelectedIndexSafe( uiGrid_Comp_ComBx, (int) MeansParams.CustomToleranceMean.Comparer );
        }

        public void SetCustomToleranceMeanTolerance( double value )
        {
            MeansParams.CustomToleranceMean.Tolerance = value;
            WinFormsHelper.SetValue( uiGrid_Toler_Num, value );
        }

        public void SetCustomToleranceMeanFinisher( CustomToleranceFinisherFunction type )
        {
            MeansParams.CustomToleranceMean.Finisher = type;
            WinFormsHelper.SetSelectedIndexSafe( uiGrid_Finish_ComBx, (int) MeansParams.CustomToleranceMean.Finisher );
        }

        private void MeansSettings_FormClosing( object sender, FormClosingEventArgs e )
        {
            Dispose();
        }

        private void LocalizeForm()
        {
            Text = Translator.GetInstance().Strings.MeansSettings.Form.Text.GetString();
        }

        private void LocalizeUi()
        {
            LocalizePowerMean();
            LocalizeCustomDifferential();
            LocalizeCustomTolerance();
        }

        private void LocalizePowerMean()
        {
            uiGrid_Power_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.Power.Power.GetString();
            uiGrid_PowRank_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.Power.PowRank.GetString();
        }

        private void LocalizeCustomDifferential()
        {
            uiGrid_CstDiff_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomDifferential.CstDiff.GetString();
            uiGrid_DiffMode_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomDifferential.DiffMode.GetString();
            AddLocalizedCustomDifferentialMeanModes( uiGrid_DiffMode_ComBx );
        }

        private void LocalizeCustomTolerance()
        {
            uiGrid_CstTol_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.CstTol.GetString();
            uiGrid_Comp_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.Comp.GetString();
            AddLocalizedCustomToleranceComparerTypes( uiGrid_Comp_ComBx );
            uiGrid_Toler_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.Toler.GetString();
            uiGrid_Finish_TxtBx.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.Finish.GetString();
            AddLocalizedCustomToleranceFinisherFunctions( uiGrid_Finish_ComBx );
            ui_Ok_Btn.Text = Translator.GetInstance().Strings.MeansSettings.Ui.CustomTolerance.Ok.GetString();
        }

        private void AddLocalizedCustomDifferentialMeanModes<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.CustomDifferentialMeanModes ) {
                control.Items.Add( item.GetString() );
            }
        }

        private void AddLocalizedCustomToleranceComparerTypes<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.CustomToleranceComparerTypes ) {
                control.Items.Add( item.GetString() );
            }
        }

        private void AddLocalizedCustomToleranceFinisherFunctions<T>( T control ) where T : ComboBox
        {
            control.Items.Clear();

            foreach ( var item in Translator.GetInstance().Strings.Enums.CustomToleranceFinisherFunctions ) {
                control.Items.Add( item.GetString() );
            }
        }

    }
}
