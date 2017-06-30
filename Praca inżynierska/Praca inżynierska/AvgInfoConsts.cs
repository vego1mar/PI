namespace PI
{
    internal class AvgInfoConsts
    {
        internal GeometricMeanConsts Geometric { get; } = new GeometricMeanConsts();
        internal ArithmeticGeometricMeanConsts Agm { get; } = new ArithmeticGeometricMeanConsts();
        internal HeronianMeanConsts Heronian { get; } = new HeronianMeanConsts();
        internal HarmonicMeanConsts Harmonic { get; } = new HarmonicMeanConsts();
        internal PowerMeanConsts Power { get; } = new PowerMeanConsts();
        internal RootMeanSquareConsts Rms { get; } = new RootMeanSquareConsts();
        internal LogarithmicMeanConsts Logarithmic { get; } = new LogarithmicMeanConsts();
        internal ExponentialMovingAverageConsts Ema { get; } = new ExponentialMovingAverageConsts();
        internal LogarithmicallyWagedMeanConsts LnWages { get; } = new LogarithmicallyWagedMeanConsts();

        internal class GeometricMeanConsts
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

        internal class ArithmeticGeometricMeanConsts
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

        internal class HeronianMeanConsts
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

        internal class HarmonicMeanConsts
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

        internal class PowerMeanConsts
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

        internal class LogarithmicMeanConsts
        {
            internal string TabPageName { get; } = "ui_Log_TbPg";
            internal string TabPageTitle { get; } = "Logarithmic";
            internal string TableLayoutName { get; } = "uiLog_TbLay";
            internal string TextBox1Name { get; } = "uiLog_Text1_TxtBx";
            internal string TextBox2Name { get; } = "uiLog_Text2_TxtBx";
            internal string PictureBox1Name { get; } = "uiLog_Pic1_PicBx";
            internal string PictureBox2Name { get; } = "uiLog_Pic2_PicBx";
            internal string TextBox1Text { get; } = "Logarithmic mean is defined as follows:";
            internal string TextBox2Text { get; } = "This has been modified into:";
        }

        internal class ExponentialMovingAverageConsts
        {
            internal string TabPageName { get; } = "ui_Ema_TbPg";
            internal string TabPageTitle { get; } = "EMA";
            internal string TableLayoutName { get; } = "uiEma_TbLay";
            internal string TextBox1Name { get; } = "uiEma_Text1_TxtBx";
            internal string TextBox2Name { get; } = "uiEma_Text2_TxtBx";
            internal string PictureBox1Name { get; } = "uiEma_Pic1_PicBx";
            internal string PictureBox2Name { get; } = "uiEma_Pic2_PicBx";
            internal string TextBox1Text { get; } = "Exponential moving average is defined as follows:";
            internal string TextBox2Text { get; } = "This has been modified into:";
        }

        internal class LogarithmicallyWagedMeanConsts
        {
            internal string TabPageName { get; } = "ui_LnWgs_TbPg";
            internal string TabPageTitle { get; } = "Ln-wages";
            internal string TableLayoutName { get; } = "uiLnWgs_TbLay";
            internal string TextBox1Name { get; } = "uiLnWgs_Text1_TxtBx";
            internal string TextBox2Name { get; } = "uiLnWgs_Text2_TxtBx";
            internal string PictureBox1Name { get; } = "uiLnWgs_Pic1_PicBx";
            internal string PictureBox2Name { get; } = "uiLnWgs_Pic2_PicBx";
            internal string TextBox1Text { get; } = "The standard waged mean is defined as follows:";
            internal string TextBox2Text { get; } = "This has been modified into logarithmically waged mean as below:";
        }

    }
}
