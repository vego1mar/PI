using PI.src.localization.general;
using static PI.src.localization.general.LanguageAssist;

namespace PI.src.localization.windows
{
    public class PatternCurveDefinerStrings
    {
        public PatternCurveDefinerFormStrings Form { get; private set; } = new PatternCurveDefinerFormStrings();
        public PatternCurveDefinerUiStrings Ui { get; private set; } = new PatternCurveDefinerUiStrings();

        public class PatternCurveDefinerFormStrings
        {
            public LocalizedString Text { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Form_Text" ); } }
        }

        public class PatternCurveDefinerUiStrings
        {
            public LocalizedString CommonCancel { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Ui_Cancel" ); } }
            public LocalizedString CommonOk { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Ui_OK" ); } }
            public LocalizedString PolynomialTitle { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Polynomial_Polynomial" ); } }
            public LocalizedString PolynomialParameters { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Polynomial_Parameters" ); } }
            public LocalizedString HyperbolicTitle { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Hyperbolic" ); } }
            public LocalizedString HyperbolicParameters { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Parameters" ); } }
            public LocalizedString HyperbolicBoundAc { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Bound_ac" ); } }
            public LocalizedString HyperbolicBoundBd { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Hyperbolic_Bound_bd" ); } }
            public LocalizedString WaveformTitle { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Waveform" ); } }
            public LocalizedString WaveformParameters { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Parameters" ); } }
            public LocalizedString WaveformWaveType { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_WaveType" ); } }
            public LocalizedString WaveformSine { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Sine" ); } }
            public LocalizedString WaveformSquare { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Square" ); } }
            public LocalizedString WaveformTriangle { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Triangle" ); } }
            public LocalizedString WaveformSawtooth { get { return new LocalizedString( CurrentLanguage, "PatternCurveDefiner_Tabs_Waveform_Sawtooth" ); } }
        }
    }
}
