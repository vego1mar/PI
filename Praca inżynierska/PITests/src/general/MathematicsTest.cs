using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.general;

namespace PITests.src.general
{
    [TestClass]
    public class MathematicsTest
    {
        [TestMethod]
        public void IsZero()
        {
            // given
            double value1 = 0.0;
            double value2 = 0.0000000595;
            double value3 = 0.00001;

            // when
            bool result1 = Mathematics.IsZero( value1 );
            bool result2 = Mathematics.IsZero( value2 );
            bool result3 = Mathematics.IsZero( value3 );

            // then
            Assert.IsTrue( result1 );
            Assert.IsTrue( result2 );
            Assert.IsFalse( result3 );
        }
    }
}
