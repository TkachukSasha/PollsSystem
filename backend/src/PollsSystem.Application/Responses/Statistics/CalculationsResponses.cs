namespace PollsSystem.Application.Responses.Statistics;

public class Calculations { };

public sealed class FullCalculations : Calculations
{
    public FullCalculations(
        BaseCalculations baseCalculationResponse,
        QuartileCalculations quartiles,
        PercentileCalculations percentiles,
        DistributionCalculations distributions,
        PopulationCalculations populations,
        AdditionalCalculations additional)
    {
        BaseCalculations = baseCalculationResponse;
        Quartiles = quartiles;
        Percentiles = percentiles;
        Distributions = distributions;
        Populations = populations;
        Additional = additional;
    }

    public BaseCalculations BaseCalculations { get; set; }
    public QuartileCalculations Quartiles { get; set; }
    public PercentileCalculations Percentiles { get; set; }
    public DistributionCalculations Distributions { get; set; }
    public PopulationCalculations Populations { get; set; }
    public AdditionalCalculations Additional { get; set; }
}

public sealed class BaseCalculations : Calculations
{
    public BaseCalculations(
        double? count,
        double? min,
        double? max,
        double? mean,
        double? median,
        double? variance,
        double? stdDev)
    {
        Count = count;
        Min = min;
        Max = max;
        Mean = mean;
        Median = median;
        Variance = variance;
        StdDev = stdDev;
    }

    public double? Count { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
    public double? Mean { get; set; }
    public double? Median { get; set; }
    public double? Variance { get; set; }
    public double? StdDev { get; set; }
}

public sealed class QuartileCalculations : Calculations
{
    public QuartileCalculations(
        double? q1,
        double? q2,
        double? q3,
        double? iQR)
    {
        Q1 = q1;
        Q2 = q2;
        Q3 = q3;
        IQR = iQR;
    }

    public double? Q1 { get; set; }
    public double? Q2 { get; set; }
    public double? Q3 { get; set; }
    public double? IQR { get; set; }
}

public sealed class PercentileCalculations : Calculations
{
    public PercentileCalculations(
        double? p25,
        double? p50,
        double? p67,
        double? p80,
        double? p85,
        double? p90,
        double? p95,
        double? p99)
    {
        P25 = p25;
        P50 = p50;
        P67 = p67;
        P80 = p80;
        P85 = p85;
        P90 = p90;
        P95 = p95;
        P99 = p99;
    }

    public double? P25 { get; set; }
    public double? P50 { get; set; }
    public double? P67 { get; set; }
    public double? P80 { get; set; }
    public double? P85 { get; set; }
    public double? P90 { get; set; }
    public double? P95 { get; set; }
    public double? P99 { get; set; }
}

public sealed class DistributionCalculations : Calculations
{
    public DistributionCalculations(
        double? skewness,
        double? kurtosis)
    {
        Skewness = skewness;
        Kurtosis = kurtosis;
    }

    public double? Skewness { get; set; }
    public double? Kurtosis { get; set; }
}

public sealed class PopulationCalculations : Calculations
{
    public PopulationCalculations(
        double? populationVariance,
        double? populationStdDev)
    {
        PopulationVariance = populationVariance;
        PopulationStdDev = populationStdDev;
    }

    public double? PopulationVariance { get; set; }
    public double? PopulationStdDev { get; set; }
}

public sealed class AdditionalCalculations : Calculations
{
    public AdditionalCalculations(
        double? absoluteMin,
        double? absoluteMax,
        double? geometricMean,
        double? harmonicMean)
    {
        AbsoluteMin = absoluteMin;
        AbsoluteMax = absoluteMax;
        GeometricMean = geometricMean;
        HarmonicMean = harmonicMean;
    }

    public double? AbsoluteMin { get; set; }
    public double? AbsoluteMax { get; set; }
    public double? GeometricMean { get; set; }
    public double? HarmonicMean { get; set; }
}