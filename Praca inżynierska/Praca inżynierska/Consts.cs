namespace PI
{
    internal static class Consts
    {
        internal static UiConsts Ui { get; } = new UiConsts();
        internal static GridPreviewerConsts Gprv { get; } = new GridPreviewerConsts();
        internal static PcdConsts Pcd { get; } = new PcdConsts();

        internal class UiConsts
        {
            internal PanelConsts Panel { get; } = new PanelConsts();
            internal ChartsConsts Charts { get; } = new ChartsConsts();
            internal MenuConsts Menu { get; } = new MenuConsts();

            internal class PanelConsts
            {
                internal GenerateConsts Generate { get; } = new GenerateConsts();
                internal DatasheetConsts Datasheet { get; } = new DatasheetConsts();
                internal ProgramConsts Program { get; } = new ProgramConsts();

                internal class GenerateConsts
                {
                    internal string TxtPolynomial { get; } = "Polynomial";
                    internal string TxtHyperbolic { get; } = "Hyperbolic";
                    internal string TxtWaveform { get; } = "Waveform";
                    internal string TxtNotChosen { get; } = "Not chosen";
                    internal string GenerateSetBtnPrerequisiteWarnTxt { get; } = "Pattern curve scaffold has not been chosen.";
                    internal string GenerateSetBtnPrerequisiteWarnCpt { get; } = "Prerequisite not matched!";
                    internal string NotEnoughCurvesForMedianaTxt { get; } = "You must specify at least 3 curves to average them using mediana.";
                    internal string NotEnoughCurvesForMedianaCpt { get; } = "Averaging method problem";
                }

                internal class DatasheetConsts
                {
                    internal string CrvTypeNotSelectedTxt { get; } = "You must choose a curve type from 'Dataset control' section.";
                    internal string CrvTypeNotSelectedCpt { get; } = "Prerequisite not matched!";
                    internal string CrvSeriesSelectionTxt { get; } = "Any curve has not been generated yet.";
                    internal string CrvSeriesSelectionCpt { get; } = "Data selection problem";
                    internal string SpecifiedCrvDoesntExistTxt { get; } = "The specified curve data doesn't exist.";
                    internal string SpecifiedCrvDoesntExistCpt { get; } = "Invalid curve index";
                    internal string OperationMalformRejectedTxt { get; } = "Results of this operation was rejected due to improper values.";
                    internal string OperationMalformRejectedCpt { get; } = "Not supported operation";
                }

                internal class ProgramConsts
                {
                    internal string TxtFailure { get; } = "Failure";
                    internal string TxtSuccess { get; } = "Success";
                    internal string InfoObtainingErrTxt { get; } = "Error. See log.";
                }
            }

            internal class ChartsConsts
            {
                internal string RefreshingErrTxt { get; } = "A problem occured when trying to refresh a chart or recalculating axes scales.";
                internal string RefreshingErrCpt { get; } = "Invalidating error";
                internal string GeneratingWarnTxt { get; } = "Some of the generated points are not valid to display them on a chart. Those points will be removed from the set.";
                internal string GeneratingWarnCpt { get; } = "Generating points warning";
            }

            internal class MenuConsts
            {
                internal UpdateConsts Update { get; } = new UpdateConsts();

                internal class UpdateConsts
                {
                    internal string DownloadingUpdateInfoErrTxt { get; } = "Cannot download update info due to some error.";
                    internal string DownloadingUpdateInfoErrCpt { get; } = "Web connection problem";
                    internal string RunningLatestAppTxt { get; } = "You're running the latest version.";
                    internal string RunningLatestAppCpt { get; } = "Up-to-date";
                    internal string RunningObsoleteAppTxt { get; } = "There is a newer version of this app. Visit https://github.com/vego1mar/PI.";
                    internal string RunningObsoleteAppCpt { get; } = "Update available";
                    internal string MatchingVersionsErrTxt { get; } = "An exception occured during matching current and latest versions.";
                    internal string MatchingVersionsErrCpt { get; } = "Update info parsing error";
                }
            }

        }

        internal class GridPreviewerConsts
        {
            internal PanelConsts Panel { get; } = new PanelConsts();
            internal ChartConsts Chart { get; } = new ChartConsts();

            internal class PanelConsts
            {
                internal string TxtAddend { get; } = "Addend:";
                internal string TxtSubtrahend { get; } = "Subtrahend:";
                internal string TxtMultiplier { get; } = "Multiplier:";
                internal string TxtDivisor { get; } = "Divisor:";
                internal string TxtExponent { get; } = "Exponent:";
                internal string TxtBasis { get; } = "Basis:";
                internal string TxtValue { get; } = "Value:";
                internal string TxtNotApplicableAbbr { get; } = "N/A:";
                internal string IdxGreaterThanAllowedTxt { get; } = "Specified index would be greater than allowed. Change rejected.";
                internal string IdxGreaterThanAllowedCpt { get; } = "Improper index value";
                internal string IdxLowerThanAllowedTxt { get; } = "Specified index would be lower than allowed. Change revoked.";
                internal string IdxLowerThanAllowedCpt { get; } = "Improper index value";
                internal string UserValueImproperTxt { get; } = "The given value is improper. Note, that numbers should be localized.";
                internal string UserValueImproperCpt { get; } = "Wrong cast, conversion or format";
                internal string OperationErrTxt { get; } = "Cannot calculate values for all specified arguments. Overflow. Changes rejected.";
                internal string OperationErrCpt { get; } = "Exception during calculation";
                internal string TxtGridPreviewerLoaded { get; } = "Grid Previewer loaded";
                internal string TxtChangesSaved { get; } = "Changes saved";
                internal string TxtPerformedAndRefreshed { get; } = "Performed & refreshed";
                internal string TxtOperationRejected { get; } = "Operation rejected";
                internal string TxtOperationRevoked { get; } = "Operation revoked";
                internal string TxtInvalidUserValue { get; } = "Invalid user value";
                internal string InvalCurvePointsTxt { get; } = "At least one of points value is too large or improper to be displayed on a chart.";
                internal string InvalCurvePointsCpt { get; } = "Invalid curve points";
                internal string TxtValuesRestored { get; } = "Values restored";
            }

            internal class ChartConsts
            {
                internal string RefreshErrTxt { get; } = "Chart refreshing error, possibly recalculating axes scales (too large to display).";
                internal string RefreshErrCpt { get; } = "Invalid operation";
                internal string TxtChartRefreshed { get; } = "Chart refreshed";
                internal string TxtChartNotRepainted { get; } = "Chart not repainted";
                internal string TxtChartRefreshError { get; } = "Chart refresh error";
            }
        }

        internal class PcdConsts
        {
            internal HyperbolicConsts Hyperbolic { get; } = new HyperbolicConsts();

            internal class HyperbolicConsts
            {
                internal string ParamsZeroDivTxt { get; } = "Cannot divide by 0.0000. A value of 0.0001 will be used instead.";
                internal string ParamsZeroDivCpt { get; } = "Division by zero";
            }
        }

    }

}
