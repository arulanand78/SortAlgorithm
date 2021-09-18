using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ISortAlgorithm
    {
        public Task<int[]> DoSort(int[] unsortedIntegers);
    }
}
