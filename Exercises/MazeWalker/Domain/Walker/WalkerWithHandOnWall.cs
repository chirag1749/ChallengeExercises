using System;
using System.Collections.Generic;
using MazeWalker.Domain.Location;
using MazeWalker.Domain.Maze;
using System.Linq;

namespace MazeWalker.Domain.Walker
{
    public abstract class WalkerWithHandOnWall:  IWalker
    {
        IMaze Maze;
        ILocation BodyLocation;
        ILocation HandLocation;
        Direction HeadingDirection;
        bool AtExitLocation;

        Dictionary<Direction, Direction> ClockWiseDirection;
        Dictionary<Direction, Direction> CounterClockWiseDirection;

        Side Hand;

        public WalkerWithHandOnWall(IMaze maze, Side hand)
        {
            Maze = maze;
            BodyLocation = Maze.GetStartLocation();
            AtExitLocation = false;
            Hand = hand;

            ClockWiseDirection = new Dictionary<Direction, Direction>();
            ClockWiseDirection.Add(Direction.North, Direction.East);
            ClockWiseDirection.Add(Direction.East, Direction.South);
            ClockWiseDirection.Add(Direction.South, Direction.West);
            ClockWiseDirection.Add(Direction.West, Direction.North);

            CounterClockWiseDirection = new Dictionary<Direction, Direction>();
            CounterClockWiseDirection.Add(Direction.North, Direction.West);
            CounterClockWiseDirection.Add(Direction.West, Direction.South);
            CounterClockWiseDirection.Add(Direction.South, Direction.East);
            CounterClockWiseDirection.Add(Direction.East, Direction.North);

            Dictionary<Direction, IBuildingBlock> neighbors = Maze.GetNeighboringBuildingBlocks(BodyLocation);

            HeadingDirection = (from kvp in neighbors
                                where kvp.Value.GetBuildingBlockType() == BuildingBlockType.Path
                                select kvp.Key).FirstOrDefault();

            Console.WriteLine(string.Format("Start: Head Direction {0}", HeadingDirection.ToString()));

            switch (HeadingDirection)
            {
                case Direction.East:
                {
                    switch (Hand)
                    {
                        case Side.Left:
                            HandLocation = neighbors[Direction.North].GetLocation();
                            break;
                        case Side.Right:
                            HandLocation = neighbors[Direction.South].GetLocation();
                            break;
                    }
                    break;
                }
                case Direction.West:
                {
                    switch (Hand)
                    {
                        case Side.Left:
                            HandLocation = neighbors[Direction.South].GetLocation();
                            break;
                        case Side.Right:
                            HandLocation = neighbors[Direction.North].GetLocation();
                            break;
                    }
                    break;
                }
                case Direction.North:
                    {
                        switch (Hand)
                        {
                            case Side.Left:
                                HandLocation = neighbors[Direction.East].GetLocation();
                                break;
                            case Side.Right:
                                HandLocation = neighbors[Direction.West].GetLocation();
                                break;
                        }
                        break;
                    }
                case Direction.South:
                    {
                        switch (Hand)
                        {
                            case Side.Left:
                                HandLocation = neighbors[Direction.West].GetLocation();
                                break;
                            case Side.Right:
                                HandLocation = neighbors[Direction.East].GetLocation();
                                break;
                        }
                        break;
                    }
            }

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
                switch (Hand)
                {
                    case Side.Left:
                        HeadingDirection = ClockWiseDirection[HeadingDirection];
                        break;
                    case Side.Right:
                        HeadingDirection = CounterClockWiseDirection[HeadingDirection];
                        break;
                }
                    
                AtExitLocation = !MoveHand(didIMoveMyHand);

                Console.WriteLine(string.Format("Head Direction {0}", HeadingDirection.ToString()));
            }
            else
            {
                switch (Hand)
                {
                    case Side.Left:
                        HeadingDirection = CounterClockWiseDirection[HeadingDirection];
                        break;
                    case Side.Right:
                        HeadingDirection = ClockWiseDirection[HeadingDirection];
                        break;
                }

                MoveBody();

                Console.WriteLine(string.Format("Head Direction {0}", HeadingDirection.ToString()));
            }
        }

        internal virtual bool MoveBody()
        {
            Dictionary<Direction, IBuildingBlock> neighbors = Maze.GetNeighboringBuildingBlocks(BodyLocation);

            if (neighbors.ContainsKey(HeadingDirection))
            {
                if (neighbors[HeadingDirection].GetBuildingBlockType() == BuildingBlockType.Path)
                {
                    BodyLocation = neighbors[HeadingDirection].GetLocation();
                    Console.WriteLine(string.Format("Body Location {0},{1}", BodyLocation.GetLatitude().GetIdentifier().ToString(), BodyLocation.GetLongitude().GetIdentifier().ToString()));
                    return true;
                }
            }

            return false;
        }

        internal virtual bool MoveHand(bool toCorner = false)
        {
            Dictionary<Direction, IBuildingBlock> neighbors = Maze.GetNeighboringBuildingBlocks(HandLocation);

            if (neighbors.ContainsKey(HeadingDirection))
            {
                if (!toCorner)
                {
                    if (neighbors[HeadingDirection].GetBuildingBlockType() == BuildingBlockType.Wall)
                    {
                        HandLocation = neighbors[HeadingDirection].GetLocation();
                        Console.WriteLine(string.Format("Hand Location {0},{1}", HandLocation.GetLatitude().GetIdentifier().ToString(), HandLocation.GetLongitude().GetIdentifier().ToString()));
                        return true;
                    }
                }
                else
                {
                    HandLocation = neighbors[HeadingDirection].GetLocation();
                    Console.WriteLine(string.Format("Hand Location {0},{1}", HandLocation.GetLatitude().GetIdentifier().ToString(), HandLocation.GetLongitude().GetIdentifier().ToString()));
                    return true;
                }
            }

            return false;
        }
    }
}
