using PI.src.enumerators;
using PI.src.general;
using PI.src.parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PI.src.averaging
{
    public static partial class Averages
    {
        public static double? Median( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            IList<double> sortedSet = Lists.GetSorted( set );
            int oddIndex = sortedSet.Count / 2;

            if ( sortedSet.Count % 2 == 0 ) {
                return (sortedSet[oddIndex - 1] + sortedSet[oddIndex]) / 2.0;
            }

            return sortedSet[oddIndex];
        }

        public static double? Maximum( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            return set.Max();
        }

        public static double? Minimum( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            return set.Min();
        }

        public static double? Arithmetic( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            return Lists.Sum( set ) / Convert.ToDouble( set.Count );
        }

        public static double? Geometric( IList<double> set, GeometricMeanVariant variant )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            switch ( variant ) {
            case GeometricMeanVariant.Sign:
                return GeometricMeanOfSign( set );
            case GeometricMeanVariant.Parity:
                return GeometricMeanOfParity( set );
            case GeometricMeanVariant.Absolute:
                return GeometricMeanOfAbsolute( set );
            case GeometricMeanVariant.Offset:
                return GeometricMeanOfOffset( set );
            }

            return null;
        }

        public static double? AGM( IList<double> set, GeometricMeanVariant variant )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            return AGM( set, variant, set.Count );
        }

        public static double? Heronian( IList<double> set, GeometricMeanVariant variant )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            double weightedAM = (Convert.ToDouble( set.Count ) / (set.Count + 1)) * Arithmetic( set ).Value;
            double weightedGM = Geometric( set, variant ).Value / (set.Count + 1);
            return weightedAM + weightedGM;
        }

        public static double? Harmonic( IList<double> set, StandardMeanVariants variant )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            switch ( variant ) {
            case StandardMeanVariants.Straight:
                return HarmonicOfStraight( set );
            case StandardMeanVariants.Offset:
                return HarmonicOfOffset( set );
            }

            return null;
        }

        public static double? Generalized( IList<double> set, StandardMeanVariants variant, int rank )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            switch ( variant ) {
            case StandardMeanVariants.Straight:
                return GeneralizedOfStraight( set, rank );
            case StandardMeanVariants.Offset:
                return GeneralizedOfOffset( set, rank );
            }

            return null;
        }

        public static double? SMA( IList<double> set )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            return MovingOfSimple( set );
        }

        public static double? Tolerance( IList<double> set, double classifier, MeanType finisher = MeanType.Harmonic, MeansParameters @params = null )
        {
            if ( set == null || set.Count == 0 ) {
                return null;
            }

            if ( @params == null ) {
                @params = new MeansParameters();
            }

            IList<double> acceptables = new List<double>();
            IList<double> oscillators = Lists.GetCopy( set );
            Lists.Subtract( oscillators, Median( set ).Value );

            for ( int i = 0; i < set.Count; i++ ) {
                if ( Math.Abs( oscillators[i] ) < Math.Abs( classifier ) ) {
                    acceptables.Add( set[i] );
                }
            }

            switch ( finisher ) {
            case MeanType.Median:
                return Median( acceptables );
            case MeanType.Maximum:
                return Maximum( acceptables );
            case MeanType.Minimum:
                return Minimum( acceptables );
            case MeanType.Arithmetic:
                return Arithmetic( acceptables );
            case MeanType.Geometric:
                return Geometric( acceptables, @params.Geometric.Variant );
            case MeanType.AGM:
                return AGM( acceptables, @params.AGM.Variant );
            case MeanType.Heronian:
                return Heronian( acceptables, @params.Heronian.Variant );
            case MeanType.Harmonic:
                return Harmonic( acceptables, @params.Harmonic.Variant );
            case MeanType.Generalized:
                return Generalized( acceptables, @params.Generalized.Variant, @params.Generalized.Rank );
            case MeanType.SMA:
                return SMA( acceptables );
            case MeanType.Central:
                return Central( acceptables, @params.Central.IntervalDivisions, @params.Central.MassPercent );
            }

            return null;
        }

        public static double? Central( IList<double> set, int intervalDivisions = 10, short massPercent = 50 )
        {
            if ( set == null || set.Count == 0 || intervalDivisions <= 0 || massPercent < 10 || massPercent > 90 ) {
                return null;
            }

            IList<double> mass = Lists.GetSelected( 
                Lists.GetSorted( set ),
                Convert.ToInt32( ((100 - massPercent) / 2.0) * 0.01 * set.Count ),
                Convert.ToInt32( (100 - ((100 - massPercent) / 2.0)) * 0.01 * set.Count )
                );

            return Harmonic( mass, StandardMeanVariants.Offset );
        }

        public static IList<double> Median( IList<IList<double>> orderedSet )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> medianas = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                medianas.Add( Median( orderedSet[x] ).Value );
            }

            return medianas;
        }

        public static IList<double> Maximum( IList<IList<double>> orderedSet )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> maximums = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                maximums.Add( Maximum( orderedSet[x] ).Value );
            }

            return maximums;
        }

        public static IList<double> Minimum( IList<IList<double>> orderedSet )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> minimums = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                minimums.Add( Minimum( orderedSet[x] ).Value );
            }

            return minimums;
        }

        public static IList<double> Arithmetic( IList<IList<double>> orderedSet )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> arithmetics = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                arithmetics.Add( Arithmetic( orderedSet[x] ).Value );
            }

            return arithmetics;
        }

        public static IList<double> Geometric( IList<IList<double>> orderedSet, GeometricMeanVariant variant )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> geometrics = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                geometrics.Add( Geometric( orderedSet[x], variant ).Value );
            }

            return geometrics;
        }

        public static IList<double> AGM( IList<IList<double>> orderedSet, GeometricMeanVariant variant )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> agms = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                agms.Add( AGM( orderedSet[x], variant ).Value );
            }

            return agms;
        }

        public static IList<double> Heronian( IList<IList<double>> orderedSet, GeometricMeanVariant variant )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> heronians = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                heronians.Add( Heronian( orderedSet[x], variant ).Value );
            }

            return heronians;
        }

        public static IList<double> Harmonic( IList<IList<double>> orderedSet, StandardMeanVariants variant )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> harmonics = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                harmonics.Add( Harmonic( orderedSet[x], variant ).Value );
            }

            return harmonics;
        }

        public static IList<double> Generalized( IList<IList<double>> orderedSet, StandardMeanVariants variant, int rank )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> means = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                means.Add( Generalized( orderedSet[x], variant, rank ).Value );
            }

            return means;
        }

        public static IList<double> SMA( IList<IList<double>> orderedSet )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> movingAverages = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                movingAverages.Add( SMA( orderedSet[x] ).Value );
            }

            return movingAverages;
        }

        public static IList<double> Tolerance( IList<IList<double>> orderedSet, double tolerance, MeanType finisher = MeanType.Harmonic, MeansParameters @params = null )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            double comparer = Median( Maximum( orderedSet ) ).Value - Median( Minimum( orderedSet ) ).Value;
            double classifier = comparer * tolerance;
            IList<double> tolerants = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                tolerants.Add( Tolerance( orderedSet[x], classifier, finisher, @params ).Value );
            }

            return tolerants;
        }

        public static IList<double> Central( IList<IList<double>> orderedSet, int intervalDivisions = 10, short massPercent = 50 )
        {
            if ( orderedSet == null || orderedSet.Count == 0 ) {
                return new List<double>().AsReadOnly();
            }

            IList<double> centrals = new List<double>();

            for ( int x = 0; x < orderedSet.Count; x++ ) {
                centrals.Add( Central( orderedSet[x], intervalDivisions, massPercent ).Value );
            }

            return centrals;
        }
    }
}
