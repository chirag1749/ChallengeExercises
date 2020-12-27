using System;
namespace MinumumSwaps.Domain
{
    public interface IIdentifier : IEquatable<IIdentifier>
    {
        object GetIdentifier();
        TypeCode GetIdentifierType();
    }
}
