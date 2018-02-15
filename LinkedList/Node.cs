
namespace LinkedList
{
    public class Node<T>
    {
        public Node(T value, Node<T> next = null)
        {
            Value = value;
            Next = next;
        }

        public T Value { get; }
        public Node<T> Next { get; set; }
    }
}
