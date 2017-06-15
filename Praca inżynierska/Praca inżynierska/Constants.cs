﻿using System.ComponentModel;

namespace PI
{
    [Description("Do not explicitly instantiate any class in this scope.")]
    internal static class Constants
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
            internal const int ENCODER_FALLBACK = 12;
            internal const int INVALID_ENUM_ARGUMENT = 13;
            internal const int INVALID_OPERATION = 14;
            internal const int THREAD_STATE = 15;
            internal const int OUT_OF_MEMORY = 16;
        }

        internal static class Ui
        {
            internal static class Panel
            {
                internal static class Generate
                {
                    internal const int SCAFFOLD_POLYNOMIAL = 0;
                    internal const int SCAFFOLD_HYPERBOLIC = 1;
                    internal const string SCAFFOLD_POLYNOMIAL_TEXT = "Polynomial";
                    internal const string SCAFFOLD_HYPERBOLIC_TEXT = "Hyperbolic";
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
