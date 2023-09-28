using Newtonsoft.Json;

namespace Config
{
    public class ParameterConfig
    {
        [JsonProperty("id")] private int _id;
        [JsonProperty("name")] private string _name;

        public int Id => _id;
        public string Name => _name;
    }
}