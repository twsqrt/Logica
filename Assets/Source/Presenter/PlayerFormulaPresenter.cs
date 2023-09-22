using Model.BlockLogic;
using Model.MapLogic;
using Model.TreeLogic;

namespace Presenter
{
    public class PlayerFormulaPresenter
    {
        private readonly BlockTree _tree;
        private readonly TreeToStringConverter _converter;

        public string GetFormulaString()
            => _tree.IsEmpty ? string.Empty : _tree.Root.Accept(_converter);

        public PlayerFormulaPresenter(BlockTree tree, TreeToStringConverter conveter)
        {
            _tree = tree;
            _converter = conveter;
        }
    }
}