using System;
namespace PassingYearbooks.Domain
{
    public class IntIdentifier : IIdentifier
    {
        int Identifier;

        public IntIdentifier(int identifier)
        {
            Identifier = identifier;
        }

        public virtual bool Equals(IIdentifier other)
        {
            return other.GetIdentifierType().Equals(TypeCode.Int32) &&
                other.GetIdentifier().Equals(Identifier);
        }

        public override int GetHashCode()
        {
            return Identifier;
        }

        public virtual object GetIdentifier()
        {
            return Identifier;
        }

        public virtual TypeCode GetIdentifierType()
        {
            return TypeCode.Int32;
        }
    }
}
