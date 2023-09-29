using Config.JsonConverter;
using Config.JsonConverterLogic;
using Model.BlockLogic.BlockDataLogic;
using Newtonsoft.Json;

namespace Config
{
    public class InventorySlotConfig
    {  
        [JsonProperty("data")] [JsonConverter(typeof(BlockDataConverter))] private IBlockData _data;
        [JsonProperty("amount")] [JsonConverter(typeof(AmountConfigConverter))] private AmountConfig _amount;

        public IBlockData Data => _data;
        public AmountConfig Amount => _amount;
    }
}