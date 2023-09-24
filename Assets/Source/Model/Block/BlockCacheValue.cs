using System;
using Converter;

namespace Model.BlockLogic
{
    public class BlockCacheValue<T>
    {
        private readonly Block _block;
        private readonly Func<Block, T> _updater;

        private bool _shouldUpdate;
        private T _value;

        public BlockCacheValue(Block block, Func<Block, T> updater)
        {
            _block = block;
            _updater = updater;

            _shouldUpdate = true;

            _block.OnSubTreeChanged += () => _shouldUpdate = true;
        }

        public T GetValue()
        {
            if(_shouldUpdate)
            {
                _value = _updater.Invoke(_block);
                _shouldUpdate = false;
            }
            
            return _value;
        }
    }
} 