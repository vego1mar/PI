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

        public static IList<T> GetSorted<T>( IList<T> source )
        {
            if ( source == null ) {
                return new List<T>().AsReadOnly();
            }

            return source.OrderBy( v => v ).ToList();
        }
    }
}
