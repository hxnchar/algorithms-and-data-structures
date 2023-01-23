using System;
using System.Collections.Generic;
using System.Text;

namespace lr2
{
    class Matrix
    {
        public static int[,] GenerateMatrix(int size, int maxSize=15, int chanceOfSingle = 40)
        {
            int[,] matrix = new int[size, size];
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j) matrix[i,j] = 0;
                    else if (matrix[j,i] == 0 && ChanceIsTrue(chanceOfSingle) && i < j) matrix[i,j] = r.Next(1, maxSize + 1);
                    else matrix[i,j] = 99999;
                }
            }

            Utilities.OutText("Введённая матрица:", true);
            OutMatrix(matrix);
            return matrix;
        }

        public static int[,] InputMyself(int size, int maxSize = 15, int chanceOfSingle = 40)
        {
            int[,] matrix = new int[size, size];
            Utilities.OutText("Введите '-' для обозначения бесконечности");
            for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                string[] row = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < Math.Sqrt(matrix.Length); j++)
                {
                    if (row[j] == "-") matrix[i, j] = 99999;
                    else matrix[i, j] = Convert.ToInt32(row[j]);
                }
            }

            Utilities.OutText("Введенная матрица:", true);
            OutMatrix(matrix);
            return matrix;
        }

        public static void OutMatrix(int[,] matrix)
        {
            for (int i = 0; i < Math.Sqrt(matrix.Length) + 1; i++)
            {
                for (int j = 0; j < Math.Sqrt(matrix.Length) + 1; j++)
                {
                    if (i == 0 && j == 0) Console.Write('\t');
                    else if (i == 0) Console.Write($"v{j}\t");
                    else if (j == 0) Console.Write($"v{i}\t");
                    else
                    {
                        if (matrix[i - 1, j - 1] == 99999) Console.Write($"inf\t");
                        else Console.Write($"{matrix[i - 1, j - 1]}\t");
                    }
                    
                }
                Console.Write('\n');
            }
        }

        private static bool ChanceIsTrue(int percantage)
        {
            Random r = new Random();
            return r.Next(0, 101) <= percantage ? true : false;
        }
    }
}
