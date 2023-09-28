using System.Collections.Generic;
using Newtonsoft.Json;

namespace Config
{
    public class LevelConfig
    {
        [JsonProperty("name")] private string _name;
        [JsonProperty("map")] private MapConfig _map;
        [JsonProperty("parameters")] private ParameterConfig[] _parameters;

        public string Name => _name;
        public MapConfig Map => _map;
        public ParametersConfig Parameters => new ParametersConfig(_parameters);
    }
}