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
            Node<string> current = list.Head;
            while (current.Next != null)
            {
                Console.WriteLine(current.Value);
                current = current.Next;
            }
            Console.WriteLine(current.Value);
        }

        public static void DeleteSameWords(MyList<string> list)
        {
            Node<string> current = list.Head;
            Node<string> next;
            while (current != null)
            {
                next = current.Next;
                while (next != null)
                {
                    if (next.Value == current.Value)
                    {
                        list.Remove(current.Value);
                    }
                    next = next.Next;
                }
                current = current.Next;
            }
        }
    }
}