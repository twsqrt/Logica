namespace Model.InventoryLogic.AmountLogic
{
    public interface IAmountBasedFactory<T>
    {
        T Create(ValueAmount amount);

        T Create(InfinityAmount amount);
    }
}