using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Graph
    {
        public static int[][] GenMatrix(int size)
        {
            int[][] matrix = new int[size][];
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                matrix[i] = new int[size];
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j >= i) matrix[i][j] = r.Next(0, 2);
                    else matrix[i][j] = matrix[j][i];
                }
            }
            return matrix;
        }

        public static bool IsEuler(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                int countInRow = 0;
                for (int j = 0; j < matrix.Length; j++)
                {
                    if (matrix[i][j] == 1 && i!=j) countInRow++;
                }
                if (countInRow % 2 == 1) return false;
            }
            return true;
        }

        private static void SetPoint(ref int[][] matrix, int firstPoint, int secondPoint, int value = 0)
        {
            matrix[firstPoint][secondPoint] = value;
            matrix[secondPoint][firstPoint] = value;
        }

        public static List<int> AlgorithmFleurys(int[][] matrix)
        {
            Utilities.OutText("Введите номер вершины, с которой следует начать строить путь:");
            int current = int.Parse(Console.ReadLine()) - 1;
            List<int> path = new List<int>();
            path.Add(current);
            foreach (var point in matrix)
            {
                for (int i = 0; i < matrix.Length; i++)
                {
                    if (matrix[current][i] == 1)
                    {
                        if (!IsBridge(matrix, current, i) || matrix[current].Count(x => x.Equals(1)) == 1)
                        {
                            int next = i;
                            SetPoint(ref matrix, current, next);
                            current = next;
                            path.Add(current);
                            break;
                        }
                    }
                }
            }
            return path;
        }

        private static bool IsBridge(int[][] matrix, int firstPoint, int secondPoint)
        {
            SetPoint(ref matrix, firstPoint, secondPoint);
            bool isBridge = true;
            Stack<int> stack = new Stack<int>();
            List<int> visited = new List<int>();
            stack.Push(firstPoint);
            visited.Add(firstPoint);
            while (stack.Count > 0)
            {
                int current = stack.Pop();
                for (int i = 0; i < matrix.Length; i++)
                {
                    if (matrix[current][i] == 1 && !visited.Contains(i))
                    {
                        if (i == secondPoint) isBridge = false;
                        stack.Push(current);
                        stack.Push(i);
                        visited.Add(i);
                        break;
                    }
                }
            }
            SetPoint(ref matrix, firstPoint, secondPoint, 1);
            return isBridge;
        }
    }
}
