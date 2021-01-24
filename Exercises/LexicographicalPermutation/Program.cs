using System;
using System.Collections.Generic;

namespace LexicographicalPermutation
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Test(1, 4, new int[] { 1, 2, 3 }, new int[] { 2, 3, 1 });
            Console.WriteLine("Test Complete.");
        }

        public static void Test(int testIdentifier, int permutationId, int[] array, int[] expectedOutcome, bool exceptionExpected = false)
        {
            int[] actualOutcome = null;
            bool asset = true;

            try
            {
                actualOutcome = GetPermutationAtIndex(permutationId - 1, array);
            }
            catch
            {
                if (exceptionExpected)
                    return;
            }

            if (actualOutcome == null || actualOutcome.Length == 0)
                asset = false;

            if (asset)
            {
                for (int index = 0; index < array.Length; index++)
                {
                    if (actualOutcome[index] != expectedOutcome[index])
                    {
                        asset = false;
                        break;
                    }
                }
            }

            if (!asset)
                Console.WriteLine(string.Format("Test case {0} failed.", testIdentifier));

        }

        public static int[] GetPermutationAtIndex(int index, int[] array)
        {
            List<int[]> permutations = new List<int[]>();
            permutations.Add(array);

            if (index != 0)
            {
                do
                {
                    if (NextPermutation(array))
                        permutations.Add(array);
                    else
                        break;

                } while (permutations.Count != index + 1);
            }


            return permutations[index];
        }

        public static bool NextPermutation(int[] array)
        {
            int lastIndex = array.Length - 1;

            //Only One Element.
            if (lastIndex == 0)
                return false;

            //Get Index where the number stops increasing
            int indexWhereNumberStopsIncreasing = GetIndexWhereLeftElementIsSmaller(array);

            //The First Element is the highest Number
            if (indexWhereNumberStopsIncreasing == 0)
                return false;

            //Get Index to Swap. Swap with the left element of increase stop from backwards
            int indexToSwapWith = lastIndex;
            while (array[indexToSwapWith] < array[indexWhereNumberStopsIncreasing - 1])
                indexToSwapWith--;

            //Swap
            int value = array[indexWhereNumberStopsIncreasing - 1];
            array[indexWhereNumberStopsIncreasing - 1] = array[indexToSwapWith];
            array[indexToSwapWith] = value;

            // Reverse until the index where increase stops
            int indexToStartReversing = array.Length - 1;
            while (indexWhereNumberStopsIncreasing < indexToStartReversing)
            {
                int valueReversing = array[indexWhereNumberStopsIncreasing];
                array[indexWhereNumberStopsIncreasing] = array[indexToStartReversing];
                array[indexToStartReversing] = valueReversing;
                indexWhereNumberStopsIncreasing++;
                indexToStartReversing--;
            }

            return true;
        }

        public static int GetIndexWhereLeftElementIsSmaller(int[] array)
        {
            //Start from back
            for (int index = array.Length - 1; index > 0; index--)
            {
                if (array[index] > array[index - 1])
                    return index;
            }

            return 0;
        }
    }
}
