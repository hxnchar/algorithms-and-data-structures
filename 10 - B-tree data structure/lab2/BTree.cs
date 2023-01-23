using System;
using System.Collections.Generic;
using System.IO;

namespace lab2
{
    class BTree
    {
        public BNode Root;
        private const int _t = 10;
        private readonly int _maxKeys = 2 * _t - 1;
        private static int _maxKeyValue = 0;
        public int Comparisons = 0;
        public int MaxKey
        {
            get => _maxKeyValue;
            set => _maxKeyValue = value;
        }
        public void ImportFromFile(string fileName = "data.csv")
        {
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                int dataKey;
                string currentLine, dataValue;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    if (currentLine == "")
                        continue;
                    dataKey = int.Parse(currentLine.Split(';')[0]);
                    dataValue = currentLine.Split(';')[1];
                    if (Search(dataKey) == null)
                        Insert(dataKey, dataValue);
                }
            }
        }
        public void Listen()
        {
            Console.WriteLine("1 - Добавить запись");
            Console.WriteLine("2 - Редактировать запись");
            Console.WriteLine("3 - Удалить запись");
            Console.WriteLine("4 - Поиск по ключу");
            Console.WriteLine("5 - Вывести все записи");
            Console.WriteLine("6 - Выход");
            Console.WriteLine("Выберите режим работы:");
            int type = int.Parse(Console.ReadLine()), key;
            string input;
            while (type != 6)
            {
                switch (type)
                {
                    case 1:
                        key = MaxKey;
                        while (Search(key) != null)
                        {
                            key++;
                        }
                        MaxKey = key;
                        Console.WriteLine("Какую запись нужно добавить?");
                        input = Console.ReadLine();
                        Insert(key, input);
                        Console.WriteLine($"Добавлено: {key} - {input}");
                        using (StreamWriter streamWriter = new StreamWriter("data.csv", true))
                        {
                            streamWriter.WriteLine($"{key};{input}");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Введите ключ для редактирования записи:");
                        key = int.Parse(Console.ReadLine());
                        if (Search(key) != null)
                        {
                            Console.WriteLine("Введите новую запись:");
                            input = Console.ReadLine();
                            EditRecord(key, input);
                            File.Copy("data.csv", "temp.csv");
                            using (StreamReader streamReader = new StreamReader("temp.csv"))
                            {
                                using StreamWriter streamWriter = new StreamWriter("data.csv");
                                string currentLine;
                                while ((currentLine = streamReader.ReadLine()) != null)
                                {
                                    if (int.Parse(currentLine.Split(';')[0]) != key)
                                        streamWriter.WriteLine(currentLine);
                                    else
                                        streamWriter.WriteLine($"{key};{input}");
                                }
                            }
                            File.Delete("temp.csv");
                        }
                        else
                            Console.WriteLine("Записи с таким ключем нет");
                        break;

                    case 3:
                        Console.WriteLine("Введите ключ для удаления:");
                        key = int.Parse(Console.ReadLine());
                        if (Search(key) != null)
                        {
                            RemoveRecord(key);
                            File.Copy("data.csv", "temp.csv");
                            using (StreamReader streamReader = new StreamReader("temp.csv"))
                            {
                                using StreamWriter streamWriter = new StreamWriter("data.csv");
                                string currentLine;
                                while ((currentLine = streamReader.ReadLine()) != null)
                                {
                                    if (int.Parse(currentLine.Split(';')[0]) != key)
                                        streamWriter.WriteLine(currentLine);
                                }
                            }
                            File.Delete("temp.csv");
                        }
                        else
                            Console.WriteLine("Записи с таким ключем нет");
                        break;

                    case 4:
                        Console.WriteLine("Введите ключ для поиска значения");
                        key = int.Parse(Console.ReadLine());
                        if (Search(key) != null)
                        {
                            Console.WriteLine($"Запись найдена: {GetRecordByKey(key)}");
                            break;
                        }
                        else
                            Console.WriteLine("Записи с таким ключем нет");
                        break;

                    case 5:
                        Print();
                        break;

                    default:
                        break;
                }
                Console.WriteLine("Что нужно сделать?");
                type = int.Parse(Console.ReadLine());
            }
        }

        public void Print()
        {
            if (Root != null) Root.Print();
        }

        public BNode Search(int key)
        {
            if (Root != null)
            {
                return Root.Search(key);
            }
            else
                return null;
        }
        public void Insert(int requiredKey, string newValue)
        {
            if (Root == null)
            {
                Root = new BNode(true);
                Root.Data[0] = new KeyValuePair<int, string>(requiredKey, newValue);
                Root.CountKeys = 1;
            }
            else
            {
                if (Root.CountKeys == _maxKeys)
                {
                    BNode upperNode = new BNode(false);
                    upperNode.Children[0] = Root;
                    upperNode.SplitChild(0, Root);
                    int insertTo = upperNode.Data[0].Key < requiredKey ? 1 : 0;
                    upperNode.Children[insertTo].InsertionToNotFull(requiredKey, newValue);
                    Root = upperNode;
                }
                else
                    Root.InsertionToNotFull(requiredKey, newValue);
            }
            _maxKeyValue += 1;
        }

        public void RemoveRecord(int requiredKey)
        {
            if (Root != null)
            {
                Root.RemoveKey(requiredKey);
                if (Root.CountKeys == 0)
                {
                    if (Root.IsLeaf)
                        Root = null;
                    else
                        Root = Root.Children[0];
                }
            }
            else
                Console.WriteLine("Дерево пустое");
        }

        public void EditRecord(int requiredKey, string newValue)
        {
            BNode nodeWithRequiredKey = Root.Search(requiredKey);
            int index = nodeWithRequiredKey.FindIndex(requiredKey);
            nodeWithRequiredKey.Data[index] = new KeyValuePair<int, string>(requiredKey, newValue);
        }

        public string GetRecordByKey(int requiredKey)
        {
            BNode nodeWithKey = Root.Search(requiredKey);
            int index = nodeWithKey.FindIndex(requiredKey);
            return nodeWithKey.Data[index].Value;
        }
    }
}
