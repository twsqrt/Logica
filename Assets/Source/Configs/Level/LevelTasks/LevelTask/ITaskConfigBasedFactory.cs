namespace Configs.LevelConfigs.LevelTasksConfigs
{
    public interface ITaskConfigBasedFactory<T>
    {
        T Create(FormulaTaskConfig formulaTask);
        T Create(AmountSaveTaskConfig amountSaveTask);
        T Create(RectangularAreaTaskConfig rectangularAreaTask);
    }
}