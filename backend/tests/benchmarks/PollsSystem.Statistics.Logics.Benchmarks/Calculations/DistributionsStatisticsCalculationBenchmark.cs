using BenchmarkDotNet.Attributes;
using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Common.Services;
using PollsSystem.Statistics.Logics.Benchmarks.Templates;

namespace PollsSystem.Statistics.Logics.Benchmarks.Calculations;

public class DistributionsStatisticsCalculationBenchmark
{
    private readonly static IStatisticCalculationService calculationService = new StatisticCalculationService();

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
}