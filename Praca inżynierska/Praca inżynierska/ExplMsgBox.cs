using System.Drawing;
using System.Windows.Forms;

namespace PI
{
    public partial class ExplMsgBox : Form
    {
        public enum SystemIcon
        {
            Application,
            Asterisk,
            Error,
            Exclamation,
            Hand,
            Information,
            Question,
            Shield,
            Warning,
            WinLogo
        }

        public enum TabPage
        {
            Info1 = 0,
            Info2 = 1
        }

        public ExplMsgBox()
        {
            InitializeComponent();
            UpdateUiByDefaultSettings();
        }

        private void UpdateUiByDefaultSettings()
        {
            SetIcon( SystemIcon.Exclamation );
            SetTitleBarText( "Explanation Message Box" );
            uiBottomLeft_ChkBx.Checked = false;
            SetInfo1Text( null );
            SetInfo2Text1( null );
            SetInfo2Text2( null );
            SetInfo2Image1( null );
            SetInfo2Image2( null );
            SetTabLabel( TabPage.Info1, "Information" );
            SetTabLabel( TabPage.Info2, "Details" );
        }

        public void SetIcon( SystemIcon icon )
        {
            switch ( icon ) {
            case SystemIcon.Application:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.Application.ToBitmap();
                break;
            case SystemIcon.Asterisk:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.Asterisk.ToBitmap();
                break;
            case SystemIcon.Error:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.Error.ToBitmap();
                break;
            case SystemIcon.Exclamation:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.Exclamation.ToBitmap();
                break;
            case SystemIcon.Hand:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.Hand.ToBitmap();
                break;
            case SystemIcon.Information:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.Information.ToBitmap();
                break;
            case SystemIcon.Question:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.Question.ToBitmap();
                break;
            case SystemIcon.Shield:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.Shield.ToBitmap();
                break;
            case SystemIcon.Warning:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.Warning.ToBitmap();
                break;
            case SystemIcon.WinLogo:
                uiTopLeft_Icon_PicBx.Image = SystemIcons.WinLogo.ToBitmap();
                break;
            }
        }

        public void SetTitleBarText( string text )
        {
            Text = text;
        }

        public bool IsChecked()
        {
            return uiBottomLeft_ChkBx.Checked;
        }

        public void SetInfo1Text( string text )
        {
            uiTopRight_Info1_TxtBx.Text = text;
        }

        public void SetInfo2Text1( string text )
        {
            uiTopRightInfo2_Text1_TxtBx.Text = text;
        }

        public void SetInfo2Text2( string text )
        {
            uiTopRightInfo2_Text2_TxtBx.Text = text;
        }

        public void SetInfo2Image1( Image image )
        {
            uiTopRightInfo2_Pic1_PicBx.Image = image;
        }

        public void SetInfo2Image2( Image image )
        {
            uiTopRightInfo2_Pic2_PicBx.Image = image;
        }

        public void SetTabLabel( TabPage tab, string label )
        {
            switch ( tab ) {
            case TabPage.Info1:
                uiTopRight_Info1_TbPg.Text = label;
                break;
            case TabPage.Info2:
                uiTopRight_Info2_TbPg.Text = label;
                break;
            }
        }

    }

}
