namespace PollsSystem.Utils.Statistics;

public static class CalculationEngine
{
    /// <summary>
    /// Get usefull calculations for all use cases
    /// </summary>
    /// <param name="values">Array of numbers</param>
    /// <returns>mean and stdDev</returns>
    public static (double mean, double stdDev) GetBaseUsefulCalculations(double[] values)
    {
        var mean = values.SetMean();
        var stdDev = values.SetStandartDev();

        return (mean, stdDev);
    }

    /// <summary>
    /// Default array sort implementation
    /// </summary>
    /// <param name="values">Array of numbers</param>
    /// <returns>sorted array</returns>
    public static double[] SortValues(double[] values)
    {
        var sortedValues = new double[values.Length];
        values.CopyTo(sortedValues, 0);
        Array.Sort(sortedValues);

        return sortedValues;
    }

    /// <summary>
    /// Set result in perrcents
    /// </summary>
    /// <param name="result">Score</param>
    /// <param name="numberOfQuestions">Number of questions</param>
    /// <returns>Percent</returns>
    public static double SetPercents(this double result, int numberOfQuestions)
        => result * 100d / numberOfQuestions;

    /// <summary>
    /// Set math minimum of the sorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Minimum value of collection</returns>
    public static double SetMinimum(this double[] values)
    {
        if (!values.Any()) return double.NaN;

        double min = double.PositiveInfinity;

        for (int item = 0; item < values.Length; item++)
            if (values[item] < min || double.IsNaN(values[item]))
                min = values[item];

        return min;
    }

    /// <summary>
    /// Set math maximum of the sorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Maximum value of collection</returns>
    public static double SetMaximum(this double[] values)
    {
        if (!values.Any()) return double.NaN;

        double max = double.NegativeInfinity;

        for (int item = 0; item < values.Length; item++)
            if (values[item] > max || double.IsNaN(values[item]))
                max = values[item];

        return max;
    }

    /// <summary>
    /// Set standart dev mean of the collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Standart dev mean of the collection</returns>
    public static Dictionary<double, double> SetMeanStandartDev(this double[] values)
        => new Dictionary<double, double>()
        {
            { SetMean(values), SetStandartDev(values) }
        };

    /// <summary>
    /// Set variance mean of the collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Variance mean of the collection</returns>
    public static Dictionary<double, double> SetMeanVariance(this double[] values)
        => new Dictionary<double, double>()
        {
            { SetMean(values), SetVariance(values) }
        };

    /// <summary>
    /// Set math absolute minimum of the unsorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Absolute minimum of collection</returns>
    public static double SetAbsoluteMinimum(this double[] values)
    {
        if (!values.Any()) return double.NaN;

        var min = double.PositiveInfinity;

        for (int item = 0; item < values.Length; item++)
            if (Math.Abs(values[item]) < min || double.IsNaN(values[item]))
                min = Math.Abs(values[item]);

        return min;
    }

    /// <summary>
    /// Set math absolute maximum of the unsorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Absolute maximum of collection</returns>
    public static double SetAbsoluteMaximum(this double[] values)
    {
        if (!values.Any()) return double.NaN;

        var max = 0.0d;

        for (int item = 0; item < values.Length; item++)
            if (Math.Abs(values[item]) > max || double.IsNaN(values[item]))
                max = Math.Abs(values[item]);

        return max;
    }

    /// <summary>
    /// Set mean of the sorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Mean value of collection result</returns>
    public static double SetMean(this double[] values)
    {
        double mean = 0d;
        ulong marker = 0;

        if (!values.Any()) return double.NaN;

        for (int item = 0; item < values.Length; item++)
            mean += (values[item] - mean) / ++marker;

        return mean;
    }

    /// <summary>
    /// Set gometric mean of the unsorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Geometric mean of the collection</returns>
    public static double SetGeometricMean(this double[] values)
    {
        double collectionSum = 0d;

        if (!values.Any()) return double.NaN;

        for (int item = 0; item < values.Length; item++)
            collectionSum += Math.Log(values[item]);

        return Math.Exp(collectionSum / values.Length);
    }

    /// <summary>
    /// Set harmonic mean of the unsorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Harmonic mean of the collection</returns>
    public static double SetHarmonicMean(this double[] values)
    {
        double collectionSum = 0d;

        if (!values.Any()) return double.NaN;

        for (int item = 0; item < values.Length; item++)
            collectionSum += 1.0 / values[item];

        return values.Length / collectionSum;
    }

    /// <summary>
    /// Set median of the unsorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Median of the collection</returns>
    public static double SetMedian(this double[] values)
    {
        if (!values.Any() || values is null) return 0;

        int collectionSize = values.Length;

        int midElement = collectionSize / 2;

        return (collectionSize % 2 is not 0) ? values[midElement] : (values[midElement] + values[midElement - 1]) / 2;
    }

    /// <summary>
    /// Set percentile of the collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <param name="percentile">N of percentile</param>
    /// <returns>Percentile of the collection</returns>
    public static double SetPercentile(this double[] values, double percentile)
    {
        double leftNumber = 0d, rightNumber = 0d;

        if (percentile >= 100d) return values[values.Length - 1];

        double position = (double)(values.Length + 1) * percentile / 100.0;

        double n = percentile / 100d * (values.Length - 1) + 1.0d;

        if (position >= 1)
        {
            leftNumber = values[(int)Math.Floor(n) - 1];
            rightNumber = values[(int)Math.Floor(n)];
        }
        else
        {
            leftNumber = values[0];
            rightNumber = values[1];
        }

        if (Equals(leftNumber, rightNumber)) return leftNumber;

        double part = n - Math.Floor(n);

        return leftNumber + part * (rightNumber - leftNumber);
    }

    /// <summary>
    /// Set first quartile of collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>First quartile of collection</returns>
    public static double SetFirstQuartile(this double[] values)
        => values.SetPercentile(25);

    /// <summary>
    /// Set third quartile of collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Third quartile of collection</returns>
    public static double SetThirdQuartile(this double[] values)
        => values.SetPercentile(75);

    /// <summary>
    /// Set interquartile range of the collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Interquartile range</returns>
    public static double SetInterqueartileRange(this double[] values)
        => values.SetPercentile(75) - values.SetPercentile(25);

    /// <summary>
    /// Set standart dev of the unsorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Standart dev of the collection>/returns>
    public static double SetStandartDev(this double[] values)
        => Math.Sqrt(SetVariance(values));

    /// <summary>
    /// Set population standart dev of the unsorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Population standart dev of the collection>/returns>
    public static double SetPopulationStandartDev(this double[] values)
        => Math.Sqrt(SetPopulationVariance(values));

    /// <summary>
    /// Set variance of the unsorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Variance of the collection</returns>
    public static double SetVariance(this double[] values)
    {
        double variance = 0d;
        double tMarker = values[0];

        if (values.Length <= 1) return double.NaN;

        for (int item = 1; item < values.Length; item++)
        {
            tMarker += values[item];
            double diff = ((item + 1) * values[item]) - tMarker;
            variance += (diff * diff) / ((item + 1.0) * item);
        }

        return variance / (values.Length - 1);
    }

    /// <summary>
    /// Set Skewness of the collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <param name="mean">Mean</param>
    /// <param name="stdDev">StdDev</param>
    /// <returns>Skewness</returns>
    public static double SetSkewness(this double[] values, double mean, double stdDev)
    {
        double n = values.Length;
        double skewnessResult = 0.0d, result = 0.0d;

        for (int item = 1; item < values.Length; item++)
            skewnessResult += Math.Pow((values[item] - mean) / stdDev, 3);

        result = n / (n - 1) / (n - 2) * skewnessResult;

        return result;
    }

    /// <summary>
    /// Set kurtosis of the collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <param name="mean">Mean</param>
    /// <param name="isUnbiased">IsUnbiased</param>
    /// <returns>Collection</returns>
    public static double SetKurtosis(this double[] values, double mean, bool isUnbiased = true)
    {
        double n = values.Length;

        double s2 = 0;
        double s4 = 0;

        for (int item = 0; item < values.Length; item++)
        {
            double dev = values[item] - mean;

            s2 += dev * dev;
            s4 += dev * dev * dev * dev;
        }

        double m2 = s2 / n;
        double m4 = s4 / n;

        if (isUnbiased)
        {
            double v = s2 / (n - 1);

            double a = (n * (n + 1)) / ((n - 1) * (n - 2) * (n - 3));
            double b = s4 / (v * v);
            double c = ((n - 1) * (n - 1)) / ((n - 2) * (n - 3));

            return a * b - 3 * c;
        }
        else
            return m4 / (m2 * m2) - 3;
    }

    /// <summary>
    /// Set population variance of the unsorted collection
    /// </summary>
    /// <param name="values">Collection</param>
    /// <returns>Population variance of the collection</returns>
    public static double SetPopulationVariance(this double[] values)
    {
        double variance = 0d;
        double tMarker = values[0];

        if (!values.Any()) return double.NaN;

        for (int item = 1; item < values.Length; item++)
        {
            tMarker += values[item];
            double diff = ((item + 1) * values[item]) - tMarker;
            variance += (diff * diff) / ((item + 1.0) * item);
        }

        return variance / values.Length;
    }

    /// <summary>
    /// Set covariance of the unsorted collection
    /// </summary>
    /// <param name="firstCollection">First collection</param>
    /// <param name="secondCollection">Second collection</param>
    /// <returns>Covariance value</returns>
    public static double SetCovariance(double[] firstCollection, double[] secondCollection)
    {
        double covariance = 0d;

        if (firstCollection.Length != secondCollection.Length) return 0d;

        if (firstCollection.Length <= 1) return double.NaN;

        double firstCollectionMean = SetMean(firstCollection);
        double secindCollectionMean = SetMean(secondCollection);

        for (int item = 0; item < firstCollection.Length; item++)
            covariance += (firstCollection[item] - firstCollectionMean) * (secondCollection[item] - secindCollectionMean);

        return covariance / (firstCollection.Length - 1);
    }

    /// <summary>
    /// Set population covariance of the unsorted collection
    /// </summary>
    /// <param name="firstCollection">First collection</param>
    /// <param name="secondCollection">Second collection</param>
    /// <returns>Population covariance value</returns>
    public static double SetPopulationCovariance(double[] firstCollection, double[] secondCollection)
    {
        double covariance = 0d;

        if (firstCollection.Length != secondCollection.Length) return 0d;

        if (firstCollection.Length <= 1) return double.NaN;

        double firstCollectionMean = SetMean(firstCollection);
        double secindCollectionMean = SetMean(secondCollection);

        for (int item = 0; item < firstCollection.Length; item++)
            covariance += (firstCollection[item] - firstCollectionMean) * (secondCollection[item] - secindCollectionMean);

        return covariance / firstCollection.Length;
    }
}