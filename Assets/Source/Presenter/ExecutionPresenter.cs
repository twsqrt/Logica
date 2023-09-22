using Model;
using Model.TreeLogic;
using UnityEngine; 

namespace Presenter
{
    public class ExecutionPresenter
    {
        private readonly BlockTree _tree;
        private readonly FormulaRule _rule;

        public bool CanExecute()
            => _tree.IsCorrect();

        public ExecutionPresenter(BlockTree tree, FormulaRule rule)
        {
            _tree = tree;
            _rule = rule;
        }

        public bool TryExecute()
        {
            if(_tree.IsCorrect() == false)
                return false;
            
            bool result = _rule.Execute(_tree);
            Debug.Log($"Rule result: {result}");
            return result;
        }
    }
}