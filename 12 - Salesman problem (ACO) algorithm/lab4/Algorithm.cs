using System;
using System.Collections.Generic;

namespace lab4
{
    class Algorithm
    {
		private int _countCities => _distancesMatrix.GetLength(0);

		private static int _countIterations = 100;

		private readonly int _countCommonAnts;
		private readonly int _countEliteAnts;
		private readonly int _countWildAnts;
		private readonly int _alpha;
		private readonly int _beta;
		private readonly double _ro;
		private readonly int _Lmin;
		private readonly bool _placedInOneCity;

		private int[,] _distancesMatrix;
		private double[,] _feramoneMatrix;
		private List<Ant> _commonAnts = new List<Ant>();
		private List<Ant> _eliteAnts = new List<Ant>();
		private List<Ant> _wildAnts = new List<Ant>();
		private List<int> _townsList = new List<int>();

		public Algorithm(int[,] distancesMatrix, int countCommonAnts, int countEliteAnts, int countWildAnts, int alpha, int beta, double ro, int Lmin, bool placedInOneCity)
		{
			_distancesMatrix = distancesMatrix;
			_countCommonAnts = countCommonAnts;
			_countEliteAnts = countEliteAnts;
			_countWildAnts = countWildAnts;
			_alpha = alpha;
			_beta = beta;
			_ro = ro;
			_Lmin = Lmin;
			_placedInOneCity = placedInOneCity;
			_feramoneMatrix = new double[_countCities, _countCities];
		}

		public void Solve()
		{
			InitAnts();
			InitFeramone();
			for (int i = 0; i < _countIterations; i++)
			{
				if (_placedInOneCity)
					PutAntsSame();
				else
					PutAntsDif();
				ReplaceAnts();
				UpdateBestSolution();
				UpdateFeramone();
                if (i % 20 == 0)
                {
                    Console.WriteLine($"Iteration: {i}. Length: {GetLength(_townsList)}");
                    //Console.WriteLine(ToString());
                }
            }
			Console.WriteLine("===============");
			Console.WriteLine($"Length: {GetLength(_townsList)}");
			Console.WriteLine(ToString());
		}

		private void InitAnts()
		{
			for (int i = 0; i < _countCommonAnts; i++)
			{
				_commonAnts.Add(new Ant());
			}
            for (int i = 0; i < _countEliteAnts; i++)
            {
                _eliteAnts.Add(new Ant());
            }
            for (int i = 0; i < _countWildAnts; i++)
            {
                _wildAnts.Add(new Ant());
            }
        }

		private void InitFeramone()
		{
			for (int i = 0; i < _countCities; i++)
			{
				for (int j = 0; j < _countCities; j++)
				{
					_feramoneMatrix[i, j] = i == j ? 0 : 0.2;
				}
			}
		}

		private void PutAntsSame()
		{
			Random r = new Random();
			int city = r.Next(_countCities);
			for (int i = 0; i < _countCommonAnts; i++)
			{
				_commonAnts[i].ForgetPath();
				_commonAnts[i].VisitedCities.Add(city);
			}
            for (int i = 0; i < _countEliteAnts; i++)
            {
                _eliteAnts[i].ForgetPath();
                _eliteAnts[i].VisitedCities.Add(city);
            }
            for (int i = 0; i < _countWildAnts; i++)
            {
                _wildAnts[i].ForgetPath();
                _wildAnts[i].VisitedCities.Add(city);
            }
        }

		private void PutAntsDif()
		{
			Random r = new Random();
			for (int i = 0; i < _countCommonAnts; i++)
			{
				_commonAnts[i].ForgetPath();
				_commonAnts[i].VisitedCities.Add(r.Next(_countCities));
			}
            for (int i = 0; i < _countEliteAnts; i++)
            {
                _eliteAnts[i].ForgetPath();
                _eliteAnts[i].VisitedCities.Add(r.Next(_countCities));
            }
            for (int i = 0; i < _countWildAnts; i++)
            {
                _wildAnts[i].ForgetPath();
                _wildAnts[i].VisitedCities.Add(r.Next(_countCities));
            }
        }

		private void ReplaceAnts()
		{
			for (int i = 0; i < _countCities - 1; i++)
			{
				foreach (var ant in _commonAnts)
				{
					ant.VisitedCities.Add(SelectTheNextCityCommon(ant));
				}
                foreach (var ant in _eliteAnts)
                {
                    ant.VisitedCities.Add(SelectTheNextCityElite(ant));
                }
                foreach (var ant in _wildAnts)
                {
                    ant.VisitedCities.Add(SelectTheNextCityWild(ant));
                }
            }
		}

		private int SelectTheNextCityCommon(Ant ant)
		{
			Random r = new Random();
			double randomValue = (double)r.Next(1001) / 1000;
			List<KeyValuePair<int, double>> probabilitiesList = СalculateProbabilities(ant);
			double probabilitiesSum = 0;
			for (int i = 0; i < probabilitiesList.Count; i++)
			{
				probabilitiesSum += probabilitiesList[i].Value;
				if (probabilitiesSum >= randomValue)
				{
					return i;
				}
			}
			return 1;
		}

		private int SelectTheNextCityElite(Ant ant)
		{
			Random r = new Random();
			double randomValue = (double)r.Next(1001) / 1000;
			List<KeyValuePair<int, double>> probabilitiesList = СalculateProbabilities(ant);
			KeyValuePair<int, double> maxPosability = new KeyValuePair<int, double>(0, double.MinValue);
			for (int i = 0; i < probabilitiesList.Count; i++)
			{
				if (maxPosability.Value < probabilitiesList[i].Value)
				{
					maxPosability = probabilitiesList[i];
				}
			}
			return maxPosability.Key;
		}

		private int SelectTheNextCityWild(Ant ant)
		{
			Random r = new Random();
			int nextCity = r.Next(_countCities);
			while (ant.VisitedCities.Contains(nextCity))
				nextCity = r.Next(_countCities);
			return nextCity;
		}

		private List<KeyValuePair<int, double>> СalculateProbabilities(Ant ant)
		{
			List<KeyValuePair<int, double>> probabilitiesList = new List<KeyValuePair<int, double>>();
			int from = ant.VisitedCities[^1];
			double denominator = 0;
			for (int i = 0; i < _countCities; i++)
			{
				if (!ant.VisitedCities.Contains(i) && ant.VisitedCities[^1] != i)
				{
					denominator += Math.Pow(_feramoneMatrix[from, i], _alpha) * Math.Pow((double)1 / _distancesMatrix[from, i], _beta);
				}
			}
			for (int i = 0; i < _countCities; i++)
			{
				if (!ant.VisitedCities.Contains(i) && ant.VisitedCities[^1] != i)
					probabilitiesList.Add(new KeyValuePair<int, double>(i, Math.Pow(_feramoneMatrix[from, i], _alpha) * Math.Pow((double)1 / _distancesMatrix[from, i], _beta) / denominator));
				else
					probabilitiesList.Add(new KeyValuePair<int, double>(i, 0));
			}
			return probabilitiesList;
		}

		private void UpdateFeramone()
		{
			for (int i = 0; i < _countCities; i++)
			{
				for (int j = 0; j < _countCities; j++)
				{
					_feramoneMatrix[i, j] *= (1 - _ro);
				}
			}
			foreach (var ant in _commonAnts)
			{
				double sum = _Lmin / ant.PathLength(_distancesMatrix);
				for (int i = 0; i < _countCities - 1; i++)
				{
					_feramoneMatrix[ant.VisitedCities[i], ant.VisitedCities[i + 1]] += sum;
				}
				_feramoneMatrix[ant.VisitedCities[_countCities - 1], ant.VisitedCities[0]] += sum;
			}
            foreach (var ant in _eliteAnts)
            {
                double sum = _Lmin / ant.PathLength(_distancesMatrix);
                for (int i = 0; i < _countCities - 1; i++)
                {
                    _feramoneMatrix[ant.VisitedCities[i], ant.VisitedCities[i + 1]] += sum;
                }
                _feramoneMatrix[ant.VisitedCities[_countCities - 1], ant.VisitedCities[0]] += sum;
            }
            foreach (var ant in _wildAnts)
            {
                double sum = _Lmin / ant.PathLength(_distancesMatrix);
                for (int i = 0; i < _countCities - 1; i++)
                {
                    _feramoneMatrix[ant.VisitedCities[i], ant.VisitedCities[i + 1]] += sum;
                }
                _feramoneMatrix[ant.VisitedCities[_countCities - 1], ant.VisitedCities[0]] += sum;
            }
        }

		private void UpdateBestSolution()
		{
			if (_townsList.Count == 0)
				_townsList = _commonAnts[0].VisitedCities;
			foreach (var ant in _commonAnts)
			{
				if (ant.PathLength(_distancesMatrix) < GetLength(_townsList))
				{
					_townsList = ant.VisitedCities;
				}
			}
            foreach (var ant in _eliteAnts)
            {
                if (ant.PathLength(_distancesMatrix) < GetLength(_townsList))
                {
                    _townsList = ant.VisitedCities;
                }
            }
            foreach (var ant in _wildAnts)
            {
                if (ant.PathLength(_distancesMatrix) < GetLength(_townsList))
                {
                    _townsList = ant.VisitedCities;
                }
            }
        }

		public override string ToString()
		{
			string result = string.Empty;
			for (int i = 0; i < _townsList.Count; i++)
			{
				result += $"{_townsList[i]} => ";
			}
			result += $"{_townsList[0]}";
			return result;
		}

		private int GetLength(List<int> townsList)
		{
			int length = 0;
			for (int i = 0; i < townsList.Count - 1; i++)
			{
				length += _distancesMatrix[townsList[i], townsList[i + 1]];
			}
			length += _distancesMatrix[townsList[^1], townsList[0]];
			return length;
		}
	}
}
