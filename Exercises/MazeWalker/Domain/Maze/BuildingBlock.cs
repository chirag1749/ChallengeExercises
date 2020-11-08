using MazeWalker.Domain.Location;

namespace MazeWalker.Domain.Maze
{
    public class BuildingBlock : IBuildingBlock
    {
        ILocation Location;
        BuildingBlockType BuildingBlockType;

        public BuildingBlock(BuildingBlockType buildingBlockType, ILocation location)
        {
            Location = location;
            BuildingBlockType = buildingBlockType;
        }

        public BuildingBlockType GetBuildingBlockType()
        {
            return BuildingBlockType;
        }

        public ILocation GetLocation()
        {
            return Location;
        }
    }
}
