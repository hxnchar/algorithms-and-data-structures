using System;
using System.IO;

namespace lab4
{
    class Graph
    {
        public static int[,] VertexMatrix;
        public int[,] DistancesMatrix => VertexMatrix;
        private int _countVertex;
        private static int _minRoute = 5;
        private static int _maxRoute = 150;

        public Graph(int countVertex)
        {
            _countVertex = countVertex;
            VertexMatrix = new int[_countVertex, _countVertex];
            GenerateRandomMatrix();
        }

        public Graph(string fileName)
        {
            ImportFromFile(fileName);
        }

        public void GenerateRandomMatrix()
        {
            Random r = new Random();
            for (int i = 0; i < _countVertex; i++)
            {
                for (int j = 0; j < _countVertex; j++)
                {
                    if (i == j)
                        VertexMatrix[i, j] = 0;
                    else if (j > i)
                        VertexMatrix[i, j] = r.Next(_minRoute, _maxRoute + 1);
                    else
                        VertexMatrix[i, j] = VertexMatrix[j, i];
                }
            }
        }

        public void ImportFromFile(string fileName)
        {
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string currentLine = streamReader.ReadLine();
                _countVertex = currentLine.Split('\t').Length - 1;
                VertexMatrix = new int[_countVertex, _countVertex];
            }
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string currentLine;
                int currentLineIndex = 0;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    string[] splitted = currentLine.Split('\t');
                    for (int i = 0; i < splitted.Length - 1; i++)
                    {
                        VertexMatrix[currentLineIndex, i] = int.Parse(splitted[i]);
                    }
                    currentLineIndex += 1;
                }
            }
        }

        public void SaveToFile(string fileName = "save.txt")
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(ToString());
            }
        }

        public override string ToString()
        {
            string result = string.Empty;
            for (int i = 0; i < VertexMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < VertexMatrix.GetLength(1); j++)
                {
                    result += $"{VertexMatrix[i, j]}\t";
                }
                result += $"\n";
            }
            return result;
        }
    }
}
