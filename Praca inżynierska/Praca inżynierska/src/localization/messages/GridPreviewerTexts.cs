using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.messages
{
    public class GridPreviewerTexts
    {
        public ExclamationIndexGreaterThanAllowed IndexGreaterThanAllowed { get; private set; } = new ExclamationIndexGreaterThanAllowed();
        public ExclamationIndexLowerThanAllowed IndexLowerThanAllowed { get; private set; } = new ExclamationIndexLowerThanAllowed();
        public ExclamationImproperUserValue ImproperUserValue { get; private set; } = new ExclamationImproperUserValue();
        public ErrorPerformOperation PerformOperation { get; private set; } = new ErrorPerformOperation();
        public ErrorInvalidCurvePoints InvalidCurvePoints { get; private set; } = new ErrorInvalidCurvePoints();
        public ErrorChartRefreshing ChartRefreshing { get; private set; } = new ErrorChartRefreshing();

        public class ExclamationIndexGreaterThanAllowed
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_IndexGreaterThanAllowed_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_IndexGreaterThanAllowed_Caption" ); } }
        }

        public class ExclamationIndexLowerThanAllowed
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_IndexLowerThanAllowed_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_IndexLowerThanAllowed_Caption" ); } }
        }

        public class ExclamationImproperUserValue
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_ImproperUserValue_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_ImproperUserValue_Caption" ); } }
        }

        public class ErrorPerformOperation
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_PerformOperation_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_PerformOperation_Caption" ); } }
        }

        public class ErrorInvalidCurvePoints
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_InvalidCurvePoints_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_InvalidCurvePoints_Caption" ); } }
        }

        public class ErrorChartRefreshing
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_ChartRefreshing_Text" ); } }
            public LocalizedString Caption { get { return new LocalizedString( CurrentLanguage, "Messages_GridPreviewer_ChartRefreshing_Caption" ); } }
        }
    }
}
