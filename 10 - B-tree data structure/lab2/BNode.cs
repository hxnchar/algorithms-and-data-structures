using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    class BNode
    {
        private static readonly int _t = 10;
        private readonly int _minKeys = _t - 1;
        private readonly int _maxKeys = 2 * _t - 1;

        public BNode[] Children;
        public KeyValuePair<int, string>[] Data;
        public int CountKeys;
        public bool IsLeaf;

        public BNode(bool leaf)
        {
            Data = new KeyValuePair<int, string>[_maxKeys];
            Children = new BNode[2 * _t];
            CountKeys = 0;
            IsLeaf = leaf;
        }

        public int FindIndex(int requiredKey)
        {
            int lastGreaterThanKey = 0;
            int index = BinarySearch(0, CountKeys, requiredKey, ref lastGreaterThanKey);
            return index == -1 ? lastGreaterThanKey : index;
        }

        public void RemoveKey(int key)
        {
            int index = FindIndex(key);

            if (index < CountKeys && Data[index].Key == key)
            {
                if (IsLeaf)
                    RemoveFromLeaf(index);
                else
                    RemoveFromNonLeaf(index);
            }
            else
            {
                if (!IsLeaf)
                {
                    if (Children[index].CountKeys < _minKeys)
                        FillMissing(index);
                     Children[index].RemoveKey(key);
                }
                else
                {
                    Console.WriteLine($"Ключа {key} нет в базе");
                    return;
                }
            }
            return;
        }

        public void RemoveFromLeaf(int index)
        {
            for (int i = index + 1; i < CountKeys; ++i)
            {
                Data[i - 1] = new KeyValuePair<int, string>(Data[i].Key, Data[i].Value);
            }
            CountKeys -= 1;
        }

        public void RemoveFromNonLeaf(int index)
        {
            int k = Data[index].Key;

            // если ребенок что есть предшественником k (C[idx]) имеет как минимум t ключей,
            // находим предшественника k в поддереве с корнем
            // C[idx]. Заменить ключ предшественником. Рекурсивно удалить предшественника
            // в C[idx]
            if (Children[index].CountKeys >= _t)
            {
                KeyValuePair<int, string> previous = GetPrevious(index);
                Data[index] = new KeyValuePair<int, string>(previous.Key, previous.Value);
                Children[index].RemoveKey(previous.Key);
            }

            // если ребенок C[idx] имеет меньше t ключей, исследуем C[idx+1].
            // если C[idx+1] имеет как минимум t ключей, то найти наследника ключа в
            // поддереве с корнем C[idx+1]
            // заменить ключ наследником
            // рекурсивно удалить succ в C[idx+1]
            else if (Children[index + 1].CountKeys >= _t)
            {
                KeyValuePair<int, string> succ = GetNext(index);
                var tempValue = Data[index].Value;
                Data[index] = new KeyValuePair<int, string>(succ.Key, tempValue);
                Children[index + 1].RemoveKey(succ.Key);
            }

            // В двух C[idx] и C[idx+1] меньше t ключей, объеденить k и все C[idx+1]
            // в C[idx]
            // сейчас C[idx] содержит 2t-1 ключей
            // освобождаем C[idx+1] и рекурсивно удаляем ключ из C[idx]
            else
            {
                Unite(index);
                Children[index].RemoveKey(k);
            }
            return;
        }

        public KeyValuePair<int, string> GetPrevious(int index)
        {
            BNode currentNode = Children[index];
            while (!currentNode.IsLeaf)
                currentNode = currentNode.Children[currentNode.CountKeys];
            return new KeyValuePair<int, string>(currentNode.Data[currentNode.CountKeys - 1].Key, currentNode.Data[currentNode.CountKeys - 1].Value);
        }

        public KeyValuePair<int, string> GetNext(int index)
        {
            BNode currentNode = Children[index + 1];
            while (!currentNode.IsLeaf)
                currentNode = currentNode.Children[0];
            return new KeyValuePair<int, string>(currentNode.Data[0].Key, currentNode.Data[0].Value);
        }

        public void FillMissing(int index)
        {
            if (index != 0 && Children[index - 1].CountKeys >= _minKeys)
                ReplaceFromPrevious(index);
            else if (index != CountKeys && Children[index + 1].CountKeys >= _minKeys)
                ReplaceFromNext(index);
            else
            {
                if (index == CountKeys)
                    Unite(index - 1);
                else
                    Unite(index);
            }
            return;
        }

        public void ReplaceFromPrevious(int index)
        {
            BNode replaceFrom = Children[index - 1];
            BNode replaceTo = Children[index];
            
            for (int i = replaceTo.CountKeys - 1; i >= 0; --i)
            {
                replaceTo.Data[i + 1] = new KeyValuePair<int, string>(replaceTo.Data[i].Key, replaceTo.Data[i].Value);
            }

            if (!replaceTo.IsLeaf)
            {
                for (int i = replaceTo.CountKeys; i >= 0; --i)
                {
                    replaceTo.Children[i + 1] = replaceTo.Children[i];
                }
            }

            replaceTo.Data[0] = new KeyValuePair<int, string>(Data[index - 1].Key, Data[index - 1].Value);

            if (!replaceTo.IsLeaf)
                replaceTo.Children[0] = replaceFrom.Children[replaceFrom.CountKeys];

            Data[index - 1] = new KeyValuePair<int, string>(replaceFrom.Data[replaceFrom.CountKeys - 1].Key, replaceFrom.Data[replaceFrom.CountKeys - 1].Value);

            replaceTo.CountKeys += 1;
            replaceFrom.CountKeys -= 1;
        }

        public void ReplaceFromNext(int index)
        {
            BNode replaceFrom = Children[index + 1];
            BNode replaceTo = Children[index];

            replaceTo.Data[replaceTo.CountKeys] = new KeyValuePair<int, string>(Data[index].Key, Data[index].Value);

            if (!replaceTo.IsLeaf)
                replaceTo.Children[replaceTo.CountKeys + 1] = replaceFrom.Children[0];

            Data[index] = new KeyValuePair<int, string>(replaceFrom.Data[0].Key, replaceFrom.Data[0].Value);

            for (int i = 1; i < replaceFrom.CountKeys; ++i)
            {
                replaceFrom.Data[i - 1] = new KeyValuePair<int, string>(replaceFrom.Data[i].Key, replaceFrom.Data[i].Value);
            }

            if (!replaceFrom.IsLeaf)
            {
                for (int i = 1; i <= replaceFrom.CountKeys; ++i)
                {
                    replaceFrom.Children[i - 1] = replaceFrom.Children[i];
                }
            }
            replaceTo.CountKeys += 1;
            replaceFrom.CountKeys -= 1;
        }

        public void Unite(int index)
        {
            BNode uniteIn = Children[index];
            BNode uniteFrom = Children[index + 1];
            uniteIn.Data[_minKeys] = new KeyValuePair<int, string>(Data[index].Key, Data[index].Value);

            for (int i = 0; i < uniteFrom.CountKeys; ++i)
            {
                uniteIn.Data[i + _t] = new KeyValuePair<int, string>(uniteFrom.Data[i].Key, uniteFrom.Data[i].Value);
            }

            if (!uniteIn.IsLeaf)
            {
                for (int i = 0; i <= uniteFrom.CountKeys; ++i)
                {
                    uniteIn.Children[i + _t] = uniteFrom.Children[i];
                }
            }

            for (int i = index + 1; i < CountKeys; ++i)
            {
                Data[i - 1] = new KeyValuePair<int, string>(Data[i].Key, Data[i].Value);
            }

            for (int i = index + 2; i <= CountKeys; ++i)
            {
                Children[i - 1] = Children[i];
            }

            uniteIn.CountKeys += uniteFrom.CountKeys + 1;
            CountKeys -= 1;
        }

        public void InsertionToNotFull(int key, string value)
        {
            int insertTo = CountKeys - 1;

            if (IsLeaf == true)
            {
                while (insertTo >= 0 && Data[insertTo].Key > key)
                {
                    Data[insertTo + 1] = new KeyValuePair<int, string>(Data[insertTo].Key, Data[insertTo].Value);
                    insertTo--;
                }

                Data[insertTo + 1] = new KeyValuePair<int, string>(key, value);
                CountKeys += 1;
            }
            else
            {
                while (insertTo >= 0 && Data[insertTo].Key > key)
                    insertTo--;

                if (Children[insertTo + 1].CountKeys == _maxKeys)
                {
                    SplitChild(insertTo + 1, Children[insertTo + 1]);

                    if (Data[insertTo + 1].Key < key)
                        insertTo++;
                }
                Children[insertTo + 1].InsertionToNotFull(key, value);
            }
        }

        public void SplitChild(int splitIndex, BNode nodeToSplit)
        {
            BNode neighborNode = new BNode(nodeToSplit.IsLeaf);
            neighborNode.CountKeys = _minKeys;

            for (int j = 0; j < _minKeys; j++)
            {
                neighborNode.Data[j] = new KeyValuePair<int, string>(nodeToSplit.Data[j + _t].Key, nodeToSplit.Data[j + _t].Value);
            }

            if (!nodeToSplit.IsLeaf)
            {
                for (int j = 0; j < _t; j++)
                {
                    neighborNode.Children[j] = nodeToSplit.Children[j + _t];
                }
            }

            nodeToSplit.CountKeys = _minKeys;
            for (int j = CountKeys; j >= splitIndex + 1; j--)
            {
                Children[j + 1] = Children[j];
            }
            Children[splitIndex + 1] = neighborNode;

            for (int j = CountKeys - 1; j >= splitIndex; j--)
            {
                Data[j + 1] = new KeyValuePair<int, string>(Data[j].Key, Data[j].Value);
            }
            Data[splitIndex] = new KeyValuePair<int, string>(nodeToSplit.Data[_minKeys].Key, nodeToSplit.Data[_minKeys].Value);
            CountKeys += 1;
        }

        public void Print()
        {
            int currentIndex;
            for (currentIndex = 0; currentIndex < CountKeys; currentIndex++)
            {
                if (!IsLeaf)
                    Children[currentIndex].Print();
                Console.WriteLine($"{Data[currentIndex].Key};{Data[currentIndex].Value}");
            }

            if (!IsLeaf)
                Children[currentIndex].Print();
        }

        public BNode Search(int key)
        {
            int lastGreaterThanKey = CountKeys;
            int index = BinarySearch(0, CountKeys - 1, key, ref lastGreaterThanKey);
            if (index != -1 && Data[index].Key == key)
            {
                return this;
            }

            else if (IsLeaf == true)
                return null;

            else if (index == -1 && !IsLeaf)
                return Children[lastGreaterThanKey].Search(key);

            return null;
        }

        public int BinarySearch(int leftBorder, int rightBorder, int requiredKey, ref int lastGreaterThanKey)
        {
            if (rightBorder >= leftBorder)
            {
                int middle = (rightBorder + leftBorder) / 2;
                if (Data[middle].Key == requiredKey)
                    return middle;
                
                else if (Data[middle].Key > requiredKey)
                {
                    lastGreaterThanKey = middle;
                    return BinarySearch(leftBorder, middle - 1, requiredKey, ref lastGreaterThanKey);
                }
                else
                    return BinarySearch(middle + 1, rightBorder, requiredKey, ref lastGreaterThanKey);
            }
            return -1;
        }
    }
}