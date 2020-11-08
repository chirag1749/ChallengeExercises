using System;
using MazeWalker.Domain.Location;
using LocationNS = MazeWalker.Domain.Location;

namespace MazeWalker.Domain.Maze
{
    public class RectangleMazeBuilder: IMazeBuilder
    {
        public virtual ILatitude CreateLatitude(object identifier)
        {
            if (identifier is int)
                return new IntLatitude((int)identifier);

            throw new NotImplementedException();
        }

        public virtual ILongitude CreateLongitude(object identifier)
        {
            if (identifier is int)
                return new IntLongitude((int)identifier);

            throw new NotImplementedException();
        }

        public virtual ILocation CreateLocation(ILatitude latitude, ILongitude longitude)
        {
            return new LocationNS.Location(latitude, longitude);
        }

        public virtual IBuildingBlockIdentifier CreateBuildingBlockIdentifier(BuildingBlockType buildingBlockType, string defination)
        {
            return new BuildingBlockIdentifier(buildingBlockType, defination);
        }

        public virtual IWall CreateWall(ILocation location)
        {
            return new Wall(location);
        }

        public virtual IPath CreatePath(ILocation location)
        {
            return new Path(location);
        }
    }
}
