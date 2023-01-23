using System;

namespace lr6
{
    class Utilities
    {
        public static void OutText(string str = "", bool upper = false, bool lower = false)
        {
            if (upper) OutEquality();
            if (str != "") Console.WriteLine(str);
            if (lower) OutEquality();
        }

        private static void OutEquality(int count = 30)
        {
            for (int i = 0; i < count; i++) Console.Write('=');
            Console.Write("\n");
        }

        public static void Log(string type)
        {
            int comparisons = 0, replacements = 0, N = 0;
            long elipsedTime = 0;
            int[] arr = ArrayFuncs.OrderedGeneration(N);
            ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);

            if (type == "Ordered")
            {
                Console.WriteLine("Ordered arrays:");

                comparisons = 0; replacements = 0; N = 10; elipsedTime = 0;
                arr = ArrayFuncs.OrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 100; elipsedTime = 0;
                arr = ArrayFuncs.OrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 1000; elipsedTime = 0;
                arr = ArrayFuncs.OrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 5000; elipsedTime = 0;
                arr = ArrayFuncs.OrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 10000; elipsedTime = 0;
                arr = ArrayFuncs.OrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 20000; elipsedTime = 0;
                arr = ArrayFuncs.OrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 50000; elipsedTime = 0;
                arr = ArrayFuncs.OrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                OutEquality();
            }
            else if (type == "Back ordered")
            {
                Console.WriteLine("Back ordered arrays:");

                comparisons = 0; replacements = 0; N = 10; elipsedTime = 0;
                arr = ArrayFuncs.BackOrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 100; elipsedTime = 0;
                arr = ArrayFuncs.BackOrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 1000; elipsedTime = 0;
                arr = ArrayFuncs.BackOrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 5000; elipsedTime = 0;
                arr = ArrayFuncs.BackOrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 10000; elipsedTime = 0;
                arr = ArrayFuncs.BackOrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 20000; elipsedTime = 0;
                arr = ArrayFuncs.BackOrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 50000; elipsedTime = 0;
                arr = ArrayFuncs.BackOrderedGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                OutEquality();
            }
            else if (type == "Random generated")
            {
                Console.WriteLine("Random generated arrays:");

                comparisons = 0; replacements = 0; N = 10; elipsedTime = 0;
                arr = ArrayFuncs.RandomGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 100; elipsedTime = 0;
                arr = ArrayFuncs.RandomGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 1000; elipsedTime = 0;
                arr = ArrayFuncs.RandomGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 5000; elipsedTime = 0;
                arr = ArrayFuncs.RandomGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 10000; elipsedTime = 0;
                arr = ArrayFuncs.RandomGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 20000; elipsedTime = 0;
                arr = ArrayFuncs.RandomGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                comparisons = 0; replacements = 0; N = 50000; elipsedTime = 0;
                arr = ArrayFuncs.RandomGeneration(N);
                ArrayFuncs.ShellSort(ref arr, ref comparisons, ref replacements, ref elipsedTime);
                Console.WriteLine($"{N}-elements array: {comparisons} / {replacements}; elipsed time: {Convert.ToDouble(elipsedTime)/10000} ms");

                OutEquality();
            }
            else Console.WriteLine("Incorrect type of array");
        }
    }
}
