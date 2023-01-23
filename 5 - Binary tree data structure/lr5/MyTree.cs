using System;

namespace lr5
{
    class MyTree
    {
        public MyTree Parent { get; set; }
        public char Data { get; set; }
        public MyTree LeftChild { get; set; }
        public MyTree RightChild { get; set; }

        public MyTree()
        {
            Data = '\0';
            LeftChild = null;
            RightChild = null;
            Parent = null;
        }

        public MyTree(char value)
        {
            Data = value;
            LeftChild = null;
            RightChild = null;
            Parent = null;
        }

        public MyTree(char value, char leftChild, char rightChild)
        {
            Data = value;
            LeftChild = new MyTree(leftChild);
            RightChild = new MyTree(rightChild);
        }

        public MyTree(char value, MyTree leftChild, char rightChild)
        {
            Data = value;
            LeftChild = leftChild;
            RightChild = new MyTree(rightChild);
        }

        public void SetData(char value)
        {
            Data = value;
        }

        public void SetLeftChild(MyTree tree)
        {
            LeftChild = tree;
            LeftChild.Parent = this;
        }

        public void SetRightChild(MyTree tree)
        {
            RightChild = tree;
            RightChild.Parent = this;
        }

        public void SetLeftChild(char value)
        {
            if (LeftChild == null)
            {
                LeftChild = new MyTree(value);
            }
            else
            {
                MyTree newNode = new MyTree(value);
                newNode.LeftChild = LeftChild;
                LeftChild = newNode;
            }
            LeftChild.Parent = this;
        }

        public void SetRightChild(char value)
        {
            if (RightChild == null)
                RightChild = new MyTree(value);
            else
            {
                MyTree newNode = new MyTree(value);
                newNode.RightChild = RightChild;
                RightChild = newNode;
            }
            RightChild.Parent = this;
        }

        public static bool HasChilds(MyTree tree)
        {
            return tree.RightChild.LeftChild == null && tree.LeftChild.LeftChild == null;
        }
    }
}
