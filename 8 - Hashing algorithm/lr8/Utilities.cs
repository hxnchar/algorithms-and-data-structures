using System;
using System.Collections.Generic;

namespace lr8
{
    class Utilities
    {
        public static void OutEquality(int size = 30)
        {
            string str = "";

            for (int i = 0; i < size; i++)
                str += "=";

            Console.WriteLine(str);
        }

        public static string GetRandomString(int size)
        {
            Random r = new Random();
            string str = "";

            for (int i = 0; i < size; i++) 
                str += (char)r.Next(33, 126);

            return str;
        }

        public static Dictionary<string, string> GenerateKeysAndValues(int count, int keyLength, int valueLength)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            for (int i = 0; i < count; i++)
                data.Add(Utilities.GetRandomString(keyLength), Utilities.GetRandomString(valueLength));

            return data;
        }
    }
}
