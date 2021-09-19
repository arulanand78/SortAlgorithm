using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SortResultSummary
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public int[] OutputData { get; set; }

        public int[] InputData { get; set; }

        public SortType.Algorithm SortAlgorithm { get; set; }
    }
}
