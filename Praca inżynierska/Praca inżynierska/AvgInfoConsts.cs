namespace PI
{
    internal class AvgInfoConsts
    {
        internal GeometricConsts Geometric { get; } = new GeometricConsts();
        internal ArithmeticGeometricConsts Agm { get; } = new ArithmeticGeometricConsts();

        internal class GeometricConsts
        {
            internal string TabPageName { get; } = "ui_Gm_TbPg";
            internal string TabPageTitle { get; } = "Geometric";
            internal string TableLayoutName { get; } = "uiGm_TbLay";
            internal string TextBox1Name { get; } = "uiGm_Text1_TxtBx";
            internal string TextBox2Name { get; } = "uiGm_Text2_TxtBx";
            internal string PictureBox1Name { get; } = "uiGm_Pic1_PicBx";
            internal string PictureBox2Name { get; } = "uiGm_Pic2_PicBx";
            internal string TextBox1Text { get; } = "Geometric mean is defined as follows:";
            internal string TextBox2Text { get; } = "This was modified into:";
        }

        internal class ArithmeticGeometricConsts
        {
            internal string TabPageName { get; } = "ui_Agm_TbPg";
            internal string TabPageTitle { get; } = "AGM";
            internal string TableLayoutName { get; } = "uiAgm_TbLay";
            internal string TextBox1Name { get; } = "uiAgm_Text1_TxtBx";
            internal string TextBox2Name { get; } = "uiAgm_Text2_TxtBx";
            internal string PictureBox1Name { get; } = "uiAgm_Pic1_PicBx";
            internal string PictureBox2Name { get; } = "uiAgm_Pic2_PicBx";
            internal string TextBox1Text { get; } = "Arithmetic-geometric mean is defined as follows:";
            internal string TextBox2Text { get; } = "This was modified into:";
        }

    }
}
