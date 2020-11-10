using System;
using System.Collections.Generic;
using System.Linq;
using MazeWalker.Domain.Location;
using MazeWalker.Domain.Maze;

namespace MazeWalker.Domain.Walker
{
    public abstract class WalkerWithHandOnWall:  IWalker
    {
        IMaze Maze;
        IHand Hand;

        protected Direction FaceDirection;
        protected ILocation BodyLocation;
        protected ILocation HandLocation;
        protected bool AtExitLocation;

        List<IPath> PathsTaken;

        public WalkerWithHandOnWall(IMaze maze, IHand hand)
        {
            Maze = maze;
            BodyLocation = Maze.GetStartLocation();
            AtExitLocation = false;
            Hand = hand;
            PathsTaken = new List<IPath>();

            Dictionary<Direction, IBuildingBlock> buildingBlocksAroundBodyLocation = Maze.GetNeighboringBuildingBlocks(BodyLocation);

            FaceDirection = (from kvp in buildingBlocksAroundBodyLocation
                             where kvp.Value.GetBuildingBlockType() == BuildingBlockType.Path
                             select kvp.Key).FirstOrDefault();

            Console.WriteLine(string.Format("Start: Face Direction {0}", FaceDirection.ToString()));

            PutHandOnWall(buildingBlocksAroundBodyLocation);

            if (HandLocation == null)
                throw new Exception("Not able to touch the wall.");

            Console.WriteLine(string.Format("Start: Hand Location {0},{1}", HandLocation.GetLatitude().GetIdentifier().ToString(), HandLocation.GetLongitude().GetIdentifier().ToString()));
            Console.WriteLine(string.Format("Start: Body Location {0},{1}", BodyLocation.GetLatitude().GetIdentifier().ToString(), BodyLocation.GetLongitude().GetIdentifier().ToString()));
        }

        public virtual bool FoundExit()
        {
            return AtExitLocation;
        }

        public virtual void Walk()
        {
            /*                    0 1 2 3 4 5 6 7 8 9 10 11
            string schema = @" 0  # # # # # # # # # # # #
                               1  # . . . # . . . . . . #
                               2  . . # . # . # # # # . #
                               3  # # # . # . . . . # . #
                               4  # . . . . # # # . # . .
                               5  # # # # . # . # . # . #
                               6  # . . # . # . # . # . #
                               7  # # . # . # . # . # . #
                               8  # . . . . . . . . # . #
                               9  # # # # # # . # # # . #
                               10 # . . . . . . # . . . #
                               11 # # # # # # # # # # # #";
            */

            bool didIWalk = MoveBody();
            bool didIMoveMyHand = MoveHand();

            if (didIWalk && didIMoveMyHand)
                return;

            if (!didIWalk && !didIMoveMyHand)
                MoveHand(true);

            if (!didIWalk)
            {
                ChangeDirectionBecauseDidNotWalk();

                AtExitLocation = !MoveHand(didIMoveMyHand);

                Console.WriteLine(string.Format("Head Direction {0}", FaceDirection.ToString()));
            }
            else
            {
                ChangeDirectionBecauseDidNotMoveHand();

                MoveBody();

                Console.WriteLine(string.Format("Head Direction {0}", FaceDirection.ToString()));
            }
        }

        protected virtual bool MoveBody()
        {
            Dictionary<Direction, IBuildingBlock> buildingBlocksAroundBodyLocatoin = Maze.GetNeighboringBuildingBlocks(BodyLocation);

            if (buildingBlocksAroundBodyLocatoin.ContainsKey(FaceDirection))
            {
                if (buildingBlocksAroundBodyLocatoin[FaceDirection].GetBuildingBlockType() == BuildingBlockType.Path)
                {
                    BodyLocation = buildingBlocksAroundBodyLocatoin[FaceDirection].GetLocation();
                    Console.WriteLine(string.Format("Body Location {0},{1}", BodyLocation.GetLatitude().GetIdentifier().ToString(), BodyLocation.GetLongitude().GetIdentifier().ToString()));
                    PathsTaken.Add(buildingBlocksAroundBodyLocatoin[FaceDirection] as IPath);
                    return true;
                }
            }

            return false;
        }

        protected virtual bool MoveHand(bool ignoreWallValidation = false)
        {
            Dictionary<Direction, IBuildingBlock> buildingBlocksAroundHandLocation = Maze.GetNeighboringBuildingBlocks(HandLocation);

            if (buildingBlocksAroundHandLocation.ContainsKey(FaceDirection))
            {
                if (ignoreWallValidation || buildingBlocksAroundHandLocation[FaceDirection].GetBuildingBlockType() == BuildingBlockType.Wall)
                {
                    HandLocation = buildingBlocksAroundHandLocation[FaceDirection].GetLocation();
                    Console.WriteLine(string.Format("Hand Location {0},{1}", HandLocation.GetLatitude().GetIdentifier().ToString(), HandLocation.GetLongitude().GetIdentifier().ToString()));
                    return true;
                }
            }

            return false;
        }

        protected virtual void PutHandOnWall(Dictionary<Direction, IBuildingBlock> buildingBlocksAroundCurrentLocation)
        {
            HandLocation = buildingBlocksAroundCurrentLocation[Hand.GetDirection(FaceDirection)].GetLocation();
        }

        protected virtual void ChangeDirectionBecauseDidNotMoveHand()
        {
            FaceDirection = Hand.GetDirection(FaceDirection);
        }

        protected virtual void ChangeDirectionBecauseDidNotWalk()
        {
            FaceDirection = Hand.GetCounterDirection(FaceDirection);
        }

        public List<IPath> GetPaths()
        {
            return PathsTaken;
        }
    }
}
