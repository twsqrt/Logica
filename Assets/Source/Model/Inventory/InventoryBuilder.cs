using System.Collections.Generic;
using Model.BlockLogic.BlockDataLogic;
using Model.InventoryLogic.AmountLogic;
using Model.BlockLogic;

namespace Model.InventoryLogic
{
    public class InventoryBuilder
    {
        private Dictionary<IBlockData, IAmount> _registeredBlocks;
        private BlockFactory _factory;

        public InventoryBuilder()
        {
            _registeredBlocks = new Dictionary<IBlockData, IAmount>();
        }

        public InventoryBuilder StartBuilding(BlockFactory factory)
        {
            _registeredBlocks.Clear();
            _factory = factory;

            return this;
        }

        public InventoryBuilder Register(IBlockData data, int amount)
        {
            _registeredBlocks.Add(data, new ValueAmount(amount));
            return this;
        }

        public InventoryBuilder RegisterInfinity(IBlockData data)
        {
            _registeredBlocks.Add(data, new InfinityAmount());
            return this;
        }

        public Inventory Build()
        {
            return new Inventory(_factory, _registeredBlocks);
        }
    }
}