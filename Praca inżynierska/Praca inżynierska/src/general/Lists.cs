using System;
using System.Collections.Generic;
using System.Linq;

namespace PI.src.general
{
    public static class Lists
    {
        public static IList<IList<T>> Get<T>( int xLength, int yLength, T initialValue = default( T ) )
        {
            if ( xLength < 0 || yLength < 0 ) {
                return new List<IList<T>>().AsReadOnly();
            }

            IList<IList<T>> list = new List<IList<T>>();

            for ( int i = 0; i < xLength; i++ ) {
                list.Add( new List<T>() );

                for ( int j = 0; j < yLength; j++ ) {
                    list[i].Add( initialValue );
                }
            }

            return list;
        }

        public static IList<T> GetCopy<T>( IList<T> source )
        {
            if ( source == null ) {
                return new List<T>().AsReadOnly();
            }

            IList<T> copy = new List<T>();

            foreach ( T item in source ) {
                copy.Add( item );
            }

            return copy;
        }

        public static IList<T> GetSorted<T>( IList<T> source )
        {
            if ( source == null ) {
                return new List<T>().AsReadOnly();
            }

            return source.OrderBy( v => v ).ToList();
        }

        public static IList<IList<double>> GetSortedIntoHistogram( IList<double> source, int intervalDivisionsNo )
        {
            if ( source == null || intervalDivisionsNo <= 0 ) {
                return new List<IList<double>>().AsReadOnly();
            }

            double max = source.Max();
            double min = source.Min();
            double intervalLength = (max - min) / intervalDivisionsNo;
            double leftLimit = min - 1.0;
            double rightLimit = min + intervalLength;
            IList<IList<double>> resultSet = Get<double>( intervalDivisionsNo, 0 );

            for ( int i = 0; i < intervalDivisionsNo; i++ ) {
                foreach ( double value in source ) {
                    if ( value > leftLimit && value <= rightLimit ) {
                        resultSet[i].Add( value );
                    }
                }

                resultSet[i].OrderBy( v => v ).ToList();
                leftLimit = rightLimit;
                rightLimit = leftLimit + intervalLength;
            }

            return resultSet;
        }

        public static void Concat<T>( IList<T> target, IList<T> source )
        {
            if ( target == null || source == null ) {
                return;
            }

            foreach ( T value in source ) {
                target.Add( value );
            }
        }

        public static double Sum( IList<double> source )
        {
            if ( source == null || source.Count == 0 ) {
                return 0.0;
            }

            return source.Sum();
        }

        public static double Product( IList<double> source )
        {
            if ( source == null || source.Count == 0 ) {
                return 0.0;
            }

            return source.Aggregate( 1.0, ( accumulator, value ) => accumulator * value );
        }

        public static void Add( IList<double> target, double addend )
        {
            if ( target == null || target.Count == 0 ) {
                return;
            }

            for ( int i = 0; i < target.Count; i++ ) {
                target[i] += addend;
            }
        }

        public static void Subtract( IList<double> target, double subtrahend )
        {
            if ( target == null || target.Count == 0 ) {
                return;
            }

            for ( int i = 0; i < target.Count; i++ ) {
                target[i] -= subtrahend;
            }
        }

        public static void Reciprocal( IList<double> target )
        {
            if ( target == null || target.Count == 0 ) {
                return;
            }

            for ( int i = 0; i < target.Count; i++ ) {
                target[i] = Mathematics.Reciprocal( target[i] );
            }
        }

        public static void Exponentiate( IList<double> target, double exponent )
        {
            if ( target == null || target.Count == 0 ) {
                return;
            }

            for ( int i = 0; i < target.Count; i++ ) {
                target[i] = Math.Pow( target[i], exponent );
            }
        }
    }
}
