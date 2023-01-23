using System;
using System.IO;

namespace lr7
{
    class GenerateFile
    {
        private static Int64 ConvertToBytes(string fileSize)
        {
            if (fileSize.Contains("Gb"))
            {
                fileSize = fileSize.Replace("Gb", "");
                return int.Parse(fileSize) * 1024 * 1024 * 1024;
            }
            else if(fileSize.Contains("Mb"))
            {
                fileSize = fileSize.Replace("Mb", "");
                return int.Parse(fileSize) * 1024 * 1024;
            }
            else return Int64.Parse(fileSize);
        }

        public static void Generate(string fileSize, int minValue = -1000, int maxValue = 1000)
        {
            Int64 bytesRemaining = ConvertToBytes(fileSize);
            Random r = new Random();
            using (var sw = new StreamWriter($"{fileSize} array.txt"))
            {
                while (bytesRemaining > 0)
                {
                    int tempValue = r.Next(minValue, maxValue);
                    sw.Write($"{tempValue} ");
                    bytesRemaining -= 4;
                }
            }
        }

        public static void Out(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                string currentString;
                while((currentString = sr.ReadLine()) != null)
                {
                    Console.WriteLine(currentString);
                }
            }
        }
    }
}
