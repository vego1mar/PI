using System;
using System.Threading;
using System.Windows.Forms;

// INTERNAL BACKLOG
// TODO: Update info
// TODO: Configuration file
// TODO: Reading set of curves from a file
// TODO: Saving set of curves into a file
// TODO: I18N
// TODO: Refreshing charts and curves datasheet automatically
// TODO: Drawing curves in charts
// TODO: Implement Gaussian noise option
// TODO: Menu rebuild - adding icons etc. + functionality
// TODO: Move timer definition to ThreadTasker and generalize the methods
// TODO: Log viewier from the application runtime context
// TODO: Center dialog windows and message boxes
// TODO: Working on threads
// TODO: Errors notification icon

namespace PI
{
    public partial class WfMainWindow : Form
    {

        #region Members
        private bool IsPropertiesPanelHidden;
        private int PropertiesPanelWidth;
        private Thread TimerThread;
        private CurvesDataset ChartsCurvesDataset;
        #endregion

        public WfMainWindow()
        {
            InitializeComponent();
            InitalizeFields();
        }

        private void InitalizeFields()
        {
            IsPropertiesPanelHidden = false;
            PropertiesPanelWidth = wfPropertiesPanel.Size.Width;
            TimerThread = null;
            DefineTimerThread();
            ThreadTasker.StartThreadSafe( TimerThread, "PI.WfMainWindow.InitializeFields()" );
            UpdateComponentRelatedWithActualStatusOfTimerThread();
            ChartsCurvesDataset = new CurvesDataset();
        }

        private void wfViewHideShowPropertiesPanelToolStripMenuItem_Click( object sender, System.EventArgs e )
        {
            if ( IsPropertiesPanelHidden ) {
                wfPropertiesPanel.Visible = true;
                wfPropertiesPanel.Enabled = true;
                var panelSize = wfPropertiesPanel.Size;
                panelSize.Width = PropertiesPanelWidth;
                wfPropertiesPanel.Size = panelSize;
                IsPropertiesPanelHidden = false;
            }
            else {
                wfPropertiesPanel.Visible = false;
                wfPropertiesPanel.Enabled = false;
                var panelSize = wfPropertiesPanel.Size;
                PropertiesPanelWidth = panelSize.Width;
                panelSize.Width = 0;
                wfPropertiesPanel.Size = panelSize;
                IsPropertiesPanelHidden = true;
            }
        }

        private void wfViewResizePropertiesPanelToolStripMenuItem_Click( object sender, System.EventArgs e )
        {
            using ( var propertiesResizerDialog = new WfPropertiesPanelResizer() ) {
                WindowsFormsHelper.ShowDialogSafe( propertiesResizerDialog, this, "PI.WfMainWindow.wfViewResizePropertiesPanelToolStripMenuItem_Click(sender, e)" );
            }
        }

        private void WfMainWindow_FormClosed( object sender, FormClosedEventArgs e )
        {
            Logger.Close();
        }

        private void WfMainWindow_Load( object sender, EventArgs e )
        {
            Logger.Initialize();
            UpdateComponentRelatedWithDotNetFrameworkVersion();
            UpdateComponentRelatedWithOSVersionName();
        }

        private void wfMenuProgramExit_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

        private void DefineTimerThread()
        {
            string methodName = "PI.WfMainWindow.DefineTimerThread()";

            TimerThread = new Thread( () => {
                try {
                    Thread.CurrentThread.IsBackground = true;
                    System.Timers.Timer timer = new System.Timers.Timer();
                    InstallEventForTimer( ref timer );
                    timer.Interval = 1000;
                    timer.Start();
                    timer.Enabled = true;
                }
                catch ( ThreadStateException x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( ObjectDisposedException x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( ArgumentOutOfRangeException x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( ArgumentException x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( Exception x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
            } );
        }

        private void InstallEventForTimer( ref System.Timers.Timer timer )
        {
            ushort numberOfSeconds = 0;
            ushort numberOfMinutes = 0;
            ushort numberOfHours = 0;
            ulong numberOfDays = 0;

            timer.Elapsed += ( object sender, System.Timers.ElapsedEventArgs e ) => {
                numberOfSeconds++;

                if ( numberOfSeconds > 59 ) {
                    numberOfSeconds = 0;
                    numberOfMinutes++;
                }

                if ( numberOfMinutes > 59 ) {
                    numberOfMinutes = 0;
                    numberOfHours++;
                }

                if ( numberOfHours > 23 ) {
                    numberOfHours = 0;
                    numberOfDays++;
                }

                string numberOfHoursText = numberOfHours.ToString( "00" );
                string numberOfMinutesText = numberOfMinutes.ToString( "00" );
                string numberOfSecondsText = numberOfSeconds.ToString( "00" );
                string numberOfDaysText = numberOfDays.ToString();
                RefreshComponentRelatedWithTimerThread( numberOfDaysText + ":" + numberOfHoursText + ":" + numberOfMinutesText + ":" + numberOfSecondsText );
            };
        }

        private void RefreshComponentRelatedWithTimerThread( string text )
        {
            string methodName = "PI.WfMainWindow.RefreshComponentRelatedWithTimerThread(text)";

            try {
                BeginInvoke( (MethodInvoker) delegate {
                    wfPropertiesProgramCounts2TextBox.Text = text;
                    wfPropertiesProgramCounts2TextBox.Refresh();
                } );
            }
            catch ( ObjectDisposedException x ) {
                Logger.WriteExceptionInfo( x, methodName );
            }
            catch ( InvalidOperationException x ) {
                Logger.WriteExceptionInfo( x, methodName );
            }
            catch ( Exception x ) {
                Logger.WriteExceptionInfo( x, methodName );
            }
        }

        private void UpdateComponentRelatedWithActualStatusOfTimerThread()
        {
            if ( TimerThread == null ) {
                wfPropertiesProgramActualState2TextBox.Text = SharedConstants.TIMER_START_FAILURE;
            }
            else {
                wfPropertiesProgramActualState2TextBox.Text = SharedConstants.TIMER_START_SUCCESS;
            }
        }

        private void wfPropertiesGenerateDefineButton_Click( object sender, EventArgs e )
        {
            using ( var PCDDialog = new PatternCurveDefiner() ) {
                string methodName = "PI.WfMainWindow.wfPropertiesGenerateDefineButton_Click(sender, e)";
                WindowsFormsHelper.ShowDialogSafe( PCDDialog, this, methodName );

                try {
                    if ( PCDDialog.DialogResult == DialogResult.OK ) {
                        PreSets.ChosenPatternCurveScaffold = PCDDialog.ChosenCurve;
                        PreSets.ParameterA = PCDDialog.ParameterA;
                        PreSets.ParameterB = PCDDialog.ParameterB;
                        PreSets.ParameterC = PCDDialog.ParameterC;
                        PreSets.ParameterD = PCDDialog.ParameterD;
                        PreSets.ParameterE = PCDDialog.ParameterE;
                        PreSets.ParameterF = PCDDialog.ParameterF;
                        UpdateComponentRelatedWithChosenPatternCurveScaffoldStatus();
                    }
                }
                catch ( System.ComponentModel.InvalidEnumArgumentException x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
                catch ( Exception x ) {
                    Logger.WriteExceptionInfo( x, methodName );
                }
            }
        }

        private void UpdateComponentRelatedWithChosenPatternCurveScaffoldStatus()
        {
            switch ( PreSets.ChosenPatternCurveScaffold ) {
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL:
                wfPropertiesGenerateCurveScaffold2TextBox.Text = SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL_TEXT;
                break;
            case SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC:
                wfPropertiesGenerateCurveScaffold2TextBox.Text = SharedConstants.CURVE_PATTERN_SCAFFOLD_HYPERBOLIC_TEXT;
                break;
            default:
                wfPropertiesGenerateCurveScaffold2TextBox.Text = SharedConstants.CURVE_PATTERN_SCAFFOLD_DEFAULT_TEXT;
                break;
            }
        }

        private void wfPropertiesDatasheetCurveTypeComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            string methodName = "PI.WfMainWindow.wfPropertiesDatasheetCurveTypeComboBox_SelectedIndexChanged(sender, e)";

            switch ( WindowsFormsHelper.GetSelectedIndexSafe( wfPropertiesDatasheetCurveTypeComboBox, methodName ) ) {
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_GENERATED:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = true;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = true;
                break;
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_PATTERN:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = false;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = false;
                break;
            default:
                wfPropertiesDatasheetCurveIndexNumericUpDown.Enabled = false;
                wfPropertiesDatasheetCurveIndexTrackBar.Enabled = false;
                break;
            }
        }

        private void wfPropertiesDatasheetCurveIndexNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            string methodName = "PI.WfMainWindow.wfPropertiesDatasheetCurveIndexNumericUpDown_ValueChanged(sender, e)";
            int numericUpDownValue = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesDatasheetCurveIndexNumericUpDown, methodName );
            WindowsFormsHelper.SetValueForTrackBar( wfPropertiesDatasheetCurveIndexTrackBar, numericUpDownValue, methodName );
        }

        private void wfPropertiesDatasheetCurveIndexTrackBar_Scroll( object sender, EventArgs e )
        {
            string methodName = "PI.WfMainWindow.wfPropertiesDatasheetCurveIndexTrackBar_Scroll(sender, e)";
            int trackBarValue = WindowsFormsHelper.GetValueFromTrackBar( wfPropertiesDatasheetCurveIndexTrackBar, methodName );
            WindowsFormsHelper.SetValueForNumericUpDown( wfPropertiesDatasheetCurveIndexNumericUpDown, trackBarValue, methodName );
        }

        private void wfPropertiesGenerateNumberOfCurves1NumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            string methodName = "PI.WfMainWindow.wfPropertiesGenerateNumberOfCurves1NumericUpDown_ValueChanged(sender, e)";
            int numberOfCurves = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfCurves1NumericUpDown, methodName );
            wfPropertiesDatasheetCurveIndexNumericUpDown.Minimum = 1;
            wfPropertiesDatasheetCurveIndexNumericUpDown.Maximum = numberOfCurves;
            wfPropertiesDatasheetCurveIndexTrackBar.Minimum = 1;
            wfPropertiesDatasheetCurveIndexTrackBar.Maximum = numberOfCurves;
            PreSets.NumberOfCurves = numberOfCurves;
        }

        private void wfPropertiesGenerateGenerateSetButton_Click( object sender, EventArgs e )
        {
            if ( wfPropertiesGenerateCurveScaffold2TextBox.Text == SharedConstants.CURVE_PATTERN_SCAFFOLD_DEFAULT_TEXT ) {
                string text = SharedConstants.GENERATE_SET_BUTTON_PREREQUISITE_WARNING_TEXT;
                string caption = SharedConstants.GENERATE_SET_BUTTON_PREREQUISITE_WARNING_CAPTION;
                string methodName = "PI.WfMainWindow.wfPropertiesGenerateGenerateSetButton_Click(sender, e)";
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop, methodName );
                return;
            }

            GrabPreSetsForCurvesGeneration();
            GenerateAndShowPatternCurve();
            // TODO: generate set of curves
        }

        private void GrabPreSetsForCurvesGeneration()
        {
            string methodName = "PI.WfMainWindow.GrabPreSetsForCurvesGeneration()";
            PreSets.NumberOfCurves = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfCurves1NumericUpDown, methodName );
            PreSets.NumberOfPoints = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown, methodName );
            PreSets.StartingXPoint = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateStartingXPointNumericUpDown, methodName );
        }

        private void UpdateComponentRelatedWithChartsInterval()
        {
            string invoker = "PI.WfMainWindow.UpdateComponentRelatedWithChartsInterval()";
            int lowerLimit = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateStartingXPointNumericUpDown, invoker );
            int numberOfPoints = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown, invoker );
            int upperLimit = lowerLimit + numberOfPoints - 1;
            string intervalText = '<' + lowerLimit.ToString() + ';' + upperLimit.ToString() + '>';
            wfPropertiesGenerateInterval2TextBox.Text = intervalText;
        }

        private void GenerateAndShowPatternCurve()
        {
            if ( ChartsCurvesDataset.GeneratePatternCurve( PreSets.ChosenPatternCurveScaffold, PreSets.NumberOfPoints, PreSets.StartingXPoint ) ) {
                wfChartsPatternCurve.Series.Clear();
                wfChartsPatternCurve.Series.Add( ChartsCurvesDataset.PatternCurveChartingSeries );
                wfChartsPatternCurve.Series[0].BorderWidth = 3;
                wfChartsPatternCurve.Visible = true;
                wfChartsPatternCurve.Invalidate();
            }

        }

        private void WfPropertiesGenerateNumberOfPointsNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            UpdateComponentRelatedWithChartsInterval();
            SetRangeForComponentsRelatedWithOperationTypeInOrdinatesEditControl();
        }

        private void wfPropertiesGenerateStartingXPointNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            UpdateComponentRelatedWithChartsInterval();
        }

        private void UpdateComponentRelatedWithDotNetFrameworkVersion()
        {
            string invoker = "PI.WfMainWindow.UpdateComponentRelatedWithDotNetFrameworkVersion()";
            string dotNetVersion = SystemInfoHelper.ObtainUsedDotNetFrameworkVersion( invoker );

            if ( dotNetVersion == null ) {
                wfPropertiesProgramDotNetFramework2TextBox.Text = SharedConstants.PROGRAM_INFO_OBTAINING_ERROR_TEXT;
                return;
            }

            wfPropertiesProgramDotNetFramework2TextBox.Text = SystemInfoHelper.ObtainUsedDotNetFrameworkVersion( invoker );

        }

        private void UpdateComponentRelatedWithOSVersionName()
        {
            string invoker = "PI.WfMainWindow.UpdateComponentRelatedWithOSVersionName()";
            string osVersion = SystemInfoHelper.ObtaingApplicationRunningOSVersion( invoker );

            if ( osVersion == null ) {
                wfPropertiesProgramOSVersion2TextBox.Text = SharedConstants.PROGRAM_INFO_OBTAINING_ERROR_TEXT;
                return;
            }

            wfPropertiesProgramOSVersion2TextBox.Text = osVersion;
        }

        private void wfPropertiesDatasheetOperationTypeComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            string invoker = "PI.WfMainWindow.wfPropertiesDatasheetOperationTypeComboBox_SelectedIndexChanged(sender, e)";
            int selectedIndex = WindowsFormsHelper.GetSelectedIndexSafe( wfPropertiesDatasheetOperationTypeComboBox, invoker );

            switch ( selectedIndex ) {
            case SharedConstants.DATASET_CURVE_OPERATION_TYPE_OVERRIDING:
                wfPropertiesDatasheetPointIndexNumericUpDown.Enabled = true;
                wfPropertiesDatasheetPointIndexTrackBar.Enabled = true;
                break;
            default:
                wfPropertiesDatasheetPointIndexNumericUpDown.Enabled = false;
                wfPropertiesDatasheetPointIndexTrackBar.Enabled = false;
                break;
            }
        }

        private void SetRangeForComponentsRelatedWithOperationTypeInOrdinatesEditControl()
        {
            string invoker = "PI.WfMainWindow.SetRangeForComponentsRelatedWithOperationTypeInOrdinatesEditControl()";
            int numberOfPoints = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesGenerateNumberOfPointsNumericUpDown, invoker );
            wfPropertiesDatasheetPointIndexNumericUpDown.Minimum = 1;
            wfPropertiesDatasheetPointIndexNumericUpDown.Maximum = numberOfPoints;
            wfPropertiesDatasheetPointIndexTrackBar.Minimum = 1;
            wfPropertiesDatasheetPointIndexTrackBar.Maximum = numberOfPoints;
        }

        private void wfPropertiesDatasheetPointIndexNumericUpDown_ValueChanged( object sender, EventArgs e )
        {
            string invoker = "PI.WfMainWindow.wfPropertiesDatasheetPointIndexNumericUpDown_ValueChanged(sender, e)";
            int index = WindowsFormsHelper.GetValueFromNumericUpDown<int>( wfPropertiesDatasheetPointIndexNumericUpDown, invoker );
            WindowsFormsHelper.SetValueForTrackBar( wfPropertiesDatasheetPointIndexTrackBar, index, invoker );
        }

        private void wfPropertiesDatasheetPointIndexTrackBar_Scroll( object sender, EventArgs e )
        {
            string invoker = "PI.WfMainWindow.wfPropertiesDatasheetPointIndexTrackBar_Scroll(sender, e)";
            int index = WindowsFormsHelper.GetValueFromTrackBar( wfPropertiesDatasheetPointIndexTrackBar, invoker );
            WindowsFormsHelper.SetValueForNumericUpDown( wfPropertiesDatasheetPointIndexNumericUpDown, index, invoker );
        }

        private void WfPropertiesDatasheetRefreshButton_Click( object sender, EventArgs e )
        {
            string invoker = "PI.WfMainWindow.WfPropertiesDatasheetRefreshButton_Click(sender, e)";
            int chosenDatasetCurveType = WindowsFormsHelper.GetSelectedIndexSafe( wfPropertiesDatasheetCurveTypeComboBox, invoker );

            switch ( chosenDatasetCurveType ) {
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_PATTERN:
                RefreshComponentRelatedWithDatasheetTabDataForPatternCurve();
                break;
            case SharedConstants.DATASET_CURVE_TYPE_CONTROL_GENERATED:
                // TODO: implement
                break;
            default:
                string text = SharedConstants.DATASET_CURVE_TYPE_CONTROL_NOT_SELECTED_TEXT;
                string caption = SharedConstants.DATASET_CURVE_TYPE_CONTROL_NOT_SELECTED_CAPTION;
                WindowsFormsHelper.ShowMessageBoxSafe( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, invoker );
                break;
            }

        }

        [Obsolete( "Pending" )]
        private void RefreshComponentRelatedWithDatasheetTabDataForPatternCurve()
        {
            const int ROW_NUMBER_FROM_END = 3;
            int currentNumberOfRowsForOrdinates = ChartsCurvesDataset.PatternCurveChartingSeries.Points.Count;

            for ( int i = 0; i < PreSets.PreviousNumberOfRowsForOrdinates; i++ ) {
                DeleteXYComponentsInDatasheetTab( ROW_NUMBER_FROM_END );
                DeleteRowInDatasheetTab( ROW_NUMBER_FROM_END );
            }

            for ( int i = 0; i < currentNumberOfRowsForOrdinates; i++ ) {
                AddRowInDatasheetTab( ROW_NUMBER_FROM_END );
                AddXYComponentsInDatasheetTab( ROW_NUMBER_FROM_END, i );
            }

            PreSets.PreviousNumberOfRowsForOrdinates = currentNumberOfRowsForOrdinates;
        }

        [Obsolete( "Pending" )]
        private void DeleteXYComponentsInDatasheetTab( int rowNumberCountedFromEnd )
        {
            int index = wfPropertiesDatasheetTableLayoutPanel.RowCount - rowNumberCountedFromEnd;
            wfPropertiesDatasheetTableLayoutPanel.Controls.RemoveAt( index );
        }

        [Obsolete( "Pending" )]
        private void DeleteRowInDatasheetTab( int rowNumberCountedFromEnd )
        {
            int lastRowIndex = wfPropertiesDatasheetTableLayoutPanel.RowCount - rowNumberCountedFromEnd;
            wfPropertiesDatasheetTableLayoutPanel.RowStyles.RemoveAt( lastRowIndex );
            wfPropertiesDatasheetTableLayoutPanel.RowCount--;
        }

        [Obsolete( "Pending" )]
        private void AddRowInDatasheetTab( int rowNumberCountedFromEnd )
        {
            int validRow = wfPropertiesDatasheetTableLayoutPanel.RowCount - rowNumberCountedFromEnd;
            RowStyle previousRowStyle = wfPropertiesDatasheetTableLayoutPanel.RowStyles[validRow];
            wfPropertiesDatasheetTableLayoutPanel.RowCount++;
            wfPropertiesDatasheetTableLayoutPanel.RowStyles.Add( new RowStyle( previousRowStyle.SizeType, previousRowStyle.Height ) );
        }

        [Obsolete( "Pending" )]
        private void AddXYComponentsInDatasheetTab( int rowNumberCountedFromEnd, int indexOfPoint )
        {
            TextBox textBoxForXAxis = new TextBox() {
                Text = ChartsCurvesDataset.PatternCurveChartingSeries.Points[indexOfPoint].XValue.ToString(),
                TextAlign = HorizontalAlignment.Right,
                Enabled = true,
                ReadOnly = true,
                Multiline = false,
                Dock = DockStyle.Fill
            };

            TextBox textBoxForYAxis = new TextBox() {
                Text = ChartsCurvesDataset.PatternCurveChartingSeries.Points[indexOfPoint].YValues[0].ToString(),
                TextAlign = HorizontalAlignment.Right,
                Enabled = true,
                ReadOnly = true,
                Multiline = false,
                Dock = DockStyle.Fill
            };

            int validRow = wfPropertiesDatasheetTableLayoutPanel.RowCount - rowNumberCountedFromEnd;
            wfPropertiesDatasheetTableLayoutPanel.Controls.Add( textBoxForXAxis, 0, validRow );
            wfPropertiesDatasheetTableLayoutPanel.Controls.Add( textBoxForYAxis, 1, validRow );
        }
    }

}
