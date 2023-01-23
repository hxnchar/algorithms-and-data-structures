using System;

namespace lr4
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Utilities.GetString();
            MyList<string> list = Utilities.ArrayToList(Utilities.StringToArray(input));
            Utilities.OutList(list);
            Utilities.DeleteSameWords(list);
            Console.WriteLine("===================");
            Utilities.OutList(list);
        }
    }
}
