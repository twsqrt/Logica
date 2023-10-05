using UnityEngine;
using Newtonsoft.Json;

namespace Configs.LevelConfigs
{
    public class TreeConfig
    {
        [JsonProperty("rootX")] private int _rootX;
        [JsonProperty("rootY")] private int _rootY;
        
        public Vector2Int RootPosition => new Vector2Int(_rootX, _rootY);
    }
}