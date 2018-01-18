﻿using PI.src.enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PI.src.general
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
    }
}
