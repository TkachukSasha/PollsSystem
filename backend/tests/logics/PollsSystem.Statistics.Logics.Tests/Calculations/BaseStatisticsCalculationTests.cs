using FluentAssertions;
using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Common.Services;
using PollsSystem.Application.Responses.Statistics;
using PollsSystem.Statistics.Logics.Tests.Templates;

namespace PollsSystem.Statistics.Logics.Tests.Calculations;

public class BaseStatisticsCalculationTests
{
    private readonly IStatisticCalculationService _calculationService;

    public BaseStatisticsCalculationTests()
        => _calculationService = new StatisticCalculationService();

    [Fact]
    public void Get_Base_Calculation_Template_Should_Be_Not_Null()
    {
        var request = RequestTemplates.Values;

        var result = _calculationService.Calculate(StatisticType.Base, request);

        result.Should().NotBeNull();
        result.Should().BeOfType<BaseCalculations>();
    }

    [Fact]
    public void Get_Base_Calculation_Template_Should_Be_Null()
    {
        var request = new double[0];

        var result = _calculationService.Calculate(StatisticType.Base, request);

        result.Should().BeNull();
    }
}