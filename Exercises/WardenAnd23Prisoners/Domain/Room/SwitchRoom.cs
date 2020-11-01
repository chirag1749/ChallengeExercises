using System.Collections.Generic;

namespace WardenAnd23Prisoners.Domain.Room
{
    public class SwitchRoom : ISwitchRoom
    {
        List<ISwitch> Switches;
        public SwitchRoom(List<ISwitch> switches)
        {
            Switches = switches;
        }

        public List<ISwitch> GetSwithes()
        {
            return Switches;
        }
    }
}
