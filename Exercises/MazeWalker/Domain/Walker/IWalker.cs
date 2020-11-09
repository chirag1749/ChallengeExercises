using System.Collections.Generic;
using MazeWalker.Domain.Maze;

namespace MazeWalker.Domain.Walker
{
    internal interface IWalker
    {
        void Walk();
        bool FoundExit();
        List<IPath> GetPaths();
    }
}