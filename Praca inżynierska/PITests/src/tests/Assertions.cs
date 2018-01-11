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
    }
}
