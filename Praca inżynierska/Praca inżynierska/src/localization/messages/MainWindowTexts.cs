using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.messages
{
    public class MainWindowTexts
    {
        public ExclamationSeriesSelection SeriesSelection { get; private set; } = new ExclamationSeriesSelection();
        public ErrorChartRefreshing ChartRefreshing { get; private set; } = new ErrorChartRefreshing();
        public AsteriskCurveTypeNotSelected CurveTypeNotSelected { get; private set; } = new AsteriskCurveTypeNotSelected();
        public StopPatternCurveNotChosen PatternCurveNotChosen { get; private set; } = new StopPatternCurveNotChosen();
        public ExclamationPointsNotValidToChart PointsNotValidToChart { get; private set; } = new ExclamationPointsNotValidToChart();
        public ExclamationSpecifiedCurveDoesNotExist SpecifiedCurveDoesNotExist { get; private set; } = new ExclamationSpecifiedCurveDoesNotExist();
        public StopOperationMalformRejected OperationMalformRejected { get; private set; } = new StopOperationMalformRejected();
         
        public class ExclamationSeriesSelection
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_SeriesSelection_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_SeriesSelection_Caption" ); } }
        }

        public class ErrorChartRefreshing
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_ChartRefreshing_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_ChartRefreshing_Caption" ); } }
        }

        public class AsteriskCurveTypeNotSelected
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_CurveTypeNotSelected_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_CurveTypeNotSelected_Caption" ); } }
        }

        public class StopPatternCurveNotChosen
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_PatternCurveNotChosen_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_PatternCurveNotChosen_Caption" ); } }
        }

        public class ExclamationPointsNotValidToChart
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_PointsNotValidToChart_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_PointsNotValidToChart_Caption" ); } }
        }

        public class ExclamationSpecifiedCurveDoesNotExist
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_SpecifiedCurveDoesNotExist_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_SpecifiedCurveDoesNotExist_Caption" ); } }
        }

        public class StopOperationMalformRejected
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_OperationMalformRejected_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_MainWindow_OperationMalformRejected_Caption" ); } }
        }
    }
}
