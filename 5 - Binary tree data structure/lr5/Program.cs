using System.Collections.Generic;

namespace lr5
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = Utilities.GetNum("a");
            int b = Utilities.GetNum("b");
            int c = Utilities.GetNum("c");
            int d = Utilities.GetNum("d");

            string task = $"(({a}+{b})*{c}-{d})";
            Stack<char> stack = Utilities.ToPolskaStack(task);
            MyTree tree = Utilities.StackToTree(stack);            

            Utilities.OutText("", true);
        }
    }
}
