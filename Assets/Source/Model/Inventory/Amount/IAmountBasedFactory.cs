namespace Model.InventoryLogic.AmountLogic
{
    public interface IAmountBasedFactory<T>
    {
        T Create(IReadOnlyValueAmount amount);

        T Create(InfinityAmount amount);
    }
}