using System;

namespace PassingYearbooks.Domain
{
    public interface IParticipant: IEquatable<IParticipant>
    {
        IIdentifier GetParticipantIdentifier();

        IYearBook GetMyYearBook();
        IYearBook PassYearBook();
        void RecieveYearBook(IYearBook yearBook);

        void Sign();
        bool IsParticipant();
        void NoLongerAllowedToParticipate();
    }
}
