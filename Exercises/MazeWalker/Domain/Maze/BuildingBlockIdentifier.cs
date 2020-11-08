using System;

namespace MazeWalker.Domain.Maze
{
    public class BuildingBlockIdentifier : IBuildingBlockIdentifier
    {
        string Defination;
        BuildingBlockType BuildingBlockType;

        public BuildingBlockIdentifier(BuildingBlockType buildingBlockType, string defination)
        {
            Defination = defination;
            BuildingBlockType = buildingBlockType;
        }

        public bool Equals(IIdentifer other)
        {
            return other.GetIdentifierType().Equals(TypeCode.String) &&
                other.GetIdentifier().Equals(Defination);
        }

        public BuildingBlockType GetBuildingBlockType()
        {
            return BuildingBlockType;
        }

        public object GetIdentifier()
        {
            return Defination;
        }

        public TypeCode GetIdentifierType()
        {
            return TypeCode.String;
        }
    }
}
