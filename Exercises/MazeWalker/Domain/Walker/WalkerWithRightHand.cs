using MazeWalker.Domain.Maze;

namespace MazeWalker.Domain.Walker
{
    public class WalkerWithRightHand : WalkerWithHandOnWall
    {
        public WalkerWithRightHand(IMaze maze) : base(maze, Side.Right.GetHand())
        { }
    }
}
