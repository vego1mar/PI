using System;
using System.Collections.Generic;

namespace PI.src.general
{
    public static partial class Averages
    {
        private static double? GeometricMeanOfSign( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            double product = Lists.Product( set );

            if ( product < 0.0 ) {
                return -Mathematics.Root( Math.Abs( product ), set.Count );
            }

            return Mathematics.Root( product, set.Count );
        }

        private static double? GeometricMeanOfParity( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            double product = Lists.Product( set );

            if ( set.Count % 2 == 0 ) {
                return Mathematics.Root( Math.Abs( product ), set.Count );
            }

            return Mathematics.Root( product, set.Count );
        }

        private static double? GeometricMeanOfAbsolute( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            return Mathematics.Root( Math.Abs( Lists.Product( set ) ), set.Count );
        }

        private static double? GeometricMeanOfOffset( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            double min = Math.Abs( Minimum( set ).Value ) + 1.0;
            IList<double> dataset = Lists.GetCopy( set );
            Lists.Add( dataset, min );
            return Mathematics.Root( Lists.Product( dataset ), dataset.Count ) - min;
        }
    }
}
