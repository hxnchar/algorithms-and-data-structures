using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Utilities.PrintMsg("Generated task:");
            Table table = new Table();
            table.Generate();
            Console.WriteLine(table);

            Utilities.PrintMsg("BFS method:");
            Tree treeBFS = new Tree(table);
            var solutionBFS = treeBFS.SolveBFS();
            Console.WriteLine(solutionBFS);
            //treeBFS.PrintStats();

            /*Utilities.PrintMsg("RBFS method:");
            Tree treeRBFS = new Tree(table);
            var solutionRBFS = treeRBFS.SolveRBFS();
            Console.WriteLine(solutionRBFS);
            treeRBFS.PrintStats();*/

        }
    }
}
