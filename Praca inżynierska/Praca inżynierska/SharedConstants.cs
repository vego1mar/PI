namespace PI
{
    public static class SharedConstants
    {

        #region Exceptions
        public const int OBJECT_DISPOSED_EXCEPTION = 1;
        public const int IO_EXCEPTION = 2;
        public const int EXCEPTION = 3;
        public const int NOT_SUPPORTED_EXCEPTION = 4;
        public const int ARGUMENT_NULL_EXCEPTION = 5;
        public const int ARGUMENT_OUT_OF_RANGE_EXCEPTION = 6;
        public const int ARGUMENT_EXCEPTION = 7;
        public const int DIRECTORY_NOT_FOUND_EXCEPTION = 8;
        public const int PATH_TOO_LONG_EXCEPTION = 9;
        public const int SECURITY_EXCEPTION = 10;
        public const int UNAUTHORIZED_ACCESS_EXCEPTION = 11;
        public const int ENCODER_FALLBACK_EXCEPTION = 12;
        public const int INVALID_ENUM_ARGUMENT_EXCEPTION = 13;
        public const int INVALID_OPERATION_EXCEPTION = 14;
        public const int THREAD_STATE_EXCEPTION = 15;
        public const int OUT_OF_MEMORY_EXCEPTION = 16;
        #endregion

        #region WfMainWindow.Properties.Generate
        public const int CURVE_PATTERN_SCAFFOLD_POLYNOMIAL = 0;
        public const int CURVE_PATTERN_SCAFFOLD_HYPERBOLIC = 1;
        public const string CURVE_PATTERN_SCAFFOLD_POLYNOMIAL_TEXT = "Polynomial";
        public const string CURVE_PATTERN_SCAFFOLD_HYPERBOLIC_TEXT = "Hyperbolic";
        public const string CURVE_PATTERN_SCAFFOLD_DEFAULT_TEXT = "Not chosen";
        public const string GENERATE_SET_BUTTON_PREREQUISITE_WARNING_TEXT = "Pattern curve scaffold has not been chosen.";
        public const string GENERATE_SET_BUTTON_PREREQUISITE_WARNING_CAPTION = "Prerequisite not matched!";
        #endregion

        #region WfMainWindow.Properties.Datasheet
        public const int DATASET_CURVE_TYPE_CONTROL_GENERATED = 0;
        public const int DATASET_CURVE_TYPE_CONTROL_PATTERN = 1;
        #endregion

        #region WfMainWindow.Properties.Program
        public const string TIMER_START_FAILURE = "Failure";
        public const string TIMER_START_SUCCESS = "Success";
        public const string PROGRAM_INFO_OBTAINING_ERROR_TEXT = "Error. See log.";
        #endregion

    }

}
