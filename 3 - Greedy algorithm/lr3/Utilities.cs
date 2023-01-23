using System;

namespace lr3
{
    public class Utilities
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

        public static string GetName()
        {
            OutText("1 — линии, напрямую соединяющие города; 2 — автомобильные маршруты");
            return Console.ReadLine() == "1" ? "townsLine.csv" : "townsRoute.csv";
        }
    }
}
