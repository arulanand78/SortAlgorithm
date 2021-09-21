using Domain;
using Service;
using System;
using Microsoft.Extensions.DependencyInjection;


namespace SortAlgorithm.Models
{
    public class AlgorithmFactory : IAlgorithmFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public AlgorithmFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISortAlgorithm GetSortAlgorithm(string algorithmType)
        {
            if ( algorithmType == SortType.Algorithm.Bubblesort.ToString())
            {
                return _serviceProvider.GetService<BubbleSortAlgorithm>();
            }
            else if (algorithmType == SortType.Algorithm.Mergesort.ToString())
            {
                return _serviceProvider.GetService<MergeSortAlgorithm>();
            }
            else
            {
                return null;
            }
        }
    }
}
