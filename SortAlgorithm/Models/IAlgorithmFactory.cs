using Service;

namespace SortAlgorithm.Models
{
    public interface IAlgorithmFactory
    {
        ISortAlgorithm GetSortAlgorithm(string algorithmType);
    }
}
