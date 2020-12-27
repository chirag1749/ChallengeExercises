using System;
using System.Collections.Generic;
using System.Diagnostics;
using PassingYearbooks.Domain;

namespace PassingYearbooks
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            testFindSignatureCounts(1, new int[] { 2, 1 }, new int[] { 2, 2 });
            testFindSignatureCounts(2, new int[] { 1, 2 }, new int[] { 1, 1 });
            testFindSignatureCounts(3, new int[] { 3, 1, 2 }, new int[] { 2, 2, 2 });
            testFindSignatureCounts(4, new int[] { 2, 1, 3 }, new int[] { 2, 2, 1 });
            testFindSignatureCounts(5, new int[] { }, new int[] { }, true);
            testFindSignatureCounts(6, null, new int[] { }, true);
            testFindSignatureCounts(7, new int[] { 0 }, new int[] { }, true);
            sw.Stop();

            Console.WriteLine(string.Format("Test Complete. Time to Finish {0}.", sw.ElapsedMilliseconds.ToString()));
        }

        static void testFindSignatureCounts(int testCaseIdentifier, int[] arr, int[] expectedResult, bool exception = false)
        {
            try
            {
                int[] actualResult = findSignatureCounts(arr);

                //Assert
                if (!areArraysEqual(actualResult, expectedResult))
                    Console.WriteLine(string.Format("Test Case {0} Failed. ", testCaseIdentifier.ToString()));
            }
            catch (Exception)
            {
                if (!exception)
                    Console.WriteLine(string.Format("Test Case {0} Failed. ", testCaseIdentifier.ToString()));
            }
        }

        static bool areArraysEqual(int[] arr_a, int[] arr_b)
        {
            if (arr_a.Length != arr_b.Length)
                return false;

            for (int index = 0; index < arr_a.Length; index++)
            {
                if (arr_a[index] != arr_b[index])
                    return false;
            }

            return true;
        }

        static int[] findSignatureCounts(int[] masterPassItToStudent)
        {
            if (masterPassItToStudent == null || masterPassItToStudent.Length == 0)
                throw new ArgumentException("Invalid Parameter");

            IGameBuilder gameBuilder = new GameBuilder();
            Dictionary<IIdentifier, IParticipant> studentDictionary = new Dictionary<IIdentifier, IParticipant>(masterPassItToStudent.Length);
            List<int> signCount = new List<int>(masterPassItToStudent.Length);

            int[] passItToStudent = masterPassItToStudent;
            int nonParticpantCount;

            do
            {
                List<int> nextPassItToStudent = new List<int>(passItToStudent.Length);
                signCount.Clear();
                nonParticpantCount = 0;

                for (int index = 0; index < passItToStudent.Length; index++)
                {
                    IIdentifier studentIdentifierPassFrom = gameBuilder.CreateIdentifier(index + 1);
                    IIdentifier studentIdentifierPassTo = gameBuilder.CreateIdentifier(passItToStudent[index]);

                    if (Convert.ToInt32(studentIdentifierPassTo.GetIdentifier()) < 1)
                        throw new Exception("Value less than zero not allowed.");

                    //If new Student then add to dictionary and sign their yearbook
                    IParticipant studentPassFrom = GetOrAddParticipantIfMissing(gameBuilder, studentDictionary, studentIdentifierPassFrom);                    //If new Student then add to dictionary and sign their yearbook
                    IParticipant studentPassTo = GetOrAddParticipantIfMissing(gameBuilder, studentDictionary, studentIdentifierPassTo);

                    if (!studentPassFrom.Equals(studentPassTo))
                    {
                        IYearBook yearBook = studentPassFrom.PassYearBook();
                        studentPassTo.RecieveYearBook(yearBook);
                    }
                    else
                        studentPassFrom.NoLongerAllowedToParticipate();

                    nextPassItToStudent.Add(masterPassItToStudent[passItToStudent[index] - 1]);
                }

                for (int index = 0; index < passItToStudent.Length; index++)
                {
                    IIdentifier studentIdentifier = gameBuilder.CreateIdentifier(index + 1);
                    IParticipant student = studentDictionary[studentIdentifier];
                    student.Sign();
                    signCount.Add(student.GetMyYearBook().GetSignedStudents().Count);

                    if (!student.IsParticipant())
                        nonParticpantCount++;
                }

                passItToStudent = nextPassItToStudent.ToArray();

            } while (nonParticpantCount != masterPassItToStudent.Length);

            return signCount.ToArray();
        }

        private static IParticipant GetOrAddParticipantIfMissing(IGameBuilder gameBuilder, Dictionary<IIdentifier, IParticipant> participantDictionary, IIdentifier participantIdentifier)
        {
            if (!participantDictionary.ContainsKey(participantIdentifier))
            {
                IParticipant student = gameBuilder.CreateParticipant(participantIdentifier);
                participantDictionary.Add(participantIdentifier, student);
                student.Sign();
            }

            return participantDictionary[participantIdentifier];
        }
    }
}
