using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.helpers;
using PITests.src.tests;
using System.Globalization;

namespace PITests.src.helpers
{
    [TestClass]
    public class StringFormatterTest
    {
        [TestMethod]
        public void TryAsNumeric()
        {
            // given
            const int ENGLISH_DECIMAL_PLACES = 10;
            const int POLISH_DECIMAL_PLACES = 4;
            const double ENGLISH_DOUBLE = 0.7395281046;
            CultureInfo englishProvider = new CultureInfo( "en-US" );
            CultureInfo polishProvider = new CultureInfo( "pl-PL" );

            // when
            string result1 = StringFormatter.TryAsNumeric( 0, null );
            string result2 = StringFormatter.TryAsNumeric( -ENGLISH_DECIMAL_PLACES, ENGLISH_DOUBLE, englishProvider );
            string result3 = StringFormatter.TryAsNumeric( int.MaxValue, ENGLISH_DOUBLE );
            string result4 = StringFormatter.TryAsNumeric( int.MinValue, double.MaxValue );
            string result5 = StringFormatter.TryAsNumeric( POLISH_DECIMAL_PLACES, ENGLISH_DOUBLE, polishProvider );

            // then
            Assert.IsTrue( Strings.IsEmpty( result1 ) );
            Assert.IsTrue( result2.Equals( ENGLISH_DOUBLE.ToString( englishProvider ) ) );
            Assert.IsTrue( result3.Equals( "N" + int.MaxValue ) );
            Assert.IsNull( result4 );
            Assert.IsTrue( result5.Equals( result5.ToString( polishProvider ).Substring( 0, POLISH_DECIMAL_PLACES + 2 ) ) );
        }
    }
}
