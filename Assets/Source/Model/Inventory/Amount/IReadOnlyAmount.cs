namespace Model.InventoryLogic.AmountLogic
{
    public interface IReadOnlyAmount
    {
        bool LessThan(int amount);

        T AcceptFactory<T>(IAmountBasedFactory<T> factory);
    }
}