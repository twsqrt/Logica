using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Config
{
    public static class LevelConfigLoader
    {
        public static LevelConfig Load(string fileName)
        {
            string fullPath = Application.dataPath + "/Resources/Configs/" + fileName;
            string jsonValue = File.ReadAllText(fullPath);
            return JsonConvert.DeserializeObject<LevelConfig>(jsonValue);
        }
    }
}