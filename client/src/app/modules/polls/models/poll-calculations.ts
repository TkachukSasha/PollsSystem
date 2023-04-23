export interface ICalculations{

}

export interface IPollCalculations extends ICalculations{
  baseCalculations: IBaseCalculations,
  quartiles: IQueartilesCalculations,
  percentiles: IPercentilesCalculations,
  distributions: IDistributionsCalculations,
  populations: IPopulationsCalculations,
  additional: IAdditionalCalculations,
}

export interface IBaseCalculations extends ICalculations{
  count: number,
  min: number,
  max: number,
  mean: number,
  median: number,
  variance: number,
  stdDev: number
}

export interface IQueartilesCalculations extends ICalculations{
  q1: number,
  q2: number,
  q3: number,
  iqr: number
}

export interface IPercentilesCalculations extends ICalculations{
  p25: number,
  p50: number,
  p67: number,
  p80: number,
  p85: number,
  p90: number,
  p95: number,
  p99: number
}

export interface IDistributionsCalculations extends ICalculations{
  skewness: number,
  kurtosis: number
}

export interface IPopulationsCalculations extends ICalculations{
  populationVariance: number,
  populationStdDev: number
}

export interface IAdditionalCalculations extends ICalculations{
  absoluteMin: number,
  absoluteMax: number,
  geometricMean: number,
  harmonicMean: number
}
