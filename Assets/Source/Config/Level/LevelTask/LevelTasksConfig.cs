using Newtonsoft.Json;

namespace Config.LevelLogic.LevelTaskLogic
{
    public class LevelTasksConfig
    {
        [JsonProperty("formulaTask")] private FormulaTaskConfig _formulaTask;

        public FormulaTaskConfig FormulaTask => _formulaTask;
    }
}