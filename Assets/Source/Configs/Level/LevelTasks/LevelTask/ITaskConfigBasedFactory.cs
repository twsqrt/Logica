namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public interface ITaskConfigBasedFactory<T>
    {
        T Create(FormulaTaskConfig formulaTaskConfig);
        T Create(AmountSaveTaskConfig amountSaveTaskConfig);
        T Create(RectangularAreaTaskConfig rectangularAreaTaskConfig);
    }
}