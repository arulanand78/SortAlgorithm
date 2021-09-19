using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Service;
using System;

namespace Tests
{
    [TestClass]
    public class BubbleSortTests
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void GivenUnsortedIntegers_WhenSortingCalled_ThenTheIntegerArraysShouldBeInSortedOrder()
        {
            var bubbleSortAlgorithm = new BubbleSortAlgorithm();
            int[] unsortedIntegers = new int[] { 10, 49, 13, 7, 24, 78, 99, 56, 54, 31, 19, 3 };

            var sortedIntegers = bubbleSortAlgorithm.DoSort(unsortedIntegers).GetAwaiter().GetResult();

            var expectedOutput = new int[] { 3, 7, 10, 13, 19, 24, 49, 54, 56, 78, 99 };

            Assert.AreEqual(expectedOutput, sortedIntegers);
        }

    }
}
