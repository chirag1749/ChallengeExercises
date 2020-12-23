using System;
using System.Collections.Generic;
using System.Diagnostics;

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
                throw new Exception("Invalid Parameter.");

            Dictionary<int, Student> studentDictionary = new Dictionary<int, Student>(masterPassItToStudent.Length);
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
                    int studentIdPassFrom = index + 1;
                    int studentIdPassTo = passItToStudent[index];

                    if (studentIdPassTo < 1)
                        throw new Exception("Value less than zero not allowed.");

                    //If new Student then add to dictionary and sign their yearbook
                    if (!studentDictionary.ContainsKey(studentIdPassFrom))
                    {
                        Student student = new Student(studentIdPassFrom);
                        studentDictionary.Add(studentIdPassFrom, student);
                        student.Sign();
                    }

                    //If new Student then add to dictionary and sign their yearbook
                    if (!studentDictionary.ContainsKey(studentIdPassTo))
                    {
                        Student student = new Student(studentIdPassTo);
                        studentDictionary.Add(studentIdPassTo, student);
                        student.Sign();
                    }

                    Student studentPassFrom = studentDictionary[studentIdPassFrom];
                    Student studentPassTo = studentDictionary[studentIdPassTo];

                    if (studentIdPassFrom != studentIdPassTo)
                    {
                        YearBook yearBook = studentPassFrom.PassYearBook();
                        studentPassTo.RecieveYearBook(yearBook);
                    }
                    else
                        studentPassFrom.NoLongerAllowedToParticipate();

                    nextPassItToStudent.Add(masterPassItToStudent[studentIdPassTo - 1]);
                }

                for (int index = 0; index < passItToStudent.Length; index++)
                {
                    int studentId = index + 1;
                    Student student = studentDictionary[studentId];
                    student.Sign();
                    signCount.Add(student.GetMyYearBook().GetSignedStudents().Count);

                    if (!student.IsParticipant())
                        nonParticpantCount++;
                }

                passItToStudent = nextPassItToStudent.ToArray();

            } while (nonParticpantCount != masterPassItToStudent.Length);

            return signCount.ToArray();
        }
    }
}
