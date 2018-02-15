
using System;

namespace SortingAlgorithms
{
    public static class MergeSort
    {
        public static void Sort(int[] input)
        {
            Sort(input, 0, input.Length - 1);
        }

        static void Sort(int[] input, int left, int right)
        {
            // Base case.
            if (right - left <= 0)
            {
                return;
            }

            var size = right - left + 1;
            int middle = (left + right) / 2;

            // Recursive call on the left side of the array.
            Sort(input, left, middle);
            // Recursive call on the right side of the array.
            Sort(input, middle + 1, right);

            // MERGE STEP
            // Create temporary array for the merge step.
            var tmp = new int[size];
            // Copy the part of the array relatively sorted by
            // the two recursive calls.
            Array.Copy(input, left, tmp, 0, size);
            int lIndex = 0;
            int tmpMiddle = middle - left;
            int rIndex = tmpMiddle + 1;

            // Copy elements from tmp array to input array in order.
            for (int i = left; i <= right; i++)
            {
                if (lIndex > tmpMiddle ||
                    (rIndex < size && 
                    tmp[rIndex] < tmp[lIndex]))
                {
                    input[i] = tmp[rIndex++];
                }
                else
                {
                    input[i] = tmp[lIndex++];
                }
            }

            return;
        }
    }
}
