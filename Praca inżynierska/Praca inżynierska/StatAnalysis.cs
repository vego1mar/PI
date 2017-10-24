using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace PI
{
    public partial class StatAnalysis : Form
    {

        internal GenSettings.UiGenSettings GenSets { get; private set; }
        private List<List<CurvesDataManager>> Data { get; set; }
        private List<double> Surroundings { get; set; }
        private List<List<List<Series>>> Averages { get; set; }

        public enum PhenomenonIndex
        {
            Peek = 0,
            Deformation = 1
        }

        public StatAnalysis()
        {
            InitializeComponent();
            InitializeProperties();
            SetWindowDefaults();
            PerformStatisticalAnalysis();
        }

        private void InitializeProperties()
        {
            InitializePropertyGenSets();
            InitializePropertySurroundings();
            InitializePropertyData();
            InitializePropertyAverages();
        }

        private void InitializePropertyGenSets()
        {
            GenSets = new GenSettings.UiGenSettings() {
                NumberOfCurves = 100,
                StartingXPoint = -3,
                EndingXPoint = 3,
                PointsDensity = 600
            };
        }

        private void InitializePropertySurroundings()
        {
            Surroundings = new List<double>() {
                0.1,
                0.5,
                1.0,
                2.0
            };
        }

        private void InitializePropertyData()
        {
            /*
             *  Data[a][b]
             *  a := { peek, deformation } - malformation type no.
             *  b := { 0.1, 0.5, 1.0, 2.0 } - noise surrounding no.
             */

            Data = new List<List<CurvesDataManager>>();

            foreach ( PhenomenonIndex idx in Enum.GetValues( typeof( PhenomenonIndex ) ) ) {
                Data.Add( new List<CurvesDataManager>() );                                      // [a][-] 

                for ( int i = 0; i < Surroundings.Count; i++ ) {
                    Data[(int) idx].Add( new CurvesDataManager( new Params() ) );               // [-][b] 
                }
            }

        }

        private void InitializePropertyAverages()
        {
            /*
             *  Averages[a][b][c]
             *  a := { peek, deformation } - malformation type no.
             *  b := { 0.1, 0.5, 1.0, 2.0 } - noise surrounding no.
             *  c := { arithemtic, geometric, mediana, ... } - mean type no.
             */

            Averages = new List<List<List<Series>>>();

            foreach ( PhenomenonIndex idx in Enum.GetValues( typeof( PhenomenonIndex ) ) ) {
                Averages.Add( new List<List<Series>>() );                                           // [a][-][-] 

                for ( int i = 0; i < Surroundings.Count; i++ ) {
                    Averages[(int) idx].Add( new List<Series>() );                                  // [-][b][-] 

                    for ( int j = 0; j < Enum.GetNames( typeof( Enums.MeanType ) ).Length; j++ ) {
                        Averages[(int) idx][i].Add( new Series() );                                 // [-][-][c]
                    }
                }
            }
        }

        private void SetWindowDefaults()
        {
            WinFormsHelper.SelectTabSafe( uiL_TbCtrl, (int) PhenomenonIndex.Peek );
            WinFormsHelper.SelectTabSafe( uiR_TbCtrl, 0 );
            CurvesDataManager.SetDefaultProperties( uiRChart_Chart );
            AddDataSetCurveTypes( uiRChartDown_CrvT_ComBx );
            WinFormsHelper.SetSelectedIndexSafe( uiRChartDown_CrvT_ComBx, (int) Enums.DataSetCurveType.Pattern );
            uiRChartDown_CrvIdx_Num.Minimum = 1;
            uiRChartDown_CrvIdx_Num.Maximum = GenSets.NumberOfCurves;
            AddPhenomenonsIndexNames( uiRChartDown_Phen_ComBx );
            WinFormsHelper.SetSelectedIndexSafe( uiRChartDown_Phen_ComBx, (int) PhenomenonIndex.Peek );
            AddSurroundings( uiRChartDown_Surr_ComBx );
            WinFormsHelper.SetSelectedIndexSafe( uiRChartDown_Surr_ComBx, 0 );
            AddMeanTypes( uiRChartDown_MeanT_ComBx );
            WinFormsHelper.SetSelectedIndexSafe( uiRChartDown_MeanT_ComBx, (int) Enums.MeanType.CustomTolerance );
        }

        private void UiStatAnalysis_Load( object sender, EventArgs e )
        {
            uiLPeek_Grid.Select();
        }

        private void AddDataSetCurveTypes( ComboBox comboBox )
        {
            foreach ( string type in Enum.GetNames( typeof( Enums.DataSetCurveType ) ) ) {
                comboBox.Items.Add( type );
            }
        }

        private void AddPhenomenonsIndexNames( ComboBox comboBox )
        {
            foreach ( string phenomenon in Enum.GetNames( typeof( PhenomenonIndex ) ) ) {
                comboBox.Items.Add( phenomenon );
            }
        }

        private void AddSurroundings( ComboBox comboBox )
        {
            foreach ( double surrounding in Surroundings ) {
                comboBox.Items.Add( surrounding.ToString( System.Globalization.CultureInfo.InvariantCulture ) );
            }
        }

        private void AddMeanTypes( ComboBox comboBox )
        {
            foreach ( string type in Enum.GetNames( typeof( Enums.MeanType ) ) ) {
                comboBox.Items.Add( type );
            }
        }

        private void PerformStatisticalAnalysis()
        {
            for ( int i = 0; i < Data.Count; i++ ) {
                for ( int j = 0; j < Surroundings.Count; j++ ) {
                    Data[i][j].GeneratePatternCurve( Enums.PatternCurveScaffold.Polynomial, GenSets.StartingXPoint, GenSets.EndingXPoint, GenSets.PointsDensity );
                    Data[i][j].SpreadPatternCurveSetToGeneratedCurveSet( GenSets.NumberOfCurves );
                    Data[i][j].MakeGaussianNoiseForGeneratedCurves( GenSets.NumberOfCurves, Surroundings[j] );

                    for ( int k = 0; k < Enum.GetNames( typeof( Enums.MeanType ) ).Length; k++ ) {
                        // TODO: MakeAverage... and RecopyIntoAverages[a][b][c]
                    }
                }
            }
        }

        private void UiRightChartDown_ShowDataset_Button_Click( object sender, EventArgs e )
        {
            /*
             *   TODO:
             *   
             *   REQUIREMENTS:
             *   curve type
             *   curve index ( if generated curve )
             *   left panel selected tab ( phenomenon )
             *   noise surrounding
             *   mean type ( if average curve )
             */
        }

    }
}
