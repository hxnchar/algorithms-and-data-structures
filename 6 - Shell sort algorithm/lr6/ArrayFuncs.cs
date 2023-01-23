using System;

namespace lr6
{
    class ArrayFuncs
    {
        public static int[] RandomGeneration(int count)
        {
            int[] arr = new int[count];
            Random r = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(0, count + 1);
            }
            return arr;
        }

        public static int[] OrderedGeneration(int count)
        {
            int[] arr = new int[count];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i+1;
            }
            return arr;
        }

        public static int[] BackOrderedGeneration(int count)
        {
            int[] arr = new int[count];
            for (int i = 0; i < count; i++)
            {
                arr[i] = count - i;
            }
            return arr;
        }

        public static void OutArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
                //Console.Write($"{arr[i]}, ");
            }
            Console.WriteLine("============");
        }

        public static void ShellSort(ref int[] array)
        {
            int gap = array.Length;
            bool addictionalStep = false;

            while (gap != 1 || !addictionalStep)
            {
                if (gap == 1)
                    addictionalStep = true;
                for (int i = 0; i + gap < array.Length; i++)
                {
                    int j = i + gap;
                    int temp = array[j];
                    while (j - gap >= 0 && temp < array[j - gap])
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }
                    array[j] = temp;
                }
                gap = Math.Max((5 * gap - 1) / 11, 1);
            }
        }

        public static void ShellSort(ref int[] array, ref int comparisons, ref int replacements, ref long elipsedtime)
        {
            long currentTime = DateTime.Now.Ticks;
            int gap = array.Length;
            bool addictionalStep = false;

            while (gap != 1 || !addictionalStep)
            {
                if (gap == 1)
                    addictionalStep = true;
                for (int i = 0; i + gap < array.Length; i++)
                {
                    int j = i + gap;
                    int temp = array[j];
                    while (j - gap >= 0 && temp < array[j - gap])
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                        replacements += 1;
                        comparisons += 1;
                    }
                    comparisons += 1;
                    array[j] = temp;
                }
                gap = Math.Max((5 * gap - 1) / 11, 1);
            }
            elipsedtime = DateTime.Now.Ticks - currentTime;
        }
    }
}
