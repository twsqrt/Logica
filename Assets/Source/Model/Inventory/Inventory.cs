using System.Collections.Generic;
using Model.BlockLogic.BlockDataLogic;
using Model.InventoryLogic.AmountLogic;
using Model.BlockLogic;
using Config;
using UnityEngine;

namespace Model.InventoryLogic
{
    public class Inventory
    {
        private readonly Dictionary<IBlockData, IAmount> _blocks;
        private readonly BlockFactory _factory;

        public IAmount this[IBlockData data]
            => _blocks[data];

        public IEnumerable<IBlockData> AllBlocksData
            => _blocks.Keys;

        public Inventory(BlockFactory factory, InventoryConfig config)
        {
            _factory = factory;

            _blocks = new Dictionary<IBlockData, IAmount>();
            foreach(InventorySlotConfig slot in config.Slots)
                _blocks.Add(slot.Data, AmountFactory.Create(slot.Amount));
        }

        public bool TryPullOut(IBlockData data, BlockContext context, out Block block)
        {
            if(_blocks.TryGetValue(data, out IAmount amount ) && amount.TryDecrease(1))
            {
                block = _factory.Create(data, context);
                block.OnDestroy += _ => amount.Increase(1);

                return true;
            }
            
            block = null;
            return false;
        }
    }
}