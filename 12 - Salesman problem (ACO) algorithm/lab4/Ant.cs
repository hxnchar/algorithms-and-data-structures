using System.Collections.Generic;

namespace lab4
{
	public class Ant
	{
		public List<int> VisitedCities;

		public Ant()
		{
			VisitedCities = new List<int>();
		}

		public void ForgetPath()
		{
			VisitedCities = new List<int>();
		}

		public int PathLength(int[,] distancesMatrix)
		{
			int length = 0;
			for (int i = 0; i < VisitedCities.Count - 1; i++)
			{
				length += distancesMatrix[VisitedCities[i], VisitedCities[i + 1]];
			}
			length += distancesMatrix[VisitedCities[^1], VisitedCities[0]];
			return length;
		}
	}
}
