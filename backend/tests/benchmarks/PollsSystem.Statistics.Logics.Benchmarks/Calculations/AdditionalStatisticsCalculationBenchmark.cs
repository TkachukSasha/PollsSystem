using BenchmarkDotNet.Attributes;
using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Common.Services;
using PollsSystem.Statistics.Logics.Benchmarks.Templates;

namespace PollsSystem.Statistics.Logics.Benchmarks.Calculations;

public class AdditionalStatisticsCalculationBenchmark
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
}