using Converter;
using Model.TreeLogic;

namespace Presenter
{
    public class PlayerFormulaPresenter
    {
        private readonly TreeStringValue _treeStringValue;

        public string GetFormulaString()
            => _treeStringValue.GetValue();

        public PlayerFormulaPresenter(TreeStringValue treeStringValue)
        {
            _treeStringValue = treeStringValue;
        }
    }
}