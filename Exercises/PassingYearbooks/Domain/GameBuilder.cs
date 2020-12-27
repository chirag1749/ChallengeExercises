using System;

namespace PassingYearbooks.Domain
{
    public class GameBuilder : IGameBuilder
    {
        public IIdentifier CreateIdentifier(object value)
        {
            return new IntIdentifier(Convert.ToInt32(value));
        }

        public IParticipant CreateParticipant(IIdentifier participantIdentifier)
        {
            return new Student(this, participantIdentifier);
        }

        public IYearBook CreateYearBook(IParticipant participant)
        {
            return new YearBook(participant);
        }
    }
}