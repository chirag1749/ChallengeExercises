namespace MinumumSwaps.Domain
{
    public class IntLeftRightChain : IChain<int>
    {
        ILink<int> RootLink;
        
        public IntLeftRightChain(int[] arr)
        {
            IIdentifier rootIdentifier = new IntIdentifier(0);
            RootLink = new IntLink(arr[0], rootIdentifier);
         
            //Add to Chain
            for (int index = 1; index < arr.Length; index++)
            {
                int value = arr[index];
                ILink<int> link = RootLink;

                do
                {
                    if (value < link.GetValue())
                    {
                        ILink<int> linked = link.GetLeftLink();

                        if (linked == null)
                        {
                            IIdentifier identifier = new IntIdentifier(index);
                            ILink<int> leftLink = new IntLink(value, identifier);

                            link.AddLeftLink(leftLink);
                            break;
                        }

                        link = linked;
                    }
                    else
                    {
                        ILink<int> linked = link.GetRightLink();

                        if (linked == null)
                        {
                            IIdentifier identifier = new IntIdentifier(index);
                            ILink<int> rightLink = new IntLink(value, identifier);

                            link.AddRightLink(rightLink);
                            break;
                        }

                        link = linked;
                    }
                }
                while (true);
            }
        }

        public ILink<int> GetRootLink()
        {
            return RootLink;
        }
    }
}
