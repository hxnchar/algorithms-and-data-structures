using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lr3
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Utilities.GetName();
            List<Town> unvisitedTowns = new List<Town>();
            List<Town> visitedTowns = new List<Town>();
            int[][] distances = DistancesMatrix(ref unvisitedTowns, name);

            OutTowns(unvisitedTowns);
            OutCSV(name);
            IncludeClosest(distances, ref unvisitedTowns, ref visitedTowns);
        }

        public static void OutTowns(List<Town> towns)
        {
            Utilities.OutText("Так как некоторые названия занимают много места, мы их сократим:", true, false);
            foreach (var town in towns)
            {
                Console.WriteLine($"{town.Name} -> {ReduceName(town.Name)}");
            }
            Utilities.OutText("", false, true);
        }

        public static void OutCSV(string name)
        {
            int count = 0;
            using (StreamReader sr = new StreamReader(name))
            {
                string currentString;

                while ((currentString = sr.ReadLine()) != null)
                {
                    ++count;
                    if (count == 1)
                    {
                        string[] townNames = currentString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (var town in townNames)
                        {
                            Console.Write($"{ReduceName(town)}\t");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        string[] distances = currentString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (var distance in distances)
                        {
                            Console.Write($"{distance}\t");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        public static string ReduceName(string name)
        {
            string[] eachTown = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (eachTown.Length > 1)
            {
                string newName = "";
                for (int i = 0; i < eachTown.Length; i++)
                {
                    newName += $"{eachTown[i][0]}";
                }
                return newName;
            }
            else
                return name;
        }

        public static void IncludeClosest(int[][] distances, ref List<Town> unvisitedTowns, ref List<Town> visitedTowns)
        {
            ToVisited(ref unvisitedTowns, ref visitedTowns, unvisitedTowns[0]);
            while (unvisitedTowns.Count != 0)
            {
                Town[] closest = FindClosest(distances, visitedTowns, unvisitedTowns);
                ToVisited(ref unvisitedTowns, ref visitedTowns, closest[0]);
                List<Town> path = new List<Town>(closest);
                path = FindFullPath(path, visitedTowns, distances);
                OutPath(path, distances);
            }
        }

        public static void ToVisited(ref List<Town> unvisitedTowns, ref List<Town> visitedTowns, Town town)
        {
            visitedTowns.Add(town);
            unvisitedTowns.Remove(town);
        }

        public static Town[] FindClosest(int[][] matrix, List<Town> visitedTowns, List<Town> unvisitedTowns)
        {
            Town closestTown = new Town();
            Town previousTown = new Town();
            int minWay = 99999;
            foreach (var unvisitedTown in unvisitedTowns)
            {
                foreach (var visitedTown in visitedTowns)
                {
                    if (matrix[unvisitedTown.Index][visitedTown.Index] < minWay)
                    {
                        minWay = matrix[unvisitedTown.Index][visitedTown.Index];
                        closestTown = unvisitedTown;
                        previousTown = visitedTown;
                    }
                }
            }
            return new[] { closestTown, previousTown };
        }

        public static List<Town> FindFullPath(List<Town> path, List<Town> visitedTowns, int[][] matrix)
        {
            while (path.Count != visitedTowns.Count)
            {
                Town lastTown = path[path.Count - 1];
                Town nextTown = new Town();
                int minWay = 99999;
                foreach (var visitedTown in visitedTowns)
                {
                    if (!path.Contains(visitedTown) && matrix[lastTown.Index][visitedTown.Index] < minWay)
                    {
                        minWay = matrix[lastTown.Index][visitedTown.Index];
                        nextTown = visitedTown;
                    }
                }
                path.Add(nextTown);
            }
            return path;
        }

        public static void OutPath(List<Town> path, int[][] distances)
        {
            int range = 0;
            string rangeString = "";
            int currentTown = path.Count - 2;
            Utilities.OutText("Расстояние между:", true, false);
            Console.Write(path[path.Count - 1].Name);
            while (currentTown >= 0)
            {
                range += distances[path[currentTown + 1].Index][path[currentTown].Index];
                rangeString += $"{distances[path[currentTown + 1].Index][path[currentTown].Index]} + ";
                Console.Write($" —> {path[currentTown].Name}");
                currentTown--;
            }
            range += distances[path[path.Count - 1].Index][path[0].Index];
            rangeString += $"{distances[path[path.Count - 1].Index][path[0].Index]}";
            Console.WriteLine($" —> {path[path.Count - 1].Name}\nсоставляет {range} км\n({rangeString})");
        }

        public static int[][] DistancesMatrix(ref List<Town> unvisitedTowns, string path)
        {
            int[][] matrix;
            using (StreamReader sr = new StreamReader(path))
            {
                string[] towns = sr.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < towns.Length; i++)
                {
                    unvisitedTowns.Add(new Town(towns[i], i));
                }

                matrix = new int[unvisitedTowns.Count][];
                for (int i = 0; i < unvisitedTowns.Count; i++)
                {
                    matrix[i] = sr.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(distance => Convert.ToInt32(distance)).ToArray();
                }
            }
            return matrix;
        }
    }
}