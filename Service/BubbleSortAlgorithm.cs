using System.Threading.Tasks;

namespace Service
{
    public class BubbleSortAlgorithm : ISortAlgorithm
    {
        public BubbleSortAlgorithm()
        {

        }

        public int[] DoSort(int[] unsortedIntegers)
        {
            var swapped = true;
            do
            {
                swapped = false;
                for (int j = 0; j < unsortedIntegers.Length - 1; j++)
                {
                    var currInt = unsortedIntegers[j];
                    var nextInt = unsortedIntegers[j + 1];

                    if (currInt > nextInt)
                    {
                        unsortedIntegers[j + 1] = currInt;
                        unsortedIntegers[j] = nextInt;
                        swapped = true;
                    }
                }
            } while (swapped);

            return unsortedIntegers;
        }
    }
}
