using System.Collections.Generic;
using MazeWalker.Domain.Location;

namespace MazeWalker.Domain.Maze
{
    public interface IMaze
    {
        List<IWall> GetWalls();
        List<IPath> GetPaths();
        ILocation GetStartLocation();
        Dictionary<Direction, IBuildingBlock> GetNeighboringBuildingBlocks(ILocation location);
    }
}
