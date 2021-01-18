using System;
using System.Collections.Generic;

namespace FibonacciSequence
{
    class MainClass
    {
        public static void Main()
        {
            TestGetFibonacciSequence(1, 0, 0);
            TestGetFibonacciSequence(2, 1, 1);
            TestGetFibonacciSequence(3, 2, 1);
            TestGetFibonacciSequence(4, 4, 3);
            TestGetFibonacciSequence(5, 50, 12586269025);

            Console.WriteLine("Test Complete.");
        }

        public static void TestGetFibonacciSequence(int testCaseIdentifier, int input, long outputExpected)
        {
            long actualOutput = GetFibonacciSequence(input);

            if (actualOutput == outputExpected)
                return;

            Console.WriteLine(string.Format("Test Case {0} Failed. ", testCaseIdentifier.ToString()));
        }

        public static long GetFibonacciSequence(int input)
        {
            Dictionary<int, long> indexValue = new Dictionary<int, long>();
            indexValue.Add(0, 0);
            indexValue.Add(1, 1);

            for(int index = 2; index < input + 1; index++)
            {
                if (!indexValue.ContainsKey(index))
                    indexValue.Add(index, indexValue[index - 1] + indexValue[index - 2]);
            }

            return indexValue[input];
        }
    }
}
