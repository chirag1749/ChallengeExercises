using System;

namespace WardenAnd23Prisoners.Domain
{
    public interface IDomainIdentifier
    {
        object GetIdentifier();
        TypeCode GetIdentifierType();
    }
}
