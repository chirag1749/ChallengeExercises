namespace MazeWalker.Domain.Maze
{
    public interface IBuildingBlockIdentifier : IIdentifer
    {
        BuildingBlockType GetBuildingBlockType();
    }
}
