using System;
using System.Collections.Generic;
using System.Text;

namespace lr2
{
    class Danzig
    {
        public static int[,] Algorithm(int[,] initialMatrix)
        {
            int[,] oldMatrix = initialMatrix;

            for (int m = 1; m <= Math.Sqrt(initialMatrix.Length); m++)
            {
                int[,] currentMatrix = new int[m, m];

                for (int i = 0; i < Math.Sqrt(currentMatrix.Length) - 1; i++)
                {
                    for (int j = 0; j < Math.Sqrt(currentMatrix.Length) - 1; j++)
                    {
                        if (i != j)
                        {
                            currentMatrix[i, j] = FindMin(oldMatrix[i, m - 2] + oldMatrix[m - 2, j], oldMatrix[i, j]);
                        }
                    }
                }

                for (int j = 0; j < Math.Sqrt(currentMatrix.Length) - 1; j++)
                {
                    int min = 99999;
                    for (int i = 0; i < Math.Sqrt(currentMatrix.Length) - 1; i++)
                    {
                        min = FindMin(oldMatrix[i, m - 2] + initialMatrix[m - 1, i], min);
                    }
                    currentMatrix[m - 1, j] = min;
                }

                for (int i = 0; i < Math.Sqrt(currentMatrix.Length) - 1; i++)
                {
                    int min = 99999;
                    for (int j = 0; j < Math.Sqrt(currentMatrix.Length) - 1; j++)
                    {
                        min = FindMin(oldMatrix[i, j] + initialMatrix[j, m - 1], min);
                    }
                    currentMatrix[i, m - 1] = min;
                }

                Utilities.OutText($"Матрица D({m}):", true);
                Out(currentMatrix);

                oldMatrix = currentMatrix;
            }
            return oldMatrix;
        }

        public static int[,] PijMatrix(int[,] initialMatrix, int[,] finalMatrix)
        {
            int[,] pijMatrix = new int[Convert.ToInt32(Math.Sqrt(initialMatrix.Length)), Convert.ToInt32(Math.Sqrt(initialMatrix.Length))];
            for (int i = 0; i < Math.Sqrt(initialMatrix.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(initialMatrix.Length); j++)
                {
                    pijMatrix[i, j] = 99999;
                    if (i != j)
                    {
                        for (int k = 0; k < Math.Sqrt(initialMatrix.Length); k++)
                        {
                            if (finalMatrix[i, k] + initialMatrix[k, j] == finalMatrix[i, j] && finalMatrix[i, j] != 99999)
                            {
                                pijMatrix[i, j] = k + 1;
                                break;
                            }
                        }
                    }
                }
            }

            Utilities.OutText($"Матрица P(i)(j):", true);
            Out(pijMatrix);

            return pijMatrix;
        }

        public static void OutPath(int[,] matr, int start, int finish)
        {
            Stack<int> path = new Stack<int>();
            start--;
            finish--;
            int current = matr[start, finish]-1;
            path.Push(finish);

            while (current != start)
            {
                path.Push(current);
                if (current > 0 && current < Math.Sqrt(matr.Length))
                {
                    current = matr[start, current] - 1;
                }
            }
            path.Push(start);

            while (path.Count != 1)
            {
                Console.Write($"{path.Pop() + 1} -> ");
            }
            Console.Write($"{path.Pop() + 1}");
        }

        private static int FindMin(int a, int b)
        {
            return a < b ? a : b;
        }

        private static void Out(int[,] matrix)
        {
            for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(matrix.Length); j++)
                {
                    if (matrix[i, j] == 99999) Console.Write($"-\t");
                    else Console.Write($"{matrix[i, j]}\t");
                }
                Console.Write('\n');
            }
        }
    }
}
