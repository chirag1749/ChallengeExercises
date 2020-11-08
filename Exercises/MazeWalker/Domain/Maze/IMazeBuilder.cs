using MazeWalker.Domain.Location;

namespace MazeWalker.Domain.Maze
{
    public interface IMazeBuilder
    {
        IBuildingBlockIdentifier CreateBuildingBlockIdentifier(BuildingBlockType buildingBlockType, string defination);
        ILatitude CreateLatitude(object identifier);
        ILongitude CreateLongitude(object identifier);
        ILocation CreateLocation(ILatitude latitude, ILongitude longitude);

        IWall CreateWall(ILocation location);
        IPath CreatePath(ILocation location);
    }
}
