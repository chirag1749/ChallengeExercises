using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosestPointsToOrigin
{
    class MainClass
    {
        public static void Main()
        {
            TestGetClosestPointsToOrigin(1, 1, new int[] { 0,0 }, new int[,] { {1,3}, {-2,2}, { -1, -3 } }, new int[,] { { -2, 2 } });
            TestGetClosestPointsToOrigin(2, 2, new int[] { 0, 0 }, new int[,] { { 3, 3 }, { 5, -1 }, { -2, 4 } }, new int[,] { { 3, 3 }, { -2, 4 } });
            Console.WriteLine("Test Complete.");
        }

        public static void TestGetClosestPointsToOrigin
        (
            int testIdentifier,
            int amountOfPointsClosestToOrigin,
            int[] origin,
            int[,] pointsToAnalyze,
            int[,] expectedOutput)
        {
            int [,] actualOutput = GetClosestPointsToOrigin(amountOfPointsClosestToOrigin, origin, pointsToAnalyze);

            bool isEqual =
                        actualOutput.Rank == expectedOutput.Rank &&
                        Enumerable.Range(0, actualOutput.Rank).All(dimension => actualOutput.GetLength(dimension) ==
                        expectedOutput.GetLength(dimension)) &&
                        actualOutput.Cast<int>().SequenceEqual(expectedOutput.Cast<int>());

            if (isEqual)
                return;

            Console.WriteLine(string.Format("Test case {0} failed.", testIdentifier));
        }


        public static int[,] GetClosestPointsToOrigin(int amountOfPointsClosestToOrigin, int[] origin, int[,] pointsToAnalyze)
        {
            List<double> distances = new List<double>();
            Dictionary<double, List<int>> dictionaryDistanceWithIndex = new Dictionary<double, List<int>>();

            for (int index = 0; index < pointsToAnalyze.GetLength(0); index++)
            {
                int pointToAnalyzeX = pointsToAnalyze[index, 0];
                int pointToAnalyzeY = pointsToAnalyze[index, 1];

                int xDistanceValue = Math.Abs(pointToAnalyzeX - origin[0]);
                int yDistanceValue = Math.Abs(pointToAnalyzeY - origin[1]);

                double distance = Math.Sqrt(Math.Pow(xDistanceValue, 2) + Math.Pow(yDistanceValue, 2));

                if (distances.Count < amountOfPointsClosestToOrigin)
                {
                    distances.Add(distance);
                    distances.Sort();

                    if (dictionaryDistanceWithIndex.ContainsKey(distance))
                        dictionaryDistanceWithIndex[distance].Add(index);
                    else
                        dictionaryDistanceWithIndex.Add(distance, new List<int>() { index });
                }
                else
                {
                    double maxDistanceCurrently = distances[distances.Count - 1];

                    if (maxDistanceCurrently > distance)
                    {
                        distances[distances.Count - 1] = distance;
                        distances.Sort();

                        if (dictionaryDistanceWithIndex.ContainsKey(distance))
                            dictionaryDistanceWithIndex[distance].Add(index);
                        else
                            dictionaryDistanceWithIndex.Add(distance, new List<int>() { index });
                    }
                }
            }

            int[,] result = new int[amountOfPointsClosestToOrigin, 2];
            int addedAmount = 0;

            for (int index = 0; index < distances.Count; index++)
            {
                List<int> originalIndexes = dictionaryDistanceWithIndex[distances[index]];

                foreach (int originalIndex in originalIndexes)
                {
                    result[index, 0] = pointsToAnalyze[originalIndex, 0];
                    result[index, 1] = pointsToAnalyze[originalIndex, 1];
                    addedAmount = addedAmount + 1;

                    if (addedAmount == amountOfPointsClosestToOrigin)
                        break;
                }

                if (addedAmount == amountOfPointsClosestToOrigin)
                    break;
            }

            return result;
        }
    }
}
