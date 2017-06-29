using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace PI
{

    public partial class AvgInfo : Form
    {
        private int CurrentTabNo { get; set; } = 0;
        private AvgInfoConsts Consts { get; } = new AvgInfoConsts();

        private enum Tabs
        {
            Geometric = 0,
            AGM = 1
        }

        private struct BuildNames
        {
            public string TabPage { get; set; }
            public string TableLayout { get; set; }
            public string TextBox1 { get; set; }
            public string TextBox2 { get; set; }
            public string PictureBox1 { get; set; }
            public string PictureBox2 { get; set; }
        }

        public AvgInfo()
        {
            InitializeComponent();
            UpdateUiByComposingWindow();
        }

        private void UpdateUiByComposingWindow()
        {
            SuspendLayout();
            BuildTabPages();
            ResumeLayout( true );
            ComposeTabPages();
        }

        private void BuildTabPage( string title )
        {
            if ( !SetNamesForTab( (Tabs) CurrentTabNo, out BuildNames names ) ) {
                CurrentTabNo--;
                return;
            }

            CurrentTabNo++;
            ui_TbCtrl.SuspendLayout();
            TabPage tab = new TabPage();
            TableLayoutPanel tbLay = new TableLayoutPanel();
            TextBox txt1 = new TextBox();
            TextBox txt2 = new TextBox();
            PictureBox pic1 = new PictureBox();
            PictureBox pic2 = new PictureBox();
            tab.SuspendLayout();
            tbLay.SuspendLayout();
            ((ISupportInitialize) (pic1)).BeginInit();
            ((ISupportInitialize) (pic2)).BeginInit();

            SetTabPage( title, names.TabPage, tab, tbLay );
            SetTableLayout( names.TableLayout, tbLay, txt1, pic1, txt2, pic2 );
            SetTextBox1( names.TextBox1, txt1 );
            SetTextBox2( names.TextBox2, txt2 );
            SetPicture1( names.PictureBox1, pic1 );
            SetPicture2( names.PictureBox2, pic2 );

            ui_TbCtrl.TabPages.Add( tab );
            tab.ResumeLayout( false );
            tbLay.ResumeLayout( false );
            tbLay.PerformLayout();
            ((ISupportInitialize) (pic1)).EndInit();
            ((ISupportInitialize) (pic2)).EndInit();
            ui_TbCtrl.ResumeLayout( false );
        }

        private void SetTabPage( string title, string name, TabPage tab, TableLayoutPanel tbLay )
        {
            tab.Controls.Add( tbLay );
            tab.Location = new Point( 4, 25 );
            tab.Name = name;
            tab.Padding = new Padding( 3 );
            tab.Size = new Size( 706, 356 );
            tab.TabIndex = 0;
            tab.Text = title;
            tab.UseVisualStyleBackColor = true;
        }

        private void SetTableLayout( string name, TableLayoutPanel tbLay, TextBox txt1, PictureBox pic1, TextBox txt2, PictureBox pic2 )
        {
            tbLay.AutoScroll = true;
            tbLay.ColumnCount = 1;
            tbLay.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
            tbLay.Controls.Add( txt1, 0, 0 );
            tbLay.Controls.Add( pic1, 0, 1 );
            tbLay.Controls.Add( txt2, 0, 2 );
            tbLay.Controls.Add( pic2, 0, 3 );
            tbLay.Dock = DockStyle.Fill;
            tbLay.Location = new Point( 3, 3 );
            tbLay.Name = name;
            tbLay.RowCount = 4;
            tbLay.RowStyles.Add( new RowStyle( SizeType.Absolute, 30F ) );
            tbLay.RowStyles.Add( new RowStyle( SizeType.Absolute, 200F ) );
            tbLay.RowStyles.Add( new RowStyle( SizeType.Absolute, 30F ) );
            tbLay.RowStyles.Add( new RowStyle( SizeType.Absolute, 200F ) );
            tbLay.Size = new Size( 700, 350 );
            tbLay.TabIndex = 0;
        }

        private void SetTextBoxWithCommonSetup( ref TextBox txt )
        {
            txt.Cursor = Cursors.Arrow;
            txt.Dock = DockStyle.Fill;
            txt.ForeColor = SystemColors.ActiveCaptionText;
            txt.BorderStyle = BorderStyle.None;
            txt.Enabled = true;
            txt.ReadOnly = true;
        }

        private void SetTextBox1( string name, TextBox txt1 )
        {
            SetTextBoxWithCommonSetup( ref txt1 );
            txt1.Location = new Point( 3, 3 );
            txt1.Name = name;
            txt1.Size = new Size( 694, 20 );
            txt1.TabIndex = 0;
        }

        private void SetTextBox2( string name, TextBox txt2 )
        {
            SetTextBoxWithCommonSetup( ref txt2 );
            txt2.Location = new Point( 3, 233 );
            txt2.Name = name;
            txt2.Size = new Size( 694, 20 );
            txt2.TabIndex = 1;
        }

        private void SetPictureWithCommonSetup( ref PictureBox pic )
        {
            pic.Dock = DockStyle.Fill;
            pic.Size = new Size( 694, 194 );
            pic.TabStop = false;
            pic.SizeMode = PictureBoxSizeMode.CenterImage;
            pic.BackColor = SystemColors.Window;
        }

        private void SetPicture1( string name, PictureBox pic1 )
        {
            SetPictureWithCommonSetup( ref pic1 );
            pic1.Location = new Point( 3, 33 );
            pic1.Name = name;
            pic1.TabIndex = 2;
        }

        private void SetPicture2( string name, PictureBox pic2 )
        {
            SetPictureWithCommonSetup( ref pic2 );
            pic2.Location = new Point( 3, 263 );
            pic2.Name = name;
            pic2.TabIndex = 3;
        }

        private bool SetNamesForTab( Tabs tab, out BuildNames names )
        {
            names = new BuildNames();

            switch ( tab ) {
            case Tabs.Geometric:
                SetNamesForGeometricTab( ref names );
                return true;
            case Tabs.AGM:
                SetNamesForAgmTab( ref names );
                return true;
            }

            return false;
        }

        private void SetNamesForGeometricTab( ref BuildNames names )
        {
            names.TabPage = Consts.Geometric.TabPageName;
            names.TableLayout = Consts.Geometric.TableLayoutName;
            names.TextBox1 = Consts.Geometric.TextBox1Name;
            names.TextBox2 = Consts.Geometric.TextBox2Name;
            names.PictureBox1 = Consts.Geometric.PictureBox1Name;
            names.PictureBox2 = Consts.Geometric.PictureBox2Name;
        }

        private void SetNamesForAgmTab( ref BuildNames names )
        {
            names.TabPage = Consts.Agm.TabPageName;
            names.TableLayout = Consts.Agm.TableLayoutName;
            names.TextBox1 = Consts.Agm.TextBox1Name;
            names.TextBox2 = Consts.Agm.TextBox2Name;
            names.PictureBox1 = Consts.Agm.PictureBox1Name;
            names.PictureBox2 = Consts.Agm.PictureBox2Name;
        }

        private void SetText1( Tabs tab, string text )
        {
            Control layout = null;

            switch ( tab ) {
            case Tabs.Geometric:
                layout = ui_TbCtrl.TabPages[Consts.Geometric.TabPageName].Controls[Consts.Geometric.TableLayoutName];
                layout.Controls[Consts.Geometric.TextBox1Name].Text = text;
                break;
            case Tabs.AGM:
                layout = ui_TbCtrl.TabPages[Consts.Agm.TabPageName].Controls[Consts.Agm.TableLayoutName];
                layout.Controls[Consts.Agm.TextBox1Name].Text = text;
                break;
            }
        }

        private void SetText2( Tabs tab, string text )
        {
            Control layout = null;

            switch ( tab ) {
            case Tabs.Geometric:
                layout = ui_TbCtrl.TabPages[Consts.Geometric.TabPageName].Controls[Consts.Geometric.TableLayoutName];
                layout.Controls[Consts.Geometric.TextBox2Name].Text = text;
                break;
            case Tabs.AGM:
                layout = ui_TbCtrl.TabPages[Consts.Agm.TabPageName].Controls[Consts.Agm.TableLayoutName];
                layout.Controls[Consts.Agm.TextBox2Name].Text = text;
                break;
            }
        }

        private void SetPicture1( Tabs tab, Image image )
        {
            Control layout = null;

            switch ( tab ) {
            case Tabs.Geometric:
                layout = ui_TbCtrl.TabPages[Consts.Geometric.TabPageName].Controls[Consts.Geometric.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Geometric.PictureBox1Name]).Image = image;
                break;
            case Tabs.AGM:
                layout = ui_TbCtrl.TabPages[Consts.Agm.TabPageName].Controls[Consts.Agm.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Agm.PictureBox1Name]).Image = image;
                break;
            }
        }

        private void SetPicture2( Tabs tab, Image image )
        {
            Control layout = null;

            switch ( tab ) {
            case Tabs.Geometric:
                layout = ui_TbCtrl.TabPages[Consts.Geometric.TabPageName].Controls[Consts.Geometric.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Geometric.PictureBox2Name]).Image = image;
                break;
            case Tabs.AGM:
                layout = ui_TbCtrl.TabPages[Consts.Agm.TabPageName].Controls[Consts.Agm.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Agm.PictureBox2Name]).Image = image;
                break;
            }
        }

        private void ComposeGeometricTabPage()
        {
            SetText1( Tabs.Geometric, Consts.Geometric.TextBox1Text );
            SetPicture1( Tabs.Geometric, Properties.Resources.GeometricMean_OriginEquation );
            SetText2( Tabs.Geometric, Consts.Geometric.TextBox2Text );
            SetPicture2( Tabs.Geometric, Properties.Resources.GeometricMean_ModifiedEquation );
        }

        private void ComposeAgmTabPage()
        {
            SetText1( Tabs.AGM, Consts.Agm.TextBox1Text );
            SetPicture1( Tabs.AGM, Properties.Resources.AGM_OriginalEquation );
            SetText2( Tabs.AGM, Consts.Agm.TextBox2Text );
            SetPicture2( Tabs.AGM, Properties.Resources.AGM_ModifiedEquation );
        }

        private void BuildTabPages()
        {
            BuildTabPage( Consts.Geometric.TabPageTitle );
            BuildTabPage( Consts.Agm.TabPageTitle );
        }

        private void ComposeTabPages()
        {
            ComposeGeometricTabPage();
            ComposeAgmTabPage();
        }

    }

}
