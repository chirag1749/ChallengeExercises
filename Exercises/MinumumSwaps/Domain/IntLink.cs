namespace MinumumSwaps.Domain
{
    public class IntLink : ILink<int>
    {
        int Value;
        IIdentifier Identifier;
        ILink<int> LeftLink;
        ILink<int> RightLink;

        public IntLink(int value, IIdentifier identifier)
        {
            Value = value;
            Identifier = identifier;
        }

        public void AddLeftLink(ILink<int> link)
        {
            LeftLink = link;
        }

        public void AddRightLink(ILink<int> link)
        {
            RightLink = link;
        }

        public bool Equals(ILink<int> other)
        {
            return other.GetLinkIdentifier().Equals(GetLinkIdentifier());
        }

        public IIdentifier GetLinkIdentifier()
        {
            return Identifier;
        }

        public ILink<int> GetLeftLink()
        {
            return LeftLink;
        }

        public ILink<int> GetRightLink()
        {
            return RightLink;
        }

        public int GetValue()
        {
            return Value;
        }
    }
}
