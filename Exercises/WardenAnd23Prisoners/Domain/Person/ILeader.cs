namespace WardenAnd23Prisoners.Domain.Person
{
    public interface ILeader : IPrisoner
    {
        void MakeAnnouncement(Notify InformWardenToVerify);
    }
}
