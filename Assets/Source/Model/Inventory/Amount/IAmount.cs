namespace Model.InventoryLogic.AmountLogic
{
    public interface IAmount
    {
        void Increase(int amount);

        bool TryDecrease(int amount);

        bool NotMoreThan(int amount);

        T AcceptFactory<T>(IAmountBasedFactory<T> factory);
    }
}