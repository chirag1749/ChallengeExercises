using MazeWalker.Domain.Location;

namespace MazeWalker.Domain.Walker
{
    public enum Side
    {
        [Hand(RotateDirection.Clockwise)]
        Right,
        [Hand(RotateDirection.CounterClockwise)]
        Left
    }
}
