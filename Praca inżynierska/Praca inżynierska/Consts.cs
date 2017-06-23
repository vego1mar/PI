namespace PI
{
    internal static class Consts
    {

        internal static class Exceptions
        {
            internal const int OBJECT_DISPOSED = 1;
            internal const int IO = 2;
            internal const int EXCEPTION = 3;
            internal const int NOT_SUPPORTED = 4;
            internal const int ARGUMENT_NULL = 5;
            internal const int ARGUMENT_OUT_OF_RANGE = 6;
            internal const int ARGUMENT = 7;
            internal const int DIRECTORY_NOT_FOUND = 8;
            internal const int PATH_TOO_LONG = 9;
            internal const int SECURITY = 10;
            internal const int UNAUTHORIZED_ACCESS = 11;
            internal const int INVALID_ENUM_ARGUMENT = 12;
            internal const int INVALID_OPERATION = 13;
            internal const int THREAD_STATE = 14;
            internal const int OUT_OF_MEMORY = 15;
        }

        internal static class Ui
        {
            internal static class Panel
            {
                internal static class Generate
                {
                    internal const int SCAFFOLD_POLYNOMIAL = 0;
                    internal const int SCAFFOLD_HYPERBOLIC = 1;
                    internal const int SCAFFOLD_WAVEFORM = 2;
                    internal const int SCAFFOLD_WAVE_SINE = -21;
                    internal const int SCAFFOLD_WAVE_SQUARE = -22;
                    internal const int SCAFFOLD_WAVE_TRIANGLE = -23;
                    internal const int SCAFFOLD_WAVE_SAWTOOTH = -24;
                    internal const string SCAFFOLD_POLYNOMIAL_TEXT = "Polynomial";
                    internal const string SCAFFOLD_HYPERBOLIC_TEXT = "Hyperbolic";
                    internal const string SCAFFOLD_WAVEFORM_TEXT = "Waveform";
                    internal const string SCAFFOLD_DEFAULT_TEXT = "Not chosen";
                    internal const string GENERATE_SET_BTN_PREREQUISITE_WARNING_TEXT = "Pattern curve scaffold has not been chosen.";
                    internal const string GENERATE_SET_BTN_PREREQUISITE_WARNING_CAPTION = "Prerequisite not matched!";
                }

                internal static class Datasheet
                {
                    internal const int CURVE_TYPE_GENERATED = 0;
                    internal const int CURVE_TYPE_PATTERN = 1;
                    internal const string CURVE_TYPE_NOT_SELECTED_TEXT = "You must choose a curve type from 'Dataset control' section.";
                    internal const string CURVE_TYPE_NOT_SELECTED_CAPTION = "Prerequisite not matched!";
                    internal const string SELECTED_CURVE_SERIES_TEXT = "Any curve has not been generated yet.";
                    internal const string SELECTED_CURVE_SERIES_CAPTION = "Data selection problem";
                }

                internal static class Program
                {
                    internal const string TIMER_START_FAILURE = "Failure";
                    internal const string TIMER_START_SUCCESS = "Success";
                    internal const string INFO_OBTAINING_ERR_TEXT = "Error. See log.";
                }
            }

            internal static class Charts
            {
                internal const string REFRESHING_ERR_TEXT = "A problem occured when trying to refresh a chart or recalculating axes scales.";
                internal const string REFRESHING_ERR_CAPTION = "Invalidating error";
                internal const string GENERATING_WARN_TEXT = "Some of the generated points are not valid to display them on a chart. Those points will be removed from the set.";
                internal const string GENERATING_WARN_CAPTION = "Generating points warning";
            }

            internal static class Menu
            {

                internal static class Update
                {
                    internal const string DOWNLOADING_UPDATE_INFO_ERR_TEXT = "Cannot download update info due to some error.";
                    internal const string DOWNLOADING_UPDATE_INFO_ERR_CAPTION = "Web connection problem";
                    internal const string RUNNING_LATEST_APP_TEXT = "You're running the latest version.";
                    internal const string RUNNING_LATEST_APP_CAPTION = "Up-to-date";
                    internal const string RUNNING_OBSOLETE_APP_TEXT = "There is a newer version of this app. Visit https://github.com/vego1mar/PI.";
                    internal const string RUNNING_OBSOLETE_APP_CAPTION = "Update available";
                    internal const string MATCHING_VERSIONS_ERR_TEXT = "An exception occured during matching current and latest versions.";
                    internal const string MATCHING_VERSIONS_ERR_CAPTION = "Update info parsing error";
                }

            }

        }

        internal static class Dsv
        {
            internal static class Panel
            {
                internal const int OPERATION_TYPE_OVERRIDING = 0;
                internal const int OPERATION_TYPE_ADDITION = 1;
                internal const int OPERATION_TYPE_SUBSTRACTION = 2;
                internal const int OPERATION_TYPE_MULTIPLICATION = 3;
                internal const int OPERATION_TYPE_DIVISION = 4;
                internal const int OPERATION_TYPE_EXPONENTIATION = 5;
                internal const int OPERATION_TYPE_LOGARITHMIC = 6;
                internal const int OPERATION_TYPE_ROOTING = 7;
                internal const int OPERATION_TYPE_CONSTANT = 8;
                internal const int OPERATION_TYPE_POSITIVE = 9;
                internal const int OPERATION_TYPE_NEGATIVE = 10;
                internal const string OPERATION_TYPE_NOT_SELECTED_TEXT = "You must choose an operation type to allow performing changes.";
                internal const string OPERATION_TYPE_NOT_SELECTED_CAPTION = "Edit control for ordinates prerequisite";
                internal const string USER_VALUE_NOT_VALID_TEXT = "The given value is improper. Please note, that the value format is localized by default.";
                internal const string USER_VALUE_NOT_VALID_CAPTION = "Wrong cast or conversion";
                internal const string EDIT_CONTROL_VALUE_TEXT = "Value:";
                internal const string EDIT_CONTROL_ADDEND_TEXT = "Addend:";
                internal const string EDIT_CONTROL_SUBTRAHEND_TEXT = "Subtrahend:";
                internal const string EDIT_CONTROL_MULTIPLIER_TEXT = "Multiplier:";
                internal const string EDIT_CONTROL_DIVISOR_TEXT = "Divisor:";
                internal const string EDIT_CONTROL_EXPONENT_TEXT = "Exponent:";
                internal const string EDIT_CONTROL_BASE_TEXT = "Base:";
                internal const string EDIT_CONTROL_LEVEL_TEXT = "Level:";
                internal const string NOT_VALID_DECIMAL_CHART_NUMBER_TEXT = "At least one value of the performed operation is overflowed. All changes will be ignored.";
                internal const string NOT_VALID_DECIMAL_CHART_NUMBER_CAPTION = "Data type overflow";
                internal const string OPERATION_ERR_OVERFLOW_TEXT = "Overflow:";
            }
        }

        internal static class Gprv
        {
            internal static class Panel
            {
                internal const string ADDEND_TXT = "Addend:";
                internal const string SUBTRAHEND_TXT = "Subtrahend:";
                internal const string MULTIPLIER_TXT = "Multiplier:";
                internal const string DIVISOR_TXT = "Divisor:";
                internal const string EXPONENT_TXT = "Exponent:";
                internal const string BASIS_TXT = "Basis:";
                internal const string VALUE_TXT = "Value:";
                internal const string NOT_APPLICABLE_ABBR_TXT = "N/A:";
                internal const string IDX_GREATER_THAN_ALLOWED_TXT = "Specified index would be greater than allowed. Change rejected.";
                internal const string IDX_GREATER_THAN_ALLOWED_CPT = "Improper index value";
                internal const string IDX_LOWER_THAN_ALLOWED_TXT = "Specified index would be lower than allowed. Change revoked.";
                internal const string IDX_LOWER_THAN_ALLOWED_CPT = "Improper index value";
                internal const string USER_VALUE_IMPROPER_TXT = "The given value is improper. Note, that numbers should be localized.";
                internal const string USER_VALUE_IMPROPER_CPT = "Wrong cast, conversion or format";
                internal const string OPERATION_ERR_TXT = "Cannot calculate values for all specified arguments. Overflow. Changes rejected.";
                internal const string OPERATION_ERR_CPT = "Exception during calculation";
                internal const string GPRV_LOADED_TXT = "Grid Previewer loaded";
                internal const string CHANGES_SAVED_TXT = "Changes saved";
                internal const string PERFORMED_AND_REFRESHED_TXT = "Performed & refreshed";
                internal const string OPERATION_REJECTED_TXT = "Operation rejected";
                internal const string OPERATION_REVOKED_TXT = "Operation revoked";
                internal const string INVL_USER_VALUE_TXT = "Invalid user value";
                internal const string INVL_CURVE_POINTS_TXT = "At least one of points value is too large or improper to be displayed on a chart.";
                internal const string INVL_CURVE_POINTS_CPT = "Invalid curve points";
                internal const string VALUES_RESTORED_TXT = "Values restored";
            }

            internal static class Chart
            {
                internal const string REFRESH_ERR_TXT = "Chart refreshing error, possibly recalculating axes scales (too large to display).";
                internal const string REFRESH_ERR_CPT = "Invalid operation";
                internal const string CHART_REFRESHED_TXT = "Chart refreshed";
                internal const string CHART_NOT_REPAINTED_TXT = "Chart not repainted";
                internal const string CHART_REFRESH_ERR_TXT = "Chart refresh error";
            }
        }

        internal static class Pcd
        {
            internal static class Hyperbolic
            {
                internal const string PARAMS_ZERO_DIVISION_TEXT = "Cannot divide by 0.0000. A value of 0.0001 will be used instead.";
                internal const string PARAMS_ZERO_DIVISION_CAPTION = "Division by zero";
            }
        }

    }

}
