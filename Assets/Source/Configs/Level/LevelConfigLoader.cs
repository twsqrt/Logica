using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Configs.LevelConfigs
{
    public static class LevelConfigLoader
    {
        private static readonly string PATH_FORMAT = Application.dataPath + "/Resources/Levels/{0}.json";

        public static LevelConfig Load(string levelName)
        {
            string path = string.Format(PATH_FORMAT, levelName);
            string jsonValue = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<LevelConfig>(jsonValue);
        }
    }
}