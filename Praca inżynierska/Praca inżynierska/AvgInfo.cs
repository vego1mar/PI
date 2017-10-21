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
            AGM = 1,
            Heronian = 2,
            Harmonic = 3,
            Power = 4,
            RMS = 5,
            Logarithmic = 6,
            EMA = 7,
            LnWages = 8
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

        private void SetTabPage( string title, string name, TabPage tab, Control tbLay )
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

        private void SetTableLayout( string name, TableLayoutPanel tbLay, Control txt1, Control pic1, Control txt2, Control pic2 )
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
            case Tabs.Heronian:
                SetNamesForHeronianTab( ref names );
                return true;
            case Tabs.Harmonic:
                SetNamesForHarmonicTab( ref names );
                return true;
            case Tabs.Power:
                SetNamesForPowerTab( ref names );
                return true;
            case Tabs.RMS:
                SetNamesForRmsTab( ref names );
                return true;
            case Tabs.Logarithmic:
                SetNamesForLogarithmicTab( ref names );
                return true;
            case Tabs.EMA:
                SetNamesForEmaTab( ref names );
                return true;
            case Tabs.LnWages:
                SetNamesForLnWagesTab( ref names );
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

        private void SetNamesForHeronianTab( ref BuildNames names )
        {
            names.TabPage = Consts.Heronian.TabPageName;
            names.TableLayout = Consts.Heronian.TableLayoutName;
            names.TextBox1 = Consts.Heronian.TextBox1Name;
            names.TextBox2 = Consts.Heronian.TextBox2Name;
            names.PictureBox1 = Consts.Heronian.PictureBox1Name;
            names.PictureBox2 = Consts.Heronian.PictureBox2Name;
        }

        private void SetNamesForHarmonicTab( ref BuildNames names )
        {
            names.TabPage = Consts.Harmonic.TabPageName;
            names.TableLayout = Consts.Harmonic.TableLayoutName;
            names.TextBox1 = Consts.Harmonic.TextBox1Name;
            names.TextBox2 = Consts.Harmonic.TextBox2Name;
            names.PictureBox1 = Consts.Harmonic.PictureBox1Name;
            names.PictureBox2 = Consts.Harmonic.PictureBox2Name;
        }

        private void SetNamesForPowerTab( ref BuildNames names )
        {
            names.TabPage = Consts.Power.TabPageName;
            names.TableLayout = Consts.Power.TableLayoutName;
            names.TextBox1 = Consts.Power.TextBox1Name;
            names.TextBox2 = Consts.Power.TextBox2Name;
            names.PictureBox1 = Consts.Power.PictureBox1Name;
            names.PictureBox2 = Consts.Power.PictureBox2Name;
        }

        private void SetNamesForRmsTab( ref BuildNames names )
        {
            names.TabPage = Consts.Rms.TabPageName;
            names.TableLayout = Consts.Rms.TableLayoutName;
            names.TextBox1 = Consts.Rms.TextBox1Name;
            names.TextBox2 = Consts.Rms.TextBox2Name;
            names.PictureBox1 = Consts.Rms.PictureBox1Name;
            names.PictureBox2 = Consts.Rms.PictureBox2Name;
        }

        private void SetNamesForLogarithmicTab( ref BuildNames names )
        {
            names.TabPage = Consts.Logarithmic.TabPageName;
            names.TableLayout = Consts.Logarithmic.TableLayoutName;
            names.TextBox1 = Consts.Logarithmic.TextBox1Name;
            names.TextBox2 = Consts.Logarithmic.TextBox2Name;
            names.PictureBox1 = Consts.Logarithmic.PictureBox1Name;
            names.PictureBox2 = Consts.Logarithmic.PictureBox2Name;
        }

        private void SetNamesForEmaTab( ref BuildNames names )
        {
            names.TabPage = Consts.Ema.TabPageName;
            names.TableLayout = Consts.Ema.TableLayoutName;
            names.TextBox1 = Consts.Ema.TextBox1Name;
            names.TextBox2 = Consts.Ema.TextBox2Name;
            names.PictureBox1 = Consts.Ema.PictureBox1Name;
            names.PictureBox2 = Consts.Ema.PictureBox2Name;
        }

        private void SetNamesForLnWagesTab( ref BuildNames names )
        {
            names.TabPage = Consts.LnWages.TabPageName;
            names.TableLayout = Consts.LnWages.TableLayoutName;
            names.TextBox1 = Consts.LnWages.TextBox1Name;
            names.TextBox2 = Consts.LnWages.TextBox2Name;
            names.PictureBox1 = Consts.LnWages.PictureBox1Name;
            names.PictureBox2 = Consts.LnWages.PictureBox2Name;
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
            case Tabs.Heronian:
                layout = ui_TbCtrl.TabPages[Consts.Heronian.TabPageName].Controls[Consts.Heronian.TableLayoutName];
                layout.Controls[Consts.Heronian.TextBox1Name].Text = text;
                break;
            case Tabs.Harmonic:
                layout = ui_TbCtrl.TabPages[Consts.Harmonic.TabPageName].Controls[Consts.Harmonic.TableLayoutName];
                layout.Controls[Consts.Harmonic.TextBox1Name].Text = text;
                break;
            case Tabs.Power:
                layout = ui_TbCtrl.TabPages[Consts.Power.TabPageName].Controls[Consts.Power.TableLayoutName];
                layout.Controls[Consts.Power.TextBox1Name].Text = text;
                break;
            case Tabs.RMS:
                layout = ui_TbCtrl.TabPages[Consts.Rms.TabPageName].Controls[Consts.Rms.TableLayoutName];
                layout.Controls[Consts.Rms.TextBox1Name].Text = text;
                break;
            case Tabs.Logarithmic:
                layout = ui_TbCtrl.TabPages[Consts.Logarithmic.TabPageName].Controls[Consts.Logarithmic.TableLayoutName];
                layout.Controls[Consts.Logarithmic.TextBox1Name].Text = text;
                break;
            case Tabs.EMA:
                layout = ui_TbCtrl.TabPages[Consts.Ema.TabPageName].Controls[Consts.Ema.TableLayoutName];
                layout.Controls[Consts.Ema.TextBox1Name].Text = text;
                break;
            case Tabs.LnWages:
                layout = ui_TbCtrl.TabPages[Consts.LnWages.TabPageName].Controls[Consts.LnWages.TableLayoutName];
                layout.Controls[Consts.LnWages.TextBox1Name].Text = text;
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
            case Tabs.Heronian:
                layout = ui_TbCtrl.TabPages[Consts.Heronian.TabPageName].Controls[Consts.Heronian.TableLayoutName];
                layout.Controls[Consts.Heronian.TextBox2Name].Text = text;
                break;
            case Tabs.Harmonic:
                layout = ui_TbCtrl.TabPages[Consts.Harmonic.TabPageName].Controls[Consts.Harmonic.TableLayoutName];
                layout.Controls[Consts.Harmonic.TextBox2Name].Text = text;
                break;
            case Tabs.Power:
                layout = ui_TbCtrl.TabPages[Consts.Power.TabPageName].Controls[Consts.Power.TableLayoutName];
                layout.Controls[Consts.Power.TextBox2Name].Text = text;
                break;
            case Tabs.RMS:
                layout = ui_TbCtrl.TabPages[Consts.Rms.TabPageName].Controls[Consts.Rms.TableLayoutName];
                layout.Controls[Consts.Rms.TextBox2Name].Text = text;
                break;
            case Tabs.Logarithmic:
                layout = ui_TbCtrl.TabPages[Consts.Logarithmic.TabPageName].Controls[Consts.Logarithmic.TableLayoutName];
                layout.Controls[Consts.Logarithmic.TextBox2Name].Text = text;
                break;
            case Tabs.EMA:
                layout = ui_TbCtrl.TabPages[Consts.Ema.TabPageName].Controls[Consts.Ema.TableLayoutName];
                layout.Controls[Consts.Ema.TextBox2Name].Text = text;
                break;
            case Tabs.LnWages:
                layout = ui_TbCtrl.TabPages[Consts.LnWages.TabPageName].Controls[Consts.LnWages.TableLayoutName];
                layout.Controls[Consts.LnWages.TextBox2Name].Text = text;
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
            case Tabs.Heronian:
                layout = ui_TbCtrl.TabPages[Consts.Heronian.TabPageName].Controls[Consts.Heronian.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Heronian.PictureBox1Name]).Image = image;
                break;
            case Tabs.Harmonic:
                layout = ui_TbCtrl.TabPages[Consts.Harmonic.TabPageName].Controls[Consts.Harmonic.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Harmonic.PictureBox1Name]).Image = image;
                break;
            case Tabs.Power:
                layout = ui_TbCtrl.TabPages[Consts.Power.TabPageName].Controls[Consts.Power.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Power.PictureBox1Name]).Image = image;
                break;
            case Tabs.RMS:
                layout = ui_TbCtrl.TabPages[Consts.Rms.TabPageName].Controls[Consts.Rms.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Rms.PictureBox1Name]).Image = image;
                break;
            case Tabs.Logarithmic:
                layout = ui_TbCtrl.TabPages[Consts.Logarithmic.TabPageName].Controls[Consts.Logarithmic.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Logarithmic.PictureBox1Name]).Image = image;
                break;
            case Tabs.EMA:
                layout = ui_TbCtrl.TabPages[Consts.Ema.TabPageName].Controls[Consts.Ema.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Ema.PictureBox1Name]).Image = image;
                break;
            case Tabs.LnWages:
                layout = ui_TbCtrl.TabPages[Consts.LnWages.TabPageName].Controls[Consts.LnWages.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.LnWages.PictureBox1Name]).Image = image;
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
            case Tabs.Heronian:
                layout = ui_TbCtrl.TabPages[Consts.Heronian.TabPageName].Controls[Consts.Heronian.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Heronian.PictureBox2Name]).Image = image;
                break;
            case Tabs.Harmonic:
                layout = ui_TbCtrl.TabPages[Consts.Harmonic.TabPageName].Controls[Consts.Harmonic.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Harmonic.PictureBox2Name]).Image = image;
                break;
            case Tabs.Power:
                layout = ui_TbCtrl.TabPages[Consts.Power.TabPageName].Controls[Consts.Power.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Power.PictureBox2Name]).Image = image;
                break;
            case Tabs.RMS:
                layout = ui_TbCtrl.TabPages[Consts.Rms.TabPageName].Controls[Consts.Rms.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Rms.PictureBox2Name]).Image = image;
                break;
            case Tabs.Logarithmic:
                layout = ui_TbCtrl.TabPages[Consts.Logarithmic.TabPageName].Controls[Consts.Logarithmic.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Logarithmic.PictureBox2Name]).Image = image;
                break;
            case Tabs.EMA:
                layout = ui_TbCtrl.TabPages[Consts.Ema.TabPageName].Controls[Consts.Ema.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.Ema.PictureBox2Name]).Image = image;
                break;
            case Tabs.LnWages:
                layout = ui_TbCtrl.TabPages[Consts.LnWages.TabPageName].Controls[Consts.LnWages.TableLayoutName];
                ((PictureBox) layout.Controls[Consts.LnWages.PictureBox2Name]).Image = image;
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

        private void ComposeHeronianTabPage()
        {
            SetText1( Tabs.Heronian, Consts.Heronian.TextBox1Text );
            SetPicture1( Tabs.Heronian, Properties.Resources.Heronian_OriginalEquation );
            SetText2( Tabs.Heronian, Consts.Heronian.TextBox2Text );
            SetPicture2( Tabs.Heronian, Properties.Resources.Heronian_ModifiedEquation );
        }

        private void ComposeHarmonicTabPage()
        {
            SetText1( Tabs.Harmonic, Consts.Harmonic.TextBox1Text );
            SetPicture1( Tabs.Harmonic, Properties.Resources.Harmonic_OriginalEquation );
            SetText2( Tabs.Harmonic, Consts.Harmonic.TextBox2Text );
            SetPicture2( Tabs.Harmonic, Properties.Resources.Harmonic_TransformedEquation );
        }

        private void ComposePowerTabPage()
        {
            SetText1( Tabs.Power, Consts.Power.TextBox1Text );
            SetPicture1( Tabs.Power, Properties.Resources.Power_OriginalEquation );
            SetText2( Tabs.Power, Consts.Power.TextBox2Text );
            SetPicture2( Tabs.Power, Properties.Resources.Power_DefaultRank );
        }

        private void ComposeRmsTabPage()
        {
            SetText1( Tabs.RMS, Consts.Rms.TextBox1Text );
            SetPicture1( Tabs.RMS, Properties.Resources.RMS_OriginalEquation );
            SetText2( Tabs.RMS, Consts.Rms.TextBox2Text );
            SetPicture2( Tabs.RMS, Properties.Resources.RMS_ComputedForm );
        }

        private void ComposeLogarithmicTabPage()
        {
            SetText1( Tabs.Logarithmic, Consts.Logarithmic.TextBox1Text );
            SetPicture1( Tabs.Logarithmic, Properties.Resources.Logarithmic_OriginalEquation );
            SetText2( Tabs.Logarithmic, Consts.Logarithmic.TextBox2Text );
            SetPicture2( Tabs.Logarithmic, Properties.Resources.Logarithmic_ModifiedEquation );
        }

        private void ComposeEmaTabPage()
        {
            SetText1( Tabs.EMA, Consts.Ema.TextBox1Text );
            SetPicture1( Tabs.EMA, Properties.Resources.EMA_OriginalEquation );
            SetText2( Tabs.EMA, Consts.Ema.TextBox2Text );
            SetPicture2( Tabs.EMA, Properties.Resources.EMA_ModifiedEquation );
        }

        private void ComposeLnWagesTabPage()
        {
            SetText1( Tabs.LnWages, Consts.LnWages.TextBox1Text );
            SetPicture1( Tabs.LnWages, Properties.Resources.LnWages_OriginalEquation );
            SetText2( Tabs.LnWages, Consts.LnWages.TextBox2Text );
            SetPicture2( Tabs.LnWages, Properties.Resources.LnWages_ModifiedEquation );
        }

        private void BuildTabPages()
        {
            BuildTabPage( Consts.Geometric.TabPageTitle );
            BuildTabPage( Consts.Agm.TabPageTitle );
            BuildTabPage( Consts.Heronian.TabPageTitle );
            BuildTabPage( Consts.Harmonic.TabPageTitle );
            BuildTabPage( Consts.Power.TabPageTitle );
            BuildTabPage( Consts.Rms.TabPageTitle );
            BuildTabPage( Consts.Logarithmic.TabPageTitle );
            BuildTabPage( Consts.Ema.TabPageTitle );
            BuildTabPage( Consts.LnWages.TabPageTitle );
        }

        private void ComposeTabPages()
        {
            ComposeGeometricTabPage();
            ComposeAgmTabPage();
            ComposeHeronianTabPage();
            ComposeHarmonicTabPage();
            ComposePowerTabPage();
            ComposeRmsTabPage();
            ComposeLogarithmicTabPage();
            ComposeEmaTabPage();
            ComposeLnWagesTabPage();
        }

    }

}
