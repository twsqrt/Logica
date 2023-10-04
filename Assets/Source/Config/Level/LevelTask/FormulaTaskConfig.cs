using System.Collections.Generic;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public class FormulaTaskConfig
    {
        [JsonProperty("parameters")] private int[] _parametersId;
        [JsonProperty("formulaViewText")] private string _viewText;
        [JsonProperty("formulaParseText")] private string _parseText;

        public IEnumerable<int> ParametersId => _parametersId;
        public string ViewText => _viewText;
        public string ParseText => _parseText;
    }
}