﻿using System.ComponentModel;

namespace PI
{

    [Description( "Do not explicitly instantiate any class in this scope." )]
    internal static class PreSets
    {

        internal static class Pcd
        {
            internal static int ChosenScaffold { get; set; } = Constants.Ui.Panel.Generate.SCAFFOLD_POLYNOMIAL;

            internal static class Parameters
            {
                internal static double A { get; set; } = 1.0;
                internal static double B { get; set; } = 1.0;
                internal static double C { get; set; } = 1.0;
                internal static double D { get; set; } = 1.0;
                internal static double E { get; set; } = 1.0;
                internal static double F { get; set; } = 1.0;
                internal static double I { get; set; } = 0.0;
                internal static double G { get; set; } = 1.0;
                internal static double J { get; set; } = 0.0;
            }

        }

        internal static class Ui
        {
            internal static int NumberOfCurves { get; set; } = 1;
            internal static int NumberOfPoints { get; set; } = 1;
            internal static int StartingXPoint { get; set; } = 1;
        }

    }
}
