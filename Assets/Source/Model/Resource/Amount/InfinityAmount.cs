namespace Model.ResourceLogic.AmountLogic
{
    public class InfinityAmount : IAmount
    {
        public void Increase(int amount) { }

        public bool TryDecrease(int amount) => true;
    }
}