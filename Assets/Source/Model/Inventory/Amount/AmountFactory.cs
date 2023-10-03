using Config.LevelLogic.InventoryLogic;

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