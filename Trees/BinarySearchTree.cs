
using LinkedList;
using System;

namespace Trees
{
    public class BinarySearchTree
    {
        public BinaryTreeNode Root { get; private set; }

        public void Insert(int value)
        {
            var node = new BinaryTreeNode(value);
            if (Root == null)
            {
                Root = node;
                return;
            }

            var current = Root;

            while (true)
            {
                // If value is smaller, it belongs to the left subtree.
                if (value < current.Value)
                {
                    // We found the place to insert.
                    if (current.LeftChild == null)
                    {
                        current.LeftChild = node;
                        return;
                    }
                    else
                    {
                        current = current.LeftChild;
                    }
                }
                // If value is greater, it belongs to the right subtree.
                else
                {
                    // We found the place to insert.
                    if (current.RightChild == null)
                    {
                        current.RightChild = node;
                        return;
                    }
                    else
                    {
                        current = current.RightChild;
                    }
                }
            }
        }

        public void PrintDfs()
        {
            PrintDfs(Root);
            Console.WriteLine();
        }

        void PrintDfs(BinaryTreeNode node)
        {
            // Base Case
            if (node == null)
            {
                return;
            }
            // Recursive call on the left child.
            PrintDfs(node.LeftChild);
            // Print node value.
            Console.Write($"{node.Value} ");
            // Recursive call on the right child.
            PrintDfs(node.RightChild);
        }

        public void PrintBfs()
        {
            if (Root == null)
            {
                return;
            }

            var queue = new Queue<BinaryTreeNode>();
            // Put the root in the queue.
            queue.Enqueue(Root);

            while (queue.Root != null)
            {
                var current = queue.Dequeue();
                Console.Write($"{current.Value} ");
                // Add children to the queue.
                if (current.LeftChild != null)
                {
                    queue.Enqueue(current.LeftChild);
                }
                if (current.RightChild != null)
                {
                    queue.Enqueue(current.RightChild);
                }
            }
        }
    }
}
