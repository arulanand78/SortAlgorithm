using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ISortAlgorithm
    {
        public int[] DoSort(int[] unsortedIntegers);
    }
}
