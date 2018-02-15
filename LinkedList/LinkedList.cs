using System;

namespace LinkedList
{
    public class LinkedList<T>
        where T : IComparable<T>
    {
        public LinkedList(Node<T> root = null)
        {
            Root = root;
        }

        public Node<T> Root { get; private set; }

        // Append a node to the end of the list.
        public void AppendNode(Node<T> toAppend)
        {
            var current = Root;

            // Can be improved using sentinel.
            if (current == null)
            {
                Root = toAppend;
            }
            else
            {
                // Search for the tail of the list.
                while (current.Next != null)
                {
                    current = current.Next;
                }

                // Append to the tail.
                current.Next = toAppend;
            }
        }

        // Remove the first node whose value is 'value'.
        public void RemoveFirst(T value)
        {
            if (Root == null)
            {
                return;
            }

            // Can be improved using sentinel.
            if (Root.Value.Equals(value))
            {
                Root = Root.Next;
                return;
            }

            // Keep reference to the previous element.
            var prev = Root;
            var current = Root.Next;

            // Search for the first node whose value is 'value'.
            while (current != null)
            {
                // If we found, remove from the list.
                if (current.Value.Equals(value))
                {
                    prev.Next = current.Next;
                    return;
                }
                // Otherwise move pointers ahead.
                prev = current;
                current = current.Next;
            }
        }

        public void InsertAt(Node<T> node, int index)
        {
            // Can be improved using sentinel.
            if (index == 0)
            {
                node.Next = Root;
                Root = node;
                return;
            }

            int i = 0;
            Node<T> current = Root;

            // Move the pointer to index - 1 position.
            while (current != null && i++ < index - 1)
            {
                current = current.Next;
            }

            // If the list had at least index - 1 elements, we can insert.
            if (current != null)
            {
                var tmp = current.Next;
                current.Next = node;
                node.Next = tmp;
            }
        }

        public void Print()
        {
            var current = Root;
            while (current != null)
            {
                Console.Write($"{current.Value} ");
                current = current.Next;
            }
            Console.WriteLine();
        }

    }
}
