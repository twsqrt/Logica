using Newtonsoft.Json;

namespace Config.LevelLogic.LevelTaskLogic
{
    public class FormulaTaskConfig
    {
        [JsonProperty("formulaViewText")] private string _viewText;
        [JsonProperty("formulaParseText")] private string _parseText;

        public string ViewText => _viewText;
        public string ParseText => _parseText;
    }
}