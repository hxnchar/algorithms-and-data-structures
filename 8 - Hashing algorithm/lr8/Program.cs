using System;
using System.Collections.Generic;

namespace lr8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сколько элементов должно быть в таблице?");
            int countElements = int.Parse(Console.ReadLine());
            Dictionary<string, string> keyValuePairs = Utilities.GenerateKeysAndValues(countElements, 20, 200);
            HashTable<Item> hashTable = new HashTable<Item>(keyValuePairs);

            hashTable.OutTable();

            Console.WriteLine("Введите ключ");
            Console.WriteLine($"Значение: {hashTable[Console.ReadLine()]}");
        }
    }
}