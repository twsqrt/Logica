using Newtonsoft.Json;

namespace Config
{
    public class MapConfig
    {
        [JsonProperty("width")] private int _width;
        [JsonProperty("height")] private int _height;

        public int Width => _width;
        public int Height => _height;
    }
}