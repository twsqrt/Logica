using Newtonsoft.Json;
using Configs.LevelConfigs.InventoryConfigs;
using Configs.LevelConfigs.LevelTasksConfigs;

namespace Configs.LevelConfigs
{
    public class LevelConfig
    {
        [JsonProperty("name")] private string _name;
        [JsonProperty("map")] private MapConfig _map;
        [JsonProperty("tree")] private TreeConfig _tree;
        [JsonProperty("inventory")] private InventoryConfig _inventory;
        [JsonProperty("tasks")] private LevelTasksConfig _tasks;

        public string Name => _name;
        public MapConfig Map => _map;
        public TreeConfig Tree => _tree;
        public InventoryConfig Inventory => _inventory;
        public LevelTasksConfig Tasks => _tasks; 
    } 
}