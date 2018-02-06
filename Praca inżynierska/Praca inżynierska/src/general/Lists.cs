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
                list.Add( Get( yLength, initialValue ) );
            }

            return list;
        }

        public static IList<T> Get<T>( int length, T initialValue = default( T ) )
        {
            if ( length < 0 ) {
                return new List<T>().AsReadOnly();
            }

            IList<T> list = new List<T>();

            for ( int i = 0; i < length; i++ ) {
                list.Add( initialValue );
            }

            return list;
        }

        public static IList<IList<T>> GetCloneable<T>( int xLength, int yLength, T initialValue = default( T ) ) where T : ICloneable
        {
            if ( xLength < 0 || yLength < 0 ) {
                return new List<IList<T>>().AsReadOnly();
            }

            IList<IList<T>> list = new List<IList<T>>();

            for ( int i = 0; i < xLength; i++ ) {
                list.Add( GetCloneable( yLength, initialValue ) );
            }

            return list;
        }

        public static IList<T> GetCloneable<T>( int length, T initialValue = default( T ) ) where T : ICloneable
        {
            if ( length < 0 ) {
                return new List<T>().AsReadOnly();
            }

            IList<T> list = new List<T>();

            for ( int i = 0; i < length; i++ ) {
                list.Add( (T) initialValue.Clone() );
            }

            return list;
        }

        public static IList<IList<IList<T>>> GetNew<T>( int xLength, int yLength, int zLength )
        {
            if ( zLength < 0 ) {
                return new List<IList<IList<T>>>().AsReadOnly();
            }

            IList<IList<IList<T>>> list = new List<IList<IList<T>>>();

            for ( int i = 0; i < xLength; i++ ) {
                list.Add( GetNew<T>( yLength, zLength ) );
            }

            return list;
        }

        public static IList<IList<T>> GetNew<T>( int xLength, int yLength )
        {
            if ( yLength < 0 ) {
                return new List<IList<T>>().AsReadOnly();
            }

            IList<IList<T>> list = new List<IList<T>>();

            for ( int i = 0; i < xLength; i++ ) {
                list.Add( GetNew<T>( yLength ) );
            }

            return list;
        }

        public static IList<T> GetNew<T>( int length )
        {
            if ( length < 0 ) {
                return new List<T>().AsReadOnly();
            }

            IList<T> list = new List<T>();

            for ( int i = 0; i < length; i++ ) {
                list.Add( Activator.CreateInstance<T>() );
            }

            return list;
        }

        public static IList<T> GetCopy<T>( IList<T> source )
        {
            if ( source == null ) {
                return new List<T>().AsReadOnly();
            }

            return GetCopy( source, source.Count );
        }

        public static IList<T> GetCopy<T>( IList<T> source, int itemsNo )
        {
            if ( source == null || itemsNo <= 0 ) {
                return new List<T>().AsReadOnly();
            }

            IList<T> copy = new List<T>();

            for ( int i = 0; i < itemsNo; i++ ) {
                copy.Add( source[i] );
            }

            return copy;
        }

        public static IList<T> GetCopyConcatenated<T>( IList<IList<T>> source, int startIndex, int endIndex )
        {
            if ( source == null || startIndex < 0 || endIndex <= 0 ) {
                return new List<T>().AsReadOnly();
            }

            IList<T> copy = new List<T>();

            for ( int i = startIndex; i < endIndex; i++ ) {
                Concat( copy, source[i] );
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

        public static IList<T> GetSelected<T>( IList<T> source, int leftLimit, int rightLimit )
        {
            if ( source == null || leftLimit < 0 || rightLimit < 0 || leftLimit > rightLimit || leftLimit > source.Count || rightLimit > source.Count ) {
                return new List<T>().AsReadOnly();
            }

            IList<T> list = new List<T>();

            for ( int i = leftLimit; i <= rightLimit; i++ ) {
                list.Add( source[i] );
            }

            return list;
        }

        public static IList<ulong> GetOrdinalValues( ulong startValue, ulong valuesNo )
        {
            IList<ulong> list = new List<ulong>() { startValue };
            ulong currentValue = startValue;

            for ( ulong i = 1; i < valuesNo; i++ ) {
                currentValue++;
                list.Add( currentValue );
            }

            return list;
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

        public static void Concat<T>( IList<IList<T>> target, IList<IList<T>> source )
        {
            if ( target == null || source == null ) {
                return;
            }

            foreach ( var list in source ) {
                target.Add( new List<T>() );
                Concat( target[target.Count - 1], list );
            }
        }

        public static IList<E> Cast<T, E>( IList<T> source )
        {
            if ( source == null ) {
                return new List<E>().AsReadOnly();
            }

            IList<E> list = new List<E>();

            foreach ( T value in source ) {
                list.Add( (E) Convert.ChangeType( value, typeof( E ) ) );
            }

            return list;
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
