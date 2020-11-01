using System;

namespace WardenAnd23Prisoners.Domain
{
    public class IntIdentifier : IDomainIdentifier
    {
        readonly int Identifier;
        public IntIdentifier(int identifier)
        {
            Identifier = identifier;
        }

        public object GetIdentifier()
        {
            return Identifier;
        }

        public TypeCode GetIdentifierType()
        {
            return TypeCode.Int32;
        }
    }
}
