using System.Collections.Generic;

namespace PassingYearbooks.Domain
{
    public class YearBook: IYearBook
    {
        List<IParticipant> StudentSigns;
        IParticipant Owner;

        public YearBook(IParticipant owner)
        {
            StudentSigns = new List<IParticipant>();
            Owner = owner;
        }

        public void Sign(IParticipant student)
        {
            StudentSigns.Add(student);
        }

        public IParticipant GetOwner()
        {
            return Owner;
        }

        public List<IParticipant> GetSignedStudents()
        {
            return StudentSigns;
        }
    }
}
