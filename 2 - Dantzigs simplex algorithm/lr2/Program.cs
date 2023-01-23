using System;

namespace lr2
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = Utilities.GetInt("Введите размерность", true);
            Utilities.OutText("", false, true);

            int[,] d0 = Utilities.Log(size);
            int[,] dm = Danzig.Algorithm(d0);
            int[,] path = Danzig.PijMatrix(d0, dm);

            int start = Utilities.GetInt("Введите начальную точку", true);
            int finish = Utilities.GetInt("Введите конечную точку");

            Danzig.OutPath(path, start, finish);
        }
    }
}
