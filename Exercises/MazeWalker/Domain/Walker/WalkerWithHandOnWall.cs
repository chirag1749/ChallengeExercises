using System;
using System.Collections.Generic;
using System.Linq;
using MazeWalker.Domain.Location;
using MazeWalker.Domain.Maze;

namespace MazeWalker.Domain.Walker
{
    public abstract class WalkerWithHandOnWall:  IWalker
    {
        protected Direction FaceDirection;
        protected ILocation BodyLocation;
        protected ILocation HandLocation;

        protected IMaze Maze;
        protected bool AtExitLocation;
        protected Hand Hand;

        List<IPath> PathsTaken;

        public WalkerWithHandOnWall(IMaze maze, Hand hand)
        {
            Maze = maze;
            BodyLocation = Maze.GetStartLocation();
            AtExitLocation = false;
            Hand = hand;
            PathsTaken = new List<IPath>();

            Dictionary<Direction, IBuildingBlock> neighbors = Maze.GetNeighboringBuildingBlocks(BodyLocation);

            FaceDirection = (from kvp in neighbors
                             where kvp.Value.GetBuildingBlockType() == BuildingBlockType.Path
                             select kvp.Key).FirstOrDefault();

            Console.WriteLine(string.Format("Start: Face Direction {0}", FaceDirection.ToString()));

            PutHandOnWall(neighbors);

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
            Dictionary<Direction, IBuildingBlock> neighbors = Maze.GetNeighboringBuildingBlocks(BodyLocation);

            if (neighbors.ContainsKey(FaceDirection))
            {
                if (neighbors[FaceDirection].GetBuildingBlockType() == BuildingBlockType.Path)
                {
                    BodyLocation = neighbors[FaceDirection].GetLocation();
                    Console.WriteLine(string.Format("Body Location {0},{1}", BodyLocation.GetLatitude().GetIdentifier().ToString(), BodyLocation.GetLongitude().GetIdentifier().ToString()));
                    PathsTaken.Add(neighbors[FaceDirection] as IPath);
                    return true;
                }
            }

            return false;
        }

        protected virtual bool MoveHand(bool toCorner = false)
        {
            Dictionary<Direction, IBuildingBlock> neighbors = Maze.GetNeighboringBuildingBlocks(HandLocation);

            if (neighbors.ContainsKey(FaceDirection))
            {
                if (!toCorner)
                {
                    if (neighbors[FaceDirection].GetBuildingBlockType() == BuildingBlockType.Wall)
                    {
                        HandLocation = neighbors[FaceDirection].GetLocation();
                        Console.WriteLine(string.Format("Hand Location {0},{1}", HandLocation.GetLatitude().GetIdentifier().ToString(), HandLocation.GetLongitude().GetIdentifier().ToString()));
                        return true;
                    }
                }
                else
                {
                    HandLocation = neighbors[FaceDirection].GetLocation();
                    Console.WriteLine(string.Format("Hand Location {0},{1}", HandLocation.GetLatitude().GetIdentifier().ToString(), HandLocation.GetLongitude().GetIdentifier().ToString()));
                    return true;
                }
            }

            return false;
        }

        protected virtual void PutHandOnWall(Dictionary<Direction, IBuildingBlock> neighbors)
        {
            HandLocation = neighbors[Hand.GetDirection(FaceDirection)].GetLocation();
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
