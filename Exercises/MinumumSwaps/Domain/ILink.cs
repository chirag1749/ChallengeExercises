using System;

namespace MinumumSwaps.Domain
{
    public interface ILink<T>: IEquatable<ILink<T>>
    {
        ILink<T> GetLeftLink();
        ILink<T> GetRightLink();
        T GetValue();

        void AddLeftLink(ILink<T> link);
        void AddRightLink(ILink<T> link);

        IIdentifier GetLinkIdentifier();
    }
}
