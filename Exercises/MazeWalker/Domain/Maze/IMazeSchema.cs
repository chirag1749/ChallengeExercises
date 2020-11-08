using System.Collections.Generic;

namespace MazeWalker.Domain.Maze
{
    public interface IMazeSchema
    {
        List<IBuildingBlockIdentifier> GetBuildingBlockDefinations();
        string GetSchema();
    };
}
