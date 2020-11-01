using System.Collections.Generic;
using WardenAnd23Prisoners.Domain.Person;
using WardenAnd23Prisoners.Domain.Room;

namespace WardenAnd23Prisoners.Domain.RecordKeeping
{
    public class SwitchRoomPicture
    {
        readonly IPrisoner Prisoner;
        List<ISwitch> Switches;

        public SwitchRoomPicture(IPrisoner prisoner, ISwitchRoom switchRoom)
        {
            Prisoner = prisoner;
            Switches = new List<ISwitch>();
            foreach (ISwitch sw in switchRoom.GetSwithes())
                Switches.Add(sw.Clone() as ISwitch);
        }

        public IPrisoner GetPrisoner()
        {
            return Prisoner;
        }

        public IReadOnlyCollection<ISwitch> GetSwitches()
        {
            return Switches;
        }
    }
}
