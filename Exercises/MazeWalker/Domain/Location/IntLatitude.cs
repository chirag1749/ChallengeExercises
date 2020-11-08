namespace MazeWalker.Domain.Location
{
    public class IntLatitude : IntIdentifer, ILatitude
    {
        public IntLatitude(int identifier) : base(identifier) { }

        public bool Equals(ILatitude other)
        {
            return base.Equals(other);
        }
    }
}
