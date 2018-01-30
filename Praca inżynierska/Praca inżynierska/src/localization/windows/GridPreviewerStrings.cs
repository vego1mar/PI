using PI.src.localization.general;
using static PI.Translator;

namespace PI.src.localization.windows
{
    public class GridPreviewerStrings
    {
        public GridPreviewerFormStrings Form { get; private set; } = new GridPreviewerFormStrings();
        public GridPreviewerUiStrings Ui { get; private set; } = new GridPreviewerUiStrings();

        public class GridPreviewerFormStrings
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Form_Text" ); } }
        }

        public class GridPreviewerUiStrings
        {
            public LocalizedString PanelDatasetGrid { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_DatasetGrid" ); } }
            public LocalizedString PanelAutoSizeColumnsMode { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_AutoSizeColumnsMode" ); } }
            public LocalizedString PanelFastEdit { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_FastEdit" ); } }
            public LocalizedString PanelOperationType { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_OperationType" ); } }
            public LocalizedString PanelStartIndex { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_StartIndex" ); } }
            public LocalizedString PanelEndIndex { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_EndIndex" ); } }
            public LocalizedString PanelValue { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Value" ); } }
            public LocalizedString PanelAddend { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Addend" ); } }
            public LocalizedString PanelSubtrahend { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Subtrahend" ); } }
            public LocalizedString PanelMultiplier { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Multiplier" ); } }
            public LocalizedString PanelDivisor { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Divisor" ); } }
            public LocalizedString PanelExponent { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Exponent" ); } }
            public LocalizedString PanelBasis { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_Basis" ); } }
            public LocalizedString PanelNotApplicable { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Value1_NotApplicable" ); } }
            public LocalizedString PanelReset { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Reset" ); } }
            public LocalizedString PanelPerform { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Perform" ); } }
            public LocalizedString PanelRefresh { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Refresh" ); } }
            public LocalizedString PanelSave { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Save" ); } }
            public LocalizedString PanelOk { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_OK" ); } }
            public LocalizedString PanelInfoGridPreviewerLoaded { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_GridPreviewerLoaded" ); } }
            public LocalizedString PanelInfoOperationRevoked { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_OperationRevoked" ); } }
            public LocalizedString PanelInfoChangesSaved { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_ChangesSaved" ); } }
            public LocalizedString PanelInfoInvalidUserValue { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_InvalidUserValue" ); } }
            public LocalizedString PanelInfoOperationRejected { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_OperationRejected" ); } }
            public LocalizedString PanelInfoPerformedAndRefreshed { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_PerformedAndRefreshed" ); } }
            public LocalizedString PanelInfoValuesRestored { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Panel_Info_ValuesRestored" ); } }
            public LocalizedString PreviewInfoChartNotRepainted { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Info_ChartNotRepainted" ); } }
            public LocalizedString PreviewInfoChartRefreshError { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Info_ChartRefreshError" ); } }
            public LocalizedString PreviewInfoChartRefreshed { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Info_ChartRefreshed" ); } }
            public LocalizedString PreviewTitle { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Preview_Preview" ); } }
            public LocalizedString DatasetTitle { get { return new LocalizedString( CurrentLanguage, "GridPreviewer_Ui_Dataset_Dataset" ); } }
        }
    }
}
