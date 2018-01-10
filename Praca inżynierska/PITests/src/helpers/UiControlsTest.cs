using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PI.src.helpers;
using PITests.src.tests;

namespace PITests.src.helpers
{
    [TestClass]
    public class UiControlsTest
    {
        [TestMethod]
        public void TrySelectTab()
        {
            // given
            const int TAB_PAGES_NO = 23;
            TabControl tabControl = TestControls.GetTestTabControl( TAB_PAGES_NO );
            const int SELECTED_TAB_1 = 3;
            const int SELECTED_TAB_2 = TAB_PAGES_NO + 1;
            const int SELECTED_TAB_3 = -TAB_PAGES_NO;
            const int SELECTED_TAB_4 = 0;

            // when
            UiControls.TrySelectTab( tabControl, SELECTED_TAB_1 );
            int selectedTab1 = tabControl.SelectedIndex;
            UiControls.TrySelectTab( tabControl, SELECTED_TAB_2 );
            int selectedTab2 = tabControl.SelectedIndex;
            UiControls.TrySelectTab( tabControl, SELECTED_TAB_3 );
            int selectedTab3 = tabControl.SelectedIndex;
            UiControls.TrySelectTab( null, SELECTED_TAB_4 );
            int selectedTab4 = tabControl.SelectedIndex;

            // then
            Assert.IsTrue( selectedTab1.Equals( SELECTED_TAB_1 ) );
            Assert.IsFalse( selectedTab2.Equals( SELECTED_TAB_2 ) );
            Assert.IsFalse( selectedTab3.Equals( SELECTED_TAB_3 ) );
            Assert.IsFalse( selectedTab4.Equals( SELECTED_TAB_4 ) );
        }

        [TestMethod]
        public void TryShowDialog()
        {
            // when
            bool result1 = UiControls.TryShowDialog( null, null );

            // then
            Assert.IsFalse( result1 );
        }

        [TestMethod]
        public void TryGetSelectedIndex()
        {
            // given
            const int CONTROL1_INDEX = 4;
            const int CONTROL1_ITEMS_NO = 77;
            ComboBox control1 = TestControls.GetTestComboBox( CONTROL1_ITEMS_NO );
            control1.SelectedIndex = CONTROL1_INDEX;

            const int ERROR_INDEX = -1;
            ComboBox control2 = new ComboBox();

            const int CONTROL3_INDEX = 7;
            const int CONTROL3_ITEMS_NO = 52;
            ListBox control3 = TestControls.GetTestListBox( CONTROL3_ITEMS_NO );
            control3.SelectedIndex = CONTROL3_INDEX;

            const int CONTROL4_TAB_PAGES_NO = 23;
            const int CONTROL4_INDEX = CONTROL4_TAB_PAGES_NO / 3;
            TabControl control4 = TestControls.GetTestTabControl( CONTROL4_TAB_PAGES_NO );
            control4.SelectTab( CONTROL4_INDEX );

            // when
            int result1 = UiControls.TryGetSelectedIndex( control1 );
            int result2 = UiControls.TryGetSelectedIndex( control2 );
            int result3 = UiControls.TryGetSelectedIndex( control3 );
            int result4 = UiControls.TryGetSelectedIndex( new Button() );
            int result5 = UiControls.TryGetSelectedIndex( null );
            int result6 = UiControls.TryGetSelectedIndex( control4 );

            // then
            Assert.IsTrue( result1.Equals( CONTROL1_INDEX ) );
            Assert.IsTrue( result2.Equals( ERROR_INDEX ) );
            Assert.IsTrue( result3.Equals( CONTROL3_INDEX ) );
            Assert.IsTrue( result4.Equals( ERROR_INDEX ) );
            Assert.IsTrue( result5.Equals( ERROR_INDEX ) );
            Assert.IsTrue( result6.Equals( CONTROL4_INDEX ) );
        }

        [TestMethod]
        public void TrySetSelectedIndex()
        {
            // given
            const int COMBO_BOX_ITEMS_NO = 111;
            ComboBox control1 = TestControls.GetTestComboBox( COMBO_BOX_ITEMS_NO );
            const int SELECTED_INDEX_1 = 66;
            const int SELECTED_INDEX_2 = -COMBO_BOX_ITEMS_NO;
            const int SELECTED_INDEX_3 = COMBO_BOX_ITEMS_NO + 1;

            const int LIST_BOX_ITEMS_NO = 31;
            ListBox control2 = TestControls.GetTestListBox( LIST_BOX_ITEMS_NO );
            const int SELECTED_INDEX_4 = -LIST_BOX_ITEMS_NO;
            const int SELECTED_INDEX_5 = LIST_BOX_ITEMS_NO / 2;
            const int SELECTED_INDEX_6 = LIST_BOX_ITEMS_NO + 1;

            // when
            UiControls.TrySetSelectedIndex( control1, SELECTED_INDEX_1 );
            int selectedIndex1 = control1.SelectedIndex;
            UiControls.TrySetSelectedIndex( control1, SELECTED_INDEX_2 );
            int selectedIndex2 = control1.SelectedIndex;
            UiControls.TrySetSelectedIndex( control1, SELECTED_INDEX_3 );
            int selectedIndex3 = control1.SelectedIndex;

            UiControls.TrySetSelectedIndex( control2, SELECTED_INDEX_4 );
            int selectedIndex4 = control2.SelectedIndex;
            UiControls.TrySetSelectedIndex( control2, SELECTED_INDEX_5 );
            int selectedIndex5 = control2.SelectedIndex;
            UiControls.TrySetSelectedIndex( control2, SELECTED_INDEX_6 );
            int selectedIndex6 = control2.SelectedIndex;

            UiControls.TrySetSelectedIndex( new ComboBox(), 0 );
            UiControls.TrySetSelectedIndex( new ListBox(), 0 );
            UiControls.TrySetSelectedIndex( new Button(), 0 );
            UiControls.TrySetSelectedIndex( null, 0 );

            // then
            Assert.IsTrue( selectedIndex1.Equals( SELECTED_INDEX_1 ) );
            Assert.IsFalse( selectedIndex2.Equals( SELECTED_INDEX_2 ) );
            Assert.IsFalse( selectedIndex3.Equals( SELECTED_INDEX_3 ) );

            Assert.IsFalse( selectedIndex4.Equals( SELECTED_INDEX_4 ) );
            Assert.IsTrue( selectedIndex5.Equals( SELECTED_INDEX_5 ) );
            Assert.IsFalse( selectedIndex6.Equals( SELECTED_INDEX_6 ) );
        }

        [TestMethod]
        public void TryGetRowStyle()
        {
            // when
            RowStyle result1 = UiControls.TryGetRowStyle( (SizeType) (-1), 0.0f );
            RowStyle result2 = UiControls.TryGetRowStyle( (SizeType) (-2), float.MinValue );
            RowStyle result3 = UiControls.TryGetRowStyle( (SizeType) (-3), float.MaxValue );
            RowStyle result4 = UiControls.TryGetRowStyle( SizeType.Absolute, 0.0f );
            RowStyle result5 = UiControls.TryGetRowStyle( SizeType.AutoSize, 0.0f );
            RowStyle result6 = UiControls.TryGetRowStyle( SizeType.Percent, -100.0f );
            RowStyle result7 = UiControls.TryGetRowStyle( SizeType.Percent, 10.0f );
            RowStyle result8 = UiControls.TryGetRowStyle( SizeType.Percent, 123.0f );

            // then
            Assert.IsNotNull( result1 );
            Assert.IsNull( result2 );
            Assert.IsNotNull( result3 );
            Assert.IsNotNull( result4 );
            Assert.IsNotNull( result5 );
            Assert.IsNull( result6 );
            Assert.IsNotNull( result7 );
            Assert.IsNotNull( result8 );
        }
    }
}
