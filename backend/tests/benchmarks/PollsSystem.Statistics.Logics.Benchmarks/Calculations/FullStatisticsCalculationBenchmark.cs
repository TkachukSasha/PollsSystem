using BenchmarkDotNet.Attributes;
using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Common.Services;
using PollsSystem.Statistics.Logics.Benchmarks.Templates;

namespace PollsSystem.Statistics.Logics.Benchmarks.Calculations;

public class FullStatisticsCalculationBenchmark
{
    private readonly static IStatisticCalculationService calculationService = new StatisticCalculationService();

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
}