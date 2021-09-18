﻿using System;
using System.Threading.Tasks;

namespace Service
{
    public class BubbleSortAlgorithm : ISortAlgorithm
    {
        public BubbleSortAlgorithm()
        {

        }

        public Task<int[]> DoSort(int[] unsortedIntegers)
        {
            for ( int i = 0; i<unsortedIntegers.Length; i++)
            {
                for (int j=0; j < unsortedIntegers.Length-1; j++)
                {
                    var currInt = unsortedIntegers[j];
                    var nextInt = unsortedIntegers[j + 1];

                    if (currInt > nextInt)
                    {
                        unsortedIntegers[j + 1] = currInt;
                        unsortedIntegers[j] = nextInt;
                    }
                }
            }

            return Task.FromResult(unsortedIntegers);
        }
    }
}
