
using System;

namespace LinkedList
{
    public class Queue<T>
    {
        // Keeping reference to tail enable O(1) for enqueue.
        Node<T> tail;
        public Node<T> Root { get; private set; }

        public void Enqueue(T value)
        {
            var node = new Node<T>(value);
            if (Root == null)
            {
                Root = node;
                tail = node;
            }
            else
            {
                tail.Next = node;
                tail = node;
            }
        }

        public T Dequeue()
        {
            if (Root == null)
            {
                throw new InvalidOperationException();
            }
            T value = Root.Value;
            Root = Root.Next;
            return value;
        }
    }
}
