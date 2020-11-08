using MazeWalker.Domain.Location;

namespace MazeWalker.Domain.Maze
{
    public class Path : BuildingBlock, IPath
    {
        public Path(ILocation location) : base(BuildingBlockType.Path, location) { }
    }
}
