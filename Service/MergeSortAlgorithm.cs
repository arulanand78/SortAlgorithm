using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MergeSortAlgorithm : ISortAlgorithm
    {
        public int[] DoSort(int[] unsortedIntegers)
        {
            int[] leftArray;
            int[] rightArray;

            if (unsortedIntegers.Length <= 1)
                return unsortedIntegers;

            int midPoint = unsortedIntegers.Length / 2;
            leftArray = new int[midPoint];

            rightArray = new int[unsortedIntegers.Length - midPoint];

            for (int i = 0; i < midPoint; i++)
                leftArray[i] = unsortedIntegers[i];

            for (int i = 0; i < unsortedIntegers.Length-midPoint; i++)
                rightArray[i] = unsortedIntegers[i + midPoint];

            leftArray = DoSort(leftArray);
            rightArray = DoSort(rightArray);
            return Merge(leftArray, rightArray);
        }

        public int[] Merge(int[] leftArray , int[] rightArray)
        {
            int[] sortedIntegers;
            sortedIntegers = new int[leftArray.Length + rightArray.Length];
            int leftArrayIndex = 0;
            int rightArrayIndex = 0;
            int sortedArrayIndex = 0;

            while (leftArrayIndex < leftArray.Length && rightArrayIndex < rightArray.Length)
            {
                if (leftArray[leftArrayIndex] <= rightArray[rightArrayIndex])
                {
                    sortedIntegers[sortedArrayIndex] = leftArray[leftArrayIndex];
                    leftArrayIndex++;
                }
                else
                {
                    sortedIntegers[sortedArrayIndex] = rightArray[rightArrayIndex];
                    rightArrayIndex++;
                }
                sortedArrayIndex++;
            }

            while (leftArrayIndex < leftArray.Length)
            {
                sortedIntegers[sortedArrayIndex] = leftArray[leftArrayIndex];
                sortedArrayIndex++;
                leftArrayIndex++;
            }

            while (rightArrayIndex < rightArray.Length)
            {
                sortedIntegers[sortedArrayIndex] = rightArray[rightArrayIndex];
                sortedArrayIndex++;
                rightArrayIndex++;
            }

            return sortedIntegers;
        }
    }
}
