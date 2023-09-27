using System.Collections.Generic;
using Model.BlockLogic.BlockDataLogic;
using Model.InventoryLogic;

namespace Model.LevelTaskLogic
{
    public class AmountTask : ILevelTask
    {
        private readonly Inventory _inventory;
        private readonly Dictionary<IBlockData, int> _amountLimits;

        public IReadOnlyDictionary<IBlockData, int> AmountLimits => _amountLimits;
        
        public AmountTask(Inventory inventory, Dictionary<IBlockData, int> amountLimits)
        {
            _inventory = inventory;
            _amountLimits = amountLimits;
        }

        public bool CheckCompletion()
        {
            foreach(var (data, limit) in _amountLimits)
            {
                if(_inventory[data].LessThan(limit))
                    return false;
            }

            return true;
        }

        public T Accept<T>(ILevelTaskVisitor<T> visitor)
            => visitor.Visit(this);
    }
}