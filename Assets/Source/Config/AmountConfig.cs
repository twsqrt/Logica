using Newtonsoft.Json;

namespace Config
{
    public class AmountConfig
    {
        private readonly bool _isInfinity; 
        private readonly int _value;

        public bool isInfinity => _isInfinity;
        public int Value => _value;

        private AmountConfig(bool isInfinity, int value)
        {
            _isInfinity = isInfinity;
            _value = value;
        }

        public static AmountConfig CreateInfinity()
            => new AmountConfig(true, -1);
        
        public static AmountConfig CreateValue(int value)
            => new AmountConfig(false, value);
    }
}