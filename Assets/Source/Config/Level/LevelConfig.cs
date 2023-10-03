using Newtonsoft.Json;
using Config.LevelLogic.ParametersLogic;
using Config.LevelLogic.InventoryLogic;
using Config.LevelLogic.LevelTaskLogic;

namespace Config
{
    public class LevelConfig
    {
        [JsonProperty("name")] private string _name;
        [JsonProperty("map")] private MapConfig _map;
        [JsonProperty("tree")] private TreeConfig _tree;
        [JsonProperty("parameters")] private ParametersConfig _parameters;
        [JsonProperty("inventory")] private InventoryConfig _inventory;
        [JsonProperty("tasks")] private LevelTasksConfig _tasks;

        public string Name => _name;
        public MapConfig Map => _map;
        public TreeConfig Tree => _tree;
        public ParametersConfig Parameters => _parameters;
        public InventoryConfig Inventory => _inventory;
        public LevelTasksConfig Tasks => _tasks; 
    } 
}