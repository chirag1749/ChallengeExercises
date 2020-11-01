namespace WardenAnd23Prisoners.Domain.Room
{
    public class Switch : ISwitch
    {
        IDomainIdentifier SwitchIdentifier;
        SwitchPosition SwitchPosition;

        public Switch(IDomainIdentifier switchIdentifier, SwitchPosition switchPosition)
        {
            SwitchIdentifier = switchIdentifier;
            SwitchPosition = switchPosition;
        }

        public IDomainIdentifier GetSwitchIdentifier()
        {
            return SwitchIdentifier;
        }
        public SwitchPosition GetSwitchPosition()
        {
            return SwitchPosition;
        }

        public void FlipSwitchPosition()
        {
            if (SwitchPosition == SwitchPosition.Up)
                SwitchPosition = SwitchPosition.Down;
            else
                SwitchPosition = SwitchPosition.Up;
        }

        public object Clone()
        {
            return new Switch(SwitchIdentifier, SwitchPosition);
        }
    }
}
