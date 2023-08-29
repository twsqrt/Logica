namespace Model.InventoryLogic.AmountLogic
{
    public class InfinityAmount : IAmount
    {
        public void Increase(int amount) { }

        public bool TryDecrease(int amount) => true;

        public T AcceptFactory<T>(IAmountBasedFactory<T> factory)
            => factory.Create(this);
    }
}