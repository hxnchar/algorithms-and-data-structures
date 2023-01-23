using System;
using System.IO;

namespace lr4
{
    class Utilities
    {
        public static string GetString(string name = "input.txt")
        {
            using (StreamReader sr = new StreamReader(name))
            {
                return sr.ReadLine();
            }
        }

        public static string[] StringToArray(string str)
        {
            return str.Split(';', StringSplitOptions.RemoveEmptyEntries);
        }

        public static MyList<string> ArrayToList(string[] words)
        {
            MyList<string> list = new MyList<string>();
            foreach (var word in words)
            {
                list.Add(word);
            }
            return list;
        }

        public static void OutList(MyList<string> list)
        {
            for (int i = 0; i <= list.Head; i++)
            {
                Console.WriteLine(list.GetElement(i));
            }
        }

        public static void DeleteSameWords(MyList<string> list)
        {
            bool someDeleted;
            for (int i = 0; i <= list.Head; i++)
            {
                someDeleted = false;
                for (int j = i+1; j <= list.Head; j++)
                {
                    if (list.GetElement(i) == list.GetElement(j))
                    {
                        someDeleted = true;
                        list.Remove(j);
                        j--;
                    }
                }
                if (someDeleted)
                {
                    list.Remove(i);
                    i--;
                }

            }
        }
    }
}
