namespace Model.InventoryLogic.AmountLogic
{
    public class InfinityAmount : IAmount
    {
        public void Increase() { }
        public bool TryDecrease() => true;
        public bool LessThan(int amount) => false;
        public bool MoreThan(int amount) => true;

        public T AcceptFactory<T>(IAmountBasedFactory<T> factory)
            => factory.Create(this);

    }
}