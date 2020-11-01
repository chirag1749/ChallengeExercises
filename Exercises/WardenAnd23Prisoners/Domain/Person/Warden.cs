using System;
using System.Collections.Generic;
using System.Linq;
using WardenAnd23Prisoners.Domain.RecordKeeping;
using WardenAnd23Prisoners.Domain.Room;

namespace WardenAnd23Prisoners.Domain.Person
{
    public class Warden : PersonOnShip, IWarden
    {
        ISwitchRoom SwitchRoom;
        List<IPrisoner> Prisoners;
        List<SwitchRoomPicture> Pictures;
        Notify AnnouncedHeard;
        bool FreePrisonersFlag = false;

        public Warden(ISwitchRoom switchRoom, List<IPrisoner> prisoners) : base(PersonOnShipRole.Warden)
        {
            SwitchRoom = switchRoom;
            Prisoners = prisoners;
            Pictures = new List<SwitchRoomPicture>();
            AnnouncedHeard = Verify;
        }

        public void Action()
        {
            IPrisoner prisoner = SelectRandomPrisoner();
            prisoner.Action(SwitchRoom, AnnouncedHeard);
            TakePictureOfPrisonerInSwitchRoomAfterAction(prisoner);
        }

        public bool FreePrisoners()
        {
            return FreePrisonersFlag;
        }

        public void Verify()
        {
            int disctinctPrisoners = Pictures.Select(x => x.GetPrisoner().GetPrisonerIdentifier().GetIdentifier()).Distinct().Count();
            if (!disctinctPrisoners.Equals(Prisoners.Count))
                throw new Exception("All Prisoners Have Not Been to the Room Yet!");

            FreePrisonersFlag = true;
            Console.WriteLine(string.Format("Amount of Pictures Warden Checked to Verify: {0}", Pictures.Count));
        }

        private IPrisoner SelectRandomPrisoner()
        {
            Random random = new Random();
            int randomNumber = random.Next(Prisoners.Count);

            return Prisoners[randomNumber];
        }

        private void TakePictureOfPrisonerInSwitchRoomAfterAction(IPrisoner prisoner)
        {
            Pictures.Add(new SwitchRoomPicture(prisoner, SwitchRoom));
        }
    }
}
