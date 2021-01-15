using System;

namespace ContiguousSubArrays
{
    class MainClass
    {
        public static void Main()
        {
            //testAreArrayEqual(1, new int[]{1,2}, new int[]{1,2}, true);
            //testAreArrayEqual(2, new int[]{1,2}, new int[]{2,2}, false);

            testContiguousSubarrays(1, new int[] { 3, 4, 1, 6, 2 }, new int[] { 1, 3, 1, 5, 1 }, false);
            testContiguousSubarrays(2, new int[] { 3 }, new int[] { 1 }, false);
            testContiguousSubarrays(3, new int[] { 3, 4 }, new int[] { 1, 2 }, false);
            testContiguousSubarrays(4, new int[] {}, new int[] {}, true);
            testContiguousSubarrays(5, null, new int[] { }, true);

            Console.WriteLine("Test Complete.");
        }

        private static int[] countSubarrays(int[] arr)
        {
            if (arr == null || arr.Length == 0 || arr.Length > 100000)
                throw new ArgumentException("Input Length not valid.");

            int[] result = new int[arr.Length];

            for (int index = 0; index < arr.Length; index++)
            {
                int count = 1;
                int currentValue = arr[index];

                if (currentValue < 1 || currentValue > 1000000000)
                    throw new ArgumentOutOfRangeException();

                for (int previousIndex = index - 1; previousIndex >= 0; previousIndex--)
                {
                    int previousValue = arr[previousIndex];

                    if (currentValue > previousValue)
                        count++;
                    else
                        break;
                }

                for (int nextIndex = index + 1; nextIndex < arr.Length; nextIndex++)
                {
                    int nextValue = arr[nextIndex];

                    if (currentValue > nextValue)
                        count++;
                    else
                        break;
                }

                //Console.WriteLine(string.Format("Current Value : {0}, Count : {1}", currentValue.ToString(), count.ToString()));

                result[index] = count;
            }

            return result;
        }

        private static void testContiguousSubarrays(int testIdentifier, int[] input, int[] expectedResult, bool exception)
        {
            try
            {
                int[] actualResult = countSubarrays(input);

                if (areArrayEqual(actualResult, expectedResult))
                    return;
            }
            catch (Exception ex)
            {
                if (exception)
                    return;
            }

            Console.WriteLine(string.Format("Test case {0} failed.", testIdentifier.ToString()));
        }

        private static void testAreArrayEqual(int testIdentifier, int[] input, int[] secondInput, bool expectedResult)
        {
            try
            {
                bool result = areArrayEqual(input, secondInput);

                if (result == expectedResult)
                    return;
            }
            catch (Exception ex)
            { }

            Console.WriteLine(string.Format("Test case {0} failed.", testIdentifier.ToString()));
        }

        private static bool areArrayEqual(int[] actual, int[] expected)
        {
            if (actual == null || expected == null)
                return false;

            if (actual.Length != expected.Length)
                return false;

            for (int index = 0; index < actual.Length; index++)
            {
                if (actual[index] != expected[index])
                    return false;
            }

            return true;
        }
    }
}
