using System;

namespace Model.InventoryLogic.AmountLogic
{
    public interface IReadOnlyValueAmount : IReadOnlyAmount
    {
        event Action<int> OnValueChange;
        int Value { get; }
    }
}