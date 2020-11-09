using MazeWalker.Utilities;

namespace MazeWalker.Domain.Walker
{
    public static class SideExtention
    {
        public static Hand GetHand(this Side value)
        {
            return value.GetAttribute<Hand>();
        }
    }
}
