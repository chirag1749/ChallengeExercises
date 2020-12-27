using System;

namespace PassingYearbooks.Domain
{
    public class Student: IParticipant
    {
        IIdentifier StudentIdentifier;
        IYearBook MyYearBook;
        IYearBook YearBookToPass;
        IYearBook YearBookRecieved;
        bool Participant = true;

        public Student(IGameBuilder gameBuilder, IIdentifier studentIdentifier)
        {
            StudentIdentifier = studentIdentifier;
            MyYearBook = gameBuilder.CreateYearBook(this);
            YearBookRecieved = MyYearBook;
        }

        public void RecieveYearBook(IYearBook yearBook)
        {
            if (yearBook.GetOwner().GetParticipantIdentifier().Equals(StudentIdentifier))
                Participant = false;

            if (!Participant)
                return;

            YearBookRecieved = yearBook;
        }

        public IYearBook PassYearBook()
        {
            IYearBook yearBook = GetYearBookToPass();
            YearBookToPass = null;
            return yearBook;
        }

        public void Sign()
        {
            IYearBook yearBook = GetRecievedYearBook();

            if (yearBook != null)
            {
                yearBook.Sign(this);
                YearBookToPass = yearBook;
                YearBookRecieved = null;
            }
        }

        public IIdentifier GetParticipantIdentifier()
        {
            return StudentIdentifier;
        }

        public IYearBook GetMyYearBook()
        {
            return MyYearBook;
        }

        public IYearBook GetYearBookToPass()
        {
            return YearBookToPass;
        }

        public IYearBook GetRecievedYearBook()
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

        public override int GetHashCode()
        {
            return StudentIdentifier.GetHashCode();
        }

        public bool Equals(IParticipant other)
        {
            return other.GetParticipantIdentifier().Equals(this.GetParticipantIdentifier());
        }
    }
}
