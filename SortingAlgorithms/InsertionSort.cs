
namespace SortingAlgorithms
{
    public static class InsertionSort
    {
        public static void Sort(int[] input)                    // | Cost |    times    |
        {                                                       // |      |             |
            for (int i = 1; i < input.Length; i++)              // |  c1  |      n      |
            {                                                   // |      |             |
                int value = input[i];                           // |  c2  |    n - 1    |
                                                                // |      |             |
                // Shift all elements greater than value to     // |      |             |
                // the right.                                   // |      |             |
                int j = i - 1;                                  // |  c4  |    n - 1    |
                while (j >= 0 && input[j] > value)              // |  c5  | sum(2,n, ti)|
                {                                               // |      |             |
                    input[j + 1] = input[j];                    // |  c6  |sum(2,n,ti-1)|
                    j = j - 1;                                  // |  c7  |sum(2,n,ti-1)|
                }                                               // |      |             |
                input[j + 1] = value;                           // |  c8  |     n - 1   |
            }                                                   // |      |             |
        }
    }
}
