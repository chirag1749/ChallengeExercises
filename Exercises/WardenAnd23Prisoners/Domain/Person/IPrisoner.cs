using WardenAnd23Prisoners.Domain.Room;

namespace WardenAnd23Prisoners.Domain.Person
{
    public interface IPrisoner : IPersonOnShip
    {
        IDomainIdentifier GetPrisonerIdentifier();
        void Action(ISwitchRoom switchRoom, Notify Announce);
    }
}
