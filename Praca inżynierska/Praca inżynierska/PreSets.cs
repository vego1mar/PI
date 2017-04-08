namespace PI
    {
    static class PreSets
        {

        #region Pattern Curve Definer dialog
        internal static int ChosenPatternCurveScaffold { get; set; } = SharedConstants.CURVE_PATTERN_SCAFFOLD_POLYNOMIAL;
        internal static double ParameterA { get; set; } = 1.0;
        internal static double ParameterB { get; set; } = 1.0;
        internal static double ParameterC { get; set; } = 1.0;
        internal static double ParameterD { get; set; } = 1.0;
        internal static double ParameterE { get; set; } = 1.0;
        internal static double ParameterF { get; set; } = 1.0;
        #endregion

        #region WfMainWindow.Properties
        internal static int NumberOfCurves { get; set; } = 1;
        #endregion

        }
    }
