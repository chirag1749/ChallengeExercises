using System;
using System.Collections.Generic;
using WardenAnd23Prisoners.Domain;
using WardenAnd23Prisoners.Domain.Person;
using WardenAnd23Prisoners.Domain.Room;

namespace WardenAnd23Prisoners
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Setup
            Random random = new Random();

            //Initalize Switches
            ISwitch switchOne = new Switch(new IntIdentifier(1), (SwitchPosition)random.Next(2));
            ISwitch switchTwo = new Switch(new IntIdentifier(2), (SwitchPosition)random.Next(2));

            //Initalize SwitchRoom
            ISwitchRoom switchRoom = new SwitchRoom(new List<ISwitch>() { switchOne, switchTwo });

            //Initalize Prisoners
            ILeader leader = new Leader(new IntIdentifier(1));
            List<IPrisoner> prisoners = new List<IPrisoner>() { leader };

            for (int prisonerId = 2; prisonerId <= 23; prisonerId++)
                prisoners.Add(new Prisoner(new IntIdentifier(prisonerId)));

            //Initalize Warden
            IWarden warden = new Warden(switchRoom, prisoners);

            //Execute
            do
            {
                warden.Action();

            } while (!warden.FreePrisoners());

            //Finalize
            Console.WriteLine("Prisoners are Free!");
        }
    }
}
