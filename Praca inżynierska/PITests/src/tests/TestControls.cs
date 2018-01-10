using System.Windows.Forms;

namespace PITests.src.tests
{
    public static class TestControls
    {
        public static ComboBox GetTestComboBox( uint itemsNo = 0 )
        {
            ComboBox comboBox = new ComboBox();

            for ( uint i = 0; i < itemsNo; i++ ) {
                comboBox.Items.Add( i + "::" + i.GetHashCode() );
            }

            return comboBox;
        }

        public static ListBox GetTestListBox( uint itemsNo = 0 )
        {
            ListBox listBox = new ListBox();

            for ( uint i = 0; i < itemsNo; i++ ) {
                listBox.Items.Add( i + "::" + i.GetHashCode() );
            }

            return listBox;
        }

        public static TabControl GetTestTabControl( uint tabPagesNo = 0 )
        {
            TabControl tabControl = new TabControl();

            for ( uint i = 0; i < tabPagesNo; i++ ) {
                tabControl.TabPages.Add( new TabPage( i + "::" + i.GetHashCode() ) );
            }

            return tabControl;
        }
    }
}
