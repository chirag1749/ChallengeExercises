using System;

namespace MazeWalker.Domain
{
    public interface IIdentifer : IEquatable<IIdentifer>
    {
        object GetIdentifier();
        TypeCode GetIdentifierType();
    }
}
