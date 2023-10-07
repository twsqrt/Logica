using Configs.LevelConfigs.JsonConverters;
using Model.BlocksLogic.BlocksData;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public class AmountLimitConfig
    {
        [JsonProperty("data")] [JsonConverter(typeof(BlockDataConverter))] private IBlockData _data;
        [JsonProperty("limit")] private int _limit;

        public IBlockData Data => _data;
        public int Limit => _limit;
    }
}