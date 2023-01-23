namespace lr4
{
    public class MyList<T>
    {
        private int _capacity;
        private T[] _list;
        private int _head;

        public int Head
        {
            get => _head;
        }

        public MyList()
        {
            _capacity = 1;
            _list = new T[_capacity];
            _head = -1;
        }

        public void Add(T item)
        {
            if (_head + 1 == _capacity)
            {
                _capacity *= 2;
                T[] newList = new T[_capacity];
                for (int i = 0; i < _list.Length; i++)
                {
                    newList[i] = _list[i];
                }
                _list = newList;
            }
            _list[++_head] = item;
        }

        public void Remove(int index)
        {
            T item = _list[index];
            for (int i = index; i < _head; i++)
            {
                _list[i] = _list[i + 1];
            }
            _head--;
        }

        public T GetElement(int index)
        {
            return _list[index];
        }
    }
}