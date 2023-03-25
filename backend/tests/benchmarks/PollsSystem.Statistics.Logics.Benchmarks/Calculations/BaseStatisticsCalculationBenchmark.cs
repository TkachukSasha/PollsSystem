using BenchmarkDotNet.Attributes;
using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Common.Services;
using PollsSystem.Statistics.Logics.Benchmarks.Templates;

namespace PollsSystem.Statistics.Logics.Benchmarks.Calculations;

public class BaseStatisticsCalculationBenchmark
{
    private readonly static IStatisticCalculationService calculationService = new StatisticCalculationService();

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
}