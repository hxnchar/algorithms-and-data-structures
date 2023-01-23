using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    class Tree
    {
        public Tree Parent;

        public Table CurrentState;

        public int Depth;

        public List<Tree> Childrens;

        public static List<Tree> LowestChildrens = new List<Tree>();

        public static int countIterations = 0;

        public static int countStates = 0;

        public Tree(Table table, Tree parent = null, int depth = 0)
        {
            Parent = parent;
            CurrentState = table;
            Depth = depth;
            Childrens = new List<Tree>(8);
        }

        public void ExpandNode(List<Tree> newLowestChildrens)
        {
            var possibleVariants = CurrentState.GetChildrens(Depth);
            for (int i = 0; i < possibleVariants.Count; i++)
            {
                var newChild = new Tree(possibleVariants[i], this, Depth + 1);
                Childrens.Add(newChild);
                newLowestChildrens.Add(newChild);
            }
        }

        public void ExpandNode()
        {
            var possibleVariants = CurrentState.GetChildrens(Depth);
            for (int i = 0; i < possibleVariants.Count; i++)
            {
                Childrens.Add(new Tree(possibleVariants[i], this, Depth + 1));
            }
        }

        public void ExpandTree()
        {
            if (Depth <= 7)
            {
                if (Depth == 0)
                {
                    ExpandNode(LowestChildrens);
                }
                else
                {
                    List<Tree> newLowestChildrens = new List<Tree>();
                    foreach (var eachChild in LowestChildrens)
                    {
                        eachChild.ExpandNode(newLowestChildrens);
                    }
                    LowestChildrens = newLowestChildrens;
                }
                Depth += 1;
                countStates += Convert.ToInt32(Math.Pow(8, Depth));
            }
        }

        public void PrintStats()
        {
            Console.WriteLine(Utilities.GetLine(10));
            Console.WriteLine("Algorithm stats:\n");
            Console.WriteLine($">>>Total iterations: {countIterations};\n>>>Total states: {countStates}");
            Console.WriteLine(Utilities.GetLine(10));
        }

        public Table SolveBFS()
        {
            Queue<Tree> queue = new Queue<Tree>();
            var currentNode = this;
            while (!currentNode.CurrentState.Solved)
            {
                //Console.WriteLine($"Incorrect:\n{currentNode.CurrentState}");
                countIterations++;
                if (queue.Count == 0)
                {
                    ExpandTree();
                    foreach (var item in LowestChildrens)
                    {
                        queue.Enqueue(item);
                    }
                }
                currentNode = queue.Dequeue();
            }
            return currentNode.CurrentState;
        }

        public Table SolveRBFS(int limit = Int32.MaxValue)
        {
            ExpandNode();
            List<Tree> a = Childrens;
            a.Sort((x, y) => x.CurrentState.Conflicts.CompareTo(y.CurrentState.Conflicts));
            /*if (a.Count == 0)
            {
                return null;
            }*/
            if (a[0].CurrentState.Solved)
            {
                return a[0].CurrentState;
            }

            if (a[0].CurrentState.Conflicts > limit)
            {
                return null;
            }

            Table result = null;
            while (result == null && a.Count > 1)
            {
                result = a[0].SolveRBFS(Math.Min(limit, a[1].CurrentState.Conflicts));
                a.RemoveAt(0);
            }
            return result;
        }
    }
}
