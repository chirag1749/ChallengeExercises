using System;
using System.Collections.Generic;
using System.Linq;

namespace PassingYearbooks
{
    class MainClass
    {
        static void Main(string[] args)
        {
            testFindSignatureCounts(1, new int[] { 2, 1 }, new int[] { 2, 2 });
            testFindSignatureCounts(2, new int[] { 1, 2 }, new int[] { 1, 1 });
            testFindSignatureCounts(3, new int[] { 3, 1, 2 }, new int[] { 2, 2, 2 });
            testFindSignatureCounts(4, new int[] { 2, 1, 3 }, new int[] { 2, 2, 1 });
            testFindSignatureCounts(5, new int[] { }, new int[] { }, true);
            testFindSignatureCounts(6, null, new int[] { }, true);
            testFindSignatureCounts(7, new int[] { 0 }, new int[] { }, true);

            Console.WriteLine("Test Complete.");
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

        private static int[] findSignatureCounts(int[] masterPassItToStudent)
        {
            if (masterPassItToStudent == null || masterPassItToStudent.Length == 0)
                throw new Exception("Invalid Parameter.");

            Dictionary<int, Student> studentDictionary = new Dictionary<int, Student>();

            //Add Students to Dictionary and have them Sign their own year book
            for (int index = 0; index < masterPassItToStudent.Length; index++)
            {
                int studentId = masterPassItToStudent[index];

                if (studentId < 1)
                    throw new Exception("Value less than zero not allowed.");

                Student student = new Student(studentId);
                studentDictionary.Add(studentId, student);

                student.Sign();
            }

            int[] passItToStudent = masterPassItToStudent;

            do
            {
                List<int> nextPassItToStudent = new List<int>(passItToStudent.Length);

                //Pass the Books
                for (int index = 0; index < passItToStudent.Length; index++)
                {
                    int studentIdPassFrom = index + 1;
                    int studentIdPassTo = passItToStudent[index];

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

                //All Sign Books
                for (int index = 0; index < passItToStudent.Length; index++)
                {
                    int studentId = index + 1;
                    studentDictionary[studentId].Sign();
                }

                passItToStudent = nextPassItToStudent.ToArray();
            }
            while (
                        //Keep the passing going until all students are not participants
                        (from student in studentDictionary
                         where student.Value.IsParticipant() == true
                         select student).ToList().Count != 0
            );

            //Get Signed Counts for each student
            List<int> signCount = new List<int>();
            for (int index = 0; index < masterPassItToStudent.Length; index++)
            {
                int studentId = index + 1;
                signCount.Add(studentDictionary[studentId].GetMyYearBook().GetSignedStudents().Count);
            }

            return signCount.ToArray();
        }
    }
}
