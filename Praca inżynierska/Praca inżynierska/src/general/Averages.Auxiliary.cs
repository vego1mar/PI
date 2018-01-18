using PI.src.enumerators;
using PI.src.helpers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PI.src.general
{
    public static partial class Averages
    {
        private static double? GeometricMeanOfSign( IList<double> set )
        {
            double product = Lists.Product( set );

            if ( product < 0.0 ) {
                return -Mathematics.Root( Math.Abs( product ), set.Count );
            }

            return Mathematics.Root( product, set.Count );
        }

        private static double? GeometricMeanOfParity( IList<double> set )
        {
            double product = Lists.Product( set );

            if ( set.Count % 2 == 0 ) {
                return Mathematics.Root( Math.Abs( product ), set.Count );
            }

            return Mathematics.Root( product, set.Count );
        }

        private static double? GeometricMeanOfAbsolute( IList<double> set )
        {
            return Mathematics.Root( Math.Abs( Lists.Product( set ) ), set.Count );
        }

        private static double? GeometricMeanOfOffset( IList<double> set )
        {
            double min = Math.Abs( Minimum( set ).Value ) + 1.0;
            IList<double> dataset = Lists.GetCopy( set );
            Lists.Add( dataset, min );
            return Mathematics.Root( Lists.Product( dataset ), dataset.Count ) - min;
        }

        private static double? AGM( IList<double> set, GeometricMeanVariant variant, int iterationsNo )
        {
            if ( iterationsNo < 0 ) {
                return null;
            }

            double previousAM = Arithmetic( set ).Value;
            double previousGM = Geometric( set, variant ).Value;
            double nextAM = default( double );
            double nextGM = default( double );
            IList<double> currentSet;

            for ( int i = 1; i < iterationsNo; i++ ) {
                currentSet = new List<double>() { previousAM, previousGM };
                nextAM = Arithmetic( currentSet ).Value;
                nextGM = Geometric( currentSet, variant ).Value;
                previousAM = nextAM;
                previousGM = nextGM;
            }

            return Convert.ToDouble( Strings.GetCommon( nextAM, nextGM ), CultureInfo.InvariantCulture );
        }

        private static double? HarmonicOfStraight( IList<double> set )
        {
            IList<double> reciprocals = Lists.GetCopy( set );
            Lists.Reciprocal( reciprocals );
            return set.Count / Lists.Sum( reciprocals );
        }

        private static double? HarmonicOfOffset( IList<double> set )
        {
            double absMin = Math.Abs( Minimum( set ).Value ) + 1.0;
            IList<double> reciprocals = Lists.GetCopy( set );
            Lists.Add( reciprocals, absMin );
            Lists.Reciprocal( reciprocals );
            return (set.Count / Lists.Sum( reciprocals )) - absMin;
        }

        private static double? GeneralizedOfStraight( IList<double> set, int rank )
        {
            IList<double> powers = Lists.GetCopy( set );
            Lists.Exponentiate( powers, rank );
            return Mathematics.Root( Lists.Sum( powers ) / set.Count, rank );
        }

        private static double? GeneralizedOfOffset( IList<double> set, int rank )
        {
            double absMin = Math.Abs( Minimum( set ).Value ) + 1.0;
            IList<double> powers = Lists.GetCopy( set );
            Lists.Add( powers, absMin );
            Lists.Exponentiate( powers, rank );
            return Mathematics.Root( Lists.Sum( powers ) / set.Count, rank ) - absMin;
        }
    }
}
