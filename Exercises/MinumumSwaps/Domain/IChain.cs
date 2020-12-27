namespace MinumumSwaps.Domain
{
    public interface IChain<T>
    {
        ILink<T> GetRootLink();
    }
}