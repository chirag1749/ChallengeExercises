namespace WardenAnd23Prisoners.Domain.Person
{
    public abstract class PersonOnShip : IPersonOnShip
    {
        PersonOnShipRole PersonOnShipRole;

        public PersonOnShip(PersonOnShipRole personOnShipRole)
        {
            PersonOnShipRole = personOnShipRole;
        }

        public PersonOnShipRole GetPersonOnShipRole()
        {
            return PersonOnShipRole;
        }
    }
}
