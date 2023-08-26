using System;

namespace Model.ResourceLogic.AmountLogic
{
    public class Amount : IAmount
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

        public Amount(int value)
        {
            _value = value;
        }

        public void Increase(int amount)
            => Value += amount;

        public bool TryDecrease(int amount)
        {
            if(_value < amount)
                return false;
            
            Value -= amount;
            return true;
        }
    }
}