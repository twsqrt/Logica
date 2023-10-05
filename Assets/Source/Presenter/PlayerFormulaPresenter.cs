using Model.TreeLogic;
using Mappers;

namespace Presenter
{
    public class PlayerFormulaPresenter
    {
        private readonly BlockTree _tree;
        private readonly FormulaMapper _stringMapper;

        public string GetFormulaString()
            => _stringMapper.From(_tree);

        public PlayerFormulaPresenter(BlockTree tree, FormulaMapper stringMapper)
        {
            _tree = tree;
            _stringMapper = stringMapper;
        }
    }
}