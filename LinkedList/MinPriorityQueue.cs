using System;

namespace LinkedList
{
    public class MinPriorityQueue<T>
    {
        const int InitialCapacity = 0;

        PriorityElement[] heap;
        int capacity;

        public int Count { get; private set; }

        public MinPriorityQueue(
            int capacity = InitialCapacity)
        {
            Count = 0;
            this.capacity = capacity + 1;
            heap = new PriorityElement[this.capacity + 1];
        }

        public void Enqueue(T item, int priority)
        {
            if (Count == capacity)
            {
                Grow();
            }

            heap[++Count] = new PriorityElement(item, priority);
            Swim(Count);
            //Debug.Assert(IsMinHeap());
        }

        public T Dequeue()
        {
            var element = heap[1];
            heap[1] = heap[Count];
            Sink(1);
            heap[Count--] = null;
            return element.Value;
        }

        private bool IsMinHeap()
        {
            return IsMinHeap(1);
        }

        // is subtree of pq[1..n] rooted at k a min heap?
        private bool IsMinHeap(int k)
        {
            if (k > Count) return true;
            int left = 2 * k;
            int right = 2 * k + 1;
            if (left <= Count && IsGreater(k, left)) return false;
            if (right <= Count && IsGreater(k, right)) return false;
            return IsMinHeap(left) && IsMinHeap(right);
        }

        void Grow()
        {
            var newheap = new PriorityElement[capacity * 2 + 1];
            Array.Copy(heap, newheap, heap.Length);
            heap = newheap;
            capacity = capacity * 2;
        }

        void Swim(int k)
        {
            while (k > 1 && IsGreater(k / 2, k))
            {
                Swap(k, k / 2);
                k = k / 2;
            }
        }

        void Sink(int k)
        {
            while (2 * k <= Count)
            {
                int j = 2 * k;
                if (j < Count && IsGreater(j, j + 1))
                {
                    j++;
                }

                if (!IsGreater(k, j))
                {
                    break;
                }

                Swap(k, j);
                k = j;
            }
        }

        void Swap(int i, int j)
        {
            var tmp = heap[i];
            heap[i] = heap[j];
            heap[j] = tmp;
        }

        bool IsGreater(int i, int j)
        {
            return heap[i].Priority - heap[j].Priority > 0;
        }

        class PriorityElement
        {
            public PriorityElement(T value, int priority)
            {
                Value = value;
                Priority = priority;
            }

            public T Value { get; }
            public int Priority { get; }
        }
    }
}
