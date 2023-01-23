using System;
using System.Collections.Generic;

namespace Lab1
{
    class Utilities
    {
        public static int[][] InputTheMatrix()
        {
            int[][] matrix;
            OutText("Эйлеров цикл какого графа необходимо найти?", true);
            OutText("1 — сгенерировать автоматически (Вероятность наличия Эйлерового цикла низка)");
            OutText("2 — ввести вручную (Вероятность зависит от введённого графа)");
            int answer = int.Parse(Console.ReadLine());
            OutText("Введите размерность графа:", true);
            int size = int.Parse(Console.ReadLine());
            if (answer == 1)
            {
                matrix = Graph.GenMatrix(size);
                OutMatrix(matrix);
            }
            else
            {
                matrix = InputMyself(size);
            }
            IsEuler(matrix);
            return matrix;
        }

        private static bool IsEuler(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                int countInRow = 0;
                for (int j = 0; j < matrix.Length; j++)
                {
                    if (matrix[i][j] == 1 && i!=j) countInRow++;
                }
                if (countInRow % 2 == 1)
                {
                    OutText("Эйлерового цикла для данного графа не существует!", true, true);
                    return false;
                }
            }
            OutText("Эйлеров цикл для данного графа существует:", true);
            return true;
        }

        private static int[][] InputMyself(int size)
        {
            OutText("Введите граф:", true);
            int[][] matrix = new int[size][];
            for (int i = 0; i < size; i++)
            {
                matrix[i] = new int[size];
                string[] str = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < size; j++)
                {
                    matrix[i][j] = Convert.ToInt32(str[j]);
                }
            }
            return matrix;
        }

        private static void OutEquality(int count = 30)
        {
            for (int i = 0; i < count; i++) Console.Write('=');
            Console.Write('\n');
        }

        public static void OutText(string str, bool upper = false, bool lower = false)
        {

            if (upper) OutEquality();
            Console.WriteLine(str);
            if (lower) OutEquality();
        }

        public static int GetSize()
        {
            OutText("Введите размерность графа:");
            return int.Parse(Console.ReadLine());
        }

        public static void OutMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length + 1; i++)
            {
                for (int j = 0; j < matrix.Length + 1; j++)
                {
                    if (i == 0 && j == 0) Console.Write('\t');
                    else if (i == 0) Console.Write($"v{j}\t");
                    else if (j == 0) Console.Write($"v{i}\t");
                    else Console.Write($"{matrix[i - 1][j - 1]}\t");
                }
                Console.Write('\n');
            }
            Console.Write('\n');
        }

        public static void AddElement(ref int[] arr, int element)
        {
            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = element;
        }

        public static bool ArrayContainsElement(int[] arr, int elem)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == elem) return true;
            }
            return false;
        }

        public static void OutPath(List<int> path)
        {
            Utilities.OutEquality();
            for (int i = 0; i < path.Count; i++)
            {
                if (i != path.Count-1) Console.Write($"v{path[i] + 1} -> ");
                else Console.Write($"v{path[i] + 1} -> ");
            }
            Console.Write($"v{path[0] + 1}\n");
            Utilities.OutEquality();
        }
    }
}
