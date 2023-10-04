using Configs.LevelConfigs.JsonConverter;
using Configs.LevelConfigs.JsonConverters;
using Model.BlockLogic.BlockDataLogic;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.InventoryConfigs
{
    public class InventorySlotConfig
    {  
        [JsonProperty("data")] [JsonConverter(typeof(BlockDataConverter))] private IBlockData _data;
        [JsonProperty("amount")] [JsonConverter(typeof(AmountConfigConverter))] private AmountConfig _amount;

        public IBlockData Data => _data;
        public AmountConfig Amount => _amount;
    }
}