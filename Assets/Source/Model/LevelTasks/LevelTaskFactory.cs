using Configs.LevelConfigs.LevelTasksConfigs;
using Mappers;
using Model.LevelTasksLogic;

namespace Model.LevelTasks
{
    public class LevelTaskFactory : ITaskConfigBasedFactory<ILevelTask>
    {
        private readonly DelegateMapper _delegateMapper;

        public LevelTaskFactory(DelegateMapper delegateMapper)
        {
            _delegateMapper = delegateMapper;
        } 

        public ILevelTask Create(FormulaTaskConfig formulaTaskConfig)
            => new FormulaTask(formulaTaskConfig, _delegateMapper);

        public ILevelTask Create(AmountSaveTaskConfig amountSaveTaskConfig)
            => new AmountSaveTask(amountSaveTaskConfig);

        public ILevelTask Create(RectangularAreaTaskConfig rectangularAreaTaskConfig)
            => new RectangularAreaTask(rectangularAreaTaskConfig);
    }
}