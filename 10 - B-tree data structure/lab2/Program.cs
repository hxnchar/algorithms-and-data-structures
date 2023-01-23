using System;
using System.IO;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree bTree = new BTree();
            bTree.ImportFromFile();
            bTree.Listen();
        }
    }
}