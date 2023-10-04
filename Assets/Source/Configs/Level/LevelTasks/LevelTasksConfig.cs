using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public class LevelTasksConfig
    {
        [JsonProperty("formulaTask")] private FormulaTaskConfig _formulaTask;

        public FormulaTaskConfig FormulaTask => _formulaTask;
    }
}