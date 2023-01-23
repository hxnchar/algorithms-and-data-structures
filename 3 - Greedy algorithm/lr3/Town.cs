namespace lr3
{
    public class Town
    {
        public string Name { get; }
        public int Index { get; }

        public Town(string townName = "", int townIndex = -1)
        {
            Name = townName;
            Index = townIndex;
        }
    }
}
