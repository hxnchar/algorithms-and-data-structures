using System;
using System.Collections.Generic;

namespace lr5
{
    class Utilities
    {
        public static MyTree StringToTree(string str)
        {
            char[] symbols = str.ToCharArray();
            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbols[i] == '+' || symbols[i] == '-' || symbols[i] == '/' || symbols[i] == '*')
                {
                    MyTree tree = new MyTree(symbols[i]);
                    tree.SetLeftChild(symbols[i - 1]);
                    tree.SetRightChild(symbols[i + 1]);
                    return tree;
                }
            }
            return null;
        }

        public static MyTree ConcatTreeStr(MyTree tree, string str)
        {
            char[] symbols = str.ToCharArray();
            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbols[i] == '+' || symbols[i] == '-' || symbols[i] == '/' || symbols[i] == '*')
                {
                    MyTree newTree = new MyTree(symbols[i]);
                    newTree.SetLeftChild(tree);
                    newTree.SetRightChild(symbols[i + 1]);
                    return newTree;
                }
            }
            return null;
        }

        public static void OutTree(MyTree tree, int count = 0)
        {
            if (tree.LeftChild == null)
            {
                Console.WriteLine($"{tree.Data}");
            }
            else
            {
                Console.WriteLine($"{tree.Data}");
                OutTabulation(count); Console.Write("├───────"); OutTree(tree.RightChild, count);
                OutTabulation(count); Console.Write("└───────"); OutTree(tree.LeftChild, ++count);
            }
        }

        private static void OutTabulation(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write("\t");
            }
        }

        public static void OutText(string str = "", bool upper = false, bool lower = false)
        {
            if (upper) OutEquality();
            if (str != "") Console.WriteLine(str);
            if (lower) OutEquality();
        }

        private static void OutEquality(int count = 30)
        {
            for (int i = 0; i < count; i++) Console.Write('=');
            Console.Write('\n');
        }

        public static int GetNum(string a)
        {
            OutText($"Введите значение {a}");
            return int.Parse(Console.ReadLine());
        }

        public static Stack<char> ToPolskaStack(string input)
        {
            Stack<char> tempStack = new Stack<char>();
            Stack<char> resultStack = new Stack<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                    tempStack.Push(input[i]);
                else if (input[i] == ')')
                {
                    while (tempStack.Count > 0 && tempStack.Peek() != '(')
                        resultStack.Push(tempStack.Pop());
                    tempStack.Pop();
                }
                else if (IsNumber(input[i]))
                {
                    resultStack.Push(input[i]);
                }
                else if (IsOperator(input[i]))
                {
                    while (tempStack.Count > 0 && tempStack.Peek() != '(' && GetPriotity(input[i]) <= GetPriotity(tempStack.Peek()))
                        resultStack.Push(tempStack.Pop());
                    tempStack.Push(input[i]);
                }
                else
                {
                    if (tempStack.Peek() != '(')
                        resultStack.Push(tempStack.Pop());
                }
            }
            while (tempStack.Count > 0)
            {
                resultStack.Push(tempStack.Pop());
            }
            Stack<char> reversedStack = new Stack<char>();
            while (resultStack.Count > 0)
            {
                reversedStack.Push(resultStack.Pop());
            }
            return reversedStack;
        }

        private static bool IsOperator(char symbol)
        {
            return (symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/');
        }

        private static bool IsNumber(char symbol)
        {
            return (symbol >= '0' && symbol <= '9');
        }

        private static int GetPriotity(char symbol)
        {
            switch (symbol)
            {
                case '+':
                    return 1;
                case '-':
                    return 1;
                case '*':
                    return 2;
                case '/':
                    return 2;
                default:
                    return 3;
            }
        }
    
        public static int Calculate(int num1, int num2, char oper)
        {
            switch (oper)
            {
                case '+':
                    return num1 + num2;
                case '-':
                    return num1 - num2;
                case '*':
                    return num1 * num2;
                default:
                    return 0;
            }
        }

        public static MyTree StackToTree(Stack<char> symbols)
        {
            Stack<char> numsStack = new Stack<char>();
            MyTree tree = new MyTree();
            int step = 0, result = 0;
            while (symbols.Count > 0)
            {
                if (IsNumber(symbols.Peek()))
                {
                    numsStack.Push(symbols.Pop());
                }
                else if (IsOperator(symbols.Peek()) && step == 0)
                {
                    tree.SetData(symbols.Peek());
                    char oper = symbols.Pop();

                    tree.SetLeftChild(numsStack.Peek());
                    int firstNum = numsStack.Pop() - '0';

                    tree.SetRightChild(numsStack.Peek());
                    int secondNum = numsStack.Pop() - '0';

                    OutText($"Шаг #{++step}:", true);
                    OutTree(tree);

                    result = Calculate(firstNum, secondNum, oper);
                    OutText($"В результате получаем {result}");
                }
                else
                {
                    char oper = symbols.Pop();
                    int secondNum = numsStack.Pop() - '0';
                    tree = new MyTree(oper, tree, Convert.ToChar(secondNum+'0'));
                    
                    OutText($"Шаг #{++step}:", true);
                    OutTree(tree);

                    result = Calculate(result, secondNum, oper);
                    OutText($"В результате получаем {result}");
                }
            }
            return tree;
        }
    }
}
