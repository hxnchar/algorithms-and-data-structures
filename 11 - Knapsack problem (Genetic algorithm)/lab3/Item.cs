using System;
namespace lab3
{
    class Item
    {
        public int Value, Weight;
        private const int _minValue = 2, _maxValue = 10;
        private const int _minWeight = 1, _maxWeight = 5;

        public Item()
        {
            Random r = new Random();
            Value = r.Next(_minValue, _maxValue + 1);
            Weight = r.Next(_minWeight, _maxWeight + 1);
        }

        public Item (int value, int weight)
        {
            Value = value;
            Weight = weight;
        }
    }
}
