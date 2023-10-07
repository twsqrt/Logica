using Newtonsoft.Json;

namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public class RectangularAreaTaskConfig : ITaskConfig
    {
        [JsonProperty("areaLimit")] private int _areaLimit;

        public int AreaLimit => _areaLimit;

        public T Accept<T>(ITaskConfigBasedFactory<T> factory)
            => factory.Create(this);
    }
}