namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Graph graph = new Graph(300);
            //graph.SaveToFile();
            Graph graph = new Graph("save.txt");
            Algorithm algorithm = new Algorithm(graph.DistancesMatrix, 13, 1, 1, 2, 3, 0.5, 200, false);
            algorithm.Solve();
        }
    }
}
