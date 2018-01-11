using System.Windows.Forms;

namespace PITests.src.tests
{
    public static class TestControls
    {
        public static ComboBox GetTestComboBox( uint itemsNo = 0 )
        {
            ComboBox comboBox = new ComboBox() {
                Name = nameof( TestControls ) + "->ComboBox"
            };

            for ( uint i = 0; i < itemsNo; i++ ) {
                comboBox.Items.Add( i + "::" + i.GetHashCode() );
            }

            return comboBox;
        }

        public static ListBox GetTestListBox( uint itemsNo = 0 )
        {
            ListBox listBox = new ListBox() {
                Name = nameof( TestControls ) + "->ListBox"
            };

            for ( uint i = 0; i < itemsNo; i++ ) {
                listBox.Items.Add( i + "::" + i.GetHashCode() );
            }

            return listBox;
        }

        public static TabControl GetTestTabControl( uint tabPagesNo = 0 )
        {
            TabControl tabControl = new TabControl() {
                Name = nameof( TestControls ) + "->TabControl"
            };

            for ( uint i = 0; i < tabPagesNo; i++ ) {
                tabControl.TabPages.Add( new TabPage( i + "::" + i.GetHashCode() ) );
            }

            return tabControl;
        }

        public static NumericUpDown GetTestNumericUpDown( int minimum = -1, int maximum = 1, int selectedValue = 0 )
        {
            return new NumericUpDown() {
                Name = nameof( TestControls ) + "->NumericUpDown",
                Minimum = minimum,
                Maximum = maximum,
                Value = selectedValue,
            };
        }

        public static TrackBar GetTestTrackBar( int minimum = -1, int maximum = 1, int selectedValue = 0 )
        {
            return new TrackBar() {
                Name = nameof( TestControls ) + "->TrackBar",
                Minimum = minimum,
                Maximum = maximum,
                Value = selectedValue
            };
        }

        public static TextBox GetTestTextBox( string text = "", bool isReadOnly = false )
        {
            return new TextBox() {
                Name = nameof( TestControls ) + "->TextBox",
                Text = text,
                MaxLength = int.MaxValue,
                Multiline = false,
                ReadOnly = isReadOnly
            };
        }
    }
}
