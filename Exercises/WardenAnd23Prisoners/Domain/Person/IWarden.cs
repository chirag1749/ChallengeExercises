namespace WardenAnd23Prisoners.Domain.Person
{
    public interface IWarden : IPersonOnShip
    {
        void Action();
        void Verify();
        bool FreePrisoners();
    }
}
