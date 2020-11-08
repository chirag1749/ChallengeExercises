namespace MazeWalker.Domain.Location
{
    public class IntLongitude : IntIdentifer, ILongitude
    {
        public IntLongitude(int identifier) : base(identifier) { }

        public bool Equals(ILongitude other)
        {
            return base.Equals(other);
        }
    }
}
