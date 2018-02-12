using PI.src.averaging.kernels;
using PI.src.enumerators;
using PI.src.general;
using System.Collections.Generic;

namespace PI.src.averaging.means
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

        public static IList<double> NadarayaWatson( IList<IList<double>> orderedSet, NadarayaWatsonVariant variant, KernelType kernelType, double kernelSize )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            KernelsCommander kernel = new KernelsCommander( kernelType );
            IList<double> set = new List<double>();
            double numerator = 0.0;
            double denominator = 0.0;
            double currentWeight;

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                for ( int i = 0; i < orderedSet.Count; i++ ) {
                    currentWeight = kernel.Execute( orderedSet[x], orderedSet[i], kernelSize );
                    numerator += currentWeight * GetNadarayaWatsonMiddlePoint( orderedSet[i], variant );
                    denominator += currentWeight;
                }

                set.Add( numerator / denominator );
                numerator = 0.0;
                denominator = 0.0;
            }

            return set;
        }

        private static double GetNadarayaWatsonMiddlePoint( IList<double> source, NadarayaWatsonVariant variant )
        {
            switch ( variant ) {
            case NadarayaWatsonVariant.Subsitution:
                return (Averages.Central( source, 50 ).Value + Averages.Median( source ).Value) / 2.0;
            case NadarayaWatsonVariant.NoiseCoursing:
                return (Averages.Maximum( source ).Value - Averages.Minimum( source ).Value) / 2.0;
            }

            return double.NaN;
        }
    }
}
