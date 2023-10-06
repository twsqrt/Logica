using Model;
using Model.LevelStateLogic;
using Model.LevelTasksLogic;
using Model.TreeLogic;
using UnityEngine; 

namespace Presenter
{
    public class ExecutionPresenter
    {
        private readonly LevelTasks _levelTasks;
        private readonly LevelState _levelState;

        public bool CanExecute()
            => _levelState.Tree.IsCorrect();

        public ExecutionPresenter(LevelState levelState, LevelTasks levelTasks)
        {
            _levelState = levelState;
            _levelTasks = levelTasks;
        }

        public void TryExecute()
        {
            if(CanExecute())
            {
                LevelScore resultScore = _levelTasks.CalculateScore(_levelState);
                Debug.Log($"Level socre: {resultScore}");
            }
        }
    }
}