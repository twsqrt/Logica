using System;
using System.Collections;
using Config;
using Model.TreeLogic;
using View;

namespace Presenter
{
    public class PlayerFormulaPresenter
    {
        private readonly CoroutineTarget _coroutineTarget;
        private readonly TreeStringValue _treeStringValue;

        public event Action<string> OnFormulaTextChanged;

        private IEnumerator ChangeFormulaOnNextFrame()
        {
            yield return null;
            OnFormulaTextChanged?.Invoke(_treeStringValue.GetValue());
        }

        public PlayerFormulaPresenter(BlockTree tree, ParametersConfig parametersConfig, CoroutineTarget coroutineTarget)
        {
            _coroutineTarget = coroutineTarget;
            _treeStringValue = new TreeStringValue(tree, parametersConfig);

            tree.OnChanged += () => _coroutineTarget.StartCoroutine(ChangeFormulaOnNextFrame());
        }
    }
}