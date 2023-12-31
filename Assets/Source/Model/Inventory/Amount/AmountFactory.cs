using Configs.LevelConfigs.InventoryConfigs;

namespace Model.InventoryLogic.AmountLogic
{
    public static class AmountFactory
    {
        public static IAmount Create(AmountConfig config)
        {
            if(config.isInfinity)
                return new InfinityAmount(); 
            else
                return new ValueAmount(config.Value);
        }
    }
} 