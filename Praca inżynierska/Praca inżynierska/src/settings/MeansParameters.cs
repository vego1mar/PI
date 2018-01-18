using PI.src.enumerators;

namespace PI.src.settings
{
    public class MeansParameters
    {
        public GeometricMeanSettings Geometric { get; set; } = new GeometricMeanSettings();
        public GeometricMeanSettings AGM { get; set; } = new GeometricMeanSettings();
        public GeometricMeanSettings Heronian { get; set; } = new GeometricMeanSettings();
        public SimpleMeanSettings Harmonic { get; set; } = new SimpleMeanSettings();
        public GeneralizedMeanSettings Generalized { get; set; } = new GeneralizedMeanSettings();
        public ToleranceMeanSettings Tolerance { get; set; } = new ToleranceMeanSettings();

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
        }
    }
}
