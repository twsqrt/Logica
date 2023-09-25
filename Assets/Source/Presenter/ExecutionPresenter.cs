using Model;
using Model.TreeLogic;
using UnityEngine; 

namespace Presenter
{
    public class ExecutionPresenter
    {
        private readonly BlockTree _tree;
        private readonly FormulaTask _formulaTask;

        public bool CanExecute()
            => _tree.IsCorrect();

        public ExecutionPresenter(BlockTree tree, FormulaTask formulaTask)
        {
            _tree = tree;
            _formulaTask = formulaTask;
        }

        public bool TryExecute()
        {
            if(_tree.IsCorrect() == false)
                return false;
            
            bool result = _formulaTask.CheckCompletion();
            Debug.Log($"Task result: {result}");
            return result;
        }
    }
}