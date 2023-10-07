using System.Collections.Generic;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public class FormulaTaskConfig : ITaskConfig
    {
        [JsonProperty("parametersId")] private int[] _parametersId;
        [JsonProperty("formulaText")] private string _formulaText;
        [JsonProperty("truthTable")] private TruthTable _truthTable;

        public IEnumerable<int> ParametersId => _parametersId;
        public string FormulaText => _formulaText;
        public TruthTable TruthTable => _truthTable;

        public T Accept<T>(ITaskConfigBasedFactory<T> factory)
            => factory.Create(this);
    }
}