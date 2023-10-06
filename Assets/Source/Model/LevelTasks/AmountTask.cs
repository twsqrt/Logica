using System.Collections.Generic;
using Model.BlocksLogic.BlocksData;
using Model.LevelStateLogic;
using Model.InventoryLogic;

namespace Model.LevelTasksLogic
{
    public class AmountTask : ILevelTask
    {
        private readonly Dictionary<IBlockData, int> _amountLimits;

        public IReadOnlyDictionary<IBlockData, int> AmountLimits => _amountLimits;
        
        public AmountTask(Dictionary<IBlockData, int> amountLimits)
        {
            _amountLimits = amountLimits;
        }

        public bool CheckCompletion(LevelState levelState)
        {
            foreach(var (data, limit) in _amountLimits)
            {
                if(levelState.Inventory[data].LessThan(limit))
                    return false;
            }

            return true;
        }

        public T Accept<T>(ILevelTaskVisitor<T> visitor)
            => visitor.Visit(this);
    }
}