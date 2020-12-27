namespace PassingYearbooks.Domain
{
    public interface IGameBuilder
    {
        IIdentifier CreateIdentifier(object value);
        IParticipant CreateParticipant(IIdentifier participantIdentifier);
        IYearBook CreateYearBook(IParticipant participant);
    }
}
