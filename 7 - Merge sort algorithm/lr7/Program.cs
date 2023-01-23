using System;
using System.Diagnostics;
using System.IO;

namespace lr7
{
    class Program
    {
        static void Main(string[] args)
        {
            //GenerateFile.Generate("10Mb", 0, 10000);

            string res1directory = $"{Directory.GetCurrentDirectory()}\\result1.txt";
            string res2directory = $"{Directory.GetCurrentDirectory()}\\result2.txt";
            string spl1directory = $"{Directory.GetCurrentDirectory()}\\splitted1.txt";
            string spl2directory = $"{Directory.GetCurrentDirectory()}\\splitted2.txt";

            Stopwatch timer = new Stopwatch();
            timer.Start();
            Sort.SplitByFiles(Directory.GetCurrentDirectory(), "\\splitted");
            while (true)
            {
                Sort.MergeSort("\\splitted", "\\result");
                if (new FileInfo(res1directory).Length == 0 && new FileInfo(res2directory).Length == 0)
                    break;
                Sort.MergeSort("\\result", "\\splitted");
                if (new FileInfo(spl1directory).Length == 0 && new FileInfo(spl2directory).Length == 0)
                    break;
            }
            timer.Stop();
            Console.WriteLine($"Eclipsed time: {timer.ElapsedMilliseconds/1000}seconds");
        }
    }
}
