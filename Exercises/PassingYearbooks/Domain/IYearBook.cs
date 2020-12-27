using System.Collections.Generic;

namespace PassingYearbooks.Domain
{
    public interface IYearBook
    {
        IParticipant GetOwner();
        void Sign(IParticipant student);
        List<IParticipant> GetSignedStudents();
    }
}