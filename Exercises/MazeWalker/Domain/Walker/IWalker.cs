namespace MazeWalker.Domain.Walker
{
    internal interface IWalker
    {
        void Walk();
        bool FoundExit();
    }
}