using PI.src.general;
using System.Collections.Generic;

namespace PI.src.averaging
{
    public static class Smoothers
    {
        public static IList<double> NearestNeighbors( IList<IList<double>> orderedSet, int kNN = 8 )
        {
            if ( orderedSet == null || orderedSet.Count == 0 || kNN <= 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<IList<double>> operative = new List<IList<double>>();
            Lists.Concat( operative, Lists.Get( kNN, 1, orderedSet[0][0] ) );
            Lists.Concat( operative, orderedSet );
            Lists.Concat( operative, Lists.Get( kNN, 1, 0.0 ) );
            IList<double> neighbors = new List<double>();
            IList<double> currentSet;

            for ( int i = kNN; i < orderedSet.Count + kNN; i++ ) {
                currentSet = Lists.GetCopyConcatenated( operative, i - kNN, i );
                neighbors.Add( Averages.Median( currentSet ).Value );
            }

            return neighbors;
        }
    }
}
