using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.windows
{
    public class StatisticalAnalysisStrings
    {
        public StatisticalAnalysisFormStrings Form { get; private set; } = new StatisticalAnalysisFormStrings();
        public StatisticalAnalysisUiStrings Ui { get; private set; } = new StatisticalAnalysisUiStrings();

        public class StatisticalAnalysisFormStrings
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Form_Text" ); } }
        }

        public class StatisticalAnalysisUiStrings
        {
            public LocalizedString StandardDeviationTitle { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Title" ); } }
            public LocalizedString StandardDeviationPeek { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Peek" ); } }
            public LocalizedString StandardDeviationSaturation { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Saturation" ); } }
            public LocalizedString StandardDeviationNoise01 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Noise01" ); } }
            public LocalizedString StandardDeviationNoise05 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Noise05" ); } }
            public LocalizedString StandardDeviationNoise1 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Noise1" ); } }
            public LocalizedString StandardDeviationNoise2 { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_StandardDeviation_Noise2" ); } }
            public LocalizedString PreviewTitle { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Title" ); } }
            public LocalizedString PreviewChart { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Chart" ); } }
            public LocalizedString PreviewDataSet { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Dataset" ); } }
            public LocalizedString PreviewCurveIndex { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_CurveIndex" ); } }
            public LocalizedString PreviewCurveType { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_CurveType" ); } }
            public LocalizedString PreviewPhenomenon { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Phenomenon" ); } }
            public LocalizedString PreviewNoise { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_Noise" ); } }
            public LocalizedString PreviewMeanType { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_MeanType" ); } }
            public LocalizedString PreviewDataSetSelection { get { return new LocalizedString( CurrentLanguage, "StatisticalAnalysis_Ui_Preview_DatasetSelection" ); } }
        }
    }

}
