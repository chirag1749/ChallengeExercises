using System.Collections.Generic;

namespace MazeWalker.Domain.Maze
{
    public class MazeSchema : IMazeSchema
    {
        string Schema;
        List<IBuildingBlockIdentifier> BuildingBlockDefinations;

        public MazeSchema(List<IBuildingBlockIdentifier> buildingBlockDefinations, string schema)
        {
            Schema = schema;
            BuildingBlockDefinations = buildingBlockDefinations;
        }

        public List<IBuildingBlockIdentifier> GetBuildingBlockDefinations()
        {
            return BuildingBlockDefinations;
        }

        public string GetSchema()
        {
            return Schema;
        }
    }
}
