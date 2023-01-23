using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    class Utilities
    {
        public static string GetLine(int count)
        {
            string res = string.Empty;
            for (int i = 0; i < count; i++)
            {
                res += "=";
            }
            return res;
        }

        public static void PrintMsg(string text, int count = 30)
        {
            Console.WriteLine(GetLine(count));
            Console.WriteLine(text);
            Console.WriteLine(GetLine(count));
        }
    }
}
