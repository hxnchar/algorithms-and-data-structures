using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    class Solution
    {
        public Population[] Populations;

        private readonly int _maxIterations = 1000;
        private readonly int _availableWeight = 150;
        private readonly int _populationCount = 100;
        private readonly double _mutationChance = 0.05;
        private int _bestPopulationIndex = 0;

        public Solution()
        {
            Population population = new Population();
            Populations = new Population[_populationCount];
            for (int i = 0; i < Populations.Length; i++)
            {
                Populations[i] = new Population();
                Populations[i].IncludedItems[i] = Populations[i].AllItems[i];
            }
            UpdateBestPopulation();
        }

        public void Solve()
        {
            Random r = new Random();
            int counter = 0;
            for (int i = 0; i < _maxIterations; i++)
            {
                var crossed = CrossSolutions();
                if (crossed.TotalWeight <= _availableWeight)
                {
                    ReplacePopulations(crossed);
                    if (r.Next(0, 101) / 100 <= _mutationChance)
                    {
                        crossed.Mutation();
                    }
                }
                Populations[_bestPopulationIndex].Upgrade();
                UpdateBestPopulation();
                if (i%20==0)
                {
                    Console.WriteLine($"{++counter}:");
                    Populations[_bestPopulationIndex].PrintStats();
                    Console.WriteLine();
                }
            }
            Populations[_bestPopulationIndex].Print();
            Populations[_bestPopulationIndex].SaveToFile();
        }

        public void UpdateBestPopulation()
        {
            for (int i = 0; i < Populations.Length; i++)
            {
                if (Populations[i].TotalPrice > Populations[_bestPopulationIndex].TotalPrice)
                {
                    _bestPopulationIndex = i;
                }
            }
        }

        public Population CrossSolutions()
        {
            Random r = new Random();
            int randomPopulationIndex = r.Next(0, _populationCount);
            while (randomPopulationIndex == _bestPopulationIndex)
            {
                randomPopulationIndex = r.Next(0, _populationCount);
            }
            Population crossedPopulation = new Population();
            for (int i = 0; i < crossedPopulation.IncludedItems.Length; i++)
            {
                crossedPopulation.IncludedItems[i] = r.NextDouble() >= 0.5 ?
                    Populations[randomPopulationIndex].IncludedItems[i] : Populations[_bestPopulationIndex].IncludedItems[i];
            }
            return crossedPopulation;
        }

        public void ReplacePopulations(Population betterPopulation)
        {
            int worstPopulationIndex = FindWorstPopulationIndex();
            if (Populations[worstPopulationIndex].TotalPrice < betterPopulation.TotalPrice)
            {
                Populations[worstPopulationIndex] = betterPopulation;
            }
        }

        public int FindWorstPopulationIndex()
        {
            int worstPopulationIndex = 0;
            for (int i = 0; i < Populations.Length; i++)
            {
                if (Populations[i].TotalPrice < Populations[worstPopulationIndex].TotalPrice)
                {
                    worstPopulationIndex = i;
                }
            }
            return worstPopulationIndex;
        }
    }
}
