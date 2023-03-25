using BenchmarkDotNet.Attributes;
using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Common.Services;
using PollsSystem.Statistics.Logics.Benchmarks.Templates;

namespace PollsSystem.Statistics.Logics.Benchmarks.Calculations;

public class QuartilesStatisticsCalculationBenchmark
{
    private readonly static IStatisticCalculationService calculationService = new StatisticCalculationService();

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