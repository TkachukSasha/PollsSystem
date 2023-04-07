using BenchmarkDotNet.Attributes;
using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Common.Services;
using PollsSystem.Statistics.Logics.Benchmarks.Templates;

namespace PollsSystem.Statistics.Logics.Benchmarks.Calculations;

public class CalculationsBenchmark
{
    private readonly static IStatisticCalculationService calculationService = new StatisticCalculationService();

    [Benchmark]
    public void CalculateAdditionalStatisticSmall()
    {
        var request = RequestTemplates.SmallCollection;

        _ = calculationService.Calculate(StatisticType.Additional, request);
    }

    [Benchmark]
    public void CalculateAdditionalStatisticMedium()
    {
        var request = RequestTemplates.MediumCollection;

        _ = calculationService.Calculate(StatisticType.Additional, request);
    }

    [Benchmark]
    public void CalculateAdditionalStatisticLarge()
    {
        var request = RequestTemplates.LargeCollection;

        _ = calculationService.Calculate(StatisticType.Additional, request);
    }

    [Benchmark]
    public void CalculateBaseStatisticSmall()
    {
        var request = RequestTemplates.SmallCollection;

        _ = calculationService.Calculate(StatisticType.Base, request);
    }

    [Benchmark]
    public void CalculateBaseStatisticMedium()
    {
        var request = RequestTemplates.MediumCollection;

        _ = calculationService.Calculate(StatisticType.Base, request);
    }

    [Benchmark]
    public void CalculateBaseStatisticLarge()
    {
        var request = RequestTemplates.LargeCollection;

        _ = calculationService.Calculate(StatisticType.Base, request);
    }

    [Benchmark]
    public void CalculateDistributionsStatisticSmall()
    {
        var request = RequestTemplates.SmallCollection;

        _ = calculationService.Calculate(StatisticType.Distributions, request);
    }

    [Benchmark]
    public void CalculateDistributionsStatisticMedium()
    {
        var request = RequestTemplates.MediumCollection;

        _ = calculationService.Calculate(StatisticType.Distributions, request);
    }

    [Benchmark]
    public void CalculateDistributionsStatisticLarge()
    {
        var request = RequestTemplates.LargeCollection;

        _ = calculationService.Calculate(StatisticType.Distributions, request);
    }

    [Benchmark]
    public void CalculateFullStatisticSmall()
    {
        var request = RequestTemplates.SmallCollection;

        _ = calculationService.Calculate(StatisticType.Full, request);
    }

    [Benchmark]
    public void CalculateFullStatisticMedium()
    {
        var request = RequestTemplates.MediumCollection;

        _ = calculationService.Calculate(StatisticType.Full, request);
    }

    [Benchmark]
    public void CalculateFullStatisticLarge()
    {
        var request = RequestTemplates.LargeCollection;

        _ = calculationService.Calculate(StatisticType.Full, request);
    }

    [Benchmark]
    public void CalculatePercentilesStatisticSmall()
    {
        var request = RequestTemplates.SmallCollection;

        _ = calculationService.Calculate(StatisticType.Percentiles, request);
    }

    [Benchmark]
    public void CalculatePercentilesStatisticMedium()
    {
        var request = RequestTemplates.MediumCollection;

        _ = calculationService.Calculate(StatisticType.Percentiles, request);
    }

    [Benchmark]
    public void CalculatePercentilesStatisticLarge()
    {
        var request = RequestTemplates.LargeCollection;

        _ = calculationService.Calculate(StatisticType.Percentiles, request);
    }

    [Benchmark]
    public void CalculatePopulationsStatisticSmall()
    {
        var request = RequestTemplates.SmallCollection;

        _ = calculationService.Calculate(StatisticType.Populations, request);
    }

    [Benchmark]
    public void CalculatePopulationsStatisticMedium()
    {
        var request = RequestTemplates.MediumCollection;

        _ = calculationService.Calculate(StatisticType.Populations, request);
    }

    [Benchmark]
    public void CalculatePopulationsStatisticLarge()
    {
        var request = RequestTemplates.LargeCollection;

        _ = calculationService.Calculate(StatisticType.Populations, request);
    }

    [Benchmark]
    public void CalculateQuartilesStatisticSmall()
    {
        var request = RequestTemplates.SmallCollection;

        _ = calculationService.Calculate(StatisticType.Quartiles, request);
    }

    [Benchmark]
    public void CalculateQuartilesStatisticMedium()
    {
        var request = RequestTemplates.MediumCollection;

        _ = calculationService.Calculate(StatisticType.Quartiles, request);
    }

    [Benchmark]
    public void CalculateQuartilesStatisticLarge()
    {
        var request = RequestTemplates.LargeCollection;

        _ = calculationService.Calculate(StatisticType.Quartiles, request);
    }
}
