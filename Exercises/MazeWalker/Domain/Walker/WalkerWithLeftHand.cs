using MazeWalker.Domain.Maze;

namespace MazeWalker.Domain.Walker
{
    public class WalkerWithLeftHand : WalkerWithHandOnWall
    {
        public WalkerWithLeftHand(IMaze maze) : base(maze, Side.Left.GetHand())
        { }
    }
}
