using Configs.LevelConfigs.JsonConverters;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    [JsonConverter(typeof(TypedTaskConfigConverter))]
    public class TypedTaskConfig
    {
        private readonly ITaskConfig _taskConfig;

        public TypedTaskConfig(ITaskConfig taskConfig)
        {
            _taskConfig = taskConfig;
        }

        public ITaskConfig TaskConfig => _taskConfig;
    }
}