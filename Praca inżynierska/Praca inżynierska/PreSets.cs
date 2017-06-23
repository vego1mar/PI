using System.ComponentModel;

namespace PI
{

    [Description( "Do not explicitly instantiate any class in this scope." )]
    internal static class PreSets
    {

        internal static class Pcd
        {
            internal static int ChosenScaffold { get; set; } = Consts.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL;
            internal static Params Parameters { get; set; } = new Params();
        }

        internal static class Ui
        {
            internal static int NumberOfCurves { get; set; } = 5;
            internal static double StartingXPoint { get; set; } = -2.0;
            internal static double EndingXPoint { get; set; } = 2.0;
            internal static int PointsDensity { get; set; } = 200;
        }

    }
}
