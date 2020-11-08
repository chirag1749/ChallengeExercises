using System;
using System.Collections.Generic;
using System.Linq;
using MazeWalker.Domain.Location;

namespace MazeWalker.Domain.Maze
{
    public class RectangleMaze : IMaze
    {
        ILocation StartLocation;
        List<IWall> Walls;
        List<IPath> Paths;
        Dictionary<ILocation, Dictionary<Direction, IBuildingBlock>> ThreeSixtyView;

        public RectangleMaze(IMazeBuilder mazeBuilder, IMazeSchema mazeSchema, ILocation startLocation)
        {
            StartLocation = startLocation;
            Walls = new List<IWall>();
            Paths = new List<IPath>();
            ThreeSixtyView = new Dictionary<ILocation, Dictionary<Direction, IBuildingBlock>>();

            string[] schemaLines = mazeSchema.GetSchema().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            IBuildingBlockIdentifier wallIdentifier = GetBuildingBlockIdentifier(mazeSchema, BuildingBlockType.Wall);
            IBuildingBlockIdentifier pathIdentifier = GetBuildingBlockIdentifier(mazeSchema, BuildingBlockType.Path);

            List<List<IBuildingBlock>> buildingBlocksByRows = CreateBuildingBlocks(mazeBuilder, schemaLines, wallIdentifier, pathIdentifier);
            PopulateThreeSixtyView(buildingBlocksByRows);

            foreach(List<IBuildingBlock> buildingBlocks in buildingBlocksByRows)
            {
                foreach(IBuildingBlock buildingBlock in buildingBlocks)
                {
                    switch(buildingBlock.GetBuildingBlockType())
                    {
                        case BuildingBlockType.Wall:
                            Walls.Add(buildingBlock as IWall);
                            break;
                        case BuildingBlockType.Path:
                            Paths.Add(buildingBlock as IPath);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }

        public ILocation GetStartLocation()
        {
            return StartLocation;
        }

        public List<IWall> GetWalls()
        {
            return Walls.AsReadOnly().ToList();
        }

        public List<IPath> GetPaths()
        {
            return Paths.AsReadOnly().ToList();
        }

        public Dictionary<Direction, IBuildingBlock> GetNeighboringBuildingBlocks(ILocation location)
        {
            return ThreeSixtyView[location];
        }

        private IBuildingBlockIdentifier GetBuildingBlockIdentifier(IMazeSchema mazeSchema, BuildingBlockType buildingBlockType)
        {
            return (from x in mazeSchema.GetBuildingBlockDefinations()
                    where x.GetBuildingBlockType() == buildingBlockType
                    select x).FirstOrDefault();
        }
        private List<List<IBuildingBlock>> CreateBuildingBlocks(IMazeBuilder mazeBuilder, string[] schemaLines, IBuildingBlockIdentifier wallIdentifier, IBuildingBlockIdentifier pathIdentifier)
        {
            List<List<IBuildingBlock>> buildingBlocksByRows = new List<List<IBuildingBlock>>();

            for (int schemaRowIndex = 0; schemaRowIndex <= schemaLines.Length - 1; schemaRowIndex++)
            {
                List<IBuildingBlock> buildingBlocksInRow = new List<IBuildingBlock>();
                buildingBlocksByRows.Add(buildingBlocksInRow);

                string schemaRow = schemaLines[schemaRowIndex].Trim();
                string[] schemaColumns = schemaRow.Split();

                int latiduteIndex = -1; //To Keep track of space which is not BuildingBlock
                ILongitude longitude = mazeBuilder.CreateLongitude(schemaRowIndex);

                for (int schemaColumnIndex = 0; schemaColumnIndex <= schemaColumns.Length - 1; schemaColumnIndex++)
                {
                    BuildingBlockType? buildingBlockType = null;

                    if (schemaColumns[schemaColumnIndex].ToString() == wallIdentifier.GetIdentifier().ToString())
                        buildingBlockType = BuildingBlockType.Wall;
                    else if (schemaColumns[schemaColumnIndex].ToString() == pathIdentifier.GetIdentifier().ToString())
                        buildingBlockType = BuildingBlockType.Path;

                    if (buildingBlockType.HasValue)
                    {
                        latiduteIndex++;
                        ILatitude latitude = mazeBuilder.CreateLatitude(latiduteIndex);
                        ILocation location = mazeBuilder.CreateLocation(latitude, longitude);
                        IBuildingBlock buildingBlock;
                        switch (buildingBlockType)
                        {
                            case BuildingBlockType.Wall:
                                buildingBlock = mazeBuilder.CreateWall(location);
                                Walls.Add(buildingBlock as IWall);
                                break;
                            case BuildingBlockType.Path:
                                buildingBlock = mazeBuilder.CreatePath(location);
                                Paths.Add(buildingBlock as IPath);
                                break;
                            default:
                                throw new NotImplementedException();
                        }

                        buildingBlocksInRow.Add(buildingBlock);
                    }
                }
            }

            return buildingBlocksByRows;
        }
        private void PopulateThreeSixtyView(List<List<IBuildingBlock>> buildingBlocksByRows)
        {
            for (int rowIndex = 0; rowIndex < buildingBlocksByRows.Count; rowIndex++)
            {
                List<IBuildingBlock> buildingBlocksInRow = buildingBlocksByRows[rowIndex];
                List<ILocation> locationsInRow = buildingBlocksInRow.Select(x => x.GetLocation()).ToList();

                //Add East Locations
                for (int columnIndex = 0; columnIndex < buildingBlocksInRow.Count - 1; columnIndex++)
                {
                    ILocation location = locationsInRow[columnIndex];

                    if (!ThreeSixtyView.ContainsKey(location))
                        ThreeSixtyView.Add(location, new Dictionary<Direction, IBuildingBlock>());

                    Dictionary<Direction, IBuildingBlock> locationInRow = ThreeSixtyView[location];
                    locationInRow.Add(Direction.East, buildingBlocksInRow[columnIndex + 1]);
                }

                //Add West Locations
                for (int columnIndex = buildingBlocksInRow.Count - 1; columnIndex > 0; columnIndex--)
                {
                    ILocation location = locationsInRow[columnIndex];

                    if (!ThreeSixtyView.ContainsKey(location))
                        ThreeSixtyView.Add(location, new Dictionary<Direction, IBuildingBlock>());

                    Dictionary<Direction, IBuildingBlock> locationInRow = ThreeSixtyView[location];
                    locationInRow.Add(Direction.West, buildingBlocksInRow[columnIndex - 1]);
                }

                //Add South Locations
                if (rowIndex != buildingBlocksByRows.Count - 1)//Last Row will have no South
                {
                    for (int columnIndex = 0; columnIndex < buildingBlocksInRow.Count; columnIndex++)
                    {
                        ILocation location = locationsInRow[columnIndex];

                        if (!ThreeSixtyView.ContainsKey(location))
                            ThreeSixtyView.Add(location, new Dictionary<Direction, IBuildingBlock>());

                        Dictionary<Direction, IBuildingBlock> locationInRow = ThreeSixtyView[location];
                        locationInRow.Add(Direction.South, buildingBlocksByRows[rowIndex + 1][columnIndex]);
                    }
                }

                //Add North Locations
                if (rowIndex != 0)
                {
                    for (int columnIndex = 0; columnIndex < buildingBlocksInRow.Count; columnIndex++)
                    {
                        ILocation location = locationsInRow[columnIndex];

                        if (!ThreeSixtyView.ContainsKey(location))
                            ThreeSixtyView.Add(location, new Dictionary<Direction, IBuildingBlock>());

                        Dictionary<Direction, IBuildingBlock> locationInRow = ThreeSixtyView[location];
                        locationInRow.Add(Direction.North, buildingBlocksByRows[rowIndex - 1][columnIndex]);
                    }
                }
            }
        }
    }
}
