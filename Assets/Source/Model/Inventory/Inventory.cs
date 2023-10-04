using Configs.LevelConfigs.InventoryConfigs;
using Model.BlocksLogic.BlocksData;
using Model.BlocksLogic;
using Model.InventoryLogic.AmountLogic;
using System.Collections.Generic;

namespace Model.InventoryLogic
{
    public class Inventory
    {
        private readonly Dictionary<IBlockData, IAmount> _amounts;
        private readonly BlockFactory _factory;

        public IReadOnlyAmount this[IBlockData data]
        {
            get
            {
                if(_amounts.TryGetValue(data, out IAmount amount))
                    return amount;
                return ValueAmount.Zero; 
            }
        }

        public IEnumerable<IBlockData> AllBlocksData
            => _amounts.Keys;

        public Inventory(BlockFactory factory, InventoryConfig config)
        {
            _factory = factory;

            _amounts = new Dictionary<IBlockData, IAmount>();
            foreach(InventorySlotConfig slot in config.Slots)
                _amounts.Add(slot.Data, AmountFactory.Create(slot.Amount));
        }

        public bool TryPullOut(IBlockData data, BlockContext context, out Block block)
        {
            if(_amounts.TryGetValue(data, out IAmount amount ) && amount.TryDecrease())
            {
                block = _factory.Create(data, context);
                block.OnDestroy += _ => amount.Increase();

                return true;
            }
            
            block = null;
            return false;
        }
    }
}