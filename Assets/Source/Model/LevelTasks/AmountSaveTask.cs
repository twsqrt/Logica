using System.Collections.Generic;
using Model.BlocksLogic.BlocksData;
using Model.LevelStateLogic;
using Model.InventoryLogic;
using Configs.LevelConfigs.LevelTasksConfigs;
using System.Linq;

namespace Model.LevelTasksLogic
{
    public class AmountSaveTask : ILevelTask
    {
        private readonly Dictionary<IBlockData, int> _amountLimits;

        public IReadOnlyDictionary<IBlockData, int> AmountLimits => _amountLimits;
        
        public AmountSaveTask(AmountSaveTaskConfig config)
        {
            _amountLimits = config.Limits.ToDictionary(c => c.Data, c => c.Limit);
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