using System;

namespace WardenAnd23Prisoners.Domain.Room
{
    public interface ISwitch : ICloneable
    {
        IDomainIdentifier GetSwitchIdentifier();
        SwitchPosition GetSwitchPosition();
        void FlipSwitchPosition();
    }
}
