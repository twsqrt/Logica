namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public interface ITaskConfig
    {
        T Accept<T>(ITaskConfigBasedFactory<T> factory);
    }
}