using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ClosestPointsToOrigin
{
    class MainClass
    {
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TestGetClosestPointsToOrigin(1, 1, new int[] { 0,0 }, new int[,] { {1,3}, {-2,2}, { -1, -3 } }, new int[,] { { -2, 2 } });
            TestGetClosestPointsToOrigin(2, 2, new int[] { 0, 0 }, new int[,] { { 3, 3 }, { 5, -1 }, { -2, 4 } }, new int[,] { { 3, 3 }, { -2, 4 } });
            sw.Stop();
            Console.WriteLine(string.Format("Test Complete. {0}", sw.ElapsedMilliseconds.ToString()));
        }

        public static void TestGetClosestPointsToOrigin
        (
            int testIdentifier,
            int amountOfPointsClosestToOrigin,
            int[] origin,
            int[,] pointsToAnalyze,
            int[,] expectedOutput)
        {
            try
            {
                int[,] actualOutput = GetClosestPointsToOrigin(amountOfPointsClosestToOrigin, origin, pointsToAnalyze);
                
                bool isEqual =
                            actualOutput.Rank == expectedOutput.Rank &&
                            Enumerable.Range(0, actualOutput.Rank).All(dimension => actualOutput.GetLength(dimension) ==
                            expectedOutput.GetLength(dimension)) &&
                            actualOutput.Cast<int>().SequenceEqual(expectedOutput.Cast<int>());

                if (isEqual)
                    return;
            }
            catch
            {}

            Console.WriteLine(string.Format("Test case {0} failed.", testIdentifier));
        }


        public static int[,] GetClosestPointsToOrigin(int amountOfPointsClosestToOrigin, int[] origin, int[,] pointsToAnalyze)
        {
            List<DistanceAndLocation> distanceAndLocations = new List<DistanceAndLocation>(amountOfPointsClosestToOrigin);
            IComparer<DistanceAndLocation> comparer = new OrderDistanceAndLocation();

            for (int index = 0; index < pointsToAnalyze.GetLength(0); index++)
            {
                int pointToAnalyzeX = pointsToAnalyze[index, 0];
                int pointToAnalyzeY = pointsToAnalyze[index, 1];

                int xDistanceValue = Math.Abs(pointToAnalyzeX - origin[0]);
                int yDistanceValue = Math.Abs(pointToAnalyzeY - origin[1]);

                double distance = Math.Sqrt(Math.Pow(xDistanceValue, 2) + Math.Pow(yDistanceValue, 2));

                if (distanceAndLocations.Count < amountOfPointsClosestToOrigin)
                {
                    DistanceAndLocation distanceAndLocation = new DistanceAndLocation()
                    {
                        X = pointToAnalyzeX,
                        Y = pointToAnalyzeY,
                        Distance = distance
                    };

                    distanceAndLocations.Add(distanceAndLocation);
                    distanceAndLocations.Sort(comparer);
                }
                else
                {
                    DistanceAndLocation maxDistanceCurrently = distanceAndLocations[distanceAndLocations.Count - 1];

                    if (maxDistanceCurrently.Distance > distance)
                    {
                        DistanceAndLocation distanceAndLocation = new DistanceAndLocation()
                        {
                            X = pointToAnalyzeX,
                            Y = pointToAnalyzeY,
                            Distance = distance
                        };

                        distanceAndLocations[distanceAndLocations.Count - 1] = distanceAndLocation;
                        distanceAndLocations.Sort(comparer);
                    }
                }
            }

            int[,] result = new int[amountOfPointsClosestToOrigin, 2];

            for (int index = 0; index < distanceAndLocations.Count; index++)
            {
                result[index, 0] = distanceAndLocations[index].X;
                result[index, 1] = distanceAndLocations[index].Y;
            }

            return result;
        }

        public struct DistanceAndLocation
        {
            public int X;
            public int Y;
            public double Distance;
        }

        public class OrderDistanceAndLocation : IComparer<DistanceAndLocation>
        {
            public int Compare(DistanceAndLocation x, DistanceAndLocation y)
            {
                int compareDistanceAndLocation = x.Distance.CompareTo(y.Distance);

                return compareDistanceAndLocation;
            }
        }
    }
}
