namespace Model.InventoryLogic.AmountLogic
{
    public interface IAmount : IReadOnlyAmount
    {
        void Increase();

        bool TryDecrease();
    }
}