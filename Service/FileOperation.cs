using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Service
{
    public class FileOperation
    {
        public bool Write(SortResultSummary resultSummary)
        {
            var fullFileName = Path.Combine(resultSummary.FilePath, resultSummary.FileName);
            using (StreamWriter sw = new StreamWriter(fullFileName))
            {
                sw.WriteLine($"Sorting Integers using        : {resultSummary.SortAlgorithm.ToString()}");
                sw.WriteLine($"Unsorted Integers             : {string.Join(',', resultSummary.InputData)}");
                sw.WriteLine($"Sorted Integers               : {string.Join(',', resultSummary.OutputData)}");
                sw.Flush();
                sw.Close();
                sw.Dispose();
            };
            
            return true;
        }

        public bool Read(string fileName)
        {
            return true;
        }
}
