using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class MergeSortTests
    {
        [TestMethod]
        public void GivenUnsortedIntegers_WhenSortingCalled_ThenTheIntegerArraysShouldBeInSortedOrder()
        {
            var mergeSortAlgorithm = new MergeSortAlgorithm();
            int[] unsortedIntegers = new int[] { 10, 49, 13, 7, 24, 78, 99, 56, 54, 31, 19, 3, 100 };
            
            var sortedIntegers = mergeSortAlgorithm.DoSort(unsortedIntegers);
            var expectedOutput = new int[] { 3, 7, 10, 13, 19, 24, 31, 49, 54, 56, 78, 99, 100 };

            sortedIntegers.Should().Equal(expectedOutput);
        }
    }
}
