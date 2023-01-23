using System.Collections.Generic;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] matrix = Utilities.InputTheMatrix();            
            if (Graph.IsEuler(matrix))
            {
                List<int> path = Graph.AlgorithmFleurys(matrix);
                Utilities.OutPath(path);
            }
        }
    }
}