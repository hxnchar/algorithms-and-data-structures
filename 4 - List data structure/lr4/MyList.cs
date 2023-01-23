public class MyList<T>
{
    public Node<T> Head { get; private set; }
    public Node<T> Tail { get; private set; }

    public int Count { get; set; }

    public void Add(T value)
    {
        Node<T> node = new Node<T>(value);
        if (Head == null)
        {
            Head = node;
        }
        else
        {
            Tail.Next = node;
        }
        Tail = node;
        Count++;
    }

    public void Remove(T value)
    {
        Node<T> current = Head;
        Node<T> previous = null;

        while (current != null)
        {
            if (current.Value.Equals(value))
            {
                if (previous == null)
                {
                    Head = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
            }
            else
            {
                previous = current;
            }
            current = current.Next;
        }
    }
}