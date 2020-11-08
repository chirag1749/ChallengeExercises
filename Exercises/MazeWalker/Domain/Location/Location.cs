namespace MazeWalker.Domain.Location
{
    public class Location : ILocation
    {
        ILatitude Latitude;
        ILongitude Longitude;

        public Location(ILatitude hortizontalIdentifier, ILongitude veritcalIdentifier)
        {
            Latitude = hortizontalIdentifier;
            Longitude = veritcalIdentifier;
        }

        public bool Equals(ILocation other)
        {
            return other.GetLatitude().Equals(Latitude) && other.GetLongitude().Equals(Longitude);
        }

        public override int GetHashCode()
        {
            return string.Format("{0},{1}", Latitude.GetHashCode(), Longitude.GetHashCode()).GetHashCode();
        }


        public ILatitude GetLatitude()
        {
            return Latitude;
        }

        public ILongitude GetLongitude()
        {
            return Longitude;
        }
    }
}
