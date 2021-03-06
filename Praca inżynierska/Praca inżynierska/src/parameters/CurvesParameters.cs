﻿using PI.src.settings;

namespace PI.src.parameters
{
    public sealed class CurvesParameters
    {
        public PolynomialParameters Polynomial { get; set; } = new PolynomialParameters();
        public HyperbolicParameters Hyperbolic { get; set; } = new HyperbolicParameters();
        public WaveformParameters Waveform { get; set; } = new WaveformParameters();
    }
}
