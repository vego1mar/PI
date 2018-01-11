using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PI.src.general;
using PITests.src.tests;
using System.Linq;

namespace PITests.src.general
{
    [TestClass]
    public class RandomsTest
    {
        [TestMethod]
        public void NextDouble()
        {
            // given
            const int DOUBLES_NO = 10_000;
            const double MIN_VALUE = -11;
            const double MAX_VALUE = 23;
            const double EXPECTED_MEAN = (MAX_VALUE - MIN_VALUE) / 2.0;
            const double ERROR = 1.0E-2;
            IList<double> doubles = new List<double>();

            // when
            for ( int i = 0; i < DOUBLES_NO; i++ ) {
                doubles.Add( Randoms.NextDouble( MIN_VALUE, MAX_VALUE ) );
            }

            // then
            Assertions.DoubleWithin( doubles, MIN_VALUE, MAX_VALUE );
            Assert.AreEqual( EXPECTED_MEAN, (doubles.Max() - doubles.Min()) / 2.0, ERROR );
        }
    }
}
