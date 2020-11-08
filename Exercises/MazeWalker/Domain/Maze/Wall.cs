using MazeWalker.Domain.Location;

namespace MazeWalker.Domain.Maze
{
    public class Wall : BuildingBlock, IWall
    {
        public Wall(ILocation location) : base(BuildingBlockType.Wall, location) { }
    }
}
