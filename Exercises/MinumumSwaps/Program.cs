using System;
using System.Diagnostics;
using MinumumSwaps.Domain;

namespace MinumumSwaps
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            TestGetAmountOfSwapsToOrder(1, new int[] { 2, 1 }, 1);
            TestGetAmountOfSwapsToOrder(2, new int[] { 1, 3, 2 }, 1);
            TestGetAmountOfSwapsToOrder(3, new int[] { 4, 3, 1, 2 }, 3);
            TestGetAmountOfSwapsToOrder(4, new int[] { 1, 2 }, 0);
            TestGetAmountOfSwapsToOrder(5, new int[] { 7, 1, 3, 2, 4, 5, 6 }, 5);
            TestGetAmountOfSwapsToOrder(6, new int[] { }, 1, true);
            TestGetAmountOfSwapsToOrder(7, null, 1, true);

            sw.Stop();

            Console.WriteLine(string.Format("Test Complete. Time to Finish {0}.", sw.ElapsedMilliseconds.ToString()));
        }

        private static void TestGetAmountOfSwapsToOrder(int testCaseIdentifier, int[] arr, int expectedResult, bool exception = false)
        {
            try
            {
                int actualResult = GetAmountOfSwapsToOrder(arr);

                //Assert
                if (!actualResult.Equals(expectedResult))
                    Console.WriteLine(string.Format("Test Case {0} Failed. ", testCaseIdentifier.ToString()));
            }
            catch (Exception)
            {
                if (!exception)
                    Console.WriteLine(string.Format("Test Case {0} Failed. ", testCaseIdentifier.ToString()));
            }
        }

        private static int GetAmountOfSwapsToOrder(int[] arr)
        {
            if (arr == null || arr.Length == 0)
                throw new ArgumentException("Invalid Parameter");

            if (arr.Length == 1)
                return 0;

            int[] currentIntArray = arr;
            int swapCount = 0;
            
            do
            {
                IChain<int> chain = new IntLeftRightChain(currentIntArray);
                ILink<int> swapOneLink = GetFirstSwapLink(chain.GetRootLink());

                if (swapOneLink != null)
                {
                    ILink<int> swapTwoLink = GetSwapWithLink(swapOneLink);
                    currentIntArray[Convert.ToInt32(swapOneLink.GetLinkIdentifier().GetIdentifier())] = swapTwoLink.GetValue();
                    currentIntArray[Convert.ToInt32(swapTwoLink.GetLinkIdentifier().GetIdentifier())] = swapOneLink.GetValue();
                    swapCount = swapCount + 1;
                }
                else
                    break;

            } while (true);

            return swapCount;
        }

        private static ILink<int> GetSwapWithLink(ILink<int> swapOneLink)
        {
            ILink<int> link = swapOneLink;

            do
            {
                ILink<int> leftLink = link.GetLeftLink();

                if (leftLink != null)
                    link = leftLink;
                else
                    return link;
            }
            while (true);
        }

        private static ILink<int> GetFirstSwapLink(ILink<int> intLink)
        {
            ILink<int> link = intLink;

            do
            {
                ILink<int> leftLink = link.GetLeftLink();

                if (leftLink != null)
                    return link;
                else
                {
                    ILink<int> rightLink = link.GetRightLink();

                    if (rightLink == null)
                        break;

                    link = rightLink;
                }
            }
            while (true);

            return null;
        }
    }
}
