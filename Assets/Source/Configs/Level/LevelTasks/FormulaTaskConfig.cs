using System.Collections.Generic;
using Configs.LevelConfigs.JsonConverters;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public class FormulaTaskConfig
    {
        [JsonProperty("parametersId")] private int[] _parametersId;
        [JsonProperty("formulaText")] private string _formulaText;
        [JsonProperty("truthTable")] [JsonConverter(typeof(TruthTableConverter))] private TruthTable _truthTable;

        public IEnumerable<int> ParametersId => _parametersId;
        public string FormulaText => _formulaText;
        public TruthTable TruthTable => _truthTable;
    }
}