using Converter;
using Model.TreeLogic;

namespace Presenter
{
    public class PlayerFormulaPresenter
    {
        private readonly BlockTree _tree;
        private readonly IConverter<BlockTree, string> _converter;

        public string GetFormulaString()
            => _converter.Convert(_tree);

        public PlayerFormulaPresenter(BlockTree tree, IConverter<BlockTree, string> conveter)
        {
            _tree = tree;
            _converter = conveter;
        }
    }
}