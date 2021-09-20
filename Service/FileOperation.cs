using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Service
{
    public class FileOperation : IFileOperation
    {
        public bool Write(SortResultSummary resultSummary)
        {
            try
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
            catch(Exception e_)
            {
                return false;
            }
        }

        public bool Read(string fileName, string filePath, out string resultSummary)
        {
            try
            {
                StringBuilder sortResultSummary = new StringBuilder();
                var fullFileName = Path.Combine(filePath, fileName);
                if (File.Exists(fullFileName))
                {
                    using (StreamReader sr = new StreamReader(fullFileName))
                    {
                        sortResultSummary.AppendLine(sr.ReadToEnd());
                        sr.Close();
                        sr.Dispose();
                    }

                    resultSummary = sortResultSummary.ToString();
                    return true;
                }
                else
                {
                    resultSummary = "File not found. File may not be generated or the File Path is incorrect";
                    return false;
                }
            }
            catch (Exception e_)
            {
                resultSummary = $"An error occured : {e_.Message}";
                return false;
            }
        }
    }
}
