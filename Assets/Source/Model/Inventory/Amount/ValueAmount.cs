using System;

namespace Model.InventoryLogic.AmountLogic
{
    public class ValueAmount : IAmount, IReadOnlyValueAmount
    {
        private int _value;

        public event Action<int> OnValueChange;

        public int Value
        {
            get => _value;
            private set 
            {
                _value = value;
                OnValueChange?.Invoke(value);
            }
        }

        public ValueAmount(int value)
        {
            _value = value;
        }

        public static ValueAmount Zero
            =>  new ValueAmount(0);

        public void Increase()
            => Value++;

        public bool TryDecrease()
        {
            if(Value == 0)
                return false;
            
            Value--;
            return true;
        }

        public bool LessThan(int amount)
            => Value < amount;

        public T AcceptFactory<T>(IAmountBasedFactory<T> factory)
            => factory.Create(this);

    }
}