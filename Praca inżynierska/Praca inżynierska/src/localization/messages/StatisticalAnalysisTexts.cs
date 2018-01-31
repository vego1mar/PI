using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.messages
{
    public class StatisticalAnalysisTexts
    {
        public ExclamationOfValueOutOfRange ValueOutOfRange { get; private set; } = new ExclamationOfValueOutOfRange();
        public ErrorOfUnrecognized Unrecognized { get; private set; } = new ErrorOfUnrecognized();
        public ExclamationOfPointsNotValidToChart PointsNotValidToChart { get; private set; } = new ExclamationOfPointsNotValidToChart();

        public class ExclamationOfValueOutOfRange
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_StatisticalAnalysis_ValueOutOfRange_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_StatisticalAnalysis_ValueOutOfRange_Caption" ); } }
        }

        public class ErrorOfUnrecognized
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_StatisticalAnalysis_Unrecognized_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_StatisticalAnalysis_Unrecognized_Caption" ); } }
        }

        public class ExclamationOfPointsNotValidToChart
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_StatisticalAnalysis_PointsNotValidToChart_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_StatisticalAnalysis_PointsNotValidToChart_Caption" ); } }
        }
    }
}
