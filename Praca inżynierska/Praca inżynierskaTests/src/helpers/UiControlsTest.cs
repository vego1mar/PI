using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.helpers;

namespace PITests.src.helpers
{
    [TestClass]
    public class UiControlsTest
    {
        [TestMethod]
        public void TryGetSelectedIndex()
        {
            // given
            object[] items = new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            const int ERROR_INDEX = -1;

            ComboBox control1 = new ComboBox();
            control1.Items.AddRange( items );
            const int CONTROL1_INDEX = 4;
            control1.SelectedIndex = CONTROL1_INDEX;

            ComboBox control2 = new ComboBox();
            Button control4 = new Button();

            ListBox control3 = new ListBox();
            control3.Items.AddRange( items );
            const int CONTROL3_INDEX = 7;
            control3.SelectedIndex = CONTROL3_INDEX;

            // when
            int result1 = UiControls.TryGetSelectedIndex( control1 );
            int result2 = UiControls.TryGetSelectedIndex( control2 );
            int result3 = UiControls.TryGetSelectedIndex( control3 );
            int result4 = UiControls.TryGetSelectedIndex( control4 );

            // then
            Assert.IsTrue( result1.Equals( CONTROL1_INDEX ) );
            Assert.IsTrue( result2.Equals( ERROR_INDEX ) );
            Assert.IsTrue( result3.Equals( CONTROL3_INDEX ) );
            Assert.IsTrue( result4.Equals( ERROR_INDEX ) );
        }
    }
}
