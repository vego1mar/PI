using System.Collections.Generic;
using System.Linq;

namespace PI.src.general
{
    public static class Averages
    {
        private const double DEFAULT_VALUE_AT_NULL = -1.0;

        public static double Median( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return DEFAULT_VALUE_AT_NULL;
            }

            IList<double> sortedSet = Lists.GetSorted( set );
            int oddIndex = sortedSet.Count / 2;

            if ( sortedSet.Count % 2 == 0 ) {
                return (sortedSet[oddIndex - 1] + sortedSet[oddIndex]) / 2.0;
            }

            return sortedSet[oddIndex];
        }

        public static IList<double> Median( IList<IList<double>> orderedSet )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> medianas = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                medianas.Add( Median( orderedSet[x] ) );
            }

            return medianas;
        }

        public static double Maximum( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return DEFAULT_VALUE_AT_NULL;
            }

            return set.Max();
        }

        public static IList<double> Maximum( IList<IList<double>> orderedSet )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> maximums = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                maximums.Add( Maximum( orderedSet[x] ) );
            }

            return maximums;
        }
    }
}
