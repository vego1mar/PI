using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.general;
using PITests.src.tests;
using System.Collections.Generic;

namespace PITests.src.general
{
    [TestClass]
    public class ArgumentsMakerTest
    {
        [TestMethod]
        public void ArgumentsMaker()
        {
            // given
            const double X_MIN_1 = -20.1;
            const double X_MAX_1 = 21.2;
            const int POINTS_NO_1 = 222;
            ArgumentsMaker maker = new ArgumentsMaker( X_MIN_1, X_MAX_1, POINTS_NO_1 );
            IList<double> points1 = new List<double>();

            // when
            while ( maker.HasNextArgument() ) {
                points1.Add( maker.GetNextArgument() );
            }

            // then
            Assert.IsTrue( points1.Count.Equals( POINTS_NO_1 + 1 ) );
            Assertions.DoubleWithin( points1, X_MIN_1, X_MAX_1 );
        }
    }
}
