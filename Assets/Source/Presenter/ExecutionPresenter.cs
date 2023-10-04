using Model;
using Model.LevelTasksLogic;
using Model.TreeLogic;
using UnityEngine; 

namespace Presenter
{
    public class ExecutionPresenter
    {
        private readonly BlockTree _tree;
        private readonly LevelTasks _levelTasks; 

        public bool CanExecute()
            => _tree.IsCorrect();

        public ExecutionPresenter(BlockTree tree, LevelTasks levelTasks)
        {
            _tree = tree;
            _levelTasks = levelTasks;
        }

        public void TryExecute()
        {
            if(_tree.IsCorrect())
            {
                LevelScore resultScore = _levelTasks.CalculateScore();
                Debug.Log($"Level socre: {resultScore}");
            }
        }
    }
}