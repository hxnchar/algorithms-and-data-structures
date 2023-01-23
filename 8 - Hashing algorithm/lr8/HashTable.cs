using System;
using System.Collections.Generic;

namespace lr8
{
    class HashTable<T>
    {
        private int _size = 1;
        private readonly int _prime = 7;
        private int _countElem = 0;
        private double _alpha;
        private Item[] _table;

        public HashTable(Dictionary<string, string> keyValuePairs)
        {
            _table = new Item[_size];
            foreach (var keyValuePair in keyValuePairs)
            {
                AddElement(keyValuePair.Key, keyValuePair.Value);
                _countElem++;
                _alpha = _countElem / _size;
                if (_alpha > 0.75) ResizeTable();
            }
        }

        public void AddElement(string key, string value)
        {
            int index = Convert.ToInt32(HashFunction(key) % _size);
            int startIndex = index;
            while (index < _size)
            {
                if (_table[index] == null)
                {
                    _table[index] = new Item(key, value);
                    _countElem++;
                    _alpha = _countElem / _size;
                    if (_alpha > 0.75) ResizeTable();
                    return;
                }
                index = (index + _prime) % _size;
                if (index == startIndex) break;
            }
        }

        public string this[string key]
        {
            get
            {
                int index = Convert.ToInt32(HashFunction(key) % _size);
                int startIndex = index;
                while (_table[index] != null)
                {
                    if (_table[index].Key == key)
                        return _table[index].Value;
                    index = (index + _prime) % _size;
                    if (index == startIndex)
                        break;
                }
                return "Такого элемента не существует";
            }
        }

        private uint HashFunction(string key)
        {
            const uint FNV_32_PRIME = 0x01000193;
            uint hval = 0x811c9dc5;
            foreach (char current in key)
            {
                hval *= FNV_32_PRIME;
                hval ^= current;
            }
            return hval;
        }

        private void ResizeTable()
        {
            Item[] copiedTable = _table;
            _size *= 2;
            _table = new Item[_size];

            foreach (var currentPair in copiedTable)
                if (currentPair != null)
                    AddElement(currentPair.Key, currentPair.Value);
        }

        public void OutTable()
        {

            foreach (var data in _table)
            {
                if (data != null)
                {
                    Console.WriteLine(data.Key);
                    Console.WriteLine("-");
                    Console.WriteLine(data.Value);
                }
                else
                    Console.WriteLine("-");
                Utilities.OutEquality();
            }
        }
    }

    class Item
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public Item(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
