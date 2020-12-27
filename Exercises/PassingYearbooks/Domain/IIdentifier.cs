using System;

namespace PassingYearbooks.Domain
{
    public interface IIdentifier: IEquatable<IIdentifier>
    {
        object GetIdentifier();
        TypeCode GetIdentifierType();
    }
}