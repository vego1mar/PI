namespace PI
{
    internal class AvgInfoConsts
    {
        internal GeometricConsts Geometric { get; } = new GeometricConsts();
        internal ArithmeticGeometricConsts Agm { get; } = new ArithmeticGeometricConsts();
        internal HeronianConsts Heronian { get; } = new HeronianConsts();
        internal HarmonicConsts Harmonic { get; } = new HarmonicConsts();
        internal PowerConsts Power { get; } = new PowerConsts();
        internal RootMeanSquareConsts Rms { get; } = new RootMeanSquareConsts();

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

        internal class HeronianConsts
        {
            internal string TabPageName { get; } = "ui_Her_TbPg";
            internal string TabPageTitle { get; } = "Heronian";
            internal string TableLayoutName { get; } = "uiHer_TbLay";
            internal string TextBox1Name { get; } = "uiHer_Text1_TxtBx";
            internal string TextBox2Name { get; } = "uiHer_Text2_TxtBx";
            internal string PictureBox1Name { get; } = "uiHer_Pic1_PicBx";
            internal string PictureBox2Name { get; } = "uiHer_Pic2_PicBx";
            internal string TextBox1Text { get; } = "Heronian mean in geometry is defined as follows:";
            internal string TextBox2Text { get; } = "This was modified into:";
        }

        internal class HarmonicConsts
        {
            internal string TabPageName { get; } = "ui_Harm_TbPg";
            internal string TabPageTitle { get; } = "Harmonic";
            internal string TableLayoutName { get; } = "uiHarm_TbLay";
            internal string TextBox1Name { get; } = "uiHarm_Text1_TxtBx";
            internal string TextBox2Name { get; } = "uiHarm_Text2_TxtBx";
            internal string PictureBox1Name { get; } = "uiHarm_Pic1_PicBx";
            internal string PictureBox2Name { get; } = "uiHarm_Pic2_PicBx";
            internal string TextBox1Text { get; } = "Harmonic mean is defined for positive real numbers as follows:";
            internal string TextBox2Text { get; } = "This has not been modified, hence it can be also expressed as:";
        }

        internal class PowerConsts
        {
            internal string TabPageName { get; } = "ui_Pow_TbPg";
            internal string TabPageTitle { get; } = "Power";
            internal string TableLayoutName { get; } = "uiPow_TbLay";
            internal string TextBox1Name { get; } = "uiPow_Text1_TxtBx";
            internal string TextBox2Name { get; } = "uiPow_Text2_TxtBx";
            internal string PictureBox1Name { get; } = "uiPow_Pic1_PicBx";
            internal string PictureBox2Name { get; } = "uiPow_Pic2_PicBx";
            internal string TextBox1Text { get; } = "Power mean is defined for a specified rank k as below:";
            internal string TextBox2Text { get; } = "This has not been modified, yet the default rank k is:";
        }

        internal class RootMeanSquareConsts
        {
            internal string TabPageName { get; } = "ui_Rms_TbPg";
            internal string TabPageTitle { get; } = "RMS";
            internal string TableLayoutName { get; } = "uiRms_TbLay";
            internal string TextBox1Name { get; } = "uiRms_Text1_TxtBx";
            internal string TextBox2Name { get; } = "uiRms_Text2_TxtBx";
            internal string PictureBox1Name { get; } = "uiRms_Pic1_PicBx";
            internal string PictureBox2Name { get; } = "uiRms_Pic2_PicBx";
            internal string TextBox1Text { get; } = "Root mean square is defined as follows:";
            internal string TextBox2Text { get; } = "This is computed using a power mean in a form as below:";
        }

    }
}
