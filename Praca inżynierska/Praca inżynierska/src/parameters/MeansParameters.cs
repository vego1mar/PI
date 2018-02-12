using PI.src.enumerators;

namespace PI.src.parameters
{
    public class MeansParameters
    {
        public GeometricMeanSettings Geometric { get; set; } = new GeometricMeanSettings();
        public GeometricMeanSettings AGM { get; set; } = new GeometricMeanSettings();
        public GeometricMeanSettings Heronian { get; set; } = new GeometricMeanSettings();
        public SimpleMeanSettings Harmonic { get; set; } = new SimpleMeanSettings();
        public GeneralizedMeanSettings Generalized { get; set; } = new GeneralizedMeanSettings();
        public ToleranceMeanSettings Tolerance { get; set; } = new ToleranceMeanSettings();
        public CentralMeanSettings Central { get; set; } = new CentralMeanSettings();
        public NearestNeighborsSettings NN { get; set; } = new NearestNeighborsSettings();
        public NadarayaWatsonSettings NadarayaWatson { get; set; } = new NadarayaWatsonSettings();

        public class GeometricMeanSettings
        {
            public GeometricMeanVariant Variant { get; set; } = GeometricMeanVariant.Offset;
        }

        public class SimpleMeanSettings
        {
            public StandardMeanVariants Variant { get; set; } = StandardMeanVariants.Offset;
        }

        public class GeneralizedMeanSettings : SimpleMeanSettings
        {
            public int Rank { get; set; } = 2;
        }

        public class ToleranceMeanSettings
        {
            public double Tolerance { get; set; } = 1.05;
            public MeanType Finisher { get; set; } = MeanType.Harmonic;
        }

        public class CentralMeanSettings
        {
            public short MassPercent { get; set; } = 50;
        }

        public class NearestNeighborsSettings
        {
            public int Amount { get; set; } = 8;
        }

        public class NadarayaWatsonSettings
        {
            public NadarayaWatsonVariant Variant { get; set; } = NadarayaWatsonVariant.Subsitution;
            public KernelType KernelType { get; set; } = KernelType.Gaussian;
            public double KernelSize { get; set; } = 0.2;
        }
    }
}
