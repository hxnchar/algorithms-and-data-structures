using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab3
{
    class Population
    {
        public static Item[] ArrayAllItems;
        public Item[] AllItems => ArrayAllItems;
        public Item[] IncludedItems;
        public int TotalPrice
        {
            get
            {
                int totalPrice = 0;
                for (int i = 0; i < IncludedItems.Length; i++)
                {
                    if (IncludedItems[i] != null)
                    {
                        totalPrice += IncludedItems[i].Value;
                    }
                }

                return totalPrice;
            }
        }
        public int TotalWeight
        {
            get
            {
                int totalWeight = 0;
                foreach (var item in IncludedItems)
                {
                    if (item != null)
                    {
                        totalWeight += item.Weight;
                    }
                }
                return totalWeight;
            }
        }

        private readonly int _countItems = 100;
        private readonly int _availableWeight = 150;

        public Population()
        {
            if (ArrayAllItems == null)
            {
                ArrayAllItems = new Item[_countItems];
                ImportFromFile();
                //for (int i = 0; i < ArrayAllItems.Length; i++)
                //{
                //    ArrayAllItems[i] = new Item();
                //}
            }
            IncludedItems = new Item[_countItems];
        }

        public void Upgrade()
        {
            Item itemToAdd = new Item(0, int.MaxValue);
            int minWeight = int.MaxValue, indexOfItem = 0;
            Item[] availableItems = new Item[_countItems];
            Item[] itemsWithMinWeight = new Item[_countItems];
            for (int i = 0; i < availableItems.Length; i++)
            {
                if (IncludedItems[i] == null)
                {
                    availableItems[i] = ArrayAllItems[i];
                }
            }
            for (int i = 0; i < availableItems.Length; i++)
            {
                if (availableItems[i] != null && availableItems[i].Weight < minWeight)
                {
                    minWeight = availableItems[i].Weight;
                }
            }
            for (int i = 0; i < availableItems.Length; i++)
            {
                if (availableItems[i] != null && availableItems[i].Weight == minWeight)
                {
                    itemsWithMinWeight[i] = availableItems[i];
                }
            }
            for (int i = 0; i < itemsWithMinWeight.Length; i++)
            {
                if (itemsWithMinWeight[i] != null && itemsWithMinWeight[i].Value > itemToAdd.Value)
                {
                    indexOfItem = i;
                    itemToAdd = itemsWithMinWeight[i];
                }
            }
            if (TotalWeight + minWeight <= _availableWeight)
            {
                IncludedItems[indexOfItem] = itemToAdd;
            }
            
        }

        public void Mutation()
        {
            Random r = new Random();
            int firstGenIndex = r.Next(0, _countItems);
            int secondGenIndex = r.Next(0, _countItems);
            while (secondGenIndex == firstGenIndex)
            {
                secondGenIndex = r.Next(0, _countItems);
            }
            Item firstItem = IncludedItems[firstGenIndex];
            Item secondItem = IncludedItems[secondGenIndex];
            var tempItem = firstItem;
            IncludedItems[firstGenIndex] = secondItem;
            IncludedItems[secondGenIndex] = tempItem;
        }

        public override string ToString()
        {
            string result = $"Stats: weight - {TotalWeight}, price - {TotalPrice}.\nIncluded items:\n";
            int counter = 1;
            for (int i = 0; i < IncludedItems.Length; i++)
            {
                if (IncludedItems[i] != null)
                {
                    result += $"{counter++}: {IncludedItems[i].Weight} - {IncludedItems[i].Value};\n";
                }
            }
            return result;
        }

        public void Print() => Console.WriteLine(ToString());

        public void PrintStats() => Console.WriteLine($"Stats: weight - {TotalWeight}, price - {TotalPrice};");
    
        public void SaveToFile()
        {
            using (StreamWriter streamWriter = new StreamWriter("save.txt"))
            {
                for (int i = 0; i < ArrayAllItems.Length; i++)
                {
                    streamWriter.WriteLine($"{ArrayAllItems[i].Value};{ArrayAllItems[i].Weight}");
                }
            }
        }

        public void ImportFromFile()
        {
            string[] splitted;
            using (StreamReader streamReader = new StreamReader("save.txt"))
            {
                for (int i = 0; i < ArrayAllItems.Length; i++)
                {
                    splitted = streamReader.ReadLine().Split(';');
                    ArrayAllItems[i] = new Item(int.Parse(splitted[0]), int.Parse(splitted[1]));
                }
            }
        }
    }
}
