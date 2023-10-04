using System.Collections.Generic;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.InventoryConfigs
{
    public class InventoryConfig
    {
        [JsonProperty("slots")] private InventorySlotConfig[] _slots;

        public IReadOnlyCollection<InventorySlotConfig> Slots => _slots;
    }
}