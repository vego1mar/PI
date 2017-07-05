namespace PI
{
    public class Params
    {
        public Polynomials Polynomial { get; set; } = new Polynomials();
        public Hyperbolics Hyperbolic { get; set; } = new Hyperbolics();
        public Waveforms Waveform { get; set; } = new Waveforms();

        public class Polynomials
        {
            public double A { get; set; } = 1.0;
            public double B { get; set; } = 3.0;
            public double C { get; set; } = 1.0;
            public double D { get; set; } = 0.0;
            public double E { get; set; } = 1.0;
            public double F { get; set; } = 1.0;
            public double I { get; set; } = 0.0;
        }

        public class Hyperbolics
        {
            public double A { get; set; } = 1.0;
            public double B { get; set; } = 2.0;
            public double C { get; set; } = 1.0;
            public double D { get; set; } = 2.0;
            public double F { get; set; } = 1.0;
            public double I { get; set; } = 0.0;
        }

        public class Waveforms
        {
            public double M { get; set; } = 1.0;
            public double N { get; set; } = 10.0;
            public double O { get; set; } = 0.0;
            public double K { get; set; } = 0.0;
        }
    }
}
