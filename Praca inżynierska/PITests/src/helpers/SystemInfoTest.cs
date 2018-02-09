using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.helpers;
using PITests.src.tests;

namespace PITests.src.helpers
{
    [TestClass]
    public class SystemInfoTest
    {
        [TestMethod]
        public void TryGetDotNetFrameworkVersion()
        {
            // given
            const string DOT_NET_FRAMEWORK_MAJOR_VERSION = "4.7";

            // when
            string result1 = SystemInfos.TryGetDotNetFrameworkVersion();

            // then
            Assert.IsTrue( result1.Contains( DOT_NET_FRAMEWORK_MAJOR_VERSION ) );
        }
    }
}
