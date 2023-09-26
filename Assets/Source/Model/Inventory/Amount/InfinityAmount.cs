namespace Model.InventoryLogic.AmountLogic
{
    public class InfinityAmount : IAmount
    {
        public void Increase(int amount) { }

        public bool TryDecrease(int amount) => true;

        public bool LessThan(int amount) => false;

        public T AcceptFactory<T>(IAmountBasedFactory<T> factory)
            => factory.Create(this);
    }
}