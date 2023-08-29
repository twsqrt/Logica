namespace Model.InventoryLogic.AmountLogic
{
    public interface IAmount
    {
        void Increase(int amount);

        bool TryDecrease(int amount);

        T AcceptFactory<T>(IAmountBasedFactory<T> factory);
    }
}