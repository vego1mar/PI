using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.helpers;
using PITests.src.tests;
using System.Globalization;

namespace PITests.src.helpers
{
    [TestClass]
    public class StringsTest
    {
        [TestMethod]
        public void TryFormatAsNumeric()
        {
            // given
            const int ENGLISH_DECIMAL_PLACES = 10;
            const int POLISH_DECIMAL_PLACES = 4;
            const double ENGLISH_DOUBLE = 0.7395281046;
            CultureInfo englishProvider = new CultureInfo( "en-US" );
            CultureInfo polishProvider = new CultureInfo( "pl-PL" );

            // when
            string result1 = Strings.TryFormatAsNumeric( 0, null );
            string result2 = Strings.TryFormatAsNumeric( -ENGLISH_DECIMAL_PLACES, ENGLISH_DOUBLE, englishProvider );
            string result3 = Strings.TryFormatAsNumeric( int.MaxValue, ENGLISH_DOUBLE );
            string result4 = Strings.TryFormatAsNumeric( int.MinValue, double.MaxValue );
            string result5 = Strings.TryFormatAsNumeric( POLISH_DECIMAL_PLACES, ENGLISH_DOUBLE, polishProvider );

            // then
            Assert.IsTrue( TestStrings.IsEmpty( result1 ) );
            Assert.IsTrue( result2.Equals( ENGLISH_DOUBLE.ToString( englishProvider ) ) );
            Assert.IsTrue( result3.Equals( "N" + int.MaxValue ) );
            Assert.IsNull( result4 );
            Assert.IsTrue( result5.Equals( result5.ToString( polishProvider ).Substring( 0, POLISH_DECIMAL_PLACES + 2 ) ) );
        }

        [TestMethod]
        public void GetCommon()
        {
            // given
            const double VALUE1_1 = 0.7395280146;
            const double VALUE1_2 = 0.7390000000;
            const string EXPECTED1 = "0.739";

            // when
            string result1 = Strings.GetCommon( VALUE1_1, VALUE1_2 );

            // then
            Assert.AreEqual( EXPECTED1, result1, false );
        }
    }
}
