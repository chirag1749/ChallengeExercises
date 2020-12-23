using System;
using System.Collections.Generic;

namespace ReverseToMakeEqual
{
    class Program
    {
        static void Main(string[] args)
        {
            testAreTheyEqual(1, new int[] { 1, 3, 5, 7, 9, 9, 1000000000 }, new int[] { 1, 3, 5, 7, 9, 9, 1000000000 }, true);
            testAreTheyEqual(2, new int[] { 1, 2, 3, 4 }, new int[] { 4, 3, 1, 2 }, true);
            testAreTheyEqual(3, new int[] { 1, 2, 3, 4 }, new int[] { 1, 4, 3, 2, 8 }, false);
            testAreTheyEqual(4, null, new int[] { 1, 4, 3, 2 }, false);

            try
            {
                testAreTheyEqual(5, new int[] { -1 }, new int[] { 1 }, false);
                testAreTheyEqual(5, new int[] { 1000000001 }, new int[] { 1 }, false);
                Console.WriteLine(string.Format("Test Case {0} Failed. ", 5));
            }
            catch { }

            Console.WriteLine("Test Complete.");
        }

        private static void testAreTheyEqual(int testCaseIdentifier, int[] arr_a, int[] arr_b, bool expectedResult)
        {
            bool actualResult = areTheyEqual(arr_a, arr_b);

            //Assert
            if (actualResult != expectedResult)
                Console.WriteLine(string.Format("Test Case {0} Failed. ", testCaseIdentifier.ToString()));
        }

        private static bool areTheyEqual(int[] arr_a, int[] arr_b)
        {
            if (!canTheyBeMadeEqual(arr_a, arr_b))
                return false;

            makeThemEqual(arr_a, arr_b);

            return true;
        }
        
        private static void makeThemEqual(int[] arr_a, int[] arr_b)
        {
            int indexToSort = 0;
            Dictionary<int, int> reverseLengthByIndex = new Dictionary<int, int>();

            do
            {
                for (int index = indexToSort; index < arr_a.Length; index++)
                {
                    if (arr_a[index] != arr_b[index])
                    {
                        if (!reverseLengthByIndex.ContainsKey(index))
                            reverseLengthByIndex.Add(index, arr_b.Length - index);
                        else
                        {
                            int reverseLength = reverseLengthByIndex[index] - 1;
                            reverseLengthByIndex[index] = reverseLength;
                        }

                        Array.Reverse(arr_b, index, reverseLengthByIndex[index]);
                        break;
                    }
                     else
                        indexToSort = indexToSort + 1;
                }
            } while (indexToSort != arr_b.Length);
        }

        private static bool canTheyBeMadeEqual(int[] arr_a, int[] arr_b)
        {
            if (arr_a == null || arr_b == null)
                return false;

            if (arr_a.Length != arr_b.Length)
                return false;

            Dictionary<int, int> valueInArrayA = new Dictionary<int, int>(arr_a.Length);

            foreach(int value in arr_a)
            {
                if (value < 0 || value > 1000000000)
                    throw new Exception("Out of Range Exception");

                if (!valueInArrayA.ContainsKey(value))
                    valueInArrayA.Add(value, 1);
                else
                {
                    int count = valueInArrayA[value];
                    valueInArrayA[value] = count + 1;
                }
            }

            foreach (int value in arr_b)
            {
                if (value < 0 || value > 1000000000)
                    throw new Exception("Out of Range Exception");

                if (!valueInArrayA.ContainsKey(value))
                    return false;
                else
                {
                    int count = valueInArrayA[value];
                    valueInArrayA[value] = count -1;
                }

                if (valueInArrayA[value] == 0)
                    valueInArrayA.Remove(value);
            }

            if (valueInArrayA.Count != 0)
                return false;

            return true;
        }
    }
}