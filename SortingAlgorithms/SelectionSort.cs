
namespace SortingAlgorithms
{
    public static class SelectionSort
    {
        public static void Sort(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                // Set input[i] to be the smallest element.
                int min = i;
                // Look for smaller element in input[i+1..n].
                for (int j = i + 1; j < input.Length; j++)
                {
                    // If smaller element found, update.
                    if (input[j] < input[min])
                    {
                        min = j;
                    }
                }

                // Swap input[i] with smallest element found.
                int tmp = input[min];
                input[min] = input[i];
                input[i] = tmp;
                // All elements in input[0..i] are in their final position.
            }
        }
    }
}
