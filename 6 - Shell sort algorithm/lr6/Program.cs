using System;
using System.Timers;

namespace lr6
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Utilities.Log("Ordered");

             Utilities.Log("Back ordered");

             Utilities.Log("Random generated");*/
            int[] arr = ArrayFuncs.RandomGeneration(100);
            ArrayFuncs.OutArray(arr);
            ArrayFuncs.ShellSort(ref arr);
            ArrayFuncs.OutArray(arr);
            Console.ReadKey();
        }
    }
}
