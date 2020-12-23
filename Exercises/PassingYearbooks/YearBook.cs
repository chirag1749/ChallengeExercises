using System.Collections.Generic;

namespace PassingYearbooks
{
    public class YearBook
    {
        List<Student> StudentSigns;
        Student Owner;

        public YearBook(Student owner)
        {
            StudentSigns = new List<Student>();
            Owner = owner;
        }

        public void Sign(Student student)
        {
            StudentSigns.Add(student);
        }

        public Student GetOwner()
        {
            return Owner;
        }

        public List<Student> GetSignedStudents()
        {
            return StudentSigns;
        }
    }
}
