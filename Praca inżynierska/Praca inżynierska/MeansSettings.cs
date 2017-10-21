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
            UpdateUiByDefaultSettings();
        }

        private void InitializeProperties()
        {
            MeansParams = new Params();
            AddCustomDifferentialMeanModes( uiGrid_DiffMode_ComBx );
            AddCustomToleranceMeanComparerTypes( uiGrid_Comp_ComBx );
            AddCustomToleranceMeanFinisherFunctions( uiGrid_Finish_ComBx );
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
            CurvesDataManagerConsts.ChartValuesConsts consts = new CurvesDataManagerConsts.ChartValuesConsts();

            if ( value > consts.AcceptableMin && value < consts.AcceptableMax ) {
                MeansParams.PowerMean.Rank = value;
            }

            WinFormsHelper.SetValue( uiGrid_PowRank_Num, MeansParams.PowerMean.Rank );
        }

        private void AddCustomDifferentialMeanModes( ComboBox comboBox )
        {
            comboBox.Items.Add( CustomDifferentialMeanMode.Mediana.ToString() );
            comboBox.Items.Add( CustomDifferentialMeanMode.Sum.ToString() );
        }

        public void SetCustomDifferentialMeanMode( CustomDifferentialMeanMode mode )
        {
            MeansParams.CustomDifferentialMean.Mode = mode;
            WinFormsHelper.SetSelectedIndexSafe( uiGrid_DiffMode_ComBx, (int) MeansParams.CustomDifferentialMean.Mode );
        }

        private void AddCustomToleranceMeanComparerTypes( ComboBox comboBox )
        {
            comboBox.Items.Add( CustomToleranceComparerType.Mediana.ToString() );
            comboBox.Items.Add( CustomToleranceComparerType.ArithmeticMean.ToString() );
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

        private void AddCustomToleranceMeanFinisherFunctions( ComboBox comboBox )
        {
            comboBox.Items.Add( CustomToleranceFinisherFunction.Mediana.ToString() );
            comboBox.Items.Add( CustomToleranceFinisherFunction.ArithmeticMean.ToString() );
            comboBox.Items.Add( CustomToleranceFinisherFunction.GeometricMean.ToString() );
            comboBox.Items.Add( CustomToleranceFinisherFunction.Maximum.ToString() );
            comboBox.Items.Add( CustomToleranceFinisherFunction.Minimum.ToString() );
        }

    }
}
