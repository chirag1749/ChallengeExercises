namespace PassingYearbooks
{
    public class Student
    {
        int StudentId;
        YearBook MyYearBook;
        YearBook YearBookToPass;
        YearBook YearBookRecieved;
        bool Participant = true;

        public Student(int studentId)
        {
            StudentId = studentId;
            MyYearBook = new YearBook(this);
            YearBookRecieved = MyYearBook;
        }

        public void RecieveYearBook(YearBook yearBook)
        {
            if (yearBook.GetOwner().GetStudentId() == StudentId)
                Participant = false;

            if (!Participant)
                return;

            YearBookRecieved = yearBook;
        }

        public YearBook PassYearBook()
        {
            YearBook yearBook = GetYearBookToPass();
            YearBookToPass = null;
            return yearBook;
        }

        public void Sign()
        {
            YearBook yearBook = GetRecievedYearBook();

            if (yearBook != null)
            {
                yearBook.Sign(this);
                YearBookToPass = yearBook;
                YearBookRecieved = null;
            }
        }

        public int GetStudentId()
        {
            return StudentId;
        }

        public YearBook GetMyYearBook()
        {
            return MyYearBook;
        }

        public YearBook GetYearBookToPass()
        {
            return YearBookToPass;
        }

        public YearBook GetRecievedYearBook()
        {
            return YearBookRecieved;
        }

        public bool IsParticipant()
        {
            return Participant;
        }

        public void NoLongerAllowedToParticipate()
        {
            Participant = false;
        }
    }
}
