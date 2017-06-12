namespace PI
{
    internal static class PreSets
    {

        internal static class Pcd
        {
            internal static int ChosenScaffold { get; set; } = Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL;
            internal static double ParameterA { get; set; } = 1.0;
            internal static double ParameterB { get; set; } = 1.0;
            internal static double ParameterC { get; set; } = 1.0;
            internal static double ParameterD { get; set; } = 1.0;
            internal static double ParameterE { get; set; } = 1.0;
            internal static double ParameterF { get; set; } = 1.0;
            internal static double ParameterG { get; set; } = 1.0;
        }

        internal static class Ui
        {
            internal static int NumberOfCurves { get; set; } = 1;
            internal static int NumberOfPoints { get; set; } = 1;
            internal static int StartingXPoint { get; set; } = 1;
        }

    }
}
