using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PITests.src.tests
{
    public static class Assertions
    {
        public const double IBM_FLOAT_SURROUNDING = 5.96E-8;

        public static void DoubleWithin( double value, double minimum, double maximum )
        {
            if ( value < minimum || value > maximum ) {
                Assert.Fail();
            }
        }

        public static void DoubleWithin( IList<double> doubles, double minimum, double maximum )
        {
            foreach ( double value in doubles ) {
                DoubleWithin( value, minimum, maximum );
            }
        }

        public static void SameValues<T>( IList<T> list1, IList<T> list2 )
        {
            if ( (list1 == null && list2 != null) || (list1 != null && list2 == null) || list1.Count != list2.Count ) {
                Assert.Fail();
            }

            for ( int i = 0; i < list1.Count; i++ ) {
                if ( !EqualityComparer<T>.Default.Equals( list1[i], list2[i] ) ) {
                    Assert.Fail();
                }
            }
        }
    }
}
