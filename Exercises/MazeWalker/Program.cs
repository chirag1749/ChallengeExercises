using System;
using System.Collections.Generic;
using MazeWalker.Domain.Location;
using MazeWalker.Domain.Maze;
using MazeWalker.Domain.Walker;

namespace MazeWalker
{
    class MainClass
    {
        public static void Main()
        {
            //Setup
            string schema = @"  # # # # # # # # # # # #
                                # . . . # . . . . . . #
                                . . # . # . # # # # . #
                                # # # . # . . . . # . #
                                # . . . . # # # . # . .
                                # # # # . # . # . # . #
                                # . . # . # . # . # . #
                                # # . # . # . # . # . #
                                # . . . . . . . . # . #
                                # # # # # # . # # # . #
                                # . . . . . . # . . . #
                                # # # # # # # # # # # #";

            IMazeBuilder mazeBuilder = new RectangleMazeBuilder();
            IBuildingBlockIdentifier wallIdentifier = mazeBuilder.CreateBuildingBlockIdentifier(BuildingBlockType.Wall, "#");
            IBuildingBlockIdentifier pathIdentifier = mazeBuilder.CreateBuildingBlockIdentifier(BuildingBlockType.Path, ".");

            IMazeSchema mazeSchema = new MazeSchema
            (
                new List<IBuildingBlockIdentifier>() { wallIdentifier, pathIdentifier },
                schema
            );

            ILatitude latitude = mazeBuilder.CreateLatitude(0);
            ILongitude longitude = mazeBuilder.CreateLongitude(2);
            ILocation startLocation = mazeBuilder.CreateLocation(latitude, longitude);

            IMaze maze = new RectangleMaze(mazeBuilder, mazeSchema, startLocation);
            IWalker walker = new WalkerWithRightHand(maze);

            //Execute
            do
            {
                walker.Walk();

            } while (!walker.FoundExit());

            //Finalize
            Console.WriteLine("Walker found the exit!");
        }
    }
}
