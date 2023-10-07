using System.Collections.Generic;
using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public class AmountSaveTaskConfig : ITaskConfig
    {
        [JsonProperty("limits")] private AmountLimitConfig[] _limits;

        public IReadOnlyCollection<AmountLimitConfig> Limits => _limits;

        public T Accept<T>(ITaskConfigBasedFactory<T> factory)
            => factory.Create(this);
    }
}