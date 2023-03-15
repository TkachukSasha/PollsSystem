using PollsSystem.Application.Common.Enums;
using PollsSystem.Application.Responses.Statistics;
using PollsSystem.Utils.Statistics;

namespace PollsSystem.Application.Common.Services;

public interface IStatisticCalculationService
{
    Calculations? Calculate(StatisticType type, double[] values);
}

public class StatisticCalculationService : IStatisticCalculationService
{
    private double[]? _sortedValues;

    public Calculations? Calculate(StatisticType type, double[] values)
    {
        if (!values.Any()) return null;

        _sortedValues = CalculationEngine.SortValues(values);

        var (mean, stdDev) = CalculationEngine.GetBaseUsefulCalculations(values);

        return type switch
        {
            StatisticType.Base => GetBaseCalculations(values),
            StatisticType.Quartiles => GetQuartilesCalculations(_sortedValues),
            StatisticType.Percentiles => GetPercentilesCalculations(_sortedValues),
            StatisticType.Distributions => GetDistributionsCalculation(values, mean, stdDev),
            StatisticType.Populations => GetPopulationsCalculation(values),
            StatisticType.Additional => GetAdditionalCalculations(values),
            StatisticType.Full => GetFullCalculations(values, _sortedValues, mean, stdDev),
            _ => null
        };
    }

    private BaseCalculations GetBaseCalculations(double[] values)
        => new BaseCalculations(
            values.Length,
            values.SetMinimum(),
            values.SetMaximum(),
            values.SetMean(),
            values.SetMedian(),
            values.SetVariance(),
            values.SetStandartDev());

    private QuartileCalculations GetQuartilesCalculations(double[] values)
        => new QuartileCalculations(
            values.SetPercentile(25),
            values.SetMean(),
            values.SetPercentile(75),
            values.SetInterqueartileRange());

    private PercentileCalculations GetPercentilesCalculations(double[] values)
        => new PercentileCalculations(
            values.SetPercentile(25),
            values.SetMean(),
            values.SetPercentile(67),
            values.SetPercentile(80),
            values.SetPercentile(85),
            values.SetPercentile(90),
            values.SetPercentile(95),
            values.SetPercentile(99));

    private DistributionCalculations GetDistributionsCalculation(double[] values, double mean, double stdDev)
        => new DistributionCalculations(
            values.SetSkewness(mean, stdDev),
            values.SetKurtosis(mean));

    private PopulationCalculations GetPopulationsCalculation(double[] values)
        => new PopulationCalculations(
            values.SetPopulationVariance(),
            values.SetPopulationStandartDev());

    private AdditionalCalculations GetAdditionalCalculations(double[] values)
        => new AdditionalCalculations(
            values.SetAbsoluteMinimum(),
            values.SetAbsoluteMaximum(),
            values.SetGeometricMean(),
            values.SetHarmonicMean());

    private FullCalculations GetFullCalculations(double[] values, double[] sortedValues, double mean, double stdDev)
        => new FullCalculations(
            GetBaseCalculations(values),
            GetQuartilesCalculations(sortedValues),
            GetPercentilesCalculations(sortedValues),
            GetDistributionsCalculation(values, mean, stdDev),
            GetPopulationsCalculation(values),
            GetAdditionalCalculations(values));
}