using FluentAssertions;
using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Common.Services;
using PollsSystem.Application.Responses.Statistics;
using PollsSystem.Statistics.Logics.Tests.Templates;

namespace PollsSystem.Statistics.Logics.Tests.Calculations;

public class DistributionsStatisticsCalculationTests
{
    private readonly IStatisticCalculationService _calculationService;

    public DistributionsStatisticsCalculationTests()
        => _calculationService = new StatisticCalculationService();

    [Fact]
    public void Get_Distributions_Calculation_Template_Should_Be_Not_Null()
    {
        var request = RequestTemplates.Values;

        var result = _calculationService.Calculate(StatisticType.Distributions, request);

        result.Should().NotBeNull();
        result.Should().BeOfType<DistributionCalculations>();
    }

    [Fact]
    public void Get_Distributions_Calculation_Template_Should_Be_Null()
    {
        var request = new double[0];

        var result = _calculationService.Calculate(StatisticType.Distributions, request);

        result.Should().BeNull();
    }
}