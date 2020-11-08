using MazeWalker.Domain.Location;

namespace MazeWalker.Domain.Maze
{
    public interface IBuildingBlock
    {
        ILocation GetLocation();
        BuildingBlockType GetBuildingBlockType();
    }
}
