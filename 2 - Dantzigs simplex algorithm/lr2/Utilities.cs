using System;
using System.Collections.Generic;
using System.Text;

namespace lr2
{
    class Utilities
    {
        public static void OutText(string str = "", bool upper = false, bool lower = false)
        {
            if (upper) OutEquality();
            if (str != "") Console.WriteLine(str);
            if (lower) OutEquality();
        }

        private static void OutEquality(int count = 30)
        {
            for (int i = 0; i < count; i++) Console.Write('=');
            Console.Write('\n');
        }

        public static int GetInt(string str, bool upper = false, bool lower = false)
        {
            OutText(str, upper, lower);
            return int.Parse(Console.ReadLine());
        }

        public static int[,] Log(int size)
        {
            int[,] matr;
            OutText("Сгенерировать матрицу случайным образом?y/n");
            if (Console.ReadLine() == "y") matr = Matrix.GenerateMatrix(size);
            else matr = Matrix.InputMyself(size);
            return matr;
        }
    }
}
