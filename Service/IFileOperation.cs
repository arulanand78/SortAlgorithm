using Domain;

namespace Service
{
    public interface IFileOperation
    {
        bool Write(SortResultSummary resultSummary);
        bool Read(string fileName, string filePath, out string resultSummary);
    }
}