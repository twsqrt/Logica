namespace Model.ResourceLogic.AmountLogic
{
    public interface IAmount
    {
        void Increase(int amount);

        bool TryDecrease(int amount);
    }
}