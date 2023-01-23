using System;
using System.IO;

namespace lr7
{
    class Sort
    {
        public static void SplitByFiles(string path, string name)
        {
            using (StreamReader sr = new StreamReader("10Mb array.txt"))
            {
                while (!sr.EndOfStream)
                {
                    StreamWriter[] streamWriters = new StreamWriter[3];
                    for (int i = 0; i < streamWriters.Length; i++)
                    {
                        string writerPath = $"{path}{name}{i}.txt";
                        streamWriters[i] = new StreamWriter(writerPath);
                    }

                    string currentString = sr.ReadLine();
                    string[] numsInString = currentString.Split(" ");
                    int[] numbers = new int[numsInString.Length];
                    for (int i = 0; i < numsInString.Length - 1; i++)
                    {
                        numbers[i] = int.Parse(numsInString[i]);
                    }

                    int temp = 0;
                    int index = 0;
                    while (index < numbers.Length - 1)
                    {
                        streamWriters[0].Write(numbers[index] + " ");
                        for (int i = index + 1; i < numbers.Length; i++)
                        {
                            if (numbers[i - 1] <= numbers[i])
                            {
                                streamWriters[0].Write(numbers[i] + " ");
                                if (i == numbers.Length - 1) index = i;
                            }
                            else
                            {
                                temp = numbers[i];
                                index = i;
                                break;
                            }
                        }

                        streamWriters[1].Write(temp + " ");
                        for (int i = index + 1; i < numbers.Length; i++)
                        {
                            if (numbers[i - 1] <= numbers[i])
                            {
                                streamWriters[1].Write(numbers[i] + " ");
                                if (i == numbers.Length - 1) index = i;
                            }
                            else
                            {
                                temp = numbers[i];
                                index = i;
                                break;
                            }
                        }

                        streamWriters[2].Write(temp + " ");
                        for (int i = index + 1; i < numbers.Length; i++)
                        {
                            if (numbers[i - 1] <= numbers[i])
                            {
                                streamWriters[2].Write(numbers[i] + " ");
                                if (i == numbers.Length - 1) index = i;
                            }
                            else
                            {
                                temp = numbers[i];
                                index = i;
                                break;
                            }
                        }
                    }

                    for (int i = 0; i < streamWriters.Length; i++)
                    {
                        streamWriters[i].Close();
                    }
                }
            }

        }
        public static void MergeSort(string nameForReader, string nameForWriter)
        {
            StreamReader[] streamReaders = new StreamReader[3];
            for (int i = 0; i < streamReaders.Length; i++)
            {
                string path = $"{Directory.GetCurrentDirectory()}{nameForReader}{i}.txt";
                if (!File.Exists(path))
                {
                    var tempFile = File.Create(path);
                    tempFile.Close();
                }
                streamReaders[i] = new StreamReader(path);
            }

            StreamWriter[] streamWriters = new StreamWriter[3];
            for (int i = 0; i < streamWriters.Length; i++)
            {
                string path = $"{Directory.GetCurrentDirectory()}{nameForWriter}{i}.txt";
                streamWriters[i] = new StreamWriter(path);
            }

            int[] series = new int[3];
            bool[] fileToBeReaded = new bool[3];
            bool keepWorking = true;

            for (int i = 0; i < 3; i++)
            {
                fileToBeReaded[i] = true;
                int firstNumber = ReadNumber(streamReaders[i]);
                if (firstNumber != -1) series[i] = firstNumber;
                else fileToBeReaded[i] = false;
            }

            while (keepWorking)
            {
                for (int i = 0; i < 3; i++)
                {
                    while (true)
                    {
                        int index = FindMin(series, fileToBeReaded);
                        if (index != -1)
                        {
                            int minElem = series[index];
                            streamWriters[i].Write(minElem + " ");
                            if (fileToBeReaded[index])
                            {
                                int nextNumber = ReadNumber(streamReaders[index]);
                                if (nextNumber < series[index]) fileToBeReaded[index] = false;
                                series[index] = nextNumber;
                            }
                        }
                        else break;
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        if (streamReaders[j] != null && !streamReaders[j].EndOfStream)
                        {
                            fileToBeReaded[j] = true;
                        }
                    }
                }
                keepWorking = false;
                for (int i = 0; i < 3; i++)
                {
                    if (streamReaders[i] != null && !streamReaders[i].EndOfStream)
                    {
                        keepWorking = true;
                        break;
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                streamWriters[i].Close();
                streamReaders[i].Close();
            }
        }
        static int ReadNumber(StreamReader sr)
        {
            string currentNumber = "";
            while (!sr.EndOfStream)
            {
                char tempChar = Convert.ToChar(sr.Read());
                if (tempChar != ' ') currentNumber += tempChar;
                else break;
            }
            if (currentNumber == "") return -1;
            return int.Parse(currentNumber);
        }
        static int FindMin(int[] elements, bool[] fileToBeReaded)
        {
            int min = 99999;
            int minIndex = -1;
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] < min && fileToBeReaded[i])
                {
                    min = elements[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }
    }
}
