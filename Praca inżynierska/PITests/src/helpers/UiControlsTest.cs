using System;
using System.Globalization;
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

        [TestMethod]
        public void TrySetValue()
        {
            // given
            const int CONTROL1_SELECTED_VALUE = 11;
            NumericUpDown control1 = TestControls.GetTestNumericUpDown( -23, 23 );

            const int CONTROL2_SELECTED_VALUE = -24;
            TrackBar control2 = TestControls.GetTestTrackBar( -100, 100, 25 );

            // when
            UiControls.TrySetValue( null, 0 );
            UiControls.TrySetValue( new Button(), 0 );
            UiControls.TrySetValue( control1, CONTROL1_SELECTED_VALUE );
            decimal selectedValue1 = control1.Value;
            UiControls.TrySetValue( control2, CONTROL2_SELECTED_VALUE );
            decimal selectedValue2 = control2.Value;

            // then
            Assert.IsTrue( selectedValue1.Equals( CONTROL1_SELECTED_VALUE ) );
            Assert.IsTrue( selectedValue2.Equals( CONTROL2_SELECTED_VALUE ) );
        }

        [TestMethod]
        public void TryGetValue()
        {
            // given
            const int CONTROL3_SELECTED_VALUE = 12;
            const int CONTROL4_SELECTED_VALUE = 213;
            const string CONTROL5_STRING = "abcdefghijklmnopqrstuvwxyząćęńóśżźABC0123";
            double control6Double = Math.Sqrt( 3.0 / 7.0 );
            string control6String = control6Double.ToString( CultureInfo.InvariantCulture );

            // when
            short result1 = UiControls.TryGetValue<short>( null );
            float result2 = UiControls.TryGetValue<float>( new Button() );
            decimal result3 = UiControls.TryGetValue<decimal>( TestControls.GetTestNumericUpDown( -100, 111, CONTROL3_SELECTED_VALUE ) );
            int result4 = UiControls.TryGetValue<int>( TestControls.GetTestTrackBar( -222, 444, CONTROL4_SELECTED_VALUE ) );
            string result5 = UiControls.TryGetValue<string>( TestControls.GetTestTextBox( CONTROL5_STRING, true ) );
            double result6 = UiControls.TryGetValue<double>( TestControls.GetTestTextBox( control6String, true ) );

            // then
            Assert.IsTrue( result1.Equals( default( short ) ) );
            Assert.AreEqual( default( float ), result2, Assertions.IBM_FLOAT_SURROUNDING );
            Assert.IsTrue( result3.Equals( CONTROL3_SELECTED_VALUE ) );
            Assert.IsTrue( result4.Equals( CONTROL4_SELECTED_VALUE ) );
            Assert.IsTrue( result5.Equals( CONTROL5_STRING ) );
            Assert.AreEqual( control6Double, result6, Assertions.IBM_FLOAT_SURROUNDING );
        }
    }
}
