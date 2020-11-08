using System;

namespace MazeWalker.Domain.Location
{
    public interface ILocation: IEquatable<ILocation>
    {
        ILatitude GetLatitude();
        ILongitude GetLongitude();
    }
}
