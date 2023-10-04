using Newtonsoft.Json;

namespace Configs.LevelConfigs
{
    public class MapConfig
    {
        [JsonProperty("width")] private int _width;
        [JsonProperty("height")] private int _height;

        public int Width => _width;
        public int Height => _height;
    }
}